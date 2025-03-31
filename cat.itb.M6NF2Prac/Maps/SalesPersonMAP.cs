using cat.itb.M6NF2Prac.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.Maps
{
    public class SalesPersonMAP : ClassMap<SalesPerson>
    {
        public SalesPersonMAP()
        {
            Table("salesperson");
            Id(x => x.Id);
            Map(x => x.Name).Column("surname");
            Map(x => x.Job).Column("job");
            Map(x => x.StartDate).Column("startdate");
            Map(x => x.Salary).Column("salary");
            Map(x => x.Commission).Column("commission").Nullable();
            Map(x => x.Dep).Column("dep");
            HasMany(x => x.Products)
                .KeyColumn("id")
                .Not.LazyLoad()
                .Inverse()
                .Cascade.AllDeleteOrphan();
        }
    }
}
