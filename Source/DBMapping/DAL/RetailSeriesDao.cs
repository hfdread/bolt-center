using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using DBMapping.BOL;

namespace DBMapping.DAL
{
    public class RetailSeriesDao : GenericDao<RetailSeries>
    {

        public void SaveSeries(RetailSeries series)
        {
            using (ISession session = getSession())
            {
                using (ITransaction trx = session.BeginTransaction())
                {
                    try
                    {
                        series.updated = DateTime.Now;
                        session.SaveOrUpdate("RetailSeries", series);
                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        SetErrorMessage(ex);
                        if (!trx.WasRolledBack)
                            trx.Rollback();
                        throw;
                    }
                }
            }
        }

        public RetailSeries LoadLatestSeries()
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<RetailSeries>()
                            .Where(q => q.Status)
                            .OrderBy(q => q.updated).Desc
                            .Take(1)
                            .SingleOrDefault<RetailSeries>();

                return query;
            }
        }

        public RetailSeries GetSeries(int start)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<RetailSeries>()
                            .Where(q => q.Start_series == start)
                            .SingleOrDefault<RetailSeries>();

                return query;
            }
        }

        public IList<RetailSeries> CheckSeriesCollition(int start, int end)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<RetailSeries>()
                            .Where(q => (q.Start_series >= start && q.Start_series <= end) || (q.End_series >= start && q.End_series <= end))
                            .List<RetailSeries>();

                return query;
            }
        }

        public IList<RetailSeries> Search(DateTime from, DateTime to)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<RetailSeries>()
                            .Where(q => q.dte >= from && q.dte <= to)
                            .List<RetailSeries>();

                return query;
            }
        }
    }
}
