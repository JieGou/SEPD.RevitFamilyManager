namespace SEPD.RevitFamilyManager
{
    partial class RevitFamilyManagerFormIO
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
            this.btn_SelectFamily = new System.Windows.Forms.Button();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.lvw_FamilyAndXls = new System.Windows.Forms.ListView();
            this.col_fName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_fPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_xName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_xPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dgv_FamilyAndXls = new System.Windows.Forms.DataGridView();
            this.btn_flash = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.dgv_parameter = new System.Windows.Forms.DataGridView();
            this.dgv_pName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_pValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_pIns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_pGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_pType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_fName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_fPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_xName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_xPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_confirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FamilyAndXls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_parameter)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_SelectFamily
            // 
            this.btn_SelectFamily.Location = new System.Drawing.Point(52, 33);
            this.btn_SelectFamily.Name = "btn_SelectFamily";
            this.btn_SelectFamily.Size = new System.Drawing.Size(116, 44);
            this.btn_SelectFamily.TabIndex = 0;
            this.btn_SelectFamily.Text = "...";
            this.btn_SelectFamily.UseVisualStyleBackColor = true;
            this.btn_SelectFamily.Click += new System.EventHandler(this.btn_SelectFamily_Click);
            // 
            // btn_Apply
            // 
            this.btn_Apply.Location = new System.Drawing.Point(201, 33);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(116, 45);
            this.btn_Apply.TabIndex = 1;
            this.btn_Apply.Text = "属性抽取";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // lvw_FamilyAndXls
            // 
            this.lvw_FamilyAndXls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_fName,
            this.col_fPath,
            this.col_xName,
            this.col_xPath});
            this.lvw_FamilyAndXls.Location = new System.Drawing.Point(52, 108);
            this.lvw_FamilyAndXls.Name = "lvw_FamilyAndXls";
            this.lvw_FamilyAndXls.Size = new System.Drawing.Size(451, 203);
            this.lvw_FamilyAndXls.TabIndex = 2;
            this.lvw_FamilyAndXls.UseCompatibleStateImageBehavior = false;
            this.lvw_FamilyAndXls.View = System.Windows.Forms.View.Details;
            this.lvw_FamilyAndXls.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvw_FamilyAndXls_ItemSelectionChanged);
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
            // dgv_FamilyAndXls
            // 
            this.dgv_FamilyAndXls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FamilyAndXls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_fName,
            this.dgv_fPath,
            this.dgv_xName,
            this.dgv_xPath});
            this.dgv_FamilyAndXls.Location = new System.Drawing.Point(52, 339);
            this.dgv_FamilyAndXls.Name = "dgv_FamilyAndXls";
            this.dgv_FamilyAndXls.RowTemplate.Height = 23;
            this.dgv_FamilyAndXls.Size = new System.Drawing.Size(451, 208);
            this.dgv_FamilyAndXls.TabIndex = 3;
            // 
            // btn_flash
            // 
            this.btn_flash.Location = new System.Drawing.Point(375, 33);
            this.btn_flash.Name = "btn_flash";
            this.btn_flash.Size = new System.Drawing.Size(128, 45);
            this.btn_flash.TabIndex = 4;
            this.btn_flash.Text = "刷新";
            this.btn_flash.UseVisualStyleBackColor = true;
            this.btn_flash.Click += new System.EventHandler(this.btn_flash_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(52, 571);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(116, 44);
            this.btn_clear.TabIndex = 5;
            this.btn_clear.Text = "清空";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
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
            this.dgv_parameter.Location = new System.Drawing.Point(536, 108);
            this.dgv_parameter.Name = "dgv_parameter";
            this.dgv_parameter.RowTemplate.Height = 23;
            this.dgv_parameter.Size = new System.Drawing.Size(549, 439);
            this.dgv_parameter.TabIndex = 6;
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
            // dgv_fName
            // 
            this.dgv_fName.HeaderText = "族名";
            this.dgv_fName.Name = "dgv_fName";
            // 
            // dgv_fPath
            // 
            this.dgv_fPath.HeaderText = "族路径";
            this.dgv_fPath.Name = "dgv_fPath";
            // 
            // dgv_xName
            // 
            this.dgv_xName.HeaderText = "表名";
            this.dgv_xName.Name = "dgv_xName";
            // 
            // dgv_xPath
            // 
            this.dgv_xPath.HeaderText = "表路径";
            this.dgv_xPath.Name = "dgv_xPath";
            // 
            // btn_confirm
            // 
            this.btn_confirm.Location = new System.Drawing.Point(915, 571);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(170, 44);
            this.btn_confirm.TabIndex = 7;
            this.btn_confirm.Text = "确认修改";
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // RevitFamilyManagerFormIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 674);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.dgv_parameter);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_flash);
            this.Controls.Add(this.dgv_FamilyAndXls);
            this.Controls.Add(this.lvw_FamilyAndXls);
            this.Controls.Add(this.btn_Apply);
            this.Controls.Add(this.btn_SelectFamily);
            this.Name = "RevitFamilyManagerFormIO";
            this.Text = "属性抽取及预览";
            this.Load += new System.EventHandler(this.RevitFamilyManagerFormGP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FamilyAndXls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_parameter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_SelectFamily;
        private System.Windows.Forms.Button btn_Apply;
        private System.Windows.Forms.ListView lvw_FamilyAndXls;
        private System.Windows.Forms.ColumnHeader col_fName;
        private System.Windows.Forms.ColumnHeader col_fPath;
        private System.Windows.Forms.ColumnHeader col_xName;
        private System.Windows.Forms.ColumnHeader col_xPath;
        private System.Windows.Forms.DataGridView dgv_FamilyAndXls;
        private System.Windows.Forms.Button btn_flash;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_fName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_fPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_xName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_xPath;
        private System.Windows.Forms.DataGridView dgv_parameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_pName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_pValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_pIns;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_pGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_pType;
        private System.Windows.Forms.Button btn_confirm;
    }
}