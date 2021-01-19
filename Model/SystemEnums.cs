using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Model
{
    static class SystemEnums
    {
        //TODO: This could be something databasey
        public static readonly Dictionary<string, long> systemNameStationIdMappings = new Dictionary<string, long>();

        static SystemEnums()
        {
            systemNameStationIdMappings.Add("JITA", 60003760);
        }
    }
}
