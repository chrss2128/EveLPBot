using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EveLPBot.API
{
    public static class MarketAPI
    {
        public static string getSellOrdersForItem(long regionId, long itemId)
        {
            string json;
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString($"https://esi.evetech.net/latest/markets/{regionId}/orders/?datasource=tranquility&order_type=sell&type_id={itemId}");
            }
            return json;
        }
        public static string getBuyOrdersForItem(long regionId, long itemId)
        {
            string json;
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString($"https://esi.evetech.net/latest/markets/{regionId}/orders/?datasource=tranquility&order_type=buy&type_id={itemId}");
            }
            
            return json;
        }
    }
}
