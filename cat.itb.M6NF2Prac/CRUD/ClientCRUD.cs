using cat.itb.M6NF2Prac.Connections;
using cat.itb.M6NF2Prac.Models;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.CRUD
{
    public class ClientCRUD
    {
        //Metodos NHibernate
        /// <summary>
        /// Selecciona clientes cuyo credito sea mayor al pasado por parametro
        /// </summary>
        /// <param name="credit"></param>
        /// <returns></returns>
        public IList<Client> SelectByCredtiHigherThan(double credit)
        {
            try
            {
                IList<Client> clients;
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    clients = session.Query<Client>()
                        .Where(c => c.Credit > credit)
                        .ToList();
                    session.Close();
                }
                return clients;
            }
            catch (Exception)
            {
                Console.WriteLine("Error al extraer los datos");
                return null;
            }
        }

        /// <summary>
        /// Selecciona todos los clientes de la base de datos
        /// </summary>
        /// <returns></returns>
        public IList<Client> SelectAll()
        {
            try
            {
                IList<Client> clients;
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    clients = session.CreateSQLQuery("SELECT * FROM client")
                        .AddEntity(typeof(Client))
                        .List<Client>();
                }
                return clients;
            }
            catch (Exception)
            {
                Console.WriteLine("Error al extraer los datos");
                return null;
            }
        }
        /// <summary>
        /// Selecciona un cliente por su nombre
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<Client> SelectByName(string name)
        {
            try
            {
                IList<Client> clients;
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    var hql = "SELECT c FROM Client c WHERE lower(c.Name) LIKE lower(:name)";
                    clients = session.CreateQuery(hql)
                        .SetParameter("name", name)
                        .List<Client>();
                    session.Close();
                }
                return clients;
            }
            catch (Exception)
            {
                Console.WriteLine("Error al extraer los datos");
                return null;
            }
        }


        // Metodos ADO
        /// <summary>
        /// Elimina un cliente de la base de datos
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool DeleteADO(Client client)
        {
            try
            {
                StoreCloudConnection db = new StoreCloudConnection();
                using (var con = db.GetConnection())
                {
                    var sql = "DELETE FROM client WHERE id = @id";
                    var cmd = new Npgsql.NpgsqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("id", client.Id);
                    cmd.Prepare();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (result == 1)
                    {
                        Console.WriteLine($"Cliente eliminado con éxito Client ID - {client.Id}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"No se encontró el cliente {client.Name}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar al cliente {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Selecciona un cliente por su nombre
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Client SelectByNameADO(string name)
        {
            try
            {
                StoreCloudConnection db = new StoreCloudConnection();
                using (var con = db.GetConnection())
                {
                    var client = new Client();
                    var sql = "SELECT * FROM client WHERE name = @name";
                    var cmd = new Npgsql.NpgsqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Prepare();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        client.Id = reader.GetInt32(0);
                        client.Code = reader.GetInt32(1);
                        client.Name = reader.GetString(2);
                        client.Credit = reader.GetDouble(3);
                    }
                    con.Close();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al extraer el cliente {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Inserta una lista de clientes en la base de datos
        /// </summary>
        /// <param name="clients"></param>
        public void InsertADO(List<Client> clients)
        {
			try
			{
				StoreCloudConnection db = new StoreCloudConnection();
                using (var conn = db.GetConnection())
                {
                    foreach (var client in clients)
                    {
                        var sql = $"INSERT INTO client (code, name, credit) VALUES ('{client.Code}', '{client.Name}', '{client.Credit}')";
                        var cmd = new Npgsql.NpgsqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Cliente {client.Name} insertado con éxito");
                    }
                    conn.Close();
                }
                Console.WriteLine("Clientes insertados correctamente");
            }
			catch (Exception ex)
			{
                Console.WriteLine($"Error al insertar los clientes {ex.Message}");
			}
        }
    }
}
