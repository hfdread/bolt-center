using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace JimBoltCenter.Reports
{
    public partial class rptViewInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public string rptDate { get; set; }
        public string filters { get; set; }

        public rptViewInvoice()
        {
            InitializeComponent();
        }

        private void rptViewInvoice_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrlblRptDate.Text = rptDate;
            xrlblFilters.Text = string.Format("Filters: {0}", filters);

            xrlblInvoiceNo.DataBindings.Add("Text", DataSource, "InvoiceID");
            xrlblSupplier.DataBindings.Add("Text", DataSource, "Supplier");
            xrlblTotalAmount.DataBindings.Add("Text", DataSource, "Invoice_Amount", "{0:#,##0.000}");
            xrlblFreightAmount.DataBindings.Add("Text", DataSource, "FreightAmount", "{0:#,##0.000}");
            xrlblDateCreated.DataBindings.Add("Text", DataSource, "CreateDate", "{0:MM/dd/yyyy}");
            xrlblEditedBy.DataBindings.Add("Text", DataSource, "_userEditedBy");
        }

    }
}
