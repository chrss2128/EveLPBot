using Discord.Commands;

using EveLPBot.API;
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

        [Command("pricecheck")]
        //Arguments Expected: region, item Id, StationId
        public async Task MarketAsync(params string[] args)
        {
            long regionId = Convert.ToInt64(args[0]);
            long itemId = getItemIdByItemName(args[1]);
            long stationId = Convert.ToInt64(args[2]);

            //long regionId = Convert.ToInt64(args.RegionId);
            //long itemId = Convert.ToInt64(args.ItemId);
            //long stationId = Convert.ToInt64(args.StationId);

            //TODO:
            // We can also access the channel from the Command Context.
            //await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
            //This looks up Tritanium in The Forge
            //"https://esi.evetech.net/latest/markets/10000002/orders/?datasource=tranquility&order_type=sell&type_id=34"
            //jita id 60003760

            //String json = MarketAPI.getOrdersForItem(10000002, 34);
            String json = MarketAPI.getSellOrdersForItem(regionId, itemId);

            List<MarketOrder> sellOrders = new List<MarketOrder>();
            sellOrders = JsonConvert.DeserializeObject<List<MarketOrder>>(json);


            json = MarketAPI.getBuyOrdersForItem(regionId, itemId);

            List<MarketOrder> buyOrders = new List<MarketOrder>();
            buyOrders = JsonConvert.DeserializeObject<List<MarketOrder>>(json);


            String message = aggregateMarketData(sellOrders, buyOrders, stationId);
            
            message = DiscordHelper.TruncateString(message);
            await Context.Channel.SendMessageAsync(message);
        }

        private string aggregateMarketData(List<MarketOrder> sellOrders, List<MarketOrder> buyOrders, long stationId)
        {
            string marketData = "";

            marketData = "Sell: " + Convert.ToString(getMinSell(sellOrders, stationId)) + Environment.NewLine; 
            marketData += "Buy: " + Convert.ToString(getMaxBuy(buyOrders, stationId));
            
            return marketData;
        }

        //TODO: this is brute forcey, look into using a dictionary to do this more cleanly
        private long getItemIdByItemName(string name)
        {
            foreach (GenericItem item in GlobalMappings.itemList)
            {
                Console.WriteLine("Checking item: " + item.name);
                if (item.name.Equals(name))
                {
                    
                    Console.WriteLine(item.id);
                    return item.id;
                }
            }

            Console.Write("Item " + name + " Not Found");
            return 0;
        }
        private double getMinSell(List<MarketOrder> sellOrders, long stationId)
        {
            double minSell = 0;
            foreach (MarketOrder order in sellOrders)
            {
                if (order.location_Id == stationId)
                {
                    if (minSell == 0)
                    {
                        minSell = order.price;
                    }
                    else if (order.price < minSell)
                    {
                        minSell = order.price;
                    }
                }
            }

            return minSell;
        }

        private double getMaxBuy(List<MarketOrder> buyOrders, long stationId)
        {
            double maxBuy = 0;
            foreach (MarketOrder order in buyOrders)
            {
                if (order.location_Id == stationId)
                {
                    if (order.price > maxBuy)
                    {
                        maxBuy = order.price;
                    }
                }
            }

            return maxBuy;
        }
    }
}
