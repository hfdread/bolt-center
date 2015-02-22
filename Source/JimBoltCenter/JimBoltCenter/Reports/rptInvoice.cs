using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using JimBoltCenter.Utils;
using DBMapping.BOL;
using DBMapping.DAL;


namespace JimBoltCenter.Reports
{
    public partial class rptInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public SalesInvoice m_invoice { get; set; }

        public rptInvoice()
        {
            InitializeComponent();
        }

        private void rptInvoice_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ForwardersDao fwDao = new ForwardersDao();
            xrlblInvoiceNo.Text = m_invoice.InvoiceID.ToString();
            xrlblDate.Text = m_invoice.InvoiceDate.ToString("MMM dd, yyy");
            xrlblArrival.Text = m_invoice.ArrivalDate.ToString("MMM dd, yyy");
            xrlblSupplier.Text = m_invoice.Supplier.ToString();
            xrlblSupplierAddress.Text = m_invoice.Supplier.CompanyAddress;
            xrlblFOR.Text = m_invoice.STORE;
            xrlblTIN.Text = m_invoice.TIN;
            xrlblCartons.Text = m_invoice.QTY_Cart.ToString(cUtils.FMT_NUMBER);
            xrlblARNo.Text = m_invoice.AR_Number.ToString();
            xrlblForwarder.Text = fwDao.GetById(m_invoice.ForwarderID).ToString();

            //details
            xrlblQTY.DataBindings.Add("Text", DataSource, "QTY", "{0:#,##0}");
            xrlblItemName.DataBindings.Add("Text", DataSource, "item.Name");
            xrlblSize.DataBindings.Add("Text", DataSource, "item.Size.Description");
            xrlblUnit.DataBindings.Add("Text", DataSource, "item.Unit");
            xrlblDiscount.DataBindings.Add("Text", DataSource, "Discount");
            xrlblPrice.DataBindings.Add("Text", DataSource, "Price", "{0:#,##0.000}");
            xrlblSubTotal.DataBindings.Add("Text", DataSource, "_SubTotal", "{0:#,##0.000}");

            xrlblTotal.Text = m_invoice.Invoice_Amount.ToString(cUtils.FMT_CURRENCY_AMT);
        }

    }
}
