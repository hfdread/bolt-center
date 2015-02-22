using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using DBMapping.BOL;


namespace DBMapping.DAL
{
    public class ItemDao : GenericDao<Item>
    {

        public IList<Item> GetSearchedItem(string searchText)
        {
            using (ISession session = getSession())
            {
                //string SQL = string.Format("from Item as I where I.Name like '{0}' or I.Description like '{0}'", searchText);
                //return session.CreateQuery(SQL).List<Item>();
                searchText = "%" + searchText + "%";

                var query = (from I in session.Query<Item>()
                             where I.Name.ToLower().Contains(searchText.ToLower())
                             orderby I.Name, I.Size.Description
                             select I)
                             .ToList<Item>();

                //var query = session.QueryOver<Item>()
                //            .Where(I => I.Name.Contains(searchText) || I.Description.Contains(searchText))
                //            .OrderBy(I=>I.UpdateDate).Desc
                //            //.ThenBy(I=>I.Name).Desc
                //            .List<Item>();
                return query;
            }
        }

        public IList<Item> GetSearchedItem2(string searchText)
        {
            var query = new List<Item>();
            using (ISession session = getSession()) 
            {
                if (!string.IsNullOrWhiteSpace(searchText) && searchText.Length > 1)
                {
                    query = (from tbl in session.Query<Item>()
                             where tbl.Name.ToLower().Contains(searchText.ToLower())
                             orderby tbl.Name, tbl.Size.Description
                             select tbl).ToList<Item>();
                }
                else 
                {
                    query = (from tbl in session.Query<Item>()
                             where tbl.Name.ToLower().StartsWith(searchText.ToLower())
                             orderby tbl.Name, tbl.Size.Description
                             select tbl).ToList<Item>();
                }

                return query;
            }
        }

        public IList<Item> GetSearchedItem(ItemSizes size)
        {
            using (ISession session = getSession())
            {
                /*var query = session.QueryOver<Item>()
                            .Where(I => I.Size.ID == size.ID)
                            .OrderBy(I => I.UpdateDate).Desc
                            .ThenBy(I => I.Name).Desc
                            .List<Item>();*/
                var query = (from I in session.Query<Item>()
                             where I.Size.ID == size.ID
                             orderby I.Name, I.Size.Description
                             select I).ToList<Item>();

                return query;
            }
        }

        public IList<Item> GetAllRecordsOrdered()
        {
            using (ISession session = getSession())
            {
                /*var query = session.QueryOver<Item>()
                            .OrderBy(I => I.Name).Asc
                            .ThenBy(I=> I.Size.Description).Desc
                            .List<Item>();*/
                var query = (from I in session.Query<Item>()
                             orderby I.Name, I.Size.Description
                             select I).ToList<Item>();

                return query;
            }
        }

        public Item GetItem(string ItemName, string SizeDescription)
        {
            using (ISession session = getSession())
            {
                var query = (from I in session.Query<Item>()
                             where I.Name == ItemName && I.Size.Description == SizeDescription
                             select I)
                             .SingleOrDefault<Item>();
                return query;
            }
        }

        public Item GetItem(Item nItem, bool bUnit2)
        {
            using (ISession session = getSession())
            {
                var query = (from I in session.Query<Item>()
                             where I.Name == nItem.Name && I.Size.Description == nItem.Size.Description && I.Unit.ToLower() == nItem.Unit.ToLower()
                             select I)
                             .SingleOrDefault<Item>();

                if (bUnit2)//query for unit2
                {
                    query = (from I in session.Query<Item>()
                             where I.Name == nItem.Name && I.Size.Description == nItem.Size.Description && I.Unit2.ToLower() == nItem.Unit2.ToLower()
                             select I)
                             .SingleOrDefault<Item>();
                }

                return query;
            }
        }

        public Item GetItem(string name, string sizeDesc, string unit)
        {
            using (ISession session = getSession())
            {
                var query = (from I in session.Query<Item>()
                             where I.Name == name && I.Size.Description == sizeDesc && I.Unit == unit
                             select I)
                             .SingleOrDefault<Item>();

                if (query == null && unit.Trim() != "")
                {
                    query = (from I in session.Query<Item>()
                             where I.Name == name && I.Size.Description == sizeDesc && I.Unit2 == unit
                             select I)
                             .SingleOrDefault<Item>();
                }

                return query;
            }
        }

        public Item CheckItemNoSize(string itemName, string unit)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<Item>()
                            .Where(Q => Q.Name == itemName && Q.Unit == unit && Q.Size == null)
                            .SingleOrDefault<Item>();

                if (query == null)//try unit2
                {
                    query = session.QueryOver<Item>()
                            .Where(Q => Q.Name == itemName && Q.Unit2 == unit && Q.Size == null)
                            .SingleOrDefault<Item>();
                }

