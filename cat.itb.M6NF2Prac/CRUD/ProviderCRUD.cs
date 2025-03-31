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
    public class ProviderCRUD
    {
        // Metodos ADO
        /// <summary>
        /// Update a provider en la base de datos
        /// </summary>
        /// <param name="provider"></param>
        public void Update(Provider provider)
        {
            try
            {
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        try
                        {
                            session.Update(provider);
                            tx.Commit();
                            Console.WriteLine($"producto {provider.Name} actualizado");
                        }
                        catch (Exception)
                        {
                            if (tx.IsActive)
                            {
                                tx.Rollback();
                                Console.WriteLine($"Error al actualizar el producto {provider.Name}");
                            }
                        }
                    }
                    session.Close();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error al actualizar el producto");
            }
        }
        /// <summary>
        /// Selecciona los proveedores cuya ciudad sea la pasada por parametro
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public IList<Provider> SelectByCity(string city)
        {
            try
            {
                IList<Provider> providers;
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    providers = session.CreateQuery("SELECT p FROM Provider p WHERE lower(p.City) LIKE lower(:city)")
                        .SetParameter("city", city)
                        .List<Provider>();
                    session.Close();
                }
                return providers;
            }
            catch (Exception)
            {
                Console.WriteLine("Error al extraer los datos");
                return null;
            }
        }
        /// <summary>
        /// Inserta una lista de proveedores en la base de datos
        /// </summary>
        /// <param name="providers"></param>
        /// <returns></returns>
        public bool Insert(IList<Provider> providers)
        {
            try
            {
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        foreach (var item in providers)
                        {
                            session.Save(item);
                            Console.WriteLine($"Empleado {item.Id} ingresado con exito");
                        }
                        tx.Commit();
                        session.Close();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// Selecciona los proveedores cuyo credito sea el menor
        /// </summary>
        /// <returns></returns>
        public IList<Provider> SelectLowestAmount()
        {
            try
            {
                IList<Provider> providers;
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    QueryOver<Provider> minAmount = QueryOver.Of<Provider>().SelectList(list => list.SelectMin(p => p.Amount));

                    IList<Provider> minAmountList = session.QueryOver<Provider>()
                        .WithSubquery.WhereProperty(p => p.Amount).Eq(minAmount)
                        .List();

                    providers = minAmountList;
                    session.Close();
                    return providers;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al extraer los datos");
                return null;
            }
        }


        /// <summary>
        /// Selecciona los proveedores cuyo credito sea menor al pasado por parametro
        /// </summary>
        /// <param name="credit"></param>
        /// <returns></returns>
        public List<Provider> SelectCreditLowerThanADO(float credit)
        {
            try
            {
                StoreCloudConnection db = new StoreCloudConnection();
                using (var con = db.GetConnection())
                {
                    //var sql = "SELECT * FROM provider WHERE credit < " + credit + "";
                    var sql = "SELECT p.*, pr.* FROM provider p JOIN product pr ON p.id = pr.id WHERE p.credit < " + credit + "";
                    var cmd = new Npgsql.NpgsqlCommand(sql, con);
                    var reader = cmd.ExecuteReader();
                    List<Provider> providers = new List<Provider>();
                    while (reader.Read())
                    {
                        Provider provider = new Provider();
                        provider.Id = reader.GetInt32(0);
                        provider.Name = reader.GetString(1);
                        provider.Addres = reader.GetString(2);
                        provider.City = reader.GetString(3);
                        provider.StCode = reader.GetString(4);
                        provider.ZipCode = reader.GetString(5);
                        provider.Area = reader.GetInt32(6);
                        provider.Phone = reader.GetString(7);
                        provider.Amount = reader.GetInt32(9);
                        provider.Credit = reader.GetFloat(10);
                        provider.Remark = reader.GetString(11).ToString();

                        Product product = new Product();
                        product.Id = reader.GetInt32(12);
                        product.Code = reader.GetInt32(13);
                        product.Description = reader.GetString(14);
                        product.CurrentStock = reader.GetInt32(15);
                        product.MinStock = reader.GetInt32(16);
                        product.Price = reader.GetFloat(17);
                        provider.Product = product;
                        providers.Add(provider);
                    }
                    con.Close();
                    return providers;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
