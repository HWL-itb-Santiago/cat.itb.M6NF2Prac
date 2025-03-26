using cat.itb.M6NF2Prac.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.Maps
{
    public class OrderProdMAP : ClassMap<OrderProd>
    {
        public OrderProdMAP()
        {
            Table("orderprod");
            Id(x => x.Id);
            Map(x => x.OrderDate).Column("orderdate");
            Map(x => x.Amount).Column("amount");
            Map(x => x.DeliveryDate).Column("deliverydate");
            Map(x => x.Cost).Column("cost");
            References(x => x._Product)
                .Column("product")
                .Not.LazyLoad()
                .Fetch.Join()
                .Unique();
            References(x => x._Client)
                .Column("client")
                .Not.LazyLoad()
                .Fetch.Join()
                .Unique();
        }
    }
}
