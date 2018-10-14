namespace SEPD.RevitFamilyManager
{
    partial class RevitFamilyManagerFormEP
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
            this.lvw_eleSelXls = new System.Windows.Forms.ListView();
            this.col_fName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_xPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_xName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_selectLocalXls = new System.Windows.Forms.Button();
            this.dgv_parameter = new System.Windows.Forms.DataGridView();
            this.dgv_pName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_pValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_pIns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_pGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_pType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_delectSelected = new System.Windows.Forms.Button();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.ckb_clearsymbol = new System.Windows.Forms.CheckBox();
            this.ckb_clearpara = new System.Windows.Forms.CheckBox();
            this.col_ffName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.dgv_parameter)).BeginInit();
            this.SuspendLayout();
            // 
            // lvw_eleSelXls
            // 
            this.lvw_eleSelXls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_fName,
            this.col_xPath,
            this.col_xName,
            this.col_ffName});
            this.lvw_eleSelXls.Location = new System.Drawing.Point(36, 75);
            this.lvw_eleSelXls.Name = "lvw_eleSelXls";
            this.lvw_eleSelXls.Size = new System.Drawing.Size(509, 272);
            this.lvw_eleSelXls.TabIndex = 0;
            this.lvw_eleSelXls.UseCompatibleStateImageBehavior = false;
            this.lvw_eleSelXls.View = System.Windows.Forms.View.Details;
            this.lvw_eleSelXls.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvw_eleSelXls_ItemSelectionChanged);
            // 
            // col_fName
            // 
            this.col_fName.Text = "实例名称";
            this.col_fName.Width = 200;
            // 
            // col_xPath
            // 
            this.col_xPath.Text = "自定义表路径";
            this.col_xPath.Width = 200;
            // 
            // col_xName
            // 
            this.col_xName.Text = "自定义表名";
            this.col_xName.Width = 100;
            // 
            // btn_selectLocalXls
            // 
            this.btn_selectLocalXls.Location = new System.Drawing.Point(36, 376);
            this.btn_selectLocalXls.Name = "btn_selectLocalXls";
            this.btn_selectLocalXls.Size = new System.Drawing.Size(152, 33);
            this.btn_selectLocalXls.TabIndex = 2;
            this.btn_selectLocalXls.Text = "选取本地属性表";
            this.btn_selectLocalXls.UseVisualStyleBackColor = true;
            this.btn_selectLocalXls.Click += new System.EventHandler(this.btn_selectLocalXls_Click);
            // 
            // dgv_parameter
            // 
            this.dgv_parameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_parameter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_pName,
            this.dgv_pValue,
            this.dgv_pIns,
            this.dgv_pGroup,
            this.dgv_pType});
            this.dgv_parameter.Location = new System.Drawing.Point(560, 75);
            this.dgv_parameter.Name = "dgv_parameter";
            this.dgv_parameter.RowTemplate.Height = 23;
            this.dgv_parameter.Size = new System.Drawing.Size(549, 439);
            this.dgv_parameter.TabIndex = 7;
            // 
            // dgv_pName
            // 
            this.dgv_pName.HeaderText = "属性名称";
            this.dgv_pName.Name = "dgv_pName";
            // 
            // dgv_pValue
            // 
            this.dgv_pValue.HeaderText = "属性值";
            this.dgv_pValue.Name = "dgv_pValue";
            // 
            // dgv_pIns
            // 
            this.dgv_pIns.HeaderText = "是否实例";
            this.dgv_pIns.Name = "dgv_pIns";
            // 
            // dgv_pGroup
            // 
            this.dgv_pGroup.HeaderText = "属性分组";
            this.dgv_pGroup.Name = "dgv_pGroup";
            // 
            // dgv_pType
            // 
            this.dgv_pType.HeaderText = "属性类别";
            this.dgv_pType.Name = "dgv_pType";
            // 
            // btn_delectSelected
            // 
            this.btn_delectSelected.Location = new System.Drawing.Point(238, 376);
            this.btn_delectSelected.Name = "btn_delectSelected";
            this.btn_delectSelected.Size = new System.Drawing.Size(152, 33);
            this.btn_delectSelected.TabIndex = 8;
            this.btn_delectSelected.Text = "删除选中项";
            this.btn_delectSelected.UseVisualStyleBackColor = true;
            this.btn_delectSelected.Click += new System.EventHandler(this.btn_delectSelected_Click);
            // 
            // btn_confirm
            // 
            this.btn_confirm.Location = new System.Drawing.Point(957, 546);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(152, 33);
            this.btn_confirm.TabIndex = 9;
            this.btn_confirm.Text = "确认修改";
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(36, 516);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(157, 63);
            this.btn_OK.TabIndex = 10;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // ckb_clearsymbol
            // 
            this.ckb_clearsymbol.AutoSize = true;
            this.ckb_clearsymbol.Location = new System.Drawing.Point(36, 440);
            this.ckb_clearsymbol.Name = "ckb_clearsymbol";
            this.ckb_clearsymbol.Size = new System.Drawing.Size(228, 16);
            this.ckb_clearsymbol.TabIndex = 11;
            this.ckb_clearsymbol.Text = "清空族内所有族类型，仅保留默认类型";
            this.ckb_clearsymbol.UseVisualStyleBackColor = true;
            // 
            // ckb_clearpara
            // 
            this.ckb_clearpara.AutoSize = true;
            this.ckb_clearpara.Location = new System.Drawing.Point(36, 473);
            this.ckb_clearpara.Name = "ckb_clearpara";
            this.ckb_clearpara.Size = new System.Drawing.Size(108, 16);
            this.ckb_clearpara.TabIndex = 12;
            this.ckb_clearpara.Text = "清空族原有属性";
            this.ckb_clearpara.UseVisualStyleBackColor = true;
            // 
            // RevitFamilyManagerFormEP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 614);
            this.Controls.Add(this.ckb_clearpara);
            this.Controls.Add(this.ckb_clearsymbol);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.btn_delectSelected);
            this.Controls.Add(this.dgv_parameter);
            this.Controls.Add(this.btn_selectLocalXls);
            this.Controls.Add(this.lvw_eleSelXls);
            this.Name = "RevitFamilyManagerFormEP";
            this.Text = "RevitFamilyManagerFormEP";
            this.Load += new System.EventHandler(this.RevitFamilyManagerFormEP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_parameter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvw_eleSelXls;
        private System.Windows.Forms.ColumnHeader col_fName;
        private System.Windows.Forms.ColumnHeader col_xPath;
        private System.Windows.Forms.ColumnHeader col_xName;
        private System.Windows.Forms.Button btn_selectLocalXls;
        private System.Windows.Forms.DataGridView dgv_parameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_pName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_pValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_pIns;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_pGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_pType;
        private System.Windows.Forms.Button btn_delectSelected;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.CheckBox ckb_clearsymbol;
        private System.Windows.Forms.CheckBox ckb_clearpara;
        private System.Windows.Forms.ColumnHeader col_ffName;
    }
}