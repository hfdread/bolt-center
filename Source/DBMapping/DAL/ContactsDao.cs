using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using DBMapping.BOL;

namespace DBMapping.DAL
{
    public class ContactsDao : GenericDao<Contacts>
    {
        public IList<Contacts> GetContactList(int contactType)
        {
            using (ISession session = getSession())
            {
                var query = (from contacts in session.QueryOver<Contacts>()
                             where contacts.Type == contactType
                             select contacts)
                             .OrderBy(c=>c.CompanyName).Asc
                             .ThenBy(c=>c.FirstName).Asc
                             .List<Contacts>();

                return query;
            }
        }
    }
}
