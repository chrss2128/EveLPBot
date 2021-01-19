using EveLPBot.API;
using EveLPBot.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Logic
{
    static class Blueprint
    {
        
        //Gets the total build cost for the first layer
        public static double getSurfaceBuildCost(GenericBlueprint genericBlueprint, Location location)
        {
            double cost = 0.0;
            genericBlueprint.Activities.Manufacturing.Materials.ForEach((blueprint) =>
                {
                    //total cost of each blueprint material on market (minimum sell * quantity)
                    cost += blueprint.Quantity * Market.getMinimumSell(MarketAPI.getSellOrdersForItem(location.regionId, blueprint.TypeId), location.stationId);
                });

            return cost;
        }
    }
}
