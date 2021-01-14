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
        public static List<GenericItem> itemList = JsonConvert.DeserializeObject<List<GenericItem>>(File.ReadAllText(Resource1.idnamemap, System.Text.Encoding.UTF8));

        public static List<GenericBlueprint> blueprintList { get; set; }

        public static void TestStaticStuff()
        {
            Console.WriteLine("TotalItems: " + itemList.Count);

        }
    }
}
