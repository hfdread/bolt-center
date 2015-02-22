using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class Contacts
    {
        public virtual Int32 ID { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string CompanyAddress { get; set; }
        public virtual string CompanyContact { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string Address { get; set; }
        public virtual string Agent { get; set; }
        public virtual string MobileNo { get; set; }
        public virtual string PhoneNo { get; set; }
        public virtual string FaxNo { get; set; }
        public virtual string TIN { get; set; }
        public virtual string BIR { get; set; }
        public virtual string SEC { get; set; }
        public virtual string NonVat { get; set; }
        public virtual string Vat { get; set; }
        public virtual string Description { get; set; }
        public virtual int Type { get; set; }

        public override string ToString()
        {
            if (CompanyName.Trim().Length > 0)
                return CompanyName;
            else
                return FirstName + " " + MiddleName + " " + LastName;
        }

        public enum cType
        {
            Supplier =1,
            Customer,
            Agent
        }

    }
}
