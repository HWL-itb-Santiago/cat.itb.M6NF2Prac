using cat.itb.M6NF2Prac.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.CRUD
{
    public class ProductCRUD
    {
        public void SelectAll()
        {
			try
			{
				using (var session = SessionFactoryStore.Open())
				{

				}
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
