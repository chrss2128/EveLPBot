using EveLPBot.Model;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EveLPBot.API
{
    static class LoyaltyPublicAPI
    {
        public static List<LPStoreItem> getLPStoreForCorporationID(long npcCorporationID)
        {
            string json;
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString($"https://esi.evetech.net/v1/loyalty/stores/{npcCorporationID}/offers/?datasource=tranquility");
            }

            //If no LP store exists
            if (json.Equals("[]"))
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<List<LPStoreItem>>(json);
            }

            
        }
    }
}
