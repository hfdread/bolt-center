using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using JimBoltCenter.Utils;

namespace JimBoltCenter.Reports
{
    public partial class rptViewRetailSeries : DevExpress.XtraReports.UI.XtraReport
    {
        public string sDate { get; set; }

        public rptViewRetailSeries()
        {
            InitializeComponent();
        }

        private void rptViewRetailSeries_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrlblStart.DataBindings.Add("Text", DataSource, "Start_series", "{0:000000}");
            xrlblEnd.DataBindings.Add("Text", DataSource, "End_series", "{0:000000}");
            xrlblLatest.DataBindings.Add("Text", DataSource, "Current", "{0:000000}");
            xrlblInvoice.DataBindings.Add("Text", DataSource, "Type");
            xrlblCreateDate.DataBindings.Add("Text", DataSource, "dte", "{0:MMM dd, yyyy}");
            xrlblUpdateDate.DataBindings.Add("Text", DataSource, "updated", "{0:MMM dd, yyyy}");

            xrlblDte.Text = sDate;
        }

    }
}
