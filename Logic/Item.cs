using EveLPBot.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Logic
{
    static class Item
    {
        //TODO: this is brute forcey, look into using a dictionary to do this more cleanly
        public static long getItemIdByItemName(string name)
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

        //TODO: this is brute forcey, look into using a dictionary to do this more cleanly
        public static GenericBlueprint getBlueprintByItemId(long itemId)
        {
            foreach (GenericBlueprint item in GlobalMappings.blueprintList)
            {
                Console.WriteLine("Checking item: " + item.BlueprintTypeId);
                if (item.BlueprintTypeId.Equals(itemId))
                {

                    Console.WriteLine(item.BlueprintTypeId);
                    return item;
                }
            }

            Console.Write("Item " + itemId + " Not Found");
            return null;
        }
    }
}
