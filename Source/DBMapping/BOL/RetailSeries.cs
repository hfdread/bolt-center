using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class RetailSeries
    {
        public virtual Int32 ID { get; set; }
        public virtual int Start_series { get; set; }
        public virtual int End_series { get; set; }
        public virtual int Current { get; set; }
        public virtual bool Status { get; set; }
        public virtual InvoiceType Type { get; set; }
        public virtual DateTime dte { get; set; }
        public virtual DateTime updated { get; set; }
    }
}
