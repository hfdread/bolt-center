using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class User_Privileges
    {
        public virtual Int32 ID { get; set; }
        public virtual Int32 Role { get; set; }
        public virtual bool Inventory_Add { get; set; }
        public virtual bool Inventory_Edit { get; set; }
        public virtual bool Inventory_View { get; set; }
        public virtual bool Invoice_Add { get; set; }
        public virtual bool Invoice_Edit { get; set; }
        public virtual bool Invoice_View { get; set; }
        public virtual bool Receipt_Add { get; set; }
        public virtual bool Receipt_Edit { get; set; }
        public virtual bool Receipt_View { get; set; }
        public virtual bool Contacts { get; set; }
    }
}
