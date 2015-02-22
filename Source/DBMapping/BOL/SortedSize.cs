using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class SortedSize
    {
        public virtual Int32 ID { get; set; }
        public virtual string Description { get; set; }
        public virtual double sort1 { get; set; }
        public virtual double sort2 { get; set; }
        public virtual double sort3 { get; set; }
        public virtual double sort4 { get; set; }

        //unmapped
        public virtual bool bChecked { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
