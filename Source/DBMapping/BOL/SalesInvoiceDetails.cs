using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class SalesInvoiceDetails
    {
        public virtual Int32 ID { get; set; }
        public virtual SalesInvoice salesinvoice { get; set; }
        public virtual Item item { get; set; }
        public virtual Int32 QTY { get; set; }
        public virtual string Discount { get; set; }
        public virtual double Price { get; set; }

        //unmapped properties
        public virtual string itemUnit{ get; set;}
        public virtual string itemdesc{ get; set; }
        public virtual string itemSizeDesc{ get; set; }
        public virtual string discountPrice { get; set; }
        public virtual string unitTotal { get; set;}
        public virtual bool bEdited { get; set; }
        public virtual double _SubTotal { get; set; }

    }
}
