namespace JimBoltCenter.Reports
{
    partial class rptPackingList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblPrice = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblSize = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblUnit = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblQty = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblAgent = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblPO = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblCustomer = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblPackListNo = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblTotalAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrlblBorder = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrlblAmount,
            this.xrlblPrice,
            this.xrlblSize,
            this.xrlblName,
            this.xrlblUnit,
            this.xrlblQty});
            this.Detail.HeightF = 19.08334F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseBorders = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrLabel4
            // 
            this.xrLabel4.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dash;
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 17.00001F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(522.4318F, 2.083336F);
            this.xrLabel4.StylePriority.UseBorderDashStyle = false;
            this.xrLabel4.StylePriority.UseBorders = false;
            // 
            // xrlblAmount
            // 
            this.xrlblAmount.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrlblAmount.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrlblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.xrlblAmount.LocationFloat = new DevExpress.Utils.PointFloat(421.4318F, 0F);
            this.xrlblAmount.Name = "xrlblAmount";
            this.xrlblAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblAmount.SizeF = new System.Drawing.SizeF(101F, 17F);
            this.xrlblAmount.StylePriority.UseBorderDashStyle = false;
            this.xrlblAmount.StylePriority.UseBorders = false;
            this.xrlblAmount.StylePriority.UseFont = false;
            this.xrlblAmount.StylePriority.UseTextAlignment = false;
            this.xrlblAmount.Text = "1,000,000.000";
            this.xrlblAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrlblPrice
            // 
            this.xrlblPrice.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrlblPrice.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrlblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.xrlblPrice.LocationFloat = new DevExpress.Utils.PointFloat(326.9435F, 0F);
            this.xrlblPrice.Multiline = true;
            this.xrlblPrice.Name = "xrlblPrice";
            this.xrlblPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblPrice.SizeF = new System.Drawing.SizeF(94.48831F, 17F);
            this.xrlblPrice.StylePriority.UseBorderDashStyle = false;
            this.xrlblPrice.StylePriority.UseBorders = false;
            this.xrlblPrice.StylePriority.UseFont = false;
            this.xrlblPrice.StylePriority.UseTextAlignment = false;
            this.xrlblPrice.Text = "102,000.500";
            this.xrlblPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrlblSize
            // 
            this.xrlblSize.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrlblSize.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrlblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.xrlblSize.LocationFloat = new DevExpress.Utils.PointFloat(242.2379F, 0F);
            this.xrlblSize.Name = "xrlblSize";
            this.xrlblSize.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblSize.SizeF = new System.Drawing.SizeF(84.7056F, 17F);
            this.xrlblSize.StylePriority.UseBorderDashStyle = false;
            this.xrlblSize.StylePriority.UseBorders = false;
            this.xrlblSize.StylePriority.UseFont = false;
            this.xrlblSize.Text = "size";
            // 
            // xrlblName
            // 
            this.xrlblName.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrlblName.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrlblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.xrlblName.LocationFloat = new DevExpress.Utils.PointFloat(69.75806F, 0F);
            this.xrlblName.Name = "xrlblName";
            this.xrlblName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblName.SizeF = new System.Drawing.SizeF(172.38F, 17F);
            this.xrlblName.StylePriority.UseBorderDashStyle = false;
            this.xrlblName.StylePriority.UseBorders = false;
            this.xrlblName.StylePriority.UseFont = false;
            this.xrlblName.Text = "name";
            // 
            // xrlblUnit
            // 
            this.xrlblUnit.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrlblUnit.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrlblUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.xrlblUnit.LocationFloat = new DevExpress.Utils.PointFloat(37.195F, 0F);
            this.xrlblUnit.Name = "xrlblUnit";
            this.xrlblUnit.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblUnit.SizeF = new System.Drawing.SizeF(32.56306F, 17F);
            this.xrlblUnit.StylePriority.UseBorderDashStyle = false;
            this.xrlblUnit.StylePriority.UseBorders = false;
            this.xrlblUnit.StylePriority.UseFont = false;
            this.xrlblUnit.Text = "pc";
            // 
            // xrlblQty
            // 
            this.xrlblQty.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrlblQty.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrlblQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.xrlblQty.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrlblQty.Name = "xrlblQty";
            this.xrlblQty.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblQty.SizeF = new System.Drawing.SizeF(37.195F, 17F);
            this.xrlblQty.StylePriority.UseBorderDashStyle = false;
            this.xrlblQty.StylePriority.UseBorders = false;
            this.xrlblQty.StylePriority.UseFont = false;
            this.xrlblQty.Text = "QTY";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 19F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 8F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7,
            this.xrlblAgent,
            this.xrLabel6,
            this.xrlblAddress,
            this.xrlblPO,
            this.xrlblDate,
            this.xrLabel8,
            this.xrLabel2,
            this.xrLabel1,
            this.xrLabel3,
            this.xrlblCustomer,
            this.xrlblPackListNo});
            this.ReportHeader.HeightF = 72.50002F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.StylePriority.UseBorders = false;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(331.6627F, 46F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(42.40591F, 23F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.Text = "Agent";
            // 
            // xrlblAgent
            // 
            this.xrlblAgent.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlblAgent.LocationFloat = new DevExpress.Utils.PointFloat(376.0686F, 46F);
            this.xrlblAgent.Name = "xrlblAgent";
            this.xrlblAgent.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblAgent.SizeF = new System.Drawing.SizeF(140F, 23F);
            this.xrlblAgent.StylePriority.UseFont = false;
            this.xrlblAgent.Text = "907702-02";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(331.6627F, 23F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(42.40591F, 23F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "PO No.";
            // 
            // xrlblAddress
            // 
            this.xrlblAddress.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlblAddress.LocationFloat = new DevExpress.Utils.PointFloat(52.82257F, 49.50002F);
            this.xrlblAddress.Name = "xrlblAddress";
            this.xrlblAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblAddress.SizeF = new System.Drawing.SizeF(265.121F, 23F);
            this.xrlblAddress.StylePriority.UseFont = false;
            this.xrlblAddress.Text = "Blah blah blah cor. road 44, block 20, Cebu City";
            // 
            // xrlblPO
            // 
            this.xrlblPO.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlblPO.LocationFloat = new DevExpress.Utils.PointFloat(376.0686F, 23F);
            this.xrlblPO.Name = "xrlblPO";
            this.xrlblPO.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblPO.SizeF = new System.Drawing.SizeF(110F, 23F);
            this.xrlblPO.StylePriority.UseFont = false;
            this.xrlblPO.Text = "907702-02";
            // 
            // xrlblDate
            // 
            this.xrlblDate.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlblDate.LocationFloat = new DevExpress.Utils.PointFloat(374.5F, 0F);
            this.xrlblDate.Name = "xrlblDate";
            this.xrlblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblDate.SizeF = new System.Drawing.SizeF(120.8333F, 23F);
            this.xrlblDate.StylePriority.UseFont = false;
            this.xrlblDate.Text = "January 31, 2014";
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(332.0941F, 0F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(42.40591F, 23F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.Text = "Date";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 23F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(69.75806F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "Ordered By";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(89.1129F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "Packing List No.";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.5F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(52.82258F, 23F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "Address";
            // 
            // xrlblCustomer
            // 
            this.xrlblCustomer.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlblCustomer.LocationFloat = new DevExpress.Utils.PointFloat(69.75806F, 23F);
            this.xrlblCustomer.Name = "xrlblCustomer";
            this.xrlblCustomer.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblCustomer.SizeF = new System.Drawing.SizeF(248.1855F, 23F);
            this.xrlblCustomer.StylePriority.UseFont = false;
            this.xrlblCustomer.Text = "Talamban Lezzel Housing Supply";
            // 
            // xrlblPackListNo
            // 
            this.xrlblPackListNo.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlblPackListNo.LocationFloat = new DevExpress.Utils.PointFloat(89.1129F, 0F);
            this.xrlblPackListNo.Name = "xrlblPackListNo";
            this.xrlblPackListNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblPackListNo.SizeF = new System.Drawing.SizeF(89.1129F, 23F);
            this.xrlblPackListNo.StylePriority.UseFont = false;
            this.xrlblPackListNo.Text = "000096";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11,
            this.xrlblTotalAmount});
            this.ReportFooter.HeightF = 94.12505F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.PrintAtBottom = true;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel14.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(128.4274F, 36.79171F);
            this.xrLabel14.Multiline = true;
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(156.2164F, 36.33335F);
            this.xrLabel14.StylePriority.UseBorders = false;
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "Received the above goods in good order and condition.";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel13.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(355.5F, 53.79172F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(129.9059F, 23F);
            this.xrLabel13.StylePriority.UseBorders = false;
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Customer\'s Signature";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel12.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(325.9543F, 30.7917F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(187.0457F, 23F);
            this.xrLabel12.StylePriority.UseBorders = false;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "By:";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel11.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(257.2379F, 4.000031F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(42.40591F, 23F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.Text = "TOTAL";
            // 
            // xrlblTotalAmount
            // 
            this.xrlblTotalAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlblTotalAmount.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlblTotalAmount.LocationFloat = new DevExpress.Utils.PointFloat(325.9543F, 0F);
            this.xrlblTotalAmount.Name = "xrlblTotalAmount";
            this.xrlblTotalAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblTotalAmount.SizeF = new System.Drawing.SizeF(187.0457F, 23F);
            this.xrlblTotalAmount.StylePriority.UseBorders = false;
            this.xrlblTotalAmount.StylePriority.UseFont = false;
            this.xrlblTotalAmount.StylePriority.UseTextAlignment = false;
            this.xrlblTotalAmount.Text = "30 Days";
            this.xrlblTotalAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // PageHeader
            // 
            this.PageHeader.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.PageHeader.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlblBorder,
            this.xrLabel19,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15});
            this.PageHeader.HeightF = 22.12499F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.StylePriority.UseBorderDashStyle = false;
            this.PageHeader.StylePriority.UseBorders = false;
            // 
            // xrlblBorder
            // 
            this.xrlblBorder.LocationFloat = new DevExpress.Utils.PointFloat(0F, 17.00001F);
            this.xrlblBorder.Name = "xrlblBorder";
            this.xrlblBorder.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblBorder.SizeF = new System.Drawing.SizeF(522.4318F, 5.124985F);
            // 
            // xrLabel19
            // 
            this.xrLabel19.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel19.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(423.8471F, 0F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(98.58475F, 17F);
            this.xrLabel19.StylePriority.UseBorders = false;
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.Text = "AMOUNT";
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel18.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(326.9435F, 0F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(94.48834F, 17F);
            this.xrLabel18.StylePriority.UseBorders = false;
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.Text = "UNIT PRICE";
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel17.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(69.75806F, 0F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(217.2379F, 17F);
            this.xrLabel17.StylePriority.UseBorders = false;
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.Text = "ARTICLES";
            // 
            // xrLabel16
            // 
            this.xrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel16.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(37.195F, 0F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(32.56306F, 17F);
            this.xrLabel16.StylePriority.UseBorders = false;
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.Text = "UNIT";
            // 
            // xrLabel15
            // 
            this.xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel15.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(37.195F, 17F);
            this.xrLabel15.StylePriority.UseBorders = false;
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.Text = "QTY";
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // rptPackingList
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter,
            this.PageHeader});
            this.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Margins = new System.Drawing.Printing.Margins(11, 3, 19, 8);
            this.PageHeight = 840;
            this.PageWidth = 560;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Version = "11.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rptPackingList_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLabel xrLabel14;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel19;
        private DevExpress.XtraReports.UI.XRLabel xrLabel18;
        private DevExpress.XtraReports.UI.XRLabel xrLabel17;
        private DevExpress.XtraReports.UI.XRLabel xrLabel16;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private DevExpress.XtraReports.UI.XRLabel xrlblBorder;
        private DevExpress.XtraReports.UI.XRLabel xrlblName;
        private DevExpress.XtraReports.UI.XRLabel xrlblUnit;
        private DevExpress.XtraReports.UI.XRLabel xrlblQty;
        private DevExpress.XtraReports.UI.XRLabel xrlblSize;
        private DevExpress.XtraReports.UI.XRLabel xrlblAmount;
        private DevExpress.XtraReports.UI.XRLabel xrlblPrice;
        public DevExpress.XtraReports.UI.XRLabel xrlblAddress;
        public DevExpress.XtraReports.UI.XRLabel xrlblPO;
        public DevExpress.XtraReports.UI.XRLabel xrlblDate;
        public DevExpress.XtraReports.UI.XRLabel xrlblCustomer;
        public DevExpress.XtraReports.UI.XRLabel xrlblPackListNo;
        public DevExpress.XtraReports.UI.XRLabel xrlblTotalAmount;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        public DevExpress.XtraReports.UI.XRLabel xrlblAgent;
    }
}
