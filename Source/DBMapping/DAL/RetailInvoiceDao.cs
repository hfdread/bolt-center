using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using DBMapping.BOL;

namespace DBMapping.DAL
{
    public class RetailInvoiceDao : GenericDao<RetailInvoice>
    {

        public void SaveRetailPartially(RetailInvoice retail)
        {
            using (ISession session = getSession())
            {
                using (ITransaction trx = session.BeginTransaction())
                {
                    try
                    {
                        retail.updated = DateTime.Now;
                        session.SaveOrUpdate("RetailInvoice", retail);
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

        public void SaveRetail(RetailInvoice retail)
        {
            using (ISession session = getSession())
            {
                using (ITransaction trx = session.BeginTransaction())
                {
                    try
                    {
                        retail.updated = DateTime.Now;
                        session.SaveOrUpdate("RetailInvoice",retail);

                        foreach (RetailInvoiceDetails dt in retail.details)
                        {
                            RetailInvoiceDetails checkDetail = GetRetailInvoiceDetails(retail.ID, dt.ID);
                            if (checkDetail != null)
                            {
                                Item updateItem = checkDetail.item;
                                Item newItem = dt.item;
                                if (checkDetail.item.ID != dt.item.ID)//change Item
                                {
                                    updateItem.OnHand += checkDetail.QTY;//revert back the stocks
                                    newItem.OnHand -= dt.QTY;//get stocks from new item

                                    session.SaveOrUpdate("Item", updateItem);
                                    session.SaveOrUpdate("Item", newItem);
                                }
                                else if (checkDetail.QTY != dt.QTY)//changed quantity
                                {
                                    if (checkDetail.QTY > dt.QTY)//old qty is bigger than the new
                                        newItem.OnHand += (checkDetail.QTY - dt.QTY);
                                    else//mas dako ang new change
                                        newItem.OnHand -= (dt.QTY - checkDetail.QTY);

                                    session.SaveOrUpdate("Item", newItem);
                                }
                            }
                            else
                            {
                                dt.item.OnHand -= dt.QTY;

                                session.SaveOrUpdate("Item", dt.item);
                            }

                            session.SaveOrUpdate("RetailInvoiceDetails", dt);
                        }

                        trx.Commit();
                    }
                    catch(Exception ex)
                    {
                        SetErrorMessage(ex);
                        if (!trx.WasRolledBack)
                            trx.Rollback();
                        throw;
                    }
                }
            }
        }

        public RetailInvoiceDetails GetRetailInvoiceDetails(Int32 retailID, Int32 detailID)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<RetailInvoiceDetails>()
                            .Where(Q => Q.retailinvoice.ID == retailID && Q.ID == detailID)
                            .SingleOrDefault<RetailInvoiceDetails>();

                return query;
            }
        }

        public IList<RetailInvoiceDetails> GetRetailDetails(Int32 retailID)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<RetailInvoiceDetails>()
                            .Where(q => q.retailinvoice.ID == retailID)
                            .List<RetailInvoiceDetails>();

                return query;
            }
        }

        public IList<RetailInvoice> GetRetailList(int start, int end)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<RetailInvoice>()
                            .Where(q => q.or_number >= start && q.or_number <= end)
                            .List<RetailInvoice>();

                return query;
            }
        }

        public RetailInvoice FindORNumber(int ornumber)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<RetailInvoice>()
                            .Where(q => q.or_number == ornumber)
                            .SingleOrDefault<RetailInvoice>();

                return query;
            }
        }

        public RetailInvoice GetByORNumber(int or_number)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<RetailInvoice>()
                            .Where(q => q.or_number == or_number)
                            .SingleOrDefault<RetailInvoice>();
                return query;
            }
        }
    }
}
