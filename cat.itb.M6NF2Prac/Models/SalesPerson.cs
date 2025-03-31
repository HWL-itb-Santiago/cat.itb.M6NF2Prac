using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.Models
{
    public class SalesPerson
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Job {  get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual float Salary { get; set; }
        public virtual float? Commission { get; set; }
        public virtual string Dep {  get; set; }
        public virtual IList<Product> Products { get; set; }// un salesperson puede vender uno o muchos productos
    }
}
