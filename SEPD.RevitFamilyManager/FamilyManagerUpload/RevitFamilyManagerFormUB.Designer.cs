namespace SEPD.RevitFamilyManager
{
    partial class RevitFamilyManagerFormUB
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
            this.btn_selectrfa = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tvw_Family = new System.Windows.Forms.TreeView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btn_selectxls = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.txt_DeviceType = new System.Windows.Forms.TextBox();
            this.txt_Manufacturer = new System.Windows.Forms.TextBox();
            this.cmb_Source = new System.Windows.Forms.ComboBox();
            this.cmb_ValLevel = new System.Windows.Forms.ComboBox();
            this.lbl_ProText = new System.Windows.Forms.Label();
            this.lbl_TypeText = new System.Windows.Forms.Label();
            this.btn_check = new System.Windows.Forms.Button();
            this.lbl_fName = new System.Windows.Forms.Label();
            this.txt_defaultXls = new System.Windows.Forms.TextBox();
            this.txt_SelectedFamilyName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_uploadx = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_selectrfa
            // 
            this.btn_selectrfa.Location = new System.Drawing.Point(208, 26);
            this.btn_selectrfa.Name = "btn_selectrfa";
            this.btn_selectrfa.Size = new System.Drawing.Size(96, 44);
            this.btn_selectrfa.TabIndex = 0;
            this.btn_selectrfa.Text = "......";
            this.btn_selectrfa.UseVisualStyleBackColor = true;
            this.btn_selectrfa.Click += new System.EventHandler(this.btn_selectrfa_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(208, 90);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(587, 191);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(848, 90);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(199, 191);
            this.pictureBox1.TabIndex = 76;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(846, 336);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 77;
            this.label1.Text = "电压等级：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(846, 376);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 78;
            this.label2.Text = "型号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(848, 413);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 79;
            this.label3.Text = "生产厂家：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(848, 454);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 80;
            this.label4.Text = "来源：";
            // 
            // tvw_Family
            // 
            this.tvw_Family.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvw_Family.Location = new System.Drawing.Point(10, 90);
            this.tvw_Family.Name = "tvw_Family";
            this.tvw_Family.Size = new System.Drawing.Size(149, 489);
            this.tvw_Family.TabIndex = 83;
            this.tvw_Family.MouseCaptureChanged += new System.EventHandler(this.tvw_Family_MouseCaptureChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(208, 362);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(587, 217);
            this.dataGridView2.TabIndex = 84;
            // 
            // btn_selectxls
            // 
            this.btn_selectxls.Location = new System.Drawing.Point(208, 312);
            this.btn_selectxls.Name = "btn_selectxls";
            this.btn_selectxls.Size = new System.Drawing.Size(96, 44);
            this.btn_selectxls.TabIndex = 85;
            this.btn_selectxls.Text = "......";
            this.btn_selectxls.UseVisualStyleBackColor = true;
            this.btn_selectxls.Click += new System.EventHandler(this.btn_selectxls_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 86;
            this.label5.Text = "所属专业：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 87;
            this.label6.Text = "所属类型：";
            // 
            // btn_confirm
            // 
            this.btn_confirm.Location = new System.Drawing.Point(699, 312);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(96, 44);
            this.btn_confirm.TabIndex = 88;
            this.btn_confirm.Text = "确认修改";
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // txt_DeviceType
            // 
            this.txt_DeviceType.Location = new System.Drawing.Point(926, 373);
            this.txt_DeviceType.Name = "txt_DeviceType";
            this.txt_DeviceType.Size = new System.Drawing.Size(121, 21);
            this.txt_DeviceType.TabIndex = 90;
            // 
            // txt_Manufacturer
            // 
            this.txt_Manufacturer.Location = new System.Drawing.Point(926, 410);
            this.txt_Manufacturer.Name = "txt_Manufacturer";
            this.txt_Manufacturer.Size = new System.Drawing.Size(121, 21);
            this.txt_Manufacturer.TabIndex = 91;
            // 
            // cmb_Source
            // 
            this.cmb_Source.FormattingEnabled = true;
            this.cmb_Source.Location = new System.Drawing.Point(926, 451);
            this.cmb_Source.Name = "cmb_Source";
            this.cmb_Source.Size = new System.Drawing.Size(121, 20);
            this.cmb_Source.TabIndex = 92;
            // 
            // cmb_ValLevel
            // 
            this.cmb_ValLevel.FormattingEnabled = true;
            this.cmb_ValLevel.Location = new System.Drawing.Point(926, 333);
            this.cmb_ValLevel.Name = "cmb_ValLevel";
            this.cmb_ValLevel.Size = new System.Drawing.Size(121, 20);
            this.cmb_ValLevel.TabIndex = 93;
            // 
            // lbl_ProText
            // 
            this.lbl_ProText.AutoSize = true;
            this.lbl_ProText.Location = new System.Drawing.Point(83, 28);
            this.lbl_ProText.Name = "lbl_ProText";
            this.lbl_ProText.Size = new System.Drawing.Size(17, 12);
            this.lbl_ProText.TabIndex = 94;
            this.lbl_ProText.Text = "NA";
            // 
            // lbl_TypeText
            // 
            this.lbl_TypeText.AutoSize = true;
            this.lbl_TypeText.Location = new System.Drawing.Point(83, 58);
            this.lbl_TypeText.Name = "lbl_TypeText";
            this.lbl_TypeText.Size = new System.Drawing.Size(17, 12);
            this.lbl_TypeText.TabIndex = 95;
            this.lbl_TypeText.Text = "NA";
            // 
            // btn_check
            // 
            this.btn_check.Location = new System.Drawing.Point(342, 26);
            this.btn_check.Name = "btn_check";
            this.btn_check.Size = new System.Drawing.Size(96, 44);
            this.btn_check.TabIndex = 96;
            this.btn_check.Text = "列表刷新";
            this.btn_check.UseVisualStyleBackColor = true;
            this.btn_check.Click += new System.EventHandler(this.btn_check_Click);
            // 
            // lbl_fName
            // 
            this.lbl_fName.AutoSize = true;
            this.lbl_fName.Location = new System.Drawing.Point(848, 284);
            this.lbl_fName.Name = "lbl_fName";
            this.lbl_fName.Size = new System.Drawing.Size(41, 12);
            this.lbl_fName.TabIndex = 97;
            this.lbl_fName.Text = "......";
            this.lbl_fName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_defaultXls
            // 
            this.txt_defaultXls.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_defaultXls.Location = new System.Drawing.Point(310, 317);
            this.txt_defaultXls.Name = "txt_defaultXls";
            this.txt_defaultXls.Size = new System.Drawing.Size(383, 31);
            this.txt_defaultXls.TabIndex = 98;
            // 
            // txt_SelectedFamilyName
            // 
            this.txt_SelectedFamilyName.Location = new System.Drawing.Point(926, 491);
            this.txt_SelectedFamilyName.Name = "txt_SelectedFamilyName";
            this.txt_SelectedFamilyName.Size = new System.Drawing.Size(121, 21);
            this.txt_SelectedFamilyName.TabIndex = 100;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(848, 494);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 101;
            this.label7.Text = "族名称：";
            // 
            // btn_uploadx
            // 
            this.btn_uploadx.Location = new System.Drawing.Point(850, 530);
            this.btn_uploadx.Name = "btn_uploadx";
            this.btn_uploadx.Size = new System.Drawing.Size(197, 49);
            this.btn_uploadx.TabIndex = 102;
            this.btn_uploadx.Text = "上传";
            this.btn_uploadx.UseVisualStyleBackColor = true;
            this.btn_uploadx.Click += new System.EventHandler(this.btn_uploadx_Click);
            // 
            // RevitFamilyManagerFormUB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 618);
            this.Controls.Add(this.btn_uploadx);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_SelectedFamilyName);
            this.Controls.Add(this.txt_defaultXls);
            this.Controls.Add(this.lbl_fName);
            this.Controls.Add(this.btn_check);
            this.Controls.Add(this.lbl_TypeText);
            this.Controls.Add(this.lbl_ProText);
            this.Controls.Add(this.cmb_ValLevel);
            this.Controls.Add(this.cmb_Source);
            this.Controls.Add(this.txt_Manufacturer);
            this.Controls.Add(this.txt_DeviceType);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_selectxls);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.tvw_Family);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_selectrfa);
            this.Name = "RevitFamilyManagerFormUB";
            this.Text = "SEPD族管理平台 by上海电力设计院；科技信息部；周昊天，吕嘉文，徐经纬";
            this.Load += new System.EventHandler(this.RevitFamilyManagerFormUB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_selectrfa;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TreeView tvw_Family;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btn_selectxls;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.TextBox txt_DeviceType;
        private System.Windows.Forms.TextBox txt_Manufacturer;
        private System.Windows.Forms.ComboBox cmb_Source;
        private System.Windows.Forms.ComboBox cmb_ValLevel;
        private System.Windows.Forms.Label lbl_ProText;
        private System.Windows.Forms.Label lbl_TypeText;
        private System.Windows.Forms.Button btn_check;
        private System.Windows.Forms.Label lbl_fName;
        private System.Windows.Forms.TextBox txt_defaultXls;
        private System.Windows.Forms.TextBox txt_SelectedFamilyName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_uploadx;
    }
}