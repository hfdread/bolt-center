using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class RetailInvoice
    {
        public virtual Int32 ID { get; set; }
        public virtual int or_number { get; set; }
        public virtual DateTime or_date { get; set; }
        public virtual string Customer { get; set; }
        public virtual string TIN { get; set; }
        public virtual string Address { get; set; }
        public virtual string BusinessStyle { get; set; }
        public virtual string Terms { get; set; }
        public virtual string OSCA_PWD_ID { get; set; }
        public virtual double VAT { get; set; }
        public virtual double TOTAL { get; set; }
        public virtual DateTime updated { get; set; }
        public virtual IList<RetailInvoiceDetails> details { get; set; }

        public RetailInvoice()
        {
            details = new List<RetailInvoiceDetails>();
        }
    }
}
