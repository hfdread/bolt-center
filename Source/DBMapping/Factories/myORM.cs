using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Cfg;

namespace DBMapping.Factories
{
    public sealed class myORM
    {
        private static bool m_Initialized = false;
        private static string m_ErrorMessage = "";
        private static ISessionFactory sessionFactory = null;

        static myORM()
        {
            InitializeORM();
        }

        private static void InitializeORM()
        {
            try
            {
                Configuration cfg = new Configuration();
                clsDbConnect db = new clsDbConnect();

                if (!db.isValid())
                    throw new Exception("Invalid DB Connection");

                cfg.Properties["connection.connection_string"] =
                    string.Format("Server={0};Database=jim_boltcenter;uid={1};pwd={2}", db.DB_SERVER, db.DB_USER, db.DB_PWD);

                cfg.AddAssembly("DBMapping");

                sessionFactory = cfg.BuildSessionFactory();
                m_Initialized = true;
            }
            catch (Exception ex)
            {
                m_Initialized = false;
                if (ex.InnerException == null)
                    m_ErrorMessage = ex.Message;
                else
                    m_ErrorMessage = string.Format("{0}\n\nInnerException:\n{1}", ex.Message, ex.InnerException.ToString());
            }
        }

        public static bool isInitialized()
        {
            return m_Initialized;
        }

        public static string lastErrorMessage()
        {
            return m_ErrorMessage;
        }

        public static ISession GetCurrentSession()
        {
            if (sessionFactory != null)
                return sessionFactory.OpenSession();
            else 
            {
                InitializeORM();
                if (sessionFactory != null)
                    return sessionFactory.OpenSession();
                else
                    return null;
            }
        }

        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
                sessionFactory.Close();
        }
    }
}
