using EveLPBot.API;
using EveLPBot.API.Database;
using EveLPBot.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Logic
{
    public class BlueprintData
    {
        //Constant price per LP, needs to be able to change later
        private static readonly long pricePerLP = 1000;
        //Constant concord LP conversion
        private static readonly double concordLPConversion = 0.8;

        private readonly GenericBlueprint blueprint;

        //Base materials (trit, t2 comps, etc)
        //ID/quanitity pair
        private readonly Dictionary<long, long> baseMaterials = new Dictionary<long, long>();
        //primary Blueprint cost (tags, etc)
        private readonly Dictionary<long, long> blueprintCostMaterials = new Dictionary<long, long>();
        //Blueprint LP/isk cost will use references in the blueprint object itself and calculate on the fly


        //begin convenience properties
        private string producedItem;

        private long producedItemId;

        private long quantityProduced;
        /// END Convenience properties
        

        public BlueprintData(GenericBlueprint blueprint)
        {
            this.blueprint = blueprint;

            blueprint.Activities.Manufacturing.Materials.ForEach((material) =>
            {
                baseMaterials.Add(material.TypeId, material.Quantity);
            });
            if (blueprint.lpStoreItem != null)
            {
                blueprint.lpStoreItem.required_items.ForEach((material) =>
                {
                    blueprintCostMaterials.Add(material.type_id, material.quantity);
                });
            }

            blueprint.Activities.Manufacturing.Products.ForEach((product) =>
            {
                producedItem = ItemInfoAPI.getItemNameByTypeId(product.TypeId).Result;
                producedItemId = product.TypeId;
                //multiplies number of runs by quantity produced (normally 1 but accounts for things like ammo)
                quantityProduced = blueprint.lpStoreItem.quantity * product.Quantity;
            });
        }

        public double getTotalBaseMaterialCost(Location location)
        {
            double cost = 0.0;
            blueprint.Activities.Manufacturing.Materials.ForEach((material) =>
            {
                //total cost of each raw blueprint material on market (minimum sell * quantity)
                cost += material.Quantity * Market.getMinimumSell(MarketAPI.getSellOrdersForItem(location.regionId, material.TypeId), location.stationId);
            });

            return cost;
        }

        public string getCostSummary(Location location)
        {
            String output = "";
            //print layout
            //Produced Item //Quantity produced
            //TODO: Invention apparently has multiple products so that will need to be looked into. But this should only iterate once for now when we care
            String producedItem = "";
            long producedItemId = blueprint.Activities.Manufacturing.Products[0].TypeId;

            String record1 = "Produced Item: " + producedItem + " Quantity: " + Convert.ToString(quantityProduced) + Environment.NewLine;

            String record2 = "";
            String record3 = "";
            String record4 = "";
            String record5 = "";
            String record6 = "";


            if (blueprint.lpStoreItem != null)
            {
                //Blueprint name
                record2 = "Blueprint: " + ItemInfoAPI.getItemNameByTypeId(blueprint.BlueprintTypeId).Result + Environment.NewLine;

                //blueprint mats
                record3 = "Costs: " + Environment.NewLine;

                record3 += getLPStoreCostSummaryString(location);

                //LP with cost per LP
                record4 = getConcordLPCostString() + Environment.NewLine;

                //Isk material
                record5 = "Isk Required: " + Convert.ToString(blueprint.lpStoreItem.isk_cost) + Environment.NewLine;

                //Blueprint cost total
                record6 = "Total Blueprint Cost: " + getLPStoreCost(location) + Environment.NewLine;
            }
            

            //Base Materials
            String record8 = "Build Costs:" + Environment.NewLine;

            record8 += getMaterialCostExtendedSummaryString(location);

            
            //Item cost total
            string record9 = getTotalCostString(location, producedItemId) + Environment.NewLine;

            //Jita sell
            
            string record10 = getSellPriceString(location, producedItemId) + Environment.NewLine;

            //Profit //ROI

            double profit = getProfit(location, producedItemId);
            double ROI = getROI(location, producedItemId);

            string record11 = getProfitString(location, producedItemId);

            output = record1 + record2 + record3 + record4 + record5 + record6 + record8 + record9 + record10 + record11;
            return output;
        }

        private string getMaterialCostExtendedSummaryString(Location location)
        {
            double materialCost = 0.0;
            string matCostOutput = "";

            blueprint.Activities.Manufacturing.Materials.ForEach((material) =>
            {
                double cost = getSellPrice(location, material.TypeId) * material.Quantity;
                materialCost += cost;
                matCostOutput += "Material: " + ItemInfoAPI.getItemNameByTypeId(material.TypeId).Result + " Quantity: " + material.Quantity + " Cost: " + Convert.ToString(cost) + Environment.NewLine;
            });
            
            return matCostOutput;
        }
        
        private double getTotalMineralCost(Location location)
        {
            double materialCost = 0.0;
            blueprint.Activities.Manufacturing.Materials.ForEach((material) =>
            {
                double cost = getSellPrice(location, material.TypeId) * material.Quantity;
                materialCost += cost;
            });

            return materialCost;
        }

        private double getLPStoreCost(Location location)
        {
            double blueprintCost = 0.0;
            blueprint.lpStoreItem.required_items.ForEach((material) =>
            {
                double cost = Market.getMinimumSell(MarketAPI.getSellOrdersForItem(location.regionId, material.type_id), location.stationId) * material.quantity;
                blueprintCost += cost;
            });

            blueprintCost += calculateConcordLPConversionCost();

            return blueprintCost;
        }

        private string getLPStoreCostSummaryString(Location location)
        {
            double blueprintCost = 0.0;
            string message = "";
            blueprint.lpStoreItem.required_items.ForEach((material) =>
            {
                double cost = Market.getMinimumSell(MarketAPI.getSellOrdersForItem(location.regionId, material.type_id), location.stationId) * material.quantity;
                blueprintCost += cost;
                message += "Material: " + ItemInfoAPI.getItemNameByTypeId(material.type_id).Result + " Quantity: " + material.quantity + " Cost: " + Convert.ToString(cost) + Environment.NewLine;
            });

            return message;
        }

        private double getTotalCost(Location location, long itemId)
        {
            double totalCost = 0.0;

            totalCost += getTotalMineralCost(location);

            //If the blueprint doesn't come to the LP store don't add the cost
            if (blueprint.lpStoreItem != null)
            {
                totalCost += getLPStoreCost(location);
            }

            return totalCost;

        }

        private string getTotalCostString(Location location, long itemId)
        {
            return "Total Cost: " + Convert.ToString(getTotalCost(location, itemId));
        }


        private double getROI(Location location, long itemId)
        {
            return getTotalCost(location, itemId) / (getSellPrice(location, itemId) * quantityProduced);
        }


        private double getProfit(Location location, long itemId)
        {
            return (getSellPrice(location, itemId) * quantityProduced) - getTotalCost(location, itemId);
        }


        private string getProfitString(Location location, long itemId)
        {
            return "Total Profit: " + getProfit(location, itemId) + " ROI: " + getROI(location, itemId);
        }


        private double getSellPrice(Location location, long itemId)
        {
            return Market.getMinimumSell(MarketAPI.getSellOrdersForItem(location.regionId, itemId), location.stationId);
        }

        

        private string getSellPriceString(Location location, long itemId)
        {
            double itemSell = getSellPrice(location, itemId);
            return "Sell price: " + Convert.ToString(itemSell) + " Quantity: " + quantityProduced + " Total Sell: " + Convert.ToString(itemSell * quantityProduced);
        }
        
        private string getConcordLPCostString()
        {
            return "Converted LP required: " + Convert.ToString(calculateConcordLPConversion()) + " Cost: " + Convert.ToString(calculateConcordLPConversionCost());
        }


        private long calculateConcordLPConversion()
        {
            return Convert.ToInt64(blueprint.lpStoreItem.lp_cost / concordLPConversion);
        }

        private long calculateConcordLPConversionCost()
        {
            return calculateConcordLPConversion() * pricePerLP;
        }
    }

    
}
