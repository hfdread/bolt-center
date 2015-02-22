using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JimBoltCenter.Utils;
using JimBoltCenter.Reports;
using DBMapping.BOL;
using DBMapping.DAL;

namespace JimBoltCenter.UI_Controls.Viewers
{
    public partial class uctlViewRetailSeries : UserControl
    {
        private RetailSeriesDao m_seriesDao;

        public uctlViewRetailSeries()
        {
            InitializeComponent();
            m_seriesDao = new RetailSeriesDao();
        }

        private void uctlViewRetailSeries_Load(object sender, EventArgs e)
        {
            //grdSeries.DataSource = m_seriesDao.getAllRecords();
            Skin.SetGridFont(grdvSeries);
            Skin.SetButtonFont(btnSearch);
            Skin.SetButtonFont(btnPrint);
            btnSearch.PerformClick();
        }

        private void uctlDateRange1_DateSelectionChanged(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            grdSeries.DataSource = m_seriesDao.Search(uctlDateRange1.getDateFrom(), uctlDateRange1.getDateTo());
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            rptViewRetailSeries rpt = new rptViewRetailSeries();

            if (uctlDateRange1.getDateFrom().ToShortDateString() == uctlDateRange1.getDateTo().ToShortDateString())
                rpt.sDate = string.Format("as of {0}", uctlDateRange1.getDateFrom().ToShortDateString());
            else
                rpt.sDate = string.Format("from {0} to {1}", uctlDateRange1.getDateFrom().ToShortDateString(), uctlDateRange1.getDateTo().ToShortDateString());

            rpt.DataSource = grdvSeries.DataSource;
            rpt.ShowPreviewDialog();
        }
    }
}
