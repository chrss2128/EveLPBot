using EveLPBot.Data;

using Npgsql;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EveLPBot.API.Database
{
    static class PostgresCommon
    {
        public static readonly string connectionString;

        static PostgresCommon()
        {
            connectionString = File.ReadAllText(Resource1.dbconfig, System.Text.Encoding.UTF8);
        }


        public static NpgsqlConnection getConnection()
        {
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);

            return conn;
        }
    }
}
