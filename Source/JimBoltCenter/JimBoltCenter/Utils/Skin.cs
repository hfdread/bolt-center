using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraNavBar;
using DevExpress.XtraRichEdit;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace JimBoltCenter.Utils
{
    public static class Skin
    {
        public static int FONTSIZE = 12;
        public static string FONTFAMILY = "Tahoma";

        public static void SetGridFont(GridView view, Font _font)
        {
            foreach (AppearanceObject ap in view.Appearance)
            {
                ap.Font = _font;
            }
        }

        public static void SetGridSelectionColor(Int32 R, Int32 G, Int32 B, GridView view)
        {
            view.Appearance.FocusedRow.BackColor = Color.FromArgb(R, G, B);
            view.Appearance.SelectedRow.BackColor = Color.FromArgb(R, G, B);
        }

        public static void SetGridFont(GridView view)
        {
            foreach (AppearanceObject apobj in view.Appearance)
            {
                apobj.Font = new Font(FONTFAMILY, FONTSIZE);
            }
        }

        public static void SetGridFont(GridView view, string fontFamily, float emSize)
        {
            foreach (AppearanceObject apobj in view.Appearance)
            {
                apobj.Font = new Font(fontFamily, emSize);
            }
        }

        public static void SetTextBoxReadOnly(List<TextEdit> controlList)
        {
            foreach (TextEdit edit in controlList)
            {
                edit.Properties.ReadOnly = true;
                edit.Properties.AppearanceReadOnly.BackColor = Color.White;
            }
        }

        public static void SetTextBoxReadOnly(TextEdit control)
        {
            control.Properties.ReadOnly = true;
            control.Properties.AppearanceReadOnly.BackColor = Color.White;
        }

        public static void SetLabelFont(LabelControl ctl)
        {
            ctl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            ctl.Appearance.Font = new Font(FONTFAMILY, FONTSIZE);
        }

        public static void SetLabelFont(LabelControl ctl, bool Bold)
        {
            ctl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            if(Bold)
                ctl.Appearance.Font = new Font(FONTFAMILY, FONTSIZE, FontStyle.Bold);
            else
                ctl.Appearance.Font = new Font(FONTFAMILY, FONTSIZE);
        }

        public static void SetTextEditFont(TextEdit ctl)
        {
            ctl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            ctl.Font = new Font(FONTFAMILY, FONTSIZE);
        }

        public static void SetButtonFont(SimpleButton button)
        {
            button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            button.Font = new Font(FONTFAMILY, FONTSIZE);
        }

        public static void SetComboBoxEditFont(ComboBoxEdit cbo)
        {
            cbo.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            cbo.Font = new Font(FONTFAMILY, FONTSIZE);
        }

        public static void SetCheckedListBoxFont(CheckedListBoxControl checkedlistbox)
        {
            checkedlistbox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            checkedlistbox.Font = new Font(FONTFAMILY, FONTSIZE);
        }

        public static void SetCheckEditFont(CheckEdit checkedbox)
        {
            checkedbox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            checkedbox.Font = new Font(FONTFAMILY, FONTSIZE);
        }

        public static void SetListBoxControlFont(ListBoxControl lst)
        {
            lst.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            lst.Font = new Font(FONTFAMILY, FONTSIZE);
        }

        public static void SetLookUpEditFont(LookUpEdit control)
        {
            control.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            control.Font = new Font(FONTFAMILY, FONTSIZE);
        }
    }
}
