using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Model
{
    class GenericBlueprint
    {
        Dictionary<GenericItem, long> resourcesRequired;

        public int MaterialEfficiency { get; set; }


        public int TimeEfficiency { get; set; }

        
        public long BuildTime { get; set; } //in seconds

        //No arg constructor for purposes of serialization
        public GenericBlueprint()
        {
        }

        public GenericBlueprint(Dictionary<GenericItem, long> resourcesRequired)
        {

        }
    }
}
