using EveLPBot.Data;
using EveLPBot.Model;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EveLPBot
{
    static class GlobalMappings
    {
        public static readonly List<GenericItem> itemList;

        public static readonly List<GenericBlueprint> blueprintList;

        public static readonly List<UniverseMap> universeMaps;

        static GlobalMappings()
        {
            Console.WriteLine("Setting up ItemId -> Name mappings");
            itemList = JsonConvert.DeserializeObject<List<GenericItem>>(File.ReadAllText(Resource1.idnamemap, System.Text.Encoding.UTF8));
            Console.WriteLine("Completed setup of ItemId -> Name mappings");
            Console.WriteLine("Setting up blueprint mappings");
            blueprintList = JsonConvert.DeserializeObject<List< GenericBlueprint >> (File.ReadAllText(Resource1.blueprintmap, System.Text.Encoding.UTF8));
            Console.WriteLine("Completed setup of blueprint mappings");
            Console.WriteLine("Setting up universe data");
            universeMaps = JsonConvert.DeserializeObject<List<UniverseMap>>(File.ReadAllText(Resource1.universemap, System.Text.Encoding.UTF8));
            Console.WriteLine("Completed setup of universe data");
        }

        public static void TestStaticStuff()
        {
            Console.WriteLine("Total Items: " + itemList.Count);
            Console.WriteLine("Total Universe Maps: " + universeMaps.Count);
            Console.WriteLine("Total Blueprints: " + blueprintList.Count);
        }
    }
}
