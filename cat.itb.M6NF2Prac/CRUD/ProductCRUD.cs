using cat.itb.M6NF2Prac.Connections;
using cat.itb.M6NF2Prac.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.CRUD
{
    public class ProductCRUD
    {
        //Metodos ADO

        public IList<object[]> SelectByPriceHigherThan(float price)
        {
            try
            {
                IList<object[]> products;
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    products = session.QueryOver<Product>()
                        .Where(p => p.Price > price)
                        .Select(p => p.Description, p => p.Price)
                        .List<object[]>();
                    session.Close();
                }
                return products;
            }
            catch (Exception)
            {
                Console.WriteLine("Error al extraer los datos");
                return null;
            }
        }
        /// <summary>
        /// Actualiza un producto en la base de datos
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool UpdateADO(Product product)
        {
            try
            {
                StoreCloudConnection db = new StoreCloudConnection();
                using (var con = db.GetConnection())
                {
                    var sql = "UPDATE product SET code = @code, description = @description, currentstock = @currentstock, minstock = @minstock, price = @price WHERE id = @id";
                    var cmd = new NpgsqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("code", product.Code);
                    cmd.Parameters.AddWithValue("description", product.Description);
                    cmd.Parameters.AddWithValue("currentstock", product.CurrentStock);
                    cmd.Parameters.AddWithValue("minstock", product.MinStock);
                    cmd.Parameters.AddWithValue("price", product.Price);
                    cmd.Parameters.AddWithValue("id", product.Id);

                    cmd.Prepare();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (result == 1)
                    {
                        Console.WriteLine($"Producto actualizado con éxito Product ID - {product.Id}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"No se encontró el producto {product.Description}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el producto {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Seleciona un producto por su codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Product SelectByCodeADO(int code)
        {
            try
            {
                StoreCloudConnection db = new StoreCloudConnection();
                using (var con = db.GetConnection())
                {
                    var product = new Product();
                    var sql = "SELECT * FROM product WHERE code = @code";
                    var cmd = new Npgsql.NpgsqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("code", code);
                    cmd.Prepare();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        product.Id = reader.GetInt32(0);
                        product.Code = reader.GetInt32(1);
                        product.Description = reader.GetString(2);
                        product.CurrentStock = reader.GetInt32(3);
                        product.MinStock = reader.GetInt32(4);
                        product.Price = reader.GetFloat(5);
                    }
                    con.Close();
                    return product;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al extraer el producto {ex.Message}");
                throw;
            }
        }

        //Metodos para NHibernate

        /// <summary>
        /// Elimina un empleado de la base de datos
        /// </summary>
        /// <param name="empleado"></param>
        public void Delete(Product empleado)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(empleado);
                        tx.Commit();
                        Console.WriteLine($"El empleado {empleado.Description} se ha eliminado correctamente");
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                            Console.WriteLine($"El empleado {empleado.Description}no se ha eliminado correctamente");
                        }
                    }
                }
                session.Close();
            }
        }
        /// <summary>
        /// Actualiza un empleado
        /// </summary>
        /// <param name="product"></param>
        public void Update(Product product)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(product);
                        tx.Commit();
                        Console.WriteLine($"producto {product.Description} actualizado");
                    }
                    catch (Exception)
                    {
                        if (tx.IsActive)
                        {
                            tx.Rollback();
                            Console.WriteLine($"Error al actualizar el producto {product.Description}");
                        }
                    }
                }
                session.Close();
            }
        }
        /// <summary>
        /// Inserta un nuevo empleado en la base de datos
        /// </summary>
        /// <param name="newProduct"></param>
        public void Insert(List<Product> newProduct)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    foreach (var item in newProduct)
                    {
                        session.Save(item);
                        Console.WriteLine($"Empleado {item.Id} ingresado con exito");
                    }
                    tx.Commit();
                    session.Close();
                }
            }
        }
        /// <summary>
        /// Selecciona todos los empleados de la base de datos
        /// </summary>
        /// <returns></returns>
        public IList<Product>? SelectAll()
        {
            try
            {
                IList<Product> prod;
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    prod = [.. session.Query<Product>()];
                    session.Close();
                }
                return prod;
            }
            catch (Exception)
            {
                Console.WriteLine("Error al encontrar los empleados");
                return null;
            }
        }
        /// <summary>
        /// Selecciona un empleado por ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Product? SelectByID(int Id)
        {
            try
            {
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    var dep = session.Get<Product>(Id);
                    if (dep == null)
                        throw new Exception($"No se encontró el empleado con ID {Id}");
                    session.Close();
                    return dep;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al encontrar al empleado");
                return null;
            }
        }
    }
}
