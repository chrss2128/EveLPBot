using Npgsql;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveLPBot.API.Database
{
    


    static class ItemInfoAPI
    {

        

        public async static Task<String> getItemNameByTypeId(long id)
        {

            String name = "";

            var conn = PostgresCommon.getConnection();

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT \"typeName\" FROM evesde.\"invTypes\" WHERE \"typeID\" = @id;", conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                name = (String)cmd.ExecuteScalarAsync().Result;

            }

            return name;
        }

        public async static Task<long> getTypeIdByItemName(String name)
        {
            long id;

            var conn = PostgresCommon.getConnection();

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT \"typeID\" FROM evesde.\"invTypes\" WHERE \"typeName\" = @name;", conn))
            {
                cmd.Parameters.AddWithValue("name", name);
                id = Convert.ToInt64(cmd.ExecuteScalarAsync().Result);

            }

            return id;
        }

    }
}
