using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using DBMapping.BOL;

namespace DBMapping.DAL
{
    public class ForwardersDao : GenericDao<Forwarders>
    {
        public int GetSelectedForwarder(Forwarders fw)
        {
            using (ISession session = getSession())
            {
                var query = (from forwarders in session.QueryOver<Forwarders>()
                             where forwarders.ID == fw.ID
                             select forwarders).SingleOrDefault();
                if (query.ID > 0)
                    return query.ID;
                else
                    return 0;
            }
        }
    }
}
