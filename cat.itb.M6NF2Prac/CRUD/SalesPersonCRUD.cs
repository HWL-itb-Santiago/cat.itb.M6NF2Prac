using cat.itb.M6NF2Prac.Connections;
using cat.itb.M6NF2Prac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.CRUD
{
    public class SalesPersonCRUD
    {
        // Metodos NHibernate

        /// <summary>
        /// Selecciona un vendedor por su nombre
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<SalesPerson> SelectBySurname(string name)
        {
            try
            {
                IList<SalesPerson> salesPersons;
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    var hql = "SELECT s FROM SalesPerson s WHERE lower(s.Name) LIKE lower(:name)";
                    salesPersons = session.CreateQuery(hql)
                        .SetParameter("name", name)
                        .List<SalesPerson>();
                    session.Close();
                }
                return salesPersons;
            }
            catch (Exception)
            {
                Console.WriteLine("Error al extraer los datos");
                return null;
            }
        }

        // Metodos ADO
        /// <summary>
        /// Inserta una lista de vendedores en la base de datos
        /// </summary>
        /// <param name="salesPerson"></param>
        public void InsertADO(List<SalesPerson> salesPerson)
        {
            try
            {
                StoreCloudConnection db = new StoreCloudConnection();
                using (var conn = db.GetConnection())
                {
                    var sql = "INSERT INTO salesperson (surname, job, startdate, commission, dep) VALUES (@surname, @job, @startdate, @commission, @dep)";
                    var cmd = new Npgsql.NpgsqlCommand(sql, conn);
                    foreach (var sales in salesPerson)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("surname", sales.Name);
                        cmd.Parameters.AddWithValue("job", sales.Job);
                        cmd.Parameters.AddWithValue("startdate", sales.StartDate);
                        cmd.Parameters.AddWithValue("commission", sales.Commission.HasValue ? (object)sales.Commission.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("dep", sales.Dep);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    Console.WriteLine("Vendedores insertados con éxito");
                }
            }
            catch (Exception ex )
            {
                Console.WriteLine($"Error al insertar los vendedores {ex.Message}");
            }
        }
    }
}
