using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.Connections
{
    public class StoreCloudConnection
    {
        private string Username = "santiagovr";
        private string Password = "Chistrees69@";
        private string Database = "santiagovr_m6nf2prac";
        private string Host = "postgresql-santiagovr.alwaysdata.net";

        public NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection($"Username = {Username}; Password = {Password}; Database = {Database}; Host = {Host}");
            conn.Open();
            return conn;
        }
    }
}
