using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using DBMapping.BOL;

namespace DBMapping.DAL
{
    public class WeightDao : GenericDao<Weight>
    {
        public int GetConverter(string Measurement)
        {
            using (ISession session = getSession())
            {
                var query = (from w in session.Query<Weight>()
                             where w.Measurement == Measurement
                             select w.Converter)
                             .SingleOrDefault<int>();
                return query;
            }
        }
    }
}
