using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JimBoltCenter.Forms;
using NHibernate;
using DBMapping;
using DBMapping.DAL;

namespace JimBoltCenter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            clsDbConnect db = new clsDbConnect();

            if (db.isValid())
            {
                Application.Run(new MainForm());
                
            }
            else
            {
                //db.setDefaults();
                if (db.isValid())
                {                   
                    UserDao dao = new UserDao();
                    if (dao.IsInitialized())
                        Application.Run(new MainForm());
                    else
                    {
                        MessageBox.Show("NHibernate Error: " + dao.ErrorMessage);
                        Application.Run(new frmDBConnect());
                    }

                }
                else
                    Application.Run(new frmDBConnect());
            }
        }
    }
}
