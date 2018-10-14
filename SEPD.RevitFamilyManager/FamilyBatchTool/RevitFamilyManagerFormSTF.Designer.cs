namespace SEPD.RevitFamilyManager
{
    partial class RevitFamilyManagerFormSTF
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
            this.btn_selectRfa = new System.Windows.Forms.Button();
            this.lvw_FamilyAndXls = new System.Windows.Forms.ListView();
            this.col_fName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_fPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_xName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_xPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_changeCat = new System.Windows.Forms.Button();
            this.dgv_xPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_xName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_fPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_fName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_FamilyAndXls = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FamilyAndXls)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_selectRfa
            // 
            this.btn_selectRfa.Location = new System.Drawing.Point(40, 23);
            this.btn_selectRfa.Name = "btn_selectRfa";
            this.btn_selectRfa.Size = new System.Drawing.Size(117, 41);
            this.btn_selectRfa.TabIndex = 0;
            this.btn_selectRfa.Text = "...";
            this.btn_selectRfa.UseVisualStyleBackColor = true;
            this.btn_selectRfa.Click += new System.EventHandler(this.btn_selectRfa_Click);
            // 
            // lvw_FamilyAndXls
            // 
            this.lvw_FamilyAndXls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_fName,
            this.col_fPath,
            this.col_xName,
            this.col_xPath});
            this.lvw_FamilyAndXls.Location = new System.Drawing.Point(40, 98);
            this.lvw_FamilyAndXls.Name = "lvw_FamilyAndXls";
            this.lvw_FamilyAndXls.Size = new System.Drawing.Size(451, 203);
            this.lvw_FamilyAndXls.TabIndex = 3;
            this.lvw_FamilyAndXls.UseCompatibleStateImageBehavior = false;
            this.lvw_FamilyAndXls.View = System.Windows.Forms.View.Details;
            // 
            // col_fName
            // 
            this.col_fName.Text = "族名";
            this.col_fName.Width = 100;
            // 
            // col_fPath
            // 
            this.col_fPath.Text = "族路径";
            this.col_fPath.Width = 100;
            // 
            // col_xName
            // 
            this.col_xName.Text = "表名";
            this.col_xName.Width = 100;
            // 
            // col_xPath
            // 
            this.col_xPath.Text = "表路径";
            this.col_xPath.Width = 100;
            // 
            // btn_changeCat
            // 
            this.btn_changeCat.Location = new System.Drawing.Point(329, 23);
            this.btn_changeCat.Name = "btn_changeCat";
            this.btn_changeCat.Size = new System.Drawing.Size(162, 41);
            this.btn_changeCat.TabIndex = 5;
            this.btn_changeCat.Text = "修改族类别";
            this.btn_changeCat.UseVisualStyleBackColor = true;
            this.btn_changeCat.Click += new System.EventHandler(this.btn_changeCat_Click);
            // 
            // dgv_xPath
            // 
            this.dgv_xPath.HeaderText = "表路径";
            this.dgv_xPath.Name = "dgv_xPath";
            // 
            // dgv_xName
            // 
            this.dgv_xName.HeaderText = "表名";
            this.dgv_xName.Name = "dgv_xName";
            // 
            // dgv_fPath
            // 
            this.dgv_fPath.HeaderText = "族路径";
            this.dgv_fPath.Name = "dgv_fPath";
            // 
            // dgv_fName
            // 
            this.dgv_fName.HeaderText = "族名";
            this.dgv_fName.Name = "dgv_fName";
            // 
            // dgv_FamilyAndXls
            // 
            this.dgv_FamilyAndXls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FamilyAndXls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_fName,
            this.dgv_fPath,
            this.dgv_xName,
            this.dgv_xPath});
            this.dgv_FamilyAndXls.Location = new System.Drawing.Point(40, 351);
            this.dgv_FamilyAndXls.Name = "dgv_FamilyAndXls";
            this.dgv_FamilyAndXls.RowTemplate.Height = 23;
            this.dgv_FamilyAndXls.Size = new System.Drawing.Size(451, 208);
            this.dgv_FamilyAndXls.TabIndex = 4;
            // 
            // RevitFamilyManagerFormSTF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 601);
            this.Controls.Add(this.btn_changeCat);
            this.Controls.Add(this.dgv_FamilyAndXls);
            this.Controls.Add(this.lvw_FamilyAndXls);
            this.Controls.Add(this.btn_selectRfa);
            this.Name = "RevitFamilyManagerFormSTF";
            this.Text = "RevitFamilyManagerFormSTF";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FamilyAndXls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_selectRfa;
        private System.Windows.Forms.ListView lvw_FamilyAndXls;
        private System.Windows.Forms.ColumnHeader col_fName;
        private System.Windows.Forms.ColumnHeader col_fPath;
        private System.Windows.Forms.ColumnHeader col_xName;
        private System.Windows.Forms.ColumnHeader col_xPath;
        private System.Windows.Forms.Button btn_changeCat;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_xPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_xName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_fPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_fName;
        private System.Windows.Forms.DataGridView dgv_FamilyAndXls;
    }
}