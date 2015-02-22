namespace JimBoltCenter.Forms
{
    partial class MyMessageBox
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBUTTON1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnBUTTON2 = new DevExpress.XtraEditors.SimpleButton();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBUTTON1
            // 
            this.btnBUTTON1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBUTTON1.Appearance.Options.UseFont = true;
            this.btnBUTTON1.Location = new System.Drawing.Point(134, 139);
            this.btnBUTTON1.Name = "btnBUTTON1";
            this.btnBUTTON1.Size = new System.Drawing.Size(75, 35);
            this.btnBUTTON1.TabIndex = 0;
            this.btnBUTTON1.Text = "OK";
            // 
            // btnBUTTON2
            // 
            this.btnBUTTON2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBUTTON2.Appearance.Options.UseFont = true;
            this.btnBUTTON2.Location = new System.Drawing.Point(227, 139);
            this.btnBUTTON2.Name = "btnBUTTON2";
            this.btnBUTTON2.Size = new System.Drawing.Size(75, 35);
            this.btnBUTTON2.TabIndex = 1;
            this.btnBUTTON2.Text = "OK";
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(12, 20);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memoEdit1.Properties.Appearance.Options.UseFont = true;
            this.memoEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.memoEdit1.Size = new System.Drawing.Size(420, 103);
            this.memoEdit1.TabIndex = 2;
            // 
            // MyMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 186);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.btnBUTTON2);
            this.Controls.Add(this.btnBUTTON1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyMessageBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageBox Title";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnBUTTON1;
        private DevExpress.XtraEditors.SimpleButton btnBUTTON2;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
    }
}