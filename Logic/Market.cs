using EveLPBot.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Logic
{
    public static class Market
    {

        public static double getMinimumSell(List<MarketOrder> marketOrders, long stationId)
        {
            double minSell = 0;
            foreach (MarketOrder order in marketOrders)
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

        public static double getMaximumBuy(List<MarketOrder> buyOrders, long stationId)
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
