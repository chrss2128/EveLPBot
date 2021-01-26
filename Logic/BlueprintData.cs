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
        private static readonly double pricePerLP = 1000;
        //Constant concord LP conversion
        private static readonly double concordLPConversion = 0.8;

        private readonly GenericBlueprint blueprint;

        //Base materials (trit, t2 comps, etc)
        //ID/quanitity pair
        private readonly Dictionary<long, long> baseMaterials = new Dictionary<long, long>();
        //primary Blueprint cost (tags, etc)
        private readonly Dictionary<long, long> blueprintCostMaterials = new Dictionary<long, long>();
        //Blueprint LP/isk cost will use references in the blueprint object itself and calculate on the fly

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
            long producedItemId = 0;
            long quantityProduced = 0;
            blueprint.Activities.Manufacturing.Products.ForEach((product) =>
            {
                producedItem = ItemInfoAPI.getItemNameByTypeId(product.TypeId).Result;
                producedItemId = product.TypeId;
                quantityProduced = product.Quantity;
            });

            String record1 = "Produced Item: " + producedItem + " Quantity: " + Convert.ToString(quantityProduced) + Environment.NewLine;


            double blueprintCost = 0.0;
            double lpCost = 0.0;
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
                
                blueprint.lpStoreItem.required_items.ForEach((material) =>
                {
                    double cost = Market.getMinimumSell(MarketAPI.getSellOrdersForItem(location.regionId, material.type_id), location.stationId) * material.quantity;
                    blueprintCost += cost;
                    record3 += "Material: " + ItemInfoAPI.getItemNameByTypeId(material.type_id).Result + " Quantity: " + material.quantity + " Cost: " + Convert.ToString(cost) + Environment.NewLine;
                });

                //LP with cost per LP
                lpCost = (blueprint.lpStoreItem.lp_cost / 0.8) * pricePerLP;
                blueprintCost += lpCost;
                record4 = "Converted LP required: " + Convert.ToString((blueprint.lpStoreItem.lp_cost / 0.8)) + " Cost: " + Convert.ToString(lpCost) + Environment.NewLine;

                //Isk material
                blueprintCost += blueprint.lpStoreItem.isk_cost;
                record5 = "Isk Required: " + Convert.ToString(blueprint.lpStoreItem.isk_cost) + Environment.NewLine;

                //Blueprint cost total
                record6 = "Total Blueprint Cost: " + blueprintCost;
            }
            

            //Base Materials
            String record8 = "Build Costs" + Environment.NewLine;
            double materialCost = 0.0;
            blueprint.Activities.Manufacturing.Materials.ForEach((material) =>
            {
                double cost = Market.getMinimumSell(MarketAPI.getSellOrdersForItem(location.regionId, material.TypeId), location.stationId) * material.Quantity;
                materialCost += cost;
                record8 += "Material: " + ItemInfoAPI.getItemNameByTypeId(material.TypeId).Result + " Quantity: " + material.Quantity + " Cost: " + Convert.ToString(cost) + Environment.NewLine;
            });

            //Item cost total
            double totalCost = blueprintCost + materialCost;
            string record9 = "Total Cost: " + Convert.ToString(totalCost);

            //Jita sell
            double itemSell = Market.getMinimumSell(MarketAPI.getSellOrdersForItem(location.regionId, producedItemId), location.stationId);
            string record10 = "Sell price: " + Convert.ToString(itemSell) + " Quantity: " + quantityProduced + " Total Sell: " + Convert.ToString(itemSell * quantityProduced) + Environment.NewLine;

            //Profit //ROI

            double profit = (itemSell * quantityProduced) - totalCost;
            double ROI = totalCost / (itemSell * quantityProduced);

            string record11 = "Total Profit: " + profit + " ROI: " + ROI;

            output = record1 + record2 + record3 + record4 + record5 + record6 + record8 + record9 + record10 + record11;
            return output;
        }
    }

    
}
