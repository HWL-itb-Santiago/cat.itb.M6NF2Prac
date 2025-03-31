using cat.itb.M6NF2Prac.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.Maps
{
    public class ClientMAP : ClassMap<Client>
    {
        public ClientMAP()
        {
            Table("client");
            Id(x => x.Id);
            Map(x => x.Name).Column("name");
            Map(x => x.Code).Column("code");
            Map(x => x.Credit).Column("credit");
            HasMany(x => x.OrderProds)
                .KeyColumn("id")
                .Not.LazyLoad()
                .Inverse()
                .Cascade.AllDeleteOrphan();
        }
    }
}
