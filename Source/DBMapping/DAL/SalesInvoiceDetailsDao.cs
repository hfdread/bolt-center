using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using DBMapping.BOL;

namespace DBMapping.DAL
{
    public class SalesInvoiceDetailsDao : GenericDao<SalesInvoiceDetails>
    {
        public IList<SalesInvoiceDetails> GetDetails(SalesInvoice invoice)
        {
            using (ISession session = getSession())
            {
                var query = (from salesinvoicedetails in session.QueryOver<SalesInvoiceDetails>()
                             where salesinvoicedetails.salesinvoice.ID == invoice.ID
                             select salesinvoicedetails).List<SalesInvoiceDetails>();

                return query;
            }
        }

        public SalesInvoiceDetails GetDetails(Int32 detailsID, SalesInvoice invoice)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<SalesInvoiceDetails>()
                            .Where(sid => sid.salesinvoice.ID == invoice.ID && sid.ID == detailsID)
                            .SingleOrDefault<SalesInvoiceDetails>();

                return query;
            }
        }
    }
}
