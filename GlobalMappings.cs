using EveLPBot.API;
using EveLPBot.API.Database;
using EveLPBot.Data;
using EveLPBot.Model;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EveLPBot
{
    static class GlobalMappings
    {
        public static readonly List<GenericItem> itemList;

        public static readonly List<GenericBlueprint> blueprintList;

        public static readonly List<NPCCorporation> npcCorporationList;

        public static readonly List<LPStore> lpStores;
        static GlobalMappings()
        {
            Console.WriteLine("Setting up ItemId -> Name mappings");
            itemList = JsonConvert.DeserializeObject<List<GenericItem>>(File.ReadAllText(Resource1.idnamemap, System.Text.Encoding.UTF8));
            Console.WriteLine("Completed setup of ItemId -> Name mappings");
            Console.WriteLine("Setting up blueprint mappings");
            
            blueprintList = JsonConvert.DeserializeObject<List< GenericBlueprint >> (File.ReadAllText(Resource1.blueprintmap, System.Text.Encoding.UTF8));
            Console.WriteLine("Completed setup of blueprint mappings");
            Console.WriteLine("Setting up universe data");

            Console.WriteLine("Completed setup of universe data");
            Console.WriteLine("Setting up NPC Corporation data");
            Console.WriteLine("Setting up NPC Corporation mappings");
            npcCorporationList = JsonConvert.DeserializeObject<List<NPCCorporation>>(File.ReadAllText(Resource1.npccorpsmap, System.Text.Encoding.UTF8));
            Console.WriteLine("Completed Setup of NPC Corporation mappings");
            Console.WriteLine("Retrieving LP Store data from ESI");
            lpStores = JsonConvert.DeserializeObject<List<LPStore>>(File.ReadAllText(Resource1.lpstoredata, System.Text.Encoding.UTF8));
            Console.WriteLine("Retrieved LP Store data from ESI");
            Console.WriteLine("Completed setup of NPC Corporation data");
            Console.WriteLine("Mapping LPStore costs to blueprints");


            mapBlueprintsToLPStoreCosts();
            Console.WriteLine("Mapped LPStore costs to blueprints");
        }

        public static void TestStaticStuff()
        {
            //foreach (GenericItem item in itemList)
            //{
            //    string name = ItemInfoAPI.getItemNameByTypeId(item.id).Result;
            //    Console.WriteLine("ID: " + Convert.ToString(item.id) + " Name: " + name);
            //}
            Console.WriteLine("Total Items: " + itemList.Count);
            Console.WriteLine("Total Blueprints: " + blueprintList.Count);
            Console.WriteLine("Total NPC Corporations: " + npcCorporationList.Count);
            //Console.WriteLine("Total LP Stores: " + lpStores.Count);
        }

        //TODO: This assumes all LP store costs are the same, that needs to be verified
        private static void mapBlueprintsToLPStoreCosts()
        {
            foreach(GenericBlueprint blueprint in blueprintList)
            {
                foreach(LPStore store in lpStores)
                {
                    foreach(LPStoreItem item in store.lpStoreItems)
                    {
                        if (blueprint.BlueprintTypeId.Equals(item.type_id))
                        {
                            blueprint.lpStoreItem = item;
                            Console.WriteLine("Mapped lpstore data to blueprint id " + blueprint.BlueprintTypeId + " Item Name: " + ItemInfoAPI.getItemNameByTypeId(blueprint.BlueprintTypeId).Result + " Quantity: " + Convert.ToString(item.quantity));
                            break;
                        }
                    }
                    if (blueprint.lpStoreItem != null)
                    {
                        break;
                    }
                }
            }
        }

        private static List<LPStore> getAllLPStoreData(List<NPCCorporation> corporations)
        {
            List<LPStore> lpStores = new List<LPStore>();

            foreach(NPCCorporation corporation in corporations)
            {
                List<LPStoreItem> storeItems = LoyaltyPublicAPI.getLPStoreForCorporationID(corporation.id);
                if (storeItems != null)
                {
                    LPStore store = new LPStore(corporation.id, corporation.name, storeItems);
                    lpStores.Add(store);
                    Console.WriteLine("LP Store data retrieved for " + corporation.name + " with ID " + corporation.id);
                }
                else
                {
                    Console.WriteLine("LP Store data did not exist for " + corporation.name + " with ID " + corporation.id);
                }
            }
            return lpStores;
        }
    }
}
