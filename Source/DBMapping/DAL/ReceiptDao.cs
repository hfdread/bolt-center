using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using DBMapping.BOL;

namespace DBMapping.DAL
{
    public class ReceiptDao : GenericDao<Receipt>
    {
        public IList<Receipt> Search(string sItemFilter, Int32 CustomerID, Int32 AgentID, DateTime dateFrom, DateTime dateTo)
        {
            using (ISession session = getSession())
            {
                string SQL = "";
                if (sItemFilter != "")
                {
                    SQL += string.Format("select distinct rd.receipt from ReceiptDetails rd where {0}", sItemFilter);
                    SQL += string.Format(" and (rd.receipt.ReceiptDate between '{0}' and '{1}')", dateFrom.ToString(FMT_MYSQL_DATE_START), dateTo.ToString(FMT_MYSQL_DATE_END));

                    SQL += string.Format(" and rd.receipt.isDeleted={0}", false);

                    if (CustomerID > 0)
                        SQL += string.Format(" and rd.receipt.Customer.ID={0}", CustomerID);
                    if(AgentID > 0)
                        SQL += string.Format(" and rd.receipt.Agent.ID={0}", AgentID);
                }
                else
                {
                    SQL += string.Format("from Receipt r where (r.ReceiptDate between '{0}' and '{1}')", dateFrom.ToString(FMT_MYSQL_DATE_START), dateTo.ToString(FMT_MYSQL_DATE_END));

                    SQL += string.Format(" and r.isDeleted={0}", false);

                    if (CustomerID > 0)
                        SQL += string.Format(" and r.Customer.ID={0}", CustomerID);
                    if (AgentID > 0)
                        SQL += string.Format(" and r.Agent.ID={0}", AgentID);
                }


                SQL += string.Format(" ORDER BY r.ReceiptDate DESC");
                return session.CreateQuery(SQL).List<Receipt>();
            }
        }

        public Int32 GetORNumber()
        {
            using (ISession session = getSession())
            {
                string SQL = "select max(r.ID) from Receipt r";

                var lastOR = session.CreateQuery(SQL).UniqueResult();

                if (lastOR == null)
                    return 1;
                else
                    return Convert.ToInt32(lastOR) + 1;
            }
        }

        public void SetReceiptDeleted(Receipt receiptItem)
        {
            using (ISession session = getSession())
            {
                using (ITransaction trx = session.BeginTransaction())
                {
                    try 
                    {
                        receiptItem.isDeleted = true;
                        session.Update(receiptItem);
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

        public void SaveChanges(Receipt receiptItem)
        {
            using (ISession session = getSession())
            {
                using (ITransaction trx = session.BeginTransaction())
                {
                    try
                    {
                        receiptItem.UpdateDate = DateTime.Now;
                        session.SaveOrUpdate("Receipt", receiptItem);

                        foreach (ReceiptDetails details in receiptItem.details)
                        {
                            if (details.bEdited)
                            {
                                ReceiptDetails old = GetReceiptDetails(details.ID, receiptItem.ID);
                                Item updateItem = details.item;
                                bool bMatch = false;

                                //if item is only edited, if new directly save item
                                if (old != null)
                                {
                                    //not same item
                                    if (old.item.ID != details.item.ID)//stockin the old item qty
                                    {
                                        bMatch = true;
                                        if (old.item.Unit.Trim() != "")
                                            old.item.OnHand += old.QTY;
                                        else
                                            old.item.OnHandWeight += old.QTY;

                                        session.SaveOrUpdate("Item", old.item);
                                    }

                                    if (updateItem.Unit.Trim() == "")//weight item
                                    {
                                        if (old.QTY > details.QTY && !bMatch)//meaning add back to stocks kay less ang new value
                                            updateItem.OnHandWeight += (old.QTY - details.QTY);
                                        else if (old.QTY < details.QTY && !bMatch)//change to a big value, so ang increase ra ang i-minus
                                            updateItem.OnHandWeight -= (details.QTY - old.QTY);
                                        else
                                            updateItem.OnHandWeight -= details.QTY;
                                    }
                                    else//non-weight item
                                    {
                                        if (old.QTY > details.QTY && !bMatch)//meaning add back to stocks kay less ang new value
                                            updateItem.OnHand += (old.QTY - details.QTY);
                                        else if (old.QTY < details.QTY && !bMatch)//change to a big value, so ang increase ra ang i-minus
                                            updateItem.OnHand -= (details.QTY - old.QTY);
                                        else
                                            updateItem.OnHand -= details.QTY;
                                    }


                                }
                                else
                                {
                                    updateItem.OnHand -= details.QTY;
                                }

                                updateItem.UpdateDate = DateTime.Now;
                                session.SaveOrUpdate("Item", updateItem);
                                session.SaveOrUpdate("ReceiptDetails", details);
                            }
                        }

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

        public void SaveReceipt(Receipt receiptItem)
        {
            using (ISession session = getSession())
            {
                using (ITransaction trx = session.BeginTransaction())
                {
                    try 
                    {
                        receiptItem.UpdateDate = DateTime.Now;
                        session.Save("Receipt",receiptItem);

                        foreach (ReceiptDetails details in receiptItem.details)
                        {
                            details.receipt = receiptItem;
                            session.Save("ReceiptDetails", details);

                            Item updateItem = details.item;
                            if (updateItem.Unit.Trim() == "")//weight item
                            {
                                updateItem.OnHandWeight -= details.QTY;
                            }
                            else//non-weight item
                            {
                                updateItem.OnHand -= details.QTY;
                            }

                            updateItem.UpdateDate = DateTime.Now;
                            session.SaveOrUpdate("Item", updateItem);
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

        public IList<ReceiptDetails> GetReceiptDetails(Receipt receiptItem)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<ReceiptDetails>()
                            .Where(rd => rd.receipt.ID == receiptItem.ID)
                            .List();

                return query;
            }
        }

        public ReceiptDetails GetReceiptDetails(Int32 detailsID, Int32 receiptID)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<ReceiptDetails>()
                            .Where(Rd => Rd.ID == detailsID && Rd.receipt.ID == receiptID)
                            .SingleOrDefault<ReceiptDetails>();

                return query;
            }
        }
    }
}
