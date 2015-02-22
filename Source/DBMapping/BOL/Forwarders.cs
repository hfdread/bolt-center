using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class Forwarders
    {
        public virtual Int32 ID { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Details { get; set; }

        public virtual bool isDirty { get; set; }

        public override string ToString()
        {
            return CompanyName;
        }
    }
}
