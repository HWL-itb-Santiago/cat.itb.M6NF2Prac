using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.Models
{
    public class OrderProd
    {
        public virtual int Id { get; set; }
        public virtual Product _Product {  get; set; } //Llave foranea
        public virtual Client _Client {  get; set; } //Llave foranea
        public virtual DateTime OrderDate { get; set; }
        public virtual int Amount { get; set; }
        public virtual DateTime DeliveryDate { get; set; }
        public virtual float Cost { get; set; }

    }
}
