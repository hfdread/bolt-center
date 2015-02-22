using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JimBoltCenter.UI_Controls.CustomControls
{
    public partial class uctlDateRange : UserControl
    {
        private const int CONST_H = 36;
        private const int CONST_W = 505;

        public enum eDateSelection
        {
            Today = 0,
            Yesterday,
            ThisWeek,
            LastWeek,
            ThisMonth,
            LastMonth,
            ThisYear,
            LastYear,
            Q1,
            Q2,
            Q3,
            Q4,
            Custom
        }

        private eDateSelection m_SelectedDate = eDateSelection.Today;

        public uctlDateRange()
        {
            InitializeComponent();

            cboRange.Items.Add("Today");
            cboRange.Items.Add("Yesterday");
            cboRange.Items.Add("This Week");
            cboRange.Items.Add("Last Week");
            cboRange.Items.Add("This Month");
            cboRange.Items.Add("Last Month");
            cboRange.Items.Add("This Year");
            cboRange.Items.Add("Last Year");
            cboRange.Items.Add("First Quarter");
            cboRange.Items.Add("Second Quarter");
            cboRange.Items.Add("Third Quarter");
            cboRange.Items.Add("Fourth Quarter");
            cboRange.Items.Add("Custom");
        }

        public event EventHandler DateSelectionChanged
        {
            add { cboRange.SelectedIndexChanged += value; }
            remove { cboRange.SelectedIndexChanged -= value; }
        }

        private void SetSelectionDate(eDateSelection sel)
        {
            m_SelectedDate = sel;
            cboRange.SelectedIndex = (int)sel;
        }

        private void uctlDateRange_Load(object sender, EventArgs e)
        {
            SetSelectionDate(eDateSelection.Today);
        }

        public DateTime getDateFrom()
        {
            return Convert.ToDateTime(dtpFrom.Value.ToString("MMM dd, yyyy") + " 12:00 AM");
        }

        public DateTime getDateTo()
        {
            return Convert.ToDateTime(dtpTo.Value.ToString("MMM dd, yyyy") + " 11:59:59 PM");
        }

        private void uctlDateRange_Resize(object sender, EventArgs e)
        {
            this.Size = new Size(CONST_W, CONST_H);
        }

        public void setDateFrom(DateTime from)
        {
            dtpFrom.Value = from;
        }

        public void setDateTo(DateTime to)
        {
            dtpTo.Value = to;
        }

        public string GetDateRange(string sFormat = "")
        {
            if (sFormat == "")
                sFormat = "MMM dd, yyyy";

            switch (m_SelectedDate)
            {
                case eDateSelection.Today:
                case eDateSelection.Yesterday:
                    return dtpFrom.Value.ToString("MMM dd, yyyy");
                case eDateSelection.ThisMonth:
                case eDateSelection.LastMonth:
                    return dtpFrom.Value.ToString("yyyy MMMMM");
                case eDateSelection.ThisYear:
                case eDateSelection.LastYear:
                    return dtpFrom.Value.ToString("yyyy");
                case eDateSelection.Q1:
                case eDateSelection.Q2:
                case eDateSelection.Q3:
                case eDateSelection.Q4:
                    return string.Format("{0} to {1} {2}, {3}", dtpFrom.Value.ToString("MMM 01, yyyy"), dtpTo.Value.ToString("MMM"), DateTime.DaysInMonth(dtpTo.Value.Year, dtpTo.Value.Month), dtpTo.Value.Year);
                default:
                    return dtpFrom.Value.ToString("MMM dd, yyyy");
            }
        }

        private void cboRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bEnable = false;
            DateTime dtNow = DateTime.Now.Date;
            int iDaysOfMonth = 0;

            if (cboRange.SelectedIndex == -1)
                cboRange.SelectedIndex = 0;

            switch (cboRange.Text)
            {
                case "Today":
                    dtpFrom.Value = dtNow;
                    dtpTo.Value = dtNow;
                    break;
                case "Yesterday":
                    dtpFrom.Value = dtNow.AddDays(-1);
                    dtpTo.Value = dtpFrom.Value;
                    break;
                case "This Week":
                    dtpFrom.Value = dtNow.AddDays((int)(dtNow.DayOfWeek - DayOfWeek.Monday) * -1);
                    dtpTo.Value = dtpFrom.Value.AddDays(6);
                    break;
                case "Last Week":
                    dtpFrom.Value = dtNow.AddDays(((int)(dtNow.DayOfWeek - DayOfWeek.Monday) * -1) - 7);
                    dtpTo.Value = dtpFrom.Value.AddDays(6);
                    break;
                case "This Month":
                    iDaysOfMonth = DateTime.DaysInMonth(dtNow.Year, dtNow.Month);
                    dtpFrom.Value =
                       DateTime.Parse(string.Format("{0} 1, {1} 12:00 AM", dtNow.ToString("MMM"), dtNow.Year));
                    dtpTo.Value =
                       DateTime.Parse(string.Format("{0} {1}, {2} 12:00 AM", dtNow.ToString("MMM"), iDaysOfMonth, dtNow.Year));
                    break;
                case "Last Month":
                    dtNow = dtNow.AddMonths(-1);
                    iDaysOfMonth = DateTime.DaysInMonth(dtNow.Year, dtNow.Month);
                    dtpFrom.Value =
                       DateTime.Parse(string.Format("{0} 1, {1} 12:00 AM", dtNow.ToString("MMM"), dtNow.Year));
                    dtpTo.Value =
                       DateTime.Parse(string.Format("{0} {1}, {2} 12:00 AM", dtNow.ToString("MMM"), iDaysOfMonth, dtNow.Year));
                    break;
                case "This Year":
                    dtNow = DateTime.Parse(string.Format("Dec 1, {0} 12:00 AM", (int)DateTime.Now.Year));
                    iDaysOfMonth = DateTime.DaysInMonth(dtNow.Year, dtNow.Month);

                    dtpFrom.Value = DateTime.Parse(string.Format("Jan 1, {0} 12:00 AM", dtNow.Year));
                    dtpTo.Value = DateTime.Parse(string.Format("Dec {0}, {1} 12:00 AM", iDaysOfMonth, dtNow.Year));
                    break;
                case "Last Year":
                    dtNow = DateTime.Parse(string.Format("Dec 1, {0} 12:00 AM", (int)DateTime.Now.AddYears(-1).Year));
                    iDaysOfMonth = DateTime.DaysInMonth(dtNow.Year, dtNow.Month);

                    dtpFrom.Value = DateTime.Parse(string.Format("Jan 1, {0} 12:00 AM", dtNow.Year));
                    dtpTo.Value = DateTime.Parse(string.Format("Dec {0}, {1} 12:00 AM", iDaysOfMonth, dtNow.Year));
                    break;
                case "First Quarter":
                    iDaysOfMonth = DateTime.DaysInMonth(dtNow.Year, 3);
                    dtpFrom.Value = DateTime.Parse(string.Format("Jan 1, {0} 12:00 AM", dtNow.Year));
                    dtpTo.Value = DateTime.Parse(string.Format("Mar {0}, {1} 11:59 PM", iDaysOfMonth, dtNow.Year));
                    break;
                case "Second Quarter":
                    iDaysOfMonth = DateTime.DaysInMonth(dtNow.Year, 6);
                    dtpFrom.Value = DateTime.Parse(string.Format("Apr 1, {0} 12:00 AM", dtNow.Year));
                    dtpTo.Value = DateTime.Parse(string.Format("Jun {0}, {1} 11:59 PM", iDaysOfMonth, dtNow.Year));
                    break;
                case "Third Quarter":
                    iDaysOfMonth = DateTime.DaysInMonth(dtNow.Year, 9);
                    dtpFrom.Value = DateTime.Parse(string.Format("Jul 1, {0} 12:00 AM", dtNow.Year));
                    dtpTo.Value = DateTime.Parse(string.Format("Sep {0}, {1} 11:59 PM", iDaysOfMonth, dtNow.Year));
                    break;
                case "Fourth Quarter":
                    iDaysOfMonth = DateTime.DaysInMonth(dtNow.Year, 12);
                    dtpFrom.Value = DateTime.Parse(string.Format("Oct 1, {0} 12:00 AM", dtNow.Year));
                    dtpTo.Value = DateTime.Parse(string.Format("Dec {0}, {1} 11:59 PM", iDaysOfMonth, dtNow.Year));
                    break;
                case "Custom":
                    dtpFrom.Value = DateTime.Now.Date;
                    dtpTo.Value = DateTime.Now.Date;
                    bEnable = true;
                    break;
            }

            m_SelectedDate = (eDateSelection)cboRange.SelectedIndex;

            dtpTo.Enabled = bEnable;
            dtpFrom.Enabled = bEnable;
        }

    }
}
