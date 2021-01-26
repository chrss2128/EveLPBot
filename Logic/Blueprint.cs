using EveLPBot.API;
using EveLPBot.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Logic
{
    static class Blueprint
    {

        private static readonly double pricePerLP = 1000;
        private static readonly double concordLPConversion = 0.8;

        //Gets the total build cost for the first layer
        public static double getTotalSurfaceBuildCost(GenericBlueprint genericBlueprint, Location location)
        {
            double cost = 0.0;
            genericBlueprint.Activities.Manufacturing.Materials.ForEach((blueprint) =>
                {
                    //total cost of each raw blueprint material on market (minimum sell * quantity)
                    cost += blueprint.Quantity * Market.getMinimumSell(MarketAPI.getSellOrdersForItem(location.regionId, blueprint.TypeId), location.stationId);
                });

            //Convert LP and multiply by price per lp
            //TODO: This only accounts for converted LP, need logic to handle Concord LP store(s)
            cost += (genericBlueprint.lpStoreItem.lp_cost / .8) * pricePerLP;

            //Add blueprint isk cost
            cost += genericBlueprint.lpStoreItem.isk_cost;

            //Add other materials for concord LP store
            genericBlueprint.lpStoreItem.required_items.ForEach((requiredItem) =>
            {
                long quantity = requiredItem.quantity;
                double value = Market.getMinimumSell(MarketAPI.getSellOrdersForItem(location.regionId, requiredItem.type_id), location.stationId);
                cost += value * quantity;
            });

            return cost;
        }
    }
}
