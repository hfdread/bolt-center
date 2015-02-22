using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class Item
    {
        public virtual Int32 ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Code { get; set; }
        public virtual string Unit { get; set; }
        public virtual string Unit2 { get; set; }
        public virtual ItemSizes Size { get; set; }
        public virtual double UnitPrice { get; set; }
        public virtual double UnitPrice2 { get; set; }
        public virtual double LastPrice { get; set; }
        public virtual double RetailPrice { get; set; }
        public virtual Int32 LowThreshold { get; set; }
        public virtual Int32 OnHand { get; set; }
        public virtual Int32 OnHandWeight { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual DateTime CreateDate { get; set; }

        //unmapped property
        public virtual bool isDirty { get; set; }

        public bool isLowCount()
        {
            if (OnHand > LowThreshold)
                return false;
            else
                return true;
        }

    }
}
