using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapping.BOL
{
    public partial class User
    {
        public virtual Int32 ID { get; set; }
        public virtual string username { get; set; }
        public virtual string password { get; set; }
        public virtual Int32 authentication { get; set; }
        public virtual string fname { get; set; }
        public virtual string lname { get; set; }

        public override string ToString()
        {
            return username;
            //return base.ToString();
        }

    }
}
