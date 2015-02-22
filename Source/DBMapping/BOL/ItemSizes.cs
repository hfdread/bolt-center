using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class ItemSizes
    {
        public virtual Int32 ID { get; set; }
        public virtual string Description { get; set; }

        //not mapped properties
        public virtual bool isSelected { get; set; }
        public virtual bool isDirty { get; set; }
        public virtual bool bSizeExist { get; set; }

        public override string ToString()
        {
            return Description;
        }

    }
}
