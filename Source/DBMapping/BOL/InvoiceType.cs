using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class InvoiceType
    {
        public virtual Int32 ID { get; set; }
        public virtual string Type { get; set; }
        public virtual string Code { get; set; }

        //unmapped
        public virtual bool isDirty { get; set; }

        public override string ToString()
        {
            return Code;
        }
    }
}
