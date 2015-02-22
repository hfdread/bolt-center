using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DBMapping.BOL;
using DBMapping.DAL;
using JimBoltCenter.Utils;


namespace JimBoltCenter.UI_Controls.Maintenance
{
    public partial class uctlAddSizes : UserControl
    {
        private ItemSizesDao m_sizeDao = null;
        private BindingSource bSrc = null;
        private DataTable tblData = null;
        private DataSet dsData = null;

        
        public uctlAddSizes()
        {
            InitializeComponent();
            m_sizeDao = new ItemSizesDao();
            bSrc = new BindingSource();
            tblData = new DataTable();
            dsData = new DataSet();
        }

        private void uctlAddSizes_Load(object sender, EventArgs e)
        {
            Skin.SetGridFont(grdvSizes);

            DataColumn dataCol = new DataColumn();
            dataCol.ColumnName = "Description";

            dsData.DataSetName = "NewDataSet";
            tblData.TableName = "tblData";

            dsData.Tables.AddRange(new DataTable[] {tblData});

            tblData.Columns.AddRange(new DataColumn[] {
                dataCol
            });

            bSrc.DataMember = "tblData";
            bSrc.DataSource = dsData;

            grdvSizes.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            grdSizes.DataSource = bSrc;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            IList<ItemSizes> sizeExist = new List<ItemSizes>();
            try
            {
                if (tblData.Rows.Count > 0)
                {
                    foreach (DataRow row in tblData.Rows)
                    {
                        ItemSizes size = new ItemSizes();
                        size.Description = row[0].ToString().Trim();

                        m_sizeDao.Save(size);

                        if (size.bSizeExist)
                        {
                            sizeExist.Add(size);
                        }
                    }

                    if (sizeExist.Count == 0)
                        cUtils.ShowMessageInformation("Size(s) Saved Successfully!", "Save Size(s)");
                    else
                    {
                        string sFormat = "";
                        foreach (ItemSizes sz in sizeExist)
                        {
                            sFormat += string.Format(" - {0}\n", sz.Description);
                        }
                        cUtils.ShowMessageInformation(string.Format("Size(s) Saved Successfully!\n But there were size(s) that are duplicates, and were not saved:\n{0}", sFormat),"Save Size(s)");
                    }

                    btnCancel.PerformClick();
                }
                else
                {
                    cUtils.ShowMessageExclamation("You have not added a new size!", "Save Size(s)");
                }
            }
            catch (Exception ex)
            {
                string error = "";
                if (ex.InnerException != null)
                    error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                else
                    error = string.Format("Error:\n{0}", ex.Message);

                cUtils.ShowMessageError(error, "Save Size(s)");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        
    }
}
