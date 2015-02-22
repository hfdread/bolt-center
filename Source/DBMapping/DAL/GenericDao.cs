using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace DBMapping.DAL
{
    public class GenericDao<T>
    {
        public const string FMT_MYSQL_DATE = "yyyy-MM-dd HH-mm-ss";
        public const string FMT_MYSQL_DATE_START = "yyyy-MM-dd 00:00:00";
        public const string FMT_MYSQL_DATE_END = "yyyy-MM-dd 23:59:59";

        public string ErrorMessage = "";
        public GenericDao()
        {
            ISession session = Factories.myORM.GetCurrentSession();
            if (!Factories.myORM.isInitialized())
                ErrorMessage = Factories.myORM.lastErrorMessage();
        }

        public bool IsInitialized()
        {
            return Factories.myORM.isInitialized();
        }

        public ISession getSession()
        {
            return Factories.myORM.GetCurrentSession();
        }

        public virtual T GetById(int id)
        {
            using (ISession session = getSession())
            {
                return (T)session.Get(typeof(T), id);
            }
        }

        public IList<T> GetByIdList(int id)
        {
            ICriteria targetObjects = getSession().CreateCriteria(typeof(T))
                .Add(Restrictions.Eq("ID", id));
            IList<T> itemList = targetObjects.List<T>();
            getSession().Close();
            return itemList;
        }

        public virtual IList<T> getAllRecords()
        {
            ICriteria targetObjects = getSession().CreateCriteria(typeof(T));
            IList<T> itemList = targetObjects.List<T>();
            return itemList;
        }

        public virtual void Save(T item)
        {
            using (ISession session = getSession())
            {
                using (session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(item);
                        session.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        session.Transaction.Rollback();
                        SetErrorMessage(ex);
                        throw;
                    }
                }
            }
        }

        public virtual void Delete(T item)
        {
            using (ISession session = getSession())
            {
                using (session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(item);
                        session.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        session.Transaction.Rollback();
                        SetErrorMessage(ex);
                        throw;
                    }
                }
            }
        }


        public virtual void SetErrorMessage(Exception ex)
        {
            if (ex.InnerException == null) ErrorMessage = ex.Message;
            else
            {
                ErrorMessage = string.Format("{0}\n\nInnerException:\n{1}", ex.Message, ex.InnerException.ToString());
            }
        }
    }
}
