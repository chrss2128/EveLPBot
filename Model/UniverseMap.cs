using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Model
{
    class UniverseMap
    {
        public long regionId { get; set; }

        public string regionName { get; set; }

        public long constellationId { get; set; }

        public string constellationName { get; set; }

        public long systemId { get; set; }

        public string systemName { get; set; }

        public long planetId { get; set; }

        public long moonId { get; set; }


        public UniverseMap()
        {

        }
    }
}
