using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using JimBoltCenter.Utils;

namespace JimBoltCenter.Reports
{
    public partial class rptViewReceipt : DevExpress.XtraReports.UI.XtraReport
    {
        public string sFilters { get; set; }
        public string rptDate { get; set; }

        public rptViewReceipt()
        {
            InitializeComponent();
        }

        private void rptViewReceipt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrlblReceiptNo.DataBindings.Add("Text", DataSource, "ID", "{0:000000}");
            xrlblCustomer.DataBindings.Add("Text", DataSource, "Customer");
            xrlblAgent.DataBindings.Add("Text", DataSource, "Agent");
            xrlblAmount.DataBindings.Add("Text", DataSource, "ReceiptAmount", "{0:#,##0.000}");
            xrlblDate.DataBindings.Add("Text", DataSource, "ReceiptDate", "{0:MMM dd, yyyy}");

            xrlblRptDate.Text = rptDate;
            xrlblFilters.Text = string.Format("Filters: {0}", sFilters);
        }

    }
}
