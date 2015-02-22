using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBMapping.BOL;
using DBMapping.DAL;
using JimBoltCenter.Utils;

namespace JimBoltCenter.UI_Controls.Viewers
{
    public partial class uctlEnterConverted : UserControl
    {
        private ItemDao _itemDao = null;
        private WeightDao _weightDao = null;

        public Item W_item { get; set; }//weight item
        public Item S_item { get; set; }//selected destination item

        public uctlEnterConverted()
        {
            InitializeComponent();
            _itemDao = new ItemDao();
            _weightDao = new WeightDao();
        }

        private void uctlEnterConverted_Load(object sender, EventArgs e)
        {
            Skin.SetButtonFont(btnOk);
            Skin.SetButtonFont(btnCancel);
            Skin.SetLabelFont(labelControl1);
            Skin.SetLabelFont(labelControl2);
            Skin.SetLabelFont(labelControl3,true);
            Skin.SetLabelFont(labelControl4);
            Skin.SetLabelFont(lblItem,true);
            Skin.SetTextEditFont(txtConvert);

            labelControl3.Text = S_item.Name + ", " + S_item.Code;
            lblItem.Text = W_item.Name + ", " + W_item.Code;

            int TotalConvert = _weightDao.GetConverter(W_item.Unit2) * W_item.OnHandWeight;
            txtConvert.Text = TotalConvert.ToString(cUtils.FMT_NUMBER);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int TotalConvert = 0, inputConvert=0;
            TotalConvert = _weightDao.GetConverter(W_item.Unit2) * W_item.OnHandWeight;
            inputConvert = cUtils.ConvertToInteger(txtConvert.Text);
            if(inputConvert > TotalConvert)
            {
                cUtils.ShowMessageError("Cannot convert more than allowable","Cannot Convert");
                txtConvert.Text = TotalConvert.ToString(cUtils.FMT_NUMBER);
                txtConvert.Focus();
            }
            else
            {
                
                int weightOnhand = 0, chkConverted=0;
                weightOnhand = inputConvert / _weightDao.GetConverter(W_item.Unit2);
                chkConverted = inputConvert % _weightDao.GetConverter(W_item.Unit2);
                if (chkConverted == 0)
                {
                    W_item.OnHandWeight -= weightOnhand;

                    try
                    {
                        //save details
                        S_item.OnHand += inputConvert;
                        _itemDao.Save(S_item);
                        _itemDao.Save(W_item);

                        cUtils.ShowMessageInformation("Successfully Converted Onhand Data", "Merge Successfull");

                        btnCancel.PerformClick();
                    }
                    catch (Exception ex)
                    {
                        cUtils.ShowMessageError(ex.Message, "Convert Error");
                    }
                }
                else
                {
                    int converter = 0;
                    converter = _weightDao.GetConverter(W_item.Unit2);
                    cUtils.ShowMessageError(string.Format("Please convert by {0}s", converter.ToString(cUtils.FMT_NUMBER)),"Convert Error");
                    txtConvert.Focus();
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void txtConvert_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk.PerformClick();
        }

        private void txtConvert_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }


    }
}
