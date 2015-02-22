using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using DBMapping.BOL;

namespace DBMapping.DAL
{
    public class SalesInvoiceDao : GenericDao<SalesInvoice>
    {

        public void SaveEditedInvoice(SalesInvoice invoice)
        {
            using (ISession session = getSession())
            {
                using (ITransaction trx = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate("SalesInvoice", invoice);

                        foreach (SalesInvoiceDetails details in invoice.details)
                        {
                            if (details.bEdited)//flag for edited
                            {
                                SalesInvoiceDetails old = GetDetailsData(details.ID, invoice.ID);
                                bool bMatch = false;

                                //not same item
                                if (old.item.ID != details.item.ID)//stockout stocks from previous item
                                {
                                    bMatch = true;
                                    if (old.item.Unit.Trim() != "")
                                    {
                                        old.item.OnHand -= old.QTY;
                                        old.item.UnitPrice = old.item.LastPrice;
                                    }
                                    else if (old.item.Unit2.Trim() != "")
                                    {
                                        old.item.OnHandWeight -= old.QTY;
                                        old.item.UnitPrice2 = old.item.LastPrice;
                                    }

                                    session.SaveOrUpdate("Item",old.item);

                                }                                

                                Item updateItem = details.item;

                                if (updateItem.Unit.Trim() == "")//weight item
                                {
                                    if (old.QTY > details.QTY && !bMatch)//meaning minus kay less ang new value
                                        updateItem.OnHandWeight -= (old.QTY - details.QTY);
                                    else if(old.QTY < details.QTY && !bMatch)//ni increase ang value so ang increase ray i.add
                                        updateItem.OnHandWeight += (details.QTY - old.QTY);
                                    else
                                        updateItem.OnHandWeight += details.QTY;

                                    if (details.Price != details.item.UnitPrice2)
                                    {
                                        updateItem.LastPrice = updateItem.UnitPrice2;
                                        updateItem.UnitPrice2 = details.Price;
                                    }
                                }
                                else//non-weight
                                {
                                    if (old.QTY > details.QTY && !bMatch)//meaning minus kay less ang new value
                                        updateItem.OnHand -= (old.QTY - details.QTY);
                                    else if (old.QTY < details.QTY && !bMatch)//ni increase ang value so ang increase ray i.add
                                        updateItem.OnHand += (details.QTY - old.QTY);
                                    else
                                        updateItem.OnHand += details.QTY;

                                    //check if price is updated
                                    if (details.Price != details.item.UnitPrice)
                                    {
                                        updateItem.LastPrice = updateItem.UnitPrice;
                                        updateItem.UnitPrice = details.Price;
                                    }
                                }

                                updateItem.UpdateDate = DateTime.Now;
                                session.SaveOrUpdate("Item", updateItem);

                                session.SaveOrUpdate("SalesInvoiceDetails",details);
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

        public SalesInvoiceDetails GetDetailsData(Int32 detailsID, Int32 invoiceID)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<SalesInvoiceDetails>()
                             .Where(detail => detail.ID == detailsID && detail.salesinvoice.ID == invoiceID)
                             .SingleOrDefault<SalesInvoiceDetails>();

                return query;
            }
        }

        public void SaveInvoice(SalesInvoice invoice)
        {
            using (ISession session = getSession())
            {
                using (ITransaction trx = session.BeginTransaction())
                {
                    try
                    {
                        session.Save("SalesInvoice", invoice);

                        foreach (SalesInvoiceDetails details in invoice.details)
                        {
                            details.salesinvoice = invoice;
                            session.Save("SalesInvoiceDetails", details);

                            Item updateItem = details.item;

                            if (updateItem.Unit.Trim() == "")//weight item
                            {
                                updateItem.OnHandWeight += details.QTY;
                                if (details.Price != details.item.UnitPrice2)
                                {
                                    updateItem.LastPrice = updateItem.UnitPrice2;
                                    updateItem.UnitPrice2 = details.Price;
                                    updateItem.RetailPrice = details.Price;
                                }
                            }
                            else//non-weight
                            {
                                updateItem.OnHand += details.QTY;
                                //check if price is updated
                                if (details.Price != details.item.UnitPrice)
                                {
                                    updateItem.LastPrice = updateItem.UnitPrice;
                                    updateItem.UnitPrice = details.Price;
                                    updateItem.RetailPrice = details.Price;
                                }
                            }

                            updateItem.UpdateDate = DateTime.Now;
                            session.SaveOrUpdate("Item", updateItem);
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

        public void SetInvoiceDeleted(SalesInvoice invoice)
        {
            using (ISession session = getSession())
            {
                using (ITransaction trx = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate("SalesInvoice", invoice);
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

        public IList<SalesInvoice> Search(string sItemFilter, Int32 CustomerID, DateTime dateFrom, DateTime dateTo, bool bIncludeDeleted)
        {
            using (ISession session = getSession())
            {
                string SQL = "";
                if (sItemFilter != "")
                {
                    SQL += string.Format("select distinct sivd.salesinvoice from SalesInvoiceDetails sivd where {0}", sItemFilter);
                    SQL += string.Format(" and sivd.salesinvoice.InvoiceDate between '{0}' and '{1}'", dateFrom.ToString(FMT_MYSQL_DATE_START), dateTo.ToString(FMT_MYSQL_DATE_END));

                    if (bIncludeDeleted)
                        SQL += string.Format(" and (sivd.salesinvoice.Deleted={0} or sivd.salesinvoice.Deleted={1})", bIncludeDeleted, !bIncludeDeleted);
                    else
                        SQL += string.Format(" and sivd.salesinvoice.Deleted={0}", bIncludeDeleted);

                    if (CustomerID > 0)
                        SQL += string.Format(" and sivd.salesinvoice.Supplier.ID={0}", CustomerID);
                }
                else
                {
                    SQL += string.Format("from SalesInvoice siv where siv.InvoiceDate between '{0}' and '{1}'", dateFrom.ToString(FMT_MYSQL_DATE_START), dateTo.ToString(FMT_MYSQL_DATE_END));

                    if (bIncludeDeleted)
                        SQL += string.Format(" and (siv.Deleted={0} or siv.Deleted={1})", bIncludeDeleted, !bIncludeDeleted);
                    else
                        SQL += string.Format(" and siv.Deleted={0} ", bIncludeDeleted);

                    if (CustomerID > 0)
                        SQL += string.Format(" and siv.Supplier.ID={0}", CustomerID);
                }

                

                return session.CreateQuery(SQL).List<SalesInvoice>();
            }
        }
    }
}
