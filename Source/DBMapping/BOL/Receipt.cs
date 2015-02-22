using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class Receipt
    {
        public virtual Int32 ID { get; set; }
        public virtual Contacts Customer { get; set; }
        public virtual Contacts Agent { get; set; }
        public virtual DateTime ReceiptDate { get; set; }
        public virtual bool isDeleted { get; set; }
        public virtual double ReceiptAmount { get; set; }
        public virtual IList<ReceiptDetails> details { get; set; }
        public virtual double PaidAmount { get; set; }
        public virtual string PO { get; set; }
        public virtual DateTime UpdateDate { get; set; }

        public Receipt()
        {
            details = new List<ReceiptDetails>();
        }

        public int ItemCount()
        {
            return details.Count;
        }
    }
}