                return query;
            }
        }

        public IList<Item> GetSearchedItem(string itemSearch, ItemSizes size)
        {
            using (ISession session = getSession())
            {
                itemSearch = "%" + itemSearch + "%";
                var query = (from I in session.Query<Item>()
                             where I.Name.ToLower().Contains(itemSearch.ToLower()) && I.Size.ID == size.ID
                             orderby I.Name, I.Size.Description
                             select I)
                             .ToList<Item>();

                //var query = session.QueryOver<Item>()
                //            .Where(Q => Q.Name.Contains(itemSearch) && Q.Size.ID == size.ID)
                //            .OrderBy(Q => Q.UpdateDate).Desc
                //            .ThenBy(Q => Q.Name).Desc
                //            .List<Item>();
                return query;
            }
        }

        public IList<Item> GetSearchedItem(string itemSearch, ItemSizes size, string unit)
        {
            using (ISession session = getSession())
            {
                itemSearch = "%" + itemSearch + "%";
                var query = (from I in session.Query<Item>()
                             where I.Name.ToLower().Contains(itemSearch.ToLower()) && I.Size.ID == size.ID && (I.Unit == unit || I.Unit2 == unit)
                             orderby I.Name, I.Size.Description
                             select I)
                             .ToList<Item>();
                return query;
            }
        }

        public IList<Item> GetSearchedItem(string itemSearch,string unit)
        {
            using (ISession session = getSession())
            {
                itemSearch = "%" + itemSearch + "%";
                var query = (from I in session.Query<Item>()
                             where I.Name.ToLower().Contains(itemSearch.ToLower()) && (I.Unit == unit || I.Unit2 == unit)
                             orderby I.Name, I.Size.Description
                             select I)
                             .ToList<Item>();
                return query;
            }
        }

        public IList<Item> SearchForSingleItem(string itemName, string Size, string Unit)
        {
            var list = new List<Item>();
            using (ISession session = getSession())
            {
                list = (from tbl in session.Query<Item>()
                        where tbl.Name.Equals(itemName)
                        && (tbl.Size.Description.Equals(Size) || Size == "")
                        && (tbl.Unit.Equals(Unit) || Unit == "")
                        select tbl).ToList<Item>();
            }
            return list;
        }

        public IList<Item> GetSearchedItem(ItemSizes size, string unit)
        {
            using (ISession session = getSession())
            {
                var query = (from I in session.Query<Item>()
                             where I.Size.ID == size.ID && (I.Unit == unit || I.Unit2 == unit)
                             orderby I.Name, I.Size.Description
                             select I)
                             .ToList<Item>();
                return query;
            }
        }

        public IList<Item> GetSearchItemByUnit(string unit)
        {
            using (ISession session = getSession())
            {
                var query = (from I in session.Query<Item>()
                             where I.Unit == unit || I.Unit2 == unit
                             orderby I.Name, I.Size.Description
                             select I)
                             .ToList<Item>();
                return query;
            }
        }

        public IList<Item> GetItemList(string itemName)
        {
            using (ISession session = getSession())
            {
                var query = (from I in session.Query<Item>()
                             where I.Name == itemName
                             select I)
                             .ToList<Item>();

                return query;
            }
        }

        public IList<string> GetUnitList(string itemName, string itemSize)
        {
            using (ISession session = getSession())
            {
                var query = (from Q in session.Query<Item>()
                             where Q.Name == itemName && Q.Size.Description == itemSize
                            select Q).ToList<Item>();

                if (itemSize.Trim() == "")
                {
                    query = (from Q in session.Query<Item>()
                             where Q.Name == itemName && Q.Size == null
                             select Q).ToList<Item>();
                }
                            

                IList<string> strList = new List<string>();
                foreach (Item I in query)
                {
                    if (I.Unit.Trim() != "")
                        strList.Add(I.Unit);
                    else if (I.Unit2.Trim() != "")
                        strList.Add(I.Unit2);
                }

                return strList;
            }
        }


        public Item GetItem(string ItemName)
        {
            using (ISession session = getSession())
            {
                var query = (from I in session.Query<Item>()
                             where I.Name == ItemName
                             select I)
                             .SingleOrDefault<Item>();
                return query;
            }
        }

        public IList<string> GetItemSizes(string itemName)
        {
            using (ISession session = getSession())
            {
                string SQL = string.Format("select size.Description from ItemSizes as size" +
                                           " left join Item as I on size.ID=I.SizeID" +
                                            " where I.Name='{0}'",itemName);
                return session.CreateSQLQuery(SQL).List<string>();
            }
        }

        public IList<ItemSizes> GetSizes(string itemName)
        {
            using (ISession session = getSession())
            {
                var query = (from I in session.QueryOver<Item>()
                             where I.Name == itemName
                             select I.Size)
                            .List<ItemSizes>();

                return query;
            }
        }


        public IList<string> GetItemNameSuggestion(string search)
        {
            using (ISession session = getSession())
            {
                search = "%" + search + "%";
                var query = (from I in session.Query<Item>()
                             where I.Name.Contains(search)
                             
                             orderby I.Name
                             select I.Name)
                             .Take(10)
                             .ToList<string>();

                return query;
            }
        }

        public IList<string> GetUniqueItemNames(string search)
        {
            using (ISession session = getSession())
            {
                search = "%" + search + "%";
                var query = (from I in session.Query<Item>()
                             where I.Name.Contains(search)
                             orderby I.Name
                             select I.Name)
                             .Distinct<string>()
                             .Take(10)
                             .ToList<string>();

                return query;
            }
        }

        public IList<string> GetItemNames(string search)
        {
            var list = new List<string>();
            using (ISession session = getSession())
            {
                if (search.Trim().Length == 1) {
                    list = (from tbl in session.Query<Item>()
                                where tbl.Name.ToLower().StartsWith(search.ToLower())
                                orderby tbl.Name
                                select tbl.Name)
                                .Distinct<string>()
                                .ToList<string>();
                }
                else if (search.Trim().Length > 1) {
                    list = (from tbl in session.Query<Item>()
                                where tbl.Name.ToLower().Contains(search.ToLower())
                                orderby tbl.Name
                                select tbl.Name)
                                .Distinct<string>()
                                .ToList<string>();
                }
            }

            return list;
        }

        public void SaveBatch(IList<Item> itemList)
        {
            using (ISession session = getSession())
            {
                using (ITransaction trx = session.BeginTransaction())
                {
                    try 
                    {
                        foreach (Item I in itemList)
                        {
                            //add this to avoid duplicate items during addition
                            Item temp = new Item();
                            if (I.Size != null)
                            {
                                if (I.Unit.Trim() != "")
                                    temp = GetItem(I, false);
                                else
                                    temp = GetItem(I, true);
                                if (temp == null)
                                {
                                    session.Save(I);
                                }
                            }
                            else
                            {
                                temp = GetItem(I.Name);
                                if (temp == null)
                                {
                                    I.Size = null;
                                    session.Save(I);
                                }
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

        public bool CheckDuplicateItem(Item newItem)
        {
            using (ISession session = getSession())
            {
                var queryItem = (from item in session.QueryOver<Item>()
                            where item.Name == newItem.Name &&
                            item.Description == newItem.Description &&
                            item.Code == newItem.Code &&
                            item.Unit == newItem.Unit &&
                            item.Size == newItem.Size
                            select item).SingleOrDefault();

                if (queryItem != null)
                    return true;
                else
                    return false;
            }
        }

        public IList<Item> Search(string sItemFilter)
        {
            using (ISession session = getSession())
            {
                string SQL = "";
                if (sItemFilter != "")
                {
                    SQL += string.Format("from Item I where {0}", sItemFilter);
                }
                else
                {
                    SQL += "from Item I";
                }

                return session.CreateQuery(SQL).List<Item>();
            }
        }

        public IList<Item> GetLimitedRecordOnly(int Max_Count)
        {
            using (ISession session = getSession())
            {
                var query = session.QueryOver<Item>()
                            .Take(Max_Count)
                            .List<Item>();

                return query;
            }
        }

        public IList<Item> GetLimitedRecordOnly_NonWeight(int Max_Count)
        {
            using (ISession session = getSession())
            {
                var query = (from item in session.Query<Item>()
                             where item.Unit2.Trim() == ""
                             select item)
                             .Take(Max_Count)
                             .ToList<Item>();

                return query;
            }
        }

        public IList<Item> GetRecords_NonWeight(string sSearch)
        {
            using (ISession session = getSession())
            {
                var query = (from item in session.Query<Item>()
                             where item.Name.Contains(sSearch) && item.Unit2.Trim() == ""
                             select item)
                             .ToList<Item>();

                return query;
            }
        }

        public IList<string> GetItemNames()
        {
            using (ISession session = getSession())
            {
                var query = (from item in session.Query<Item>()
                             select item.Name)
                             .Distinct<string>()
                             .ToList<string>();

                return query;
            }
        }

        public bool CheckItemHistory(Item I)
        {
            bool bCheck = false;//return false if no history, true otherwise.
            using (ISession session = getSession())
            {
                var query1 = session.QueryOver<SalesInvoiceDetails>()
                             .Where(q1 => q1.item.ID == I.ID)
                             .List<SalesInvoiceDetails>();

                if (query1.Count > 0)
                {
                    bCheck = true;
                    return bCheck;
                }

                var query2 = session.QueryOver<ReceiptDetails>()
                            .Where(q2 => q2.item.ID == I.ID)
                            .List<ReceiptDetails>();

                if (query2.Count > 0)
                {
                    bCheck = true;
                    return bCheck;
                }
            }

            return bCheck;
        }
    }
}
