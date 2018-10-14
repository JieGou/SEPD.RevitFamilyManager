namespace SEPD.RevitFamilyManager
{
    partial class RevitFamilyManagerFormED
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
            this.tvw_Family = new System.Windows.Forms.TreeView();
            this.tvw_Family2 = new System.Windows.Forms.TreeView();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.lbl_pageCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_currentPage = new System.Windows.Forms.Label();
            this.lbl_recordCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_endPage = new System.Windows.Forms.Label();
            this.lbl_nextPage = new System.Windows.Forms.Label();
            this.lbl_pervPage = new System.Windows.Forms.Label();
            this.lbl_firstPage = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgv_Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgv_Pics = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgv_FamilyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ValLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Manufacturer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_DeviceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Hash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_FamilyLoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ParaLoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_defaultXls = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lvw_SelectedFamilies = new System.Windows.Forms.ListView();
            this.se = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_transform = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_familyCat = new System.Windows.Forms.Label();
            this.lbl_familyPro = new System.Windows.Forms.Label();
            this.btn_confChange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tvw_Family
            // 
            this.tvw_Family.Location = new System.Drawing.Point(19, 47);
            this.tvw_Family.Name = "tvw_Family";
            this.tvw_Family.Size = new System.Drawing.Size(139, 311);
            this.tvw_Family.TabIndex = 0;
            this.tvw_Family.MouseCaptureChanged += new System.EventHandler(this.tvw_Family_MouseCaptureChanged);
            // 
            // tvw_Family2
            // 
            this.tvw_Family2.Location = new System.Drawing.Point(941, 304);
            this.tvw_Family2.Name = "tvw_Family2";
            this.tvw_Family2.Size = new System.Drawing.Size(139, 291);
            this.tvw_Family2.TabIndex = 3;
            this.tvw_Family2.MouseCaptureChanged += new System.EventHandler(this.tvw_Family2_MouseCaptureChanged);
            // 
            // btn_add
            // 
            this.btn_add.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_add.Location = new System.Drawing.Point(398, 304);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(58, 54);
            this.btn_add.TabIndex = 4;
            this.btn_add.Text = "↓";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_del
            // 
            this.btn_del.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_del.Location = new System.Drawing.Point(645, 304);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(58, 54);
            this.btn_del.TabIndex = 5;
            this.btn_del.Text = "↑";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // lbl_pageCount
            // 
            this.lbl_pageCount.AutoSize = true;
            this.lbl_pageCount.Location = new System.Drawing.Point(493, 23);
            this.lbl_pageCount.Name = "lbl_pageCount";
            this.lbl_pageCount.Size = new System.Drawing.Size(23, 12);
            this.lbl_pageCount.TabIndex = 6;
            this.lbl_pageCount.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(440, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "总页数:";
            // 
            // lbl_currentPage
            // 
            this.lbl_currentPage.AutoSize = true;
            this.lbl_currentPage.Location = new System.Drawing.Point(411, 23);
            this.lbl_currentPage.Name = "lbl_currentPage";
            this.lbl_currentPage.Size = new System.Drawing.Size(23, 12);
            this.lbl_currentPage.TabIndex = 8;
            this.lbl_currentPage.Text = "...";
            // 
            // lbl_recordCount
            // 
            this.lbl_recordCount.AutoSize = true;
            this.lbl_recordCount.Location = new System.Drawing.Point(587, 23);
            this.lbl_recordCount.Name = "lbl_recordCount";
            this.lbl_recordCount.Size = new System.Drawing.Size(23, 12);
            this.lbl_recordCount.TabIndex = 9;
            this.lbl_recordCount.Text = "...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(358, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "当前页:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(522, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "总记录数:";
            // 
            // lbl_endPage
            // 
            this.lbl_endPage.AutoSize = true;
            this.lbl_endPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_endPage.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_endPage.Location = new System.Drawing.Point(297, 23);
            this.lbl_endPage.Name = "lbl_endPage";
            this.lbl_endPage.Size = new System.Drawing.Size(29, 12);
            this.lbl_endPage.TabIndex = 102;
            this.lbl_endPage.Text = "末页";
            this.lbl_endPage.Click += new System.EventHandler(this.lbl_endPage_Click);
            // 
            // lbl_nextPage
            // 
            this.lbl_nextPage.AutoSize = true;
            this.lbl_nextPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_nextPage.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_nextPage.Location = new System.Drawing.Point(250, 23);
            this.lbl_nextPage.Name = "lbl_nextPage";
            this.lbl_nextPage.Size = new System.Drawing.Size(41, 12);
            this.lbl_nextPage.TabIndex = 101;
            this.lbl_nextPage.Text = "下一页";
            this.lbl_nextPage.Click += new System.EventHandler(this.lbl_nextPage_Click);
            // 
            // lbl_pervPage
            // 
            this.lbl_pervPage.AutoSize = true;
            this.lbl_pervPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_pervPage.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_pervPage.Location = new System.Drawing.Point(203, 23);
            this.lbl_pervPage.Name = "lbl_pervPage";
            this.lbl_pervPage.Size = new System.Drawing.Size(41, 12);
            this.lbl_pervPage.TabIndex = 100;
            this.lbl_pervPage.Text = "上一页";
            this.lbl_pervPage.Click += new System.EventHandler(this.lbl_pervPage_Click);
            // 
            // lbl_firstPage
            // 
            this.lbl_firstPage.AutoSize = true;
            this.lbl_firstPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_firstPage.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_firstPage.Location = new System.Drawing.Point(168, 23);
            this.lbl_firstPage.Name = "lbl_firstPage";
            this.lbl_firstPage.Size = new System.Drawing.Size(29, 12);
            this.lbl_firstPage.TabIndex = 99;
            this.lbl_firstPage.Text = "首页";
            this.lbl_firstPage.Click += new System.EventHandler(this.lbl_firstPage_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_Selected,
            this.dgv_Pics,
            this.dgv_FamilyName,
            this.dgv_ValLevel,
            this.dgv_Manufacturer,
            this.dgv_DeviceType,
            this.dgv_Source,
            this.dgv_Version,
            this.dgv_Date,
            this.dgv_Type,
            this.dgv_Hash,
            this.dgv_Note,
            this.dgv_FamilyLoc,
            this.dgv_ParaLoc,
            this.dgv_defaultXls});
            this.dataGridView1.Font = new System.Drawing.Font("黑体", 12F);
            this.dataGridView1.Location = new System.Drawing.Point(164, 47);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(916, 242);
            this.dataGridView1.TabIndex = 103;
            // 
            // dgv_Selected
            // 
            this.dgv_Selected.HeaderText = "选择";
            this.dgv_Selected.Name = "dgv_Selected";
            this.dgv_Selected.Width = 60;
            // 
            // dgv_Pics
            // 
            this.dgv_Pics.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_Pics.FillWeight = 10F;
            this.dgv_Pics.HeaderText = "预览";
            this.dgv_Pics.Name = "dgv_Pics";
            this.dgv_Pics.Width = 46;
            // 
            // dgv_FamilyName
            // 
            this.dgv_FamilyName.HeaderText = "名称";
            this.dgv_FamilyName.Name = "dgv_FamilyName";
            this.dgv_FamilyName.Width = 150;
            // 
            // dgv_ValLevel
            // 
            this.dgv_ValLevel.HeaderText = "电压等级";
            this.dgv_ValLevel.Name = "dgv_ValLevel";
            // 
            // dgv_Manufacturer
            // 
            this.dgv_Manufacturer.HeaderText = "生产厂家";
            this.dgv_Manufacturer.Name = "dgv_Manufacturer";
            // 
            // dgv_DeviceType
            // 
            this.dgv_DeviceType.HeaderText = "型号";
            this.dgv_DeviceType.Name = "dgv_DeviceType";
            // 
            // dgv_Source
            // 
            this.dgv_Source.HeaderText = "族来源";
            this.dgv_Source.Name = "dgv_Source";
            // 
            // dgv_Version
            // 
            this.dgv_Version.HeaderText = "版本";
            this.dgv_Version.Name = "dgv_Version";
            this.dgv_Version.Width = 80;
            // 
            // dgv_Date
            // 
            this.dgv_Date.HeaderText = "日期";
            this.dgv_Date.Name = "dgv_Date";
            this.dgv_Date.ReadOnly = true;
            this.dgv_Date.Width = 80;
            // 
            // dgv_Type
            // 
            this.dgv_Type.HeaderText = "类型";
            this.dgv_Type.Name = "dgv_Type";
            // 
            // dgv_Hash
            // 
            this.dgv_Hash.HeaderText = "MD5";
            this.dgv_Hash.Name = "dgv_Hash";
            this.dgv_Hash.ReadOnly = true;
            this.dgv_Hash.Width = 50;
            // 
            // dgv_Note
            // 
            this.dgv_Note.HeaderText = "备注";
            this.dgv_Note.Name = "dgv_Note";
            // 
            // dgv_FamilyLoc
            // 
            this.dgv_FamilyLoc.HeaderText = "族下载";
            this.dgv_FamilyLoc.Name = "dgv_FamilyLoc";
            this.dgv_FamilyLoc.ReadOnly = true;
            // 
            // dgv_ParaLoc
            // 
            this.dgv_ParaLoc.HeaderText = "绑定表";
            this.dgv_ParaLoc.Name = "dgv_ParaLoc";
            this.dgv_ParaLoc.ReadOnly = true;
            // 
            // dgv_defaultXls
            // 
            this.dgv_defaultXls.HeaderText = "自定义表";
            this.dgv_defaultXls.Name = "dgv_defaultXls";
            // 
            // lvw_SelectedFamilies
            // 
            this.lvw_SelectedFamilies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvw_SelectedFamilies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.se,
            this.lvw_Name,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader9,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader1,
            this.columnHeader16});
            this.lvw_SelectedFamilies.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvw_SelectedFamilies.FullRowSelect = true;
            this.lvw_SelectedFamilies.HideSelection = false;
            this.lvw_SelectedFamilies.Location = new System.Drawing.Point(19, 375);
            this.lvw_SelectedFamilies.Name = "lvw_SelectedFamilies";
            this.lvw_SelectedFamilies.Size = new System.Drawing.Size(916, 220);
            this.lvw_SelectedFamilies.TabIndex = 104;
            this.lvw_SelectedFamilies.UseCompatibleStateImageBehavior = false;
            this.lvw_SelectedFamilies.View = System.Windows.Forms.View.Details;
            // 
            // se
            // 
            this.se.Text = "×";
            this.se.Width = 0;
            // 
            // lvw_Name
            // 
            this.lvw_Name.Text = "名称";
            this.lvw_Name.Width = 200;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "族下载";
            this.columnHeader10.Width = 0;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "绑定表";
            this.columnHeader11.Width = 0;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "日期";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "电压等级";
            this.columnHeader12.Width = 100;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "生产厂家";
            this.columnHeader13.Width = 100;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "设备型号";
            this.columnHeader14.Width = 100;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "哈希";
            this.columnHeader15.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "族来源";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "自定义表";
            this.columnHeader16.Width = 89;
            // 
            // btn_transform
            // 
            this.btn_transform.Location = new System.Drawing.Point(709, 608);
            this.btn_transform.Name = "btn_transform";
            this.btn_transform.Size = new System.Drawing.Size(226, 45);
            this.btn_transform.TabIndex = 105;
            this.btn_transform.Text = "转到该分类";
            this.btn_transform.UseVisualStyleBackColor = true;
            this.btn_transform.Click += new System.EventHandler(this.btn_transform_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(205, 608);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(169, 45);
            this.btn_cancel.TabIndex = 106;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // txt_Search
            // 
            this.txt_Search.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Search.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_Search.Location = new System.Drawing.Point(636, 18);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(299, 23);
            this.txt_Search.TabIndex = 107;
            this.txt_Search.Text = "使用逗号或空格分隔";
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(954, 18);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(75, 23);
            this.btn_Search.TabIndex = 108;
            this.btn_Search.Text = "搜索";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(19, 608);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(139, 45);
            this.btn_Delete.TabIndex = 109;
            this.btn_Delete.Text = "删除该族";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(969, 608);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 110;
            this.label4.Text = "专业：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(969, 631);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 111;
            this.label5.Text = "类型：";
            // 
            // lbl_familyCat
            // 
            this.lbl_familyCat.AutoSize = true;
            this.lbl_familyCat.Location = new System.Drawing.Point(1016, 631);
            this.lbl_familyCat.Name = "lbl_familyCat";
            this.lbl_familyCat.Size = new System.Drawing.Size(17, 12);
            this.lbl_familyCat.TabIndex = 113;
            this.lbl_familyCat.Text = "NA";
            this.lbl_familyCat.Click += new System.EventHandler(this.lbl_familyCat_Click);
            // 
            // lbl_familyPro
            // 
            this.lbl_familyPro.AutoSize = true;
            this.lbl_familyPro.Location = new System.Drawing.Point(1016, 608);
            this.lbl_familyPro.Name = "lbl_familyPro";
            this.lbl_familyPro.Size = new System.Drawing.Size(17, 12);
            this.lbl_familyPro.TabIndex = 112;
            this.lbl_familyPro.Text = "NA";
            this.lbl_familyPro.Click += new System.EventHandler(this.lbl_familyPro_Click);
            // 
            // btn_confChange
            // 
            this.btn_confChange.Location = new System.Drawing.Point(441, 608);
            this.btn_confChange.Name = "btn_confChange";
            this.btn_confChange.Size = new System.Drawing.Size(235, 45);
            this.btn_confChange.TabIndex = 114;
            this.btn_confChange.Text = "确认上述列表内信息修改";
            this.btn_confChange.UseVisualStyleBackColor = true;
            this.btn_confChange.Click += new System.EventHandler(this.btn_confChange_Click);
            // 
            // RevitFamilyManagerFormED
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 674);
            this.Controls.Add(this.btn_confChange);
            this.Controls.Add(this.lbl_familyCat);
            this.Controls.Add(this.lbl_familyPro);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.txt_Search);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_transform);
            this.Controls.Add(this.lvw_SelectedFamilies);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbl_endPage);
            this.Controls.Add(this.lbl_nextPage);
            this.Controls.Add(this.lbl_pervPage);
            this.Controls.Add(this.lbl_firstPage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_recordCount);
            this.Controls.Add(this.lbl_currentPage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_pageCount);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.tvw_Family2);
            this.Controls.Add(this.tvw_Family);
            this.Name = "RevitFamilyManagerFormED";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.RevitFamilyManagerFormED_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvw_Family;
        private System.Windows.Forms.TreeView tvw_Family2;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Label lbl_pageCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_currentPage;
        private System.Windows.Forms.Label lbl_recordCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_endPage;
        private System.Windows.Forms.Label lbl_nextPage;
        private System.Windows.Forms.Label lbl_pervPage;
        private System.Windows.Forms.Label lbl_firstPage;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListView lvw_SelectedFamilies;
        private System.Windows.Forms.ColumnHeader se;
        private System.Windows.Forms.ColumnHeader lvw_Name;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.Button btn_transform;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgv_Selected;
        private System.Windows.Forms.DataGridViewImageColumn dgv_Pics;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_FamilyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ValLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Manufacturer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_DeviceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Source;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Hash;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Note;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_FamilyLoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ParaLoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_defaultXls;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_familyCat;
        private System.Windows.Forms.Label lbl_familyPro;
        private System.Windows.Forms.Button btn_confChange;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}