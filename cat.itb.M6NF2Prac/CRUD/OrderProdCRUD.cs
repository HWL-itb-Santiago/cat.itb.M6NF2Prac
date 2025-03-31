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
    public class OrderProdCRUD
    {
        // Metodos NHibernate

        /// <summary>
        /// Selecciona las comandas cuyo coste sea mayor al pasado por parametro
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public IList<OrderProd> SelectByCostHigherThan(float cost, int amount)
        {
            try
            {
                IList<OrderProd> orderProducts;
                using (var session = SessionFactoryStoreCloud.Open())
                {
                    orderProducts = session.CreateCriteria<OrderProd>()
                        .Add(Restrictions.Gt("Cost", cost))
                        .Add(Restrictions.Eq("Amount", amount))
                        .List<OrderProd>();
                    session.Close();
                }
                return orderProducts;
            }
            catch (Exception)
            {
                Console.WriteLine("Error al extraer los datos");
                return null;
            }
        }
    }
}
