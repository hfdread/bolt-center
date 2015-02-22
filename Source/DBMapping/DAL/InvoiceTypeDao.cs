using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using DBMapping.BOL;

namespace DBMapping.DAL
{
    public class InvoiceTypeDao : GenericDao<InvoiceType>
    {
        public int GetSelectedInvoiceType(InvoiceType invtype)
        {
            using (ISession session = getSession() )
            {
                var query = (from invoicetype in session.QueryOver<InvoiceType>()
                             where invoicetype.ID == invtype.ID
                             select invoicetype).SingleOrDefault();
                if (query.ID > 0)
                    return query.ID;
                else
                    return 0;
            }
        }

        public int GetSelectedInvoiceType(string Code)
        {
            using (ISession session = getSession())
            {
                var query = (from type in session.Query<InvoiceType>()
                             where type.Code == Code
                             select type)
                             .SingleOrDefault();

                if (query == null)
                    return 0;
                else
                    return query.ID;
            }
        }
    }
}
