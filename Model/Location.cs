using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Model
{
    public class Location
    {
        public readonly long regionId;

        public readonly long stationId;

        public Location(long regionId, long stationId)
        {
            this.regionId = regionId;
            this.stationId = stationId;
        }
    }
}
