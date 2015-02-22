using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using DBMapping.BOL;


namespace DBMapping.DAL
{
    public class UserDao : GenericDao<User>
    {
        public User LogIn(string username, string password)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<User>()
                            .Where(U => U.username == username && U.password == password)
                            .SingleOrDefault<User>();

                return query;
            }
        }

        public void CreateAdmin()
        {
            using (ISession session = getSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        User user = session.QueryOver<User>()
                                    .Where(U=> U.username == "Admin")
                                    .SingleOrDefault<User>();
                        if (user == null)
                        {
                            user = new User();
                            user.username = "Admin";
                            user.password = "admin";
                            user.authentication = 3;
                            user.fname = "";
                            user.lname = "";

                            session.SaveOrUpdate(user);
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public string GetUserName(Int32 uID)
        {
            using (ISession session = getSession())
            {
                var _user = session.QueryOver<User>()
                            .Where(U => U.ID == uID)
                            .SingleOrDefault<User>();

                if (_user != null)
                    return _user.username;
                else
                    return "";
            }
        }

        public IList<User> GetUserList(int role)
        { 
            /*
            0-sales
            1-cashier
            2-maintenance
            3-administrator
            */

            using (ISession session = getSession())
            {
                var query = session.QueryOver<User>()
                            .Where(U => U.authentication == role)
                            .List<User>();

                return query;
            }
        }
    }
}
