using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Model
{
    public class RequiredItem
    {
        public int quantity { get; set; }
        public int type_id { get; set; }
    }

    public class LPStoreItem
    {
        public int ak_cost { get; set; }
        public int isk_cost { get; set; }
        public int lp_cost { get; set; }
        public int offer_id { get; set; }
        public int quantity { get; set; }
        public List<RequiredItem> required_items { get; set; }
        public int type_id { get; set; }
    }


}
