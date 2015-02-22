using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class SalesInvoice
    {
        public virtual Int32 ID { get; set; }
        public virtual Int32 InvoiceType { get; set; }
        public virtual Int32 InvoiceID { get; set; }
        public virtual Contacts Supplier { get; set; }
        public virtual Int32 ForwarderID { get; set; }
        public virtual DateTime InvoiceDate { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual DateTime ArrivalDate { get; set; }
        public virtual double FreightAmount { get; set; }
        public virtual Int32 AR_Number { get; set; }
        public virtual Int32 QTY_Cart { get; set; }
        public virtual double Invoice_Amount { get; set; }
        public virtual IList<SalesInvoiceDetails> details { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual Int32 User { get; set; }
        public virtual DateTime EditedDate { get; set; }
        public virtual string TIN { get; set; }
        public virtual string STORE { get; set; }

        //unmapped properties
        public virtual Int32 nEdit_DetailsID { get; set; }
        public virtual string _userEditedBy { get; set; }

        public SalesInvoice()
        {
            details = new List<SalesInvoiceDetails>();
        }
    }
}
