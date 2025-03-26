using cat.itb.M6NF2Prac.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.Maps
{
    public class ProviderMAP : ClassMap<Provider>
    {
        public ProviderMAP()
        {
            Table("provider");
            Id(x => x.Id);
            Map(x => x.Name).Column("name");
            Map(x => x.Addres).Column("address");
            Map(x => x.City).Column("city");
            Map(x => x.StCode).Column("stcode");
            Map(x => x.ZipCode).Column("zipcode");
            Map(x => x.Area).Column("area");
            Map(x => x.Phone).Column("phone");
            Map(x => x.Amount).Column("amount");
            Map(x => x.Credit).Column("credit");
            Map(x => x.Remark).Column("remark");
            References(x => x.Product)
                .Column("product")
                .Not.LazyLoad()
                .Fetch.Join()
                .Unique();
        }
    }
}
