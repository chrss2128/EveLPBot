using Npgsql;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveLPBot.API.Database
{
    public static class UserEnumsAPI
    {
        public static async Task<long> getStationIdByStationEnum(string name)
        {
            long id;

            var conn = PostgresCommon.getConnection();

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT stationid FROM evesde.useruniversemappingenums WHERE stationenum = @name;", conn))
            {
                cmd.Parameters.AddWithValue("name", name.ToLower());
                var result = cmd.ExecuteScalarAsync().Result;

                if (result != null)
                {
                    id = Convert.ToInt64(result);

                }
                else
                {
                    id = 0;
                }
                
            }

            return id;
        }


    }
}
