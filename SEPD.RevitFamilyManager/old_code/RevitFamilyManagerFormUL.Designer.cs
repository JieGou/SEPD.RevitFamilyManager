using System.Drawing;

namespace SEPD.RevitFamilyManager
{
    partial class RevitFamilyManagerFormUL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RevitFamilyManagerFormUL));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_DeviceType = new System.Windows.Forms.TextBox();
            this.txt_SelectedFamilyName = new System.Windows.Forms.TextBox();
            this.cmb_ValLevel = new System.Windows.Forms.ComboBox();
            this.txt_Manufacturer = new System.Windows.Forms.TextBox();
            this.txt_openConfigParaPath = new System.Windows.Forms.TextBox();
            this.btn_UploadFamily = new System.Windows.Forms.Button();
            this.btn_openFamilyPath = new System.Windows.Forms.Button();
            this.txt_openFamilyPath = new System.Windows.Forms.TextBox();
            this.btn_openConfigParaPath = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dgv_ParaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ParaValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ParaTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.tvw_Family = new System.Windows.Forms.TreeView();
            this.lbl_ProText = new System.Windows.Forms.Label();
            this.lbl_TypeText = new System.Windows.Forms.Label();
            this.btn_winclose = new System.Windows.Forms.Button();
            this.btn_winminiz = new System.Windows.Forms.Button();
            this.cmb_Source = new System.Windows.Forms.ComboBox();
            this.btn_defaultXls = new System.Windows.Forms.Button();
            this.btn_checkpara = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(1219, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(187, 191);
            this.pictureBox1.TabIndex = 75;
            this.pictureBox1.TabStop = false;
            // 
            // txt_DeviceType
            // 
            this.txt_DeviceType.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_DeviceType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_DeviceType.Location = new System.Drawing.Point(1134, 443);
            this.txt_DeviceType.Name = "txt_DeviceType";
            this.txt_DeviceType.Size = new System.Drawing.Size(369, 31);
            this.txt_DeviceType.TabIndex = 74;
            this.txt_DeviceType.Text = "NA";
            this.txt_DeviceType.TextChanged += new System.EventHandler(this.txt_DeviceType_TextChanged);
            // 
            // txt_SelectedFamilyName
            // 
            this.txt_SelectedFamilyName.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_SelectedFamilyName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_SelectedFamilyName.Location = new System.Drawing.Point(1134, 595);
            this.txt_SelectedFamilyName.Name = "txt_SelectedFamilyName";
            this.txt_SelectedFamilyName.Size = new System.Drawing.Size(369, 31);
            this.txt_SelectedFamilyName.TabIndex = 72;
            // 
            // cmb_ValLevel
            // 
            this.cmb_ValLevel.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_ValLevel.FormattingEnabled = true;
            this.cmb_ValLevel.Location = new System.Drawing.Point(1134, 367);
            this.cmb_ValLevel.Name = "cmb_ValLevel";
            this.cmb_ValLevel.Size = new System.Drawing.Size(369, 29);
            this.cmb_ValLevel.TabIndex = 67;
            // 
            // txt_Manufacturer
            // 
            this.txt_Manufacturer.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Manufacturer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_Manufacturer.Location = new System.Drawing.Point(1133, 519);
            this.txt_Manufacturer.Name = "txt_Manufacturer";
            this.txt_Manufacturer.Size = new System.Drawing.Size(370, 31);
            this.txt_Manufacturer.TabIndex = 66;
            this.txt_Manufacturer.Text = "NA";
            // 
            // txt_openConfigParaPath
            // 
            this.txt_openConfigParaPath.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_openConfigParaPath.Location = new System.Drawing.Point(464, 160);
            this.txt_openConfigParaPath.Name = "txt_openConfigParaPath";
            this.txt_openConfigParaPath.Size = new System.Drawing.Size(357, 32);
            this.txt_openConfigParaPath.TabIndex = 64;
            // 
            // btn_UploadFamily
            // 
            this.btn_UploadFamily.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_UploadFamily.BackgroundImage")));
            this.btn_UploadFamily.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_UploadFamily.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_UploadFamily.Location = new System.Drawing.Point(1130, 760);
            this.btn_UploadFamily.Name = "btn_UploadFamily";
            this.btn_UploadFamily.Size = new System.Drawing.Size(373, 78);
            this.btn_UploadFamily.TabIndex = 60;
            this.btn_UploadFamily.UseVisualStyleBackColor = true;
            this.btn_UploadFamily.Click += new System.EventHandler(this.btn_UploadFamily_Click);
            // 
            // btn_openFamilyPath
            // 
            this.btn_openFamilyPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_openFamilyPath.BackgroundImage")));
            this.btn_openFamilyPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_openFamilyPath.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_openFamilyPath.Location = new System.Drawing.Point(855, 99);
            this.btn_openFamilyPath.Name = "btn_openFamilyPath";
            this.btn_openFamilyPath.Size = new System.Drawing.Size(129, 39);
            this.btn_openFamilyPath.TabIndex = 59;
            this.btn_openFamilyPath.UseVisualStyleBackColor = true;
            this.btn_openFamilyPath.Click += new System.EventHandler(this.btn_openFamilyPath_Click);
            // 
            // txt_openFamilyPath
            // 
            this.txt_openFamilyPath.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_openFamilyPath.Location = new System.Drawing.Point(464, 103);
            this.txt_openFamilyPath.Name = "txt_openFamilyPath";
            this.txt_openFamilyPath.Size = new System.Drawing.Size(357, 32);
            this.txt_openFamilyPath.TabIndex = 61;
            // 
            // btn_openConfigParaPath
            // 
            this.btn_openConfigParaPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_openConfigParaPath.BackgroundImage")));
            this.btn_openConfigParaPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_openConfigParaPath.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_openConfigParaPath.Location = new System.Drawing.Point(855, 158);
            this.btn_openConfigParaPath.Name = "btn_openConfigParaPath";
            this.btn_openConfigParaPath.Size = new System.Drawing.Size(129, 39);
            this.btn_openConfigParaPath.TabIndex = 79;
            this.btn_openConfigParaPath.UseVisualStyleBackColor = true;
            this.btn_openConfigParaPath.Click += new System.EventHandler(this.btn_openConfigParaPath_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_ParaName,
            this.dgv_ParaValue,
            this.dgv_ParaTag});
            this.dataGridView2.Font = new System.Drawing.Font("黑体", 12F);
            this.dataGridView2.Location = new System.Drawing.Point(331, 236);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(653, 507);
            this.dataGridView2.TabIndex = 80;
            // 
            // dgv_ParaName
            // 
            this.dgv_ParaName.HeaderText = "参数名";
            this.dgv_ParaName.Name = "dgv_ParaName";
            this.dgv_ParaName.Width = 300;
            // 
            // dgv_ParaValue
            // 
            this.dgv_ParaValue.HeaderText = "参数值";
            this.dgv_ParaValue.Name = "dgv_ParaValue";
            this.dgv_ParaValue.Width = 220;
            // 
            // dgv_ParaTag
            // 
            this.dgv_ParaTag.HeaderText = "是否实例";
            this.dgv_ParaTag.Name = "dgv_ParaTag";
            this.dgv_ParaTag.Width = 120;
            // 
            // btn_confirm
            // 
            this.btn_confirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_confirm.BackgroundImage")));
            this.btn_confirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_confirm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_confirm.Location = new System.Drawing.Point(738, 761);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(246, 78);
            this.btn_confirm.TabIndex = 81;
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // tvw_Family
            // 
            this.tvw_Family.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvw_Family.Location = new System.Drawing.Point(84, 216);
            this.tvw_Family.Name = "tvw_Family";
            this.tvw_Family.Size = new System.Drawing.Size(207, 623);
            this.tvw_Family.TabIndex = 82;
            this.tvw_Family.MouseCaptureChanged += new System.EventHandler(this.tvw_Family_MouseCaptureChanged);
            // 
            // lbl_ProText
            // 
            this.lbl_ProText.AutoSize = true;
            this.lbl_ProText.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_ProText.Location = new System.Drawing.Point(228, 119);
            this.lbl_ProText.Name = "lbl_ProText";
            this.lbl_ProText.Size = new System.Drawing.Size(29, 19);
            this.lbl_ProText.TabIndex = 83;
            this.lbl_ProText.Text = "NA";
            this.lbl_ProText.Click += new System.EventHandler(this.lbl_ProText_Click);
            // 
            // lbl_TypeText
            // 
            this.lbl_TypeText.AutoSize = true;
            this.lbl_TypeText.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_TypeText.Location = new System.Drawing.Point(228, 158);
            this.lbl_TypeText.Name = "lbl_TypeText";
            this.lbl_TypeText.Size = new System.Drawing.Size(29, 19);
            this.lbl_TypeText.TabIndex = 84;
            this.lbl_TypeText.Text = "NA";
            // 
            // btn_winclose
            // 
            this.btn_winclose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_winclose.BackgroundImage")));
            this.btn_winclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_winclose.Location = new System.Drawing.Point(1527, 12);
            this.btn_winclose.Name = "btn_winclose";
            this.btn_winclose.Size = new System.Drawing.Size(60, 30);
            this.btn_winclose.TabIndex = 85;
            this.btn_winclose.UseVisualStyleBackColor = true;
            this.btn_winclose.Click += new System.EventHandler(this.btn_winclose_Click);
            // 
            // btn_winminiz
            // 
            this.btn_winminiz.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_winminiz.BackgroundImage")));
            this.btn_winminiz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_winminiz.Location = new System.Drawing.Point(1452, 12);
            this.btn_winminiz.Name = "btn_winminiz";
            this.btn_winminiz.Size = new System.Drawing.Size(60, 30);
            this.btn_winminiz.TabIndex = 86;
            this.btn_winminiz.UseVisualStyleBackColor = true;
            this.btn_winminiz.Click += new System.EventHandler(this.btn_winminiz_Click);
            // 
            // cmb_Source
            // 
            this.cmb_Source.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Source.FormattingEnabled = true;
            this.cmb_Source.Location = new System.Drawing.Point(1134, 674);
            this.cmb_Source.Name = "cmb_Source";
            this.cmb_Source.Size = new System.Drawing.Size(369, 29);
            this.cmb_Source.TabIndex = 87;
            this.cmb_Source.Text = "SEPD-KXB-OLD-VERSION";
            // 
            // btn_defaultXls
            // 
            this.btn_defaultXls.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_defaultXls.Location = new System.Drawing.Point(1009, 157);
            this.btn_defaultXls.Name = "btn_defaultXls";
            this.btn_defaultXls.Size = new System.Drawing.Size(108, 40);
            this.btn_defaultXls.TabIndex = 88;
            this.btn_defaultXls.Text = "无附表";
            this.btn_defaultXls.UseVisualStyleBackColor = true;
            this.btn_defaultXls.Click += new System.EventHandler(this.btn_defaultXls_Click);
            // 
            // btn_checkpara
            // 
            this.btn_checkpara.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_checkpara.Location = new System.Drawing.Point(1009, 97);
            this.btn_checkpara.Name = "btn_checkpara";
            this.btn_checkpara.Size = new System.Drawing.Size(108, 40);
            this.btn_checkpara.TabIndex = 89;
            this.btn_checkpara.Text = "检查属性";
            this.btn_checkpara.UseVisualStyleBackColor = true;
            this.btn_checkpara.Click += new System.EventHandler(this.btn_checkpara_Click);
            // 
            // RevitFamilyManagerFormUL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.Controls.Add(this.btn_checkpara);
            this.Controls.Add(this.btn_defaultXls);
            this.Controls.Add(this.cmb_Source);
            this.Controls.Add(this.btn_winminiz);
            this.Controls.Add(this.btn_winclose);
            this.Controls.Add(this.lbl_TypeText);
            this.Controls.Add(this.lbl_ProText);
            this.Controls.Add(this.tvw_Family);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btn_openConfigParaPath);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txt_DeviceType);
            this.Controls.Add(this.txt_SelectedFamilyName);
            this.Controls.Add(this.cmb_ValLevel);
            this.Controls.Add(this.txt_Manufacturer);
            this.Controls.Add(this.txt_openConfigParaPath);
            this.Controls.Add(this.btn_UploadFamily);
            this.Controls.Add(this.btn_openFamilyPath);
            this.Controls.Add(this.txt_openFamilyPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RevitFamilyManagerFormUL";
            this.Text = "SEPD族管理平台V0.5 by 上海电力设计院；科技信息部；周昊天，吕嘉文，徐经纬";
            this.Load += new System.EventHandler(this.RevitFamilyManagerFormUL_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RevitFamilyManagerFormUL_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txt_DeviceType;
        private System.Windows.Forms.TextBox txt_SelectedFamilyName;
        private System.Windows.Forms.ComboBox cmb_ValLevel;
        private System.Windows.Forms.TextBox txt_Manufacturer;
        private System.Windows.Forms.TextBox txt_openConfigParaPath;
        private System.Windows.Forms.Button btn_UploadFamily;
        private System.Windows.Forms.Button btn_openFamilyPath;
        public System.Windows.Forms.TextBox txt_openFamilyPath;
        private System.Windows.Forms.Button btn_openConfigParaPath;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.TreeView tvw_Family;
        private System.Windows.Forms.Label lbl_ProText;
        private System.Windows.Forms.Label lbl_TypeText;
        private System.Windows.Forms.Button btn_winclose;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ParaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ParaValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ParaTag;
        private System.Windows.Forms.Button btn_winminiz;
        private System.Windows.Forms.ComboBox cmb_Source;
        private System.Windows.Forms.Button btn_defaultXls;
        private System.Windows.Forms.Button btn_checkpara;
    }
}