using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.Models
{
    public class Client
    {
        public virtual int Id { get; set; }
        public virtual int Code { get; set; }
        public virtual string Name { get; set; }
        public virtual double Credit { get; set; }
        public virtual IList<OrderProd> OrderProds { get; set; }
    }
}
