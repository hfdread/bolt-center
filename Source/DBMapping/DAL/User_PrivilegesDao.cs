using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using DBMapping.BOL;


namespace DBMapping.DAL
{
    public class User_PrivilegesDao : GenericDao<User_Privileges>
    {

        public User_Privileges GetPrivileges(Int32 iRole)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<User_Privileges>()
                            .Where(P => P.Role == iRole)
                            .SingleOrDefault<User_Privileges>();

                return query;
            }
        }
    }
}
