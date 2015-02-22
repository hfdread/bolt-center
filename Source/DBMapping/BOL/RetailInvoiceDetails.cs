using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class RetailInvoiceDetails
    {
        public virtual Int32 ID { get; set; }
        public virtual RetailInvoice retailinvoice { get; set; }
        public virtual Item item { get; set; }
        public virtual int QTY { get; set; }
        public virtual double UnitPrice { get; set; }
        public virtual double Amount { get; set; }
    }
}
