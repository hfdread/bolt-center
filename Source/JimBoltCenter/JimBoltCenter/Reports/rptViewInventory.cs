using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using JimBoltCenter.Utils;

namespace JimBoltCenter.Reports
{
    public partial class rptViewInventory : DevExpress.XtraReports.UI.XtraReport
    {

        public rptViewInventory()
        {
            InitializeComponent();
        }

        private void rptViewInventory_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblName.DataBindings.Add("Text", DataSource, "Name");
            lblCode.DataBindings.Add("Text", DataSource, "Code");
            lblSize.DataBindings.Add("Text", DataSource, "size.Description");
            lblUnit.DataBindings.Add("Text", DataSource, "Unit");
            lblWTUnit.DataBindings.Add("Text", DataSource, "Unit2");
            lblPrice.DataBindings.Add("Text", DataSource, "UnitPrice", "{0:#,##0.000}");
            lblWTPrice.DataBindings.Add("Text", DataSource, "UnitPrice2", "{0:#,##0.000}");
            lblOnHand.DataBindings.Add("Text", DataSource, "OnHand", "{0:#,##0}");
            lblWTOnHand.DataBindings.Add("Text", DataSource, "OnHandWeight", "{0:#,##0}");
            lblLastPrice.DataBindings.Add("Text", DataSource, "LastPrice", "{0:#,##0.000}");
            lblThreshold.DataBindings.Add("Text", DataSource, "LowThreshold", "{0:#,##0}");

            xrlblDate1.Text = DateTime.Now.ToShortDateString();
        }
    }
}
