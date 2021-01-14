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

        public static List<GenericBlueprint> blueprintList { get; set; }

        static GlobalMappings()
        {
            Console.WriteLine("Setting up ItemId -> Name mappings");
            itemList = JsonConvert.DeserializeObject<List<GenericItem>>(File.ReadAllText(Resource1.idnamemap, System.Text.Encoding.UTF8));
            Console.WriteLine("Completed setup of ItemId -> Name mappings");
        }

        public static void TestStaticStuff()
        {
            Console.WriteLine("TotalItems: " + itemList.Count);

        }
    }
}
