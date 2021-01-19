using EveLPBot.Model;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EveLPBot.API
{
    public static class MarketAPI
    {
        public static List<MarketOrder> getSellOrdersForItem(long regionId, long itemId)
        {
            string json;
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString($"https://esi.evetech.net/latest/markets/{regionId}/orders/?datasource=tranquility&order_type=sell&type_id={itemId}");
            }

            return JsonConvert.DeserializeObject<List<MarketOrder>>(json);
        }
        public static List<MarketOrder> getBuyOrdersForItem(long regionId, long itemId)
        {
            string json;
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString($"https://esi.evetech.net/latest/markets/{regionId}/orders/?datasource=tranquility&order_type=buy&type_id={itemId}");
            }
            
            return JsonConvert.DeserializeObject<List<MarketOrder>>(json);
        }
    }
}
