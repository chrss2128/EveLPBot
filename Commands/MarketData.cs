using Discord.Commands;

using EveLPBot.API;
using EveLPBot.API.Database;
using EveLPBot.Logic;
using EveLPBot.Model;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EveLPBot.Commands
{
    public class MarketData : ModuleBase<SocketCommandContext>
    {

        [NamedArgumentType]
        public class MarketDataArguments
        {
            public string RegionId { get; set; }
            public string ItemId { get; set; }
            public string StationId { get; set; }
        }

        [Command("checkbuildcost")]
        //Arguments Expected: region, item Id, StationId
        public async Task BlueprintBuildCostAsync([Remainder] string args)
        {
            string[] parsedArgs = DiscordHelper.parseCommands(args);
            long itemId = ItemInfoAPI.getTypeIdByItemName(parsedArgs[1]).Result;
            Location location = new Location(Convert.ToInt64(parsedArgs[0]), getStationId(parsedArgs[2]));

            //TODO:
            // We can also access the channel from the Command Context.
            //This looks up Tritanium in The Forge
            //"https://esi.evetech.net/latest/markets/10000002/orders/?datasource=tranquility&order_type=sell&type_id=34"
            //jita id 60003760

            //String json = MarketAPI.getOrdersForItem(10000002, 34);

            //Blueprint.getSurfaceBuildCost()
            //List<MarketOrder> sellOrders = new List<MarketOrder>();
            //sellOrders = MarketAPI.getSellOrdersForItem(location.regionId, itemId);

// double cost = Blueprint.getTotalSurfaceBuildCost(Item.getBlueprintByItemId(itemId), location);

            //List<MarketOrder> buyOrders = new List<MarketOrder>();
            //buyOrders = MarketAPI.getBuyOrdersForItem(location.regionId, itemId);
            BlueprintData bd = new BlueprintData(Item.getBlueprintByItemId(itemId));

            String message = bd.getCostSummary(location);

            message = DiscordHelper.TruncateString(message);
            await Context.Channel.SendMessageAsync(message);
        }

        [Command("pricecheck")]
        //Arguments Expected: region, item Id, StationId
        public async Task MarketAsync([Remainder]string args)
        {
            string[] parsedArgs = DiscordHelper.parseCommands(args);
            long itemId = ItemInfoAPI.getTypeIdByItemName(parsedArgs[1]).Result; 
            Location location = new Location(Convert.ToInt64(parsedArgs[0]) , getStationId(parsedArgs[2]));

            //TODO:
            // We can also access the channel from the Command Context.
            //This looks up Tritanium in The Forge
            //"https://esi.evetech.net/latest/markets/10000002/orders/?datasource=tranquility&order_type=sell&type_id=34"
            //jita id 60003760

            //String json = MarketAPI.getOrdersForItem(10000002, 34);

            List<MarketOrder> sellOrders = new List<MarketOrder>();
            sellOrders = MarketAPI.getSellOrdersForItem(location.regionId, itemId);


            List<MarketOrder> buyOrders = new List<MarketOrder>();
            buyOrders = MarketAPI.getBuyOrdersForItem(location.regionId, itemId);

            String message = await aggregateMarketData(sellOrders, buyOrders, location.stationId);
            
            message = DiscordHelper.TruncateString(message);
            await Context.Channel.SendMessageAsync(message);
        }

        private async static Task<string> aggregateMarketData(List<MarketOrder> sellOrders, List<MarketOrder> buyOrders, long stationId)
        {
            string marketData = "";

            marketData = "Sell: " + Convert.ToString(Market.getMinimumSell(sellOrders, stationId)) + Environment.NewLine; 
            marketData += "Buy: " + Convert.ToString(Market.getMaximumBuy(buyOrders, stationId));
            
            return marketData;
        }

       

        private long getStationId(string stationID)
        {
            long returnId = 0;
            returnId = SystemEnums.systemNameStationIdMappings.GetValueOrDefault(stationID, 0);

            if (returnId == 0)
            {
                returnId = Convert.ToInt64(stationID);
            }

            return returnId;
        }
    } 

}
