using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Security.AccessControl;

namespace DBMapping
{
    public class clsDbConnect
    {
        private const string REG_PROJNAME = "SOFTWARE\\JimBoltCenter";

        public string DB_SERVER { get; set; }
        public string DB_USER { get; set; }
        public string DB_PWD { get; set; }

        private RegistryKey rk = Registry.LocalMachine.OpenSubKey("SOFTWARE");
        private RegistryKey subRk;

        public clsDbConnect()
        {
            DB_SERVER = "";
            DB_USER = "";
            DB_PWD = "";

            string user = Environment.UserDomainName + "\\" + Environment.UserName;

            RegistrySecurity rs = new RegistrySecurity();
            rs.AddAccessRule(new RegistryAccessRule(user,
                                                    RegistryRights.ReadKey | RegistryRights.Delete,
                                                    InheritanceFlags.None,
                                                    PropagationFlags.None,
                                                    AccessControlType.Allow));

            rs.AddAccessRule(new RegistryAccessRule(user,
                                                 RegistryRights.WriteKey | RegistryRights.ChangePermissions,
                                                 InheritanceFlags.None,
                                                 PropagationFlags.None,
                                                 AccessControlType.Allow));


            subRk = Registry.LocalMachine.OpenSubKey(REG_PROJNAME, true);

            if (subRk != null)
            {
                subRk.SetAccessControl(rs);
                if (subRk.GetValue("DB_Server") != null)
                    DB_SERVER = subRk.GetValue("DB_Server").ToString();
                if (subRk.GetValue("DB_User") != null)
                    DB_USER = subRk.GetValue("DB_User").ToString();
                if (subRk.GetValue("DB_Password") != null)
                    DB_PWD = subRk.GetValue("DB_Password").ToString();
            }
            else
            {
                subRk = Registry.LocalMachine.CreateSubKey(REG_PROJNAME, RegistryKeyPermissionCheck.Default, rs);
            }

        }

        public void setDefaults()
        {
            RegistrySecurity rs = setAccessControl();

            //manual first, create an automation for this
            subRk.SetValue("DB_Server", "localhost",RegistryValueKind.String);
            subRk.SetValue("DB_User", "root", RegistryValueKind.String);
            subRk.SetValue("DB_Password", "adm1n", RegistryValueKind.String);

            DB_SERVER = subRk.GetValue("DB_Server").ToString();
            DB_USER = subRk.GetValue("DB_User").ToString();
            DB_PWD = subRk.GetValue("DB_Password").ToString();

        }

        public void SaveSettings()
        {
            if (subRk != null)
            {
                subRk.SetValue("DB_Server", DB_SERVER);
                subRk.SetValue("DB_User", DB_USER);
                subRk.SetValue("DB_Password", DB_PWD);
            }
        }

        private RegistrySecurity setAccessControl()
        {
            string _user = Environment.UserDomainName + "\\" + Environment.UserName;
            RegistrySecurity _rs = new RegistrySecurity();
            _rs.AddAccessRule(new RegistryAccessRule(_user,
                                                     RegistryRights.FullControl,
                                                     InheritanceFlags.None,
                                                     PropagationFlags.None,
                                                     AccessControlType.Allow));
            return _rs;
        }

        public bool isValid()
        {
            if (DB_SERVER.Trim() == "" || DB_USER.Trim() == "" || DB_PWD.Trim() == "")
                return false;
            else
            {
                return true;
            }
        }

        public bool isNull(string strVal)
        {
            if (strVal == null || strVal.Length == 0)
                return false;
            else
                return true;
        }
    }
}
