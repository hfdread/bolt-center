using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class ReceiptDetails
    {
        public virtual Int32 ID { get; set; }
        public virtual Int32 ItemIndex { get; set; }
        public virtual Receipt receipt { get; set; }
        public virtual Int32 QTY { get; set; }
        public virtual Item item { get; set; }
        public virtual double UnitPrice { get; set; }
        public virtual string Discount { get; set; }
        public virtual double SubTotal { get; set; }

        //unmapped properties
        public virtual string itemName { get; set; }
        public virtual string itemSize { get; set; }
        public virtual string DiscountedPrice { get; set; }
        public virtual double OrigPrice { get; set; }
        public virtual bool bEdited { get; set; }
        public virtual string _Unit { get; set; }
        public virtual string _ItemPriceDiscount { get; set; }
    }
}
