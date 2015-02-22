using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using DBMapping.BOL;

namespace DBMapping.DAL
{
    public class ItemSizesDao : GenericDao<ItemSizes>
    {
        public ItemSizes GetByDescription(string desc)
        {
            using (ISession session = getSession())
            {
                var Size = (from itemsizes in session.QueryOver<ItemSizes>()
                         where itemsizes.Description == desc
                         select itemsizes).SingleOrDefault<ItemSizes>();

                return Size;
            }
        }

        public IList<ItemSizes> SearchSizes(string sSearch)
        {
            using (ISession session = getSession())
            {
                var query = (from itemsizes in session.Query<ItemSizes>()
                             where itemsizes.Description.Contains(sSearch)
                             select itemsizes).ToList<ItemSizes>();

                return query;
            }
        }

        public bool CheckSizeExist(string sDescription)
        {
            using (ISession session = getSession())
            {
                var query = session.Query<ItemSizes>()
                            .Where(Q=> Q.Description.Replace(" ","") == sDescription.Replace(" ",""))
                            .SingleOrDefault<ItemSizes>();

                if (query == null)
                    return false;
                else
                    return true;

                //string cmpre = query.Description.Trim();
                //if (cmpre != "")
                //    return true;
                //else
                //{
                //    //check again
                //    string str, str2;
                //    str = query.Description.Replace(" ","");
                //    str2 = sDescription.Replace(" ","");

                //    if (str != str2)
                //        return true;
                //    else
                //        return false;
                //}
            }
        }

        public void EditSave(ItemSizes item)
        {
            base.Save(item);
        }

        public override void Save(ItemSizes item)
        {
            //item.bSizeExist = false;
            using (ISession session = getSession())
            {
                var query = (from itemsizes in session.Query<ItemSizes>()
                             where itemsizes.Description.Trim() == item.Description.Trim()
                             select itemsizes).SingleOrDefault<ItemSizes>();


                if (!CheckSizeExist(item.Description))
                    base.Save(item);
                else
                    item.bSizeExist = true;
            }
        }
    }
}
