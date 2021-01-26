using Npgsql;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveLPBot.API.Database
{
    public static class UniverseInfoAPI
    {

        public static async Task<String> getRegionNameById(long id)
        {
            String name = "";

            var conn = PostgresCommon.getConnection();

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT \"regionName\" FROM evesde.\"mapRegions\" WHERE \"regionID\" = @id;", conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                name = (String)cmd.ExecuteScalarAsync().Result;

            }

            return name;
        }

        public static async Task<String> getConstellationNameById(long id)
        {
            String name = "";

            var conn = PostgresCommon.getConnection();

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT \"constellationID\" FROM evesde.\"mapConstellations\" WHERE \"constellationID\" = @id;", conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                name = (String)cmd.ExecuteScalarAsync().Result;

            }

            return name;
        }

        public static async Task<String> getSystemNameById(long id)
        {
            String name = "";

            var conn = PostgresCommon.getConnection();

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT \"solarSystemName\" FROM evesde.\"mapSolarSystems\" WHERE \"solarSystemID\" = @id;", conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                name = (String)cmd.ExecuteScalarAsync().Result;

            }

            return name;
        }

        public static async Task<String> getStationNameById(long id)
        {
            String name = "";

            var conn = PostgresCommon.getConnection();

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT \"stationName\" FROM evesde.\"staStations\" WHERE \"stationID\" = @id;", conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                name = (String)cmd.ExecuteScalarAsync().Result;

            }

            return name;
        }


        public static async Task<long> getRegionIdByName(string name)
        {
            long id;

            var conn = PostgresCommon.getConnection();

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT \"regionID\" FROM evesde.\"mapRegions\" WHERE \"regionName\" = @name;", conn))
            {
                cmd.Parameters.AddWithValue("name", name);
                id = Convert.ToInt64(cmd.ExecuteScalarAsync().Result);

            }

            return id;
        }

        public static async Task<long> getConstellationIdByName(string name)
        {
            long id;

            var conn = PostgresCommon.getConnection();

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT \"constellationID\" FROM evesde.\"mapConstellations\" WHERE \"constellationName\" = @name;", conn))
            {
                cmd.Parameters.AddWithValue("name", name);
                id = Convert.ToInt64(cmd.ExecuteScalarAsync().Result);

            }

            return id;
        }

        public static async Task<long> getSystemIdByName(string name)
        {
            long id;

            var conn = PostgresCommon.getConnection();

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT \"solarSystemID\" FROM evesde.\"mapSolarSystems\" WHERE \"solarSystemName\" = @name;", conn))
            {
                cmd.Parameters.AddWithValue("name", name);
                id = Convert.ToInt64(cmd.ExecuteScalarAsync().Result);

            }

            return id;
        }

        public static async Task<long> getStationIdByName(string name)
        {
            long id;

            var conn = PostgresCommon.getConnection();

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT \"stationID\" FROM evesde.\"staStations\" WHERE \"stationName\" = @name;", conn))
            {
                cmd.Parameters.AddWithValue("name", name);
                id = Convert.ToInt64(cmd.ExecuteScalarAsync().Result);

            }

            return id;
        }
    }
}
