using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using NHibernate;
using DBMapping.BOL;

namespace DBMapping.DAL
{
    public class SortedSizeDao : GenericDao<SortedSize>
    {

        public IList<SortedSize> RemoveCheckedFromSource(IList<SortedSize> source)
        {
            var query = source
                        .Where(Q=> Q.bChecked == false)
                        .ToList<SortedSize>();

            return query;
        }


        //called first time the source is binded
        public IList<SortedSize> SetSourceCheckedFalse(IList<SortedSize> source)
        {
            var query = source
                        .Select(Q => { Q.bChecked = false; return Q; })
                        .ToList<SortedSize>();
            return query;
        }
    }
}
