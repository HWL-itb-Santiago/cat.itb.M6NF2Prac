using cat.itb.M6NF2Prac.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.Maps
{
    public class ProductMAP : ClassMap<Product>
    {
        public ProductMAP()
        {
            Table("product");
            Id(x => x.Id);
            Map(x => x.Description).Column("description");
            Map(x => x.CurrentStock).Column("currentstock");
            Map(x => x.MinStock).Column("minstock");
            Map(x => x.Price).Column("price");
            References(x => x.SalesP)
                .Column("salesp")
                .Not.LazyLoad()
                .Fetch.Join()
                .Unique();
            HasOne(x => x._Provider)
                .PropertyRef(nameof(Provider.Id))
                .Not.LazyLoad()
                .Cascade.AllDeleteOrphan()
                .Fetch.Join();
        }
    }
}
