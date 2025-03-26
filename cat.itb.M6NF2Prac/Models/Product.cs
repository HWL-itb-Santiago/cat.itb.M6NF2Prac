﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.Models
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual int Code { get; set; }
        public virtual string Description { get; set; }
        public virtual int CurrentStock { get; set; }
        public virtual int MinStock { get; set; }
        public virtual float Price { get; set; }
        public virtual SalesPerson SalesP {  get; set; } //one-many
        public virtual IList<OrderProd> OrderProds { get; set; }
        public virtual Provider _Provider { get; set; }
    }
}
