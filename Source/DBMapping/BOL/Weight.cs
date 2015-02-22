using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class Weight
    {
        public virtual Int32 ID { get; set; }
        public virtual string Measurement { get; set; }
        public virtual int Converter { get; set; }
        public virtual string Description { get; set; }

        //unmapped properties
        public virtual bool isDirty { get; set; }

        public override string ToString()
        {
            return Measurement;
        }
    }
}
