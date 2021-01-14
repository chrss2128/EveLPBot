using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Model
{
    public class MarketOrder
    {
        public int duration { get; set; }
        public long location_Id { get; set; }
        public long min_volume { get; set; }
        public long order_id { get; set; }
        public double price { get; set; }
        public string range { get; set; }
        public long system_id { get; set; }
        public long type_id { get; set; }
        public long volume_remain { get; set; } //quantity of remaining of order
        public long volume_total { get; set; } //quantity of initial order


        public MarketOrder()
        {

        }

        public string toString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
