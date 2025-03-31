using cat.itb.M6NF2Prac.Connections;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.CRUD
{
    public class GeneralCRUD
    {
        public void CreateTables()
        {
			try
			{
                var sqlPath = "../../../store.sql";
                if (!File.Exists(sqlPath))
                {
                    Console.WriteLine("El archivo de creacion sql no existe");
                    return;
                }    
                var streamReader = new StreamReader(sqlPath);
                var command = streamReader.ReadToEnd();

                var db = new StoreCloudConnection();
                using (var conn = db.GetConnection())
                {
                    using (var cmd = new NpgsqlCommand(command, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
			}
			catch (Exception)
			{
                Console.WriteLine("Error al crear las tablas");
			}
        }

        public void DeleteTables(List<string> tableNames)
        {
            try
            {
                var db = new StoreCloudConnection();
                using (var conn = db.GetConnection())
                {
                    foreach (var table in tableNames)
                    {
                        var sql = $"DROP TABLE IF EXISTS {table} CASCADE";
                        var cmd = new NpgsqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Tabla {table} eliminada con éxito");
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar las tablas {ex.Message}");
            }
}
    }
}
