using System.Drawing;

namespace SEPD.RevitFamilyManager
{
    partial class RevitFamilyManagerFormDL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RevitFamilyManagerFormDL));
            this.tvw_Family = new System.Windows.Forms.TreeView();
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
            this.btn_Search = new System.Windows.Forms.Button();
            this.txt_Search = new System.Windows.Forms.TextBox();
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
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_DeleteFromListBox = new System.Windows.Forms.Button();
            this.btn_AddToListBox = new System.Windows.Forms.Button();
            this.btn_justLoadRfa = new System.Windows.Forms.Button();
            this.btn_loadRfa = new System.Windows.Forms.Button();
            this.btn_Download = new System.Windows.Forms.Button();
            this.btn_ViewFamilyPara = new System.Windows.Forms.Button();
            this.lvw_ParaListSelectedFamily = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_config = new System.Windows.Forms.Button();
            this.btn_loadRfa0 = new System.Windows.Forms.Button();
            this.btn_winminiz = new System.Windows.Forms.Button();
            this.btn_winclose = new System.Windows.Forms.Button();
            this.txt_defaultXls = new System.Windows.Forms.TextBox();
            this.btn_defaultXls = new System.Windows.Forms.Button();
            this.btn_xlsOK = new System.Windows.Forms.Button();
            this.lbl_instandXls = new System.Windows.Forms.Label();
            this.lbl_firstPage = new System.Windows.Forms.Label();
            this.lbl_pervPage = new System.Windows.Forms.Label();
            this.lbl_nextPage = new System.Windows.Forms.Label();
            this.lbl_endPage = new System.Windows.Forms.Label();
            this.lbl_currentPage = new System.Windows.Forms.Label();
            this.lbl_pageCount = new System.Windows.Forms.Label();
            this.lbl_recordCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tvw_Family
            // 
            this.tvw_Family.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvw_Family.Location = new System.Drawing.Point(82, 103);
            this.tvw_Family.Name = "tvw_Family";
            this.tvw_Family.Size = new System.Drawing.Size(184, 785);
            this.tvw_Family.TabIndex = 2;
            this.tvw_Family.MouseCaptureChanged += new System.EventHandler(this.tvw_Family_MouseCaptureChanged);
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
            this.dataGridView1.Location = new System.Drawing.Point(284, 185);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(612, 324);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
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
            // 
            // dgv_ParaLoc
            // 
            this.dgv_ParaLoc.HeaderText = "绑定表";
            this.dgv_ParaLoc.Name = "dgv_ParaLoc";
            // 
            // dgv_defaultXls
            // 
            this.dgv_defaultXls.HeaderText = "自定义表";
            this.dgv_defaultXls.Name = "dgv_defaultXls";
            // 
            // btn_Search
            // 
            this.btn_Search.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Search.BackgroundImage")));
            this.btn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Search.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Search.Location = new System.Drawing.Point(676, 103);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(163, 64);
            this.btn_Search.TabIndex = 37;
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // txt_Search
            // 
            this.txt_Search.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Search.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_Search.Location = new System.Drawing.Point(284, 103);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(377, 31);
            this.txt_Search.TabIndex = 36;
            this.txt_Search.Text = "查找条件使用逗号或空格分隔";
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
            this.columnHeader16});
            this.lvw_SelectedFamilies.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvw_SelectedFamilies.FullRowSelect = true;
            this.lvw_SelectedFamilies.HideSelection = false;
            this.lvw_SelectedFamilies.Location = new System.Drawing.Point(1005, 185);
            this.lvw_SelectedFamilies.Name = "lvw_SelectedFamilies";
            this.lvw_SelectedFamilies.Size = new System.Drawing.Size(498, 357);
            this.lvw_SelectedFamilies.TabIndex = 42;
            this.lvw_SelectedFamilies.UseCompatibleStateImageBehavior = false;
            this.lvw_SelectedFamilies.View = System.Windows.Forms.View.Details;
            // 
            // se
            // 
            this.se.DisplayIndex = 3;
            this.se.Text = "×";
            this.se.Width = 0;
            // 
            // lvw_Name
            // 
            this.lvw_Name.DisplayIndex = 0;
            this.lvw_Name.Text = "名称";
            this.lvw_Name.Width = 200;
            // 
            // columnHeader10
            // 
            this.columnHeader10.DisplayIndex = 1;
            this.columnHeader10.Text = "族下载";
            this.columnHeader10.Width = 0;
            // 
            // columnHeader11
            // 
            this.columnHeader11.DisplayIndex = 2;
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
            // columnHeader16
            // 
            this.columnHeader16.Text = "自定义表";
            // 
            // btn_DeleteFromListBox
            // 
            this.btn_DeleteFromListBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_DeleteFromListBox.BackgroundImage")));
            this.btn_DeleteFromListBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_DeleteFromListBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DeleteFromListBox.Location = new System.Drawing.Point(918, 409);
            this.btn_DeleteFromListBox.Name = "btn_DeleteFromListBox";
            this.btn_DeleteFromListBox.Size = new System.Drawing.Size(66, 58);
            this.btn_DeleteFromListBox.TabIndex = 41;
            this.btn_DeleteFromListBox.UseVisualStyleBackColor = true;
            this.btn_DeleteFromListBox.Click += new System.EventHandler(this.btn_DeleteFromListBox_Click);
            // 
            // btn_AddToListBox
            // 
            this.btn_AddToListBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_AddToListBox.BackgroundImage")));
            this.btn_AddToListBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AddToListBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_AddToListBox.Location = new System.Drawing.Point(918, 267);
            this.btn_AddToListBox.Name = "btn_AddToListBox";
            this.btn_AddToListBox.Size = new System.Drawing.Size(66, 58);
            this.btn_AddToListBox.TabIndex = 40;
            this.btn_AddToListBox.UseVisualStyleBackColor = true;
            this.btn_AddToListBox.Click += new System.EventHandler(this.btn_AddToListBox_Click);
            // 
            // btn_justLoadRfa
            // 
            this.btn_justLoadRfa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_justLoadRfa.BackgroundImage")));
            this.btn_justLoadRfa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_justLoadRfa.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_justLoadRfa.Location = new System.Drawing.Point(1131, 563);
            this.btn_justLoadRfa.Name = "btn_justLoadRfa";
            this.btn_justLoadRfa.Size = new System.Drawing.Size(120, 111);
            this.btn_justLoadRfa.TabIndex = 45;
            this.btn_justLoadRfa.UseVisualStyleBackColor = true;
            this.btn_justLoadRfa.Click += new System.EventHandler(this.btn_justLoadRfa_Click);
            // 
            // btn_loadRfa
            // 
            this.btn_loadRfa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_loadRfa.BackgroundImage")));
            this.btn_loadRfa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_loadRfa.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_loadRfa.Location = new System.Drawing.Point(1383, 564);
            this.btn_loadRfa.Name = "btn_loadRfa";
            this.btn_loadRfa.Size = new System.Drawing.Size(120, 111);
            this.btn_loadRfa.TabIndex = 44;
            this.btn_loadRfa.UseVisualStyleBackColor = true;
            this.btn_loadRfa.Click += new System.EventHandler(this.btn_loadRfa_Click);
            // 
            // btn_Download
            // 
            this.btn_Download.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Download.BackgroundImage")));
            this.btn_Download.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Download.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Download.Location = new System.Drawing.Point(1005, 563);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(120, 111);
            this.btn_Download.TabIndex = 43;
            this.btn_Download.UseVisualStyleBackColor = true;
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // btn_ViewFamilyPara
            // 
            this.btn_ViewFamilyPara.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ViewFamilyPara.BackgroundImage")));
            this.btn_ViewFamilyPara.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ViewFamilyPara.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ViewFamilyPara.Location = new System.Drawing.Point(284, 554);
            this.btn_ViewFamilyPara.Name = "btn_ViewFamilyPara";
            this.btn_ViewFamilyPara.Size = new System.Drawing.Size(199, 42);
            this.btn_ViewFamilyPara.TabIndex = 46;
            this.btn_ViewFamilyPara.UseVisualStyleBackColor = true;
            this.btn_ViewFamilyPara.Click += new System.EventHandler(this.btn_ViewFamilyPara_Click);
            // 
            // lvw_ParaListSelectedFamily
            // 
            this.lvw_ParaListSelectedFamily.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvw_ParaListSelectedFamily.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvw_ParaListSelectedFamily.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvw_ParaListSelectedFamily.Location = new System.Drawing.Point(284, 607);
            this.lvw_ParaListSelectedFamily.Name = "lvw_ParaListSelectedFamily";
            this.lvw_ParaListSelectedFamily.Size = new System.Drawing.Size(612, 209);
            this.lvw_ParaListSelectedFamily.TabIndex = 47;
            this.lvw_ParaListSelectedFamily.UseCompatibleStateImageBehavior = false;
            this.lvw_ParaListSelectedFamily.View = System.Windows.Forms.View.Details;
            this.lvw_ParaListSelectedFamily.SelectedIndexChanged += new System.EventHandler(this.lvw_ParaListSelectedFamily_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.DisplayIndex = 3;
            this.columnHeader1.Text = "*";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.DisplayIndex = 0;
            this.columnHeader2.Text = "参数名";
            this.columnHeader2.Width = 250;
            // 
            // columnHeader3
            // 
            this.columnHeader3.DisplayIndex = 1;
            this.columnHeader3.Text = "参数值";
            this.columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.DisplayIndex = 2;
            this.columnHeader4.Text = "是否实例";
            this.columnHeader4.Width = 100;
            // 
            // btn_config
            // 
            this.btn_config.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_config.BackgroundImage")));
            this.btn_config.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_config.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_config.Location = new System.Drawing.Point(1211, 103);
            this.btn_config.Name = "btn_config";
            this.btn_config.Size = new System.Drawing.Size(292, 54);
            this.btn_config.TabIndex = 49;
            this.btn_config.UseVisualStyleBackColor = true;
            this.btn_config.Visible = false;
            this.btn_config.Click += new System.EventHandler(this.btn_config_Click);
            // 
            // btn_loadRfa0
            // 
            this.btn_loadRfa0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_loadRfa0.BackgroundImage")));
            this.btn_loadRfa0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_loadRfa0.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_loadRfa0.Location = new System.Drawing.Point(1257, 564);
            this.btn_loadRfa0.Name = "btn_loadRfa0";
            this.btn_loadRfa0.Size = new System.Drawing.Size(120, 111);
            this.btn_loadRfa0.TabIndex = 50;
            this.btn_loadRfa0.UseVisualStyleBackColor = true;
            this.btn_loadRfa0.Click += new System.EventHandler(this.btn_loadRfa0_Click);
            // 
            // btn_winminiz
            // 
            this.btn_winminiz.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_winminiz.BackgroundImage")));
            this.btn_winminiz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_winminiz.Location = new System.Drawing.Point(1455, 12);
            this.btn_winminiz.Name = "btn_winminiz";
            this.btn_winminiz.Size = new System.Drawing.Size(60, 30);
            this.btn_winminiz.TabIndex = 89;
            this.btn_winminiz.UseVisualStyleBackColor = true;
            this.btn_winminiz.Click += new System.EventHandler(this.btn_winminiz_Click);
            // 
            // btn_winclose
            // 
            this.btn_winclose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_winclose.BackgroundImage")));
            this.btn_winclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_winclose.Location = new System.Drawing.Point(1526, 12);
            this.btn_winclose.Name = "btn_winclose";
            this.btn_winclose.Size = new System.Drawing.Size(60, 30);
            this.btn_winclose.TabIndex = 88;
            this.btn_winclose.UseVisualStyleBackColor = true;
            this.btn_winclose.Click += new System.EventHandler(this.btn_winclose_Click);
            // 
            // txt_defaultXls
            // 
            this.txt_defaultXls.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_defaultXls.Location = new System.Drawing.Point(284, 859);
            this.txt_defaultXls.Name = "txt_defaultXls";
            this.txt_defaultXls.Size = new System.Drawing.Size(433, 29);
            this.txt_defaultXls.TabIndex = 90;
            // 
            // btn_defaultXls
            // 
            this.btn_defaultXls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_defaultXls.BackgroundImage")));
            this.btn_defaultXls.Location = new System.Drawing.Point(723, 859);
            this.btn_defaultXls.Name = "btn_defaultXls";
            this.btn_defaultXls.Size = new System.Drawing.Size(82, 29);
            this.btn_defaultXls.TabIndex = 91;
            this.btn_defaultXls.Text = "...";
            this.btn_defaultXls.UseVisualStyleBackColor = true;
            this.btn_defaultXls.Click += new System.EventHandler(this.btn_defaultXls_Click);
            // 
            // btn_xlsOK
            // 
            this.btn_xlsOK.Location = new System.Drawing.Point(811, 860);
            this.btn_xlsOK.Name = "btn_xlsOK";
            this.btn_xlsOK.Size = new System.Drawing.Size(87, 29);
            this.btn_xlsOK.TabIndex = 92;
            this.btn_xlsOK.Text = "应用";
            this.btn_xlsOK.UseVisualStyleBackColor = true;
            this.btn_xlsOK.Click += new System.EventHandler(this.btn_xlsOK_Click);
            // 
            // lbl_instandXls
            // 
            this.lbl_instandXls.AutoSize = true;
            this.lbl_instandXls.Location = new System.Drawing.Point(282, 834);
            this.lbl_instandXls.Name = "lbl_instandXls";
            this.lbl_instandXls.Size = new System.Drawing.Size(119, 12);
            this.lbl_instandXls.TabIndex = 93;
            this.lbl_instandXls.Text = "*可用于替换附表文件";
            // 
            // lbl_firstPage
            // 
            this.lbl_firstPage.AutoSize = true;
            this.lbl_firstPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_firstPage.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_firstPage.Location = new System.Drawing.Point(292, 530);
            this.lbl_firstPage.Name = "lbl_firstPage";
            this.lbl_firstPage.Size = new System.Drawing.Size(29, 12);
            this.lbl_firstPage.TabIndex = 95;
            this.lbl_firstPage.Text = "首页";
            this.lbl_firstPage.Click += new System.EventHandler(this.lbl_firstPage_Click);
            // 
            // lbl_pervPage
            // 
            this.lbl_pervPage.AutoSize = true;
            this.lbl_pervPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_pervPage.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_pervPage.Location = new System.Drawing.Point(348, 530);
            this.lbl_pervPage.Name = "lbl_pervPage";
            this.lbl_pervPage.Size = new System.Drawing.Size(41, 12);
            this.lbl_pervPage.TabIndex = 96;
            this.lbl_pervPage.Text = "上一页";
            this.lbl_pervPage.Click += new System.EventHandler(this.lbl_pervPage_Click);
            // 
            // lbl_nextPage
            // 
            this.lbl_nextPage.AutoSize = true;
            this.lbl_nextPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_nextPage.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_nextPage.Location = new System.Drawing.Point(418, 530);
            this.lbl_nextPage.Name = "lbl_nextPage";
            this.lbl_nextPage.Size = new System.Drawing.Size(41, 12);
            this.lbl_nextPage.TabIndex = 97;
            this.lbl_nextPage.Text = "下一页";
            this.lbl_nextPage.Click += new System.EventHandler(this.lbl_nextPage_Click);
            // 
            // lbl_endPage
            // 
            this.lbl_endPage.AutoSize = true;
            this.lbl_endPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_endPage.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_endPage.Location = new System.Drawing.Point(490, 530);
            this.lbl_endPage.Name = "lbl_endPage";
            this.lbl_endPage.Size = new System.Drawing.Size(29, 12);
            this.lbl_endPage.TabIndex = 98;
            this.lbl_endPage.Text = "末页";
            this.lbl_endPage.Click += new System.EventHandler(this.lbl_endPage_Click);
            // 
            // lbl_currentPage
            // 
            this.lbl_currentPage.AutoSize = true;
            this.lbl_currentPage.Location = new System.Drawing.Point(684, 530);
            this.lbl_currentPage.Name = "lbl_currentPage";
            this.lbl_currentPage.Size = new System.Drawing.Size(23, 12);
            this.lbl_currentPage.TabIndex = 99;
            this.lbl_currentPage.Text = "...";
            // 
            // lbl_pageCount
            // 
            this.lbl_pageCount.AutoSize = true;
            this.lbl_pageCount.Location = new System.Drawing.Point(764, 530);
            this.lbl_pageCount.Name = "lbl_pageCount";
            this.lbl_pageCount.Size = new System.Drawing.Size(23, 12);
            this.lbl_pageCount.TabIndex = 100;
            this.lbl_pageCount.Text = "...";
            // 
            // lbl_recordCount
            // 
            this.lbl_recordCount.AutoSize = true;
            this.lbl_recordCount.Location = new System.Drawing.Point(861, 530);
            this.lbl_recordCount.Name = "lbl_recordCount";
            this.lbl_recordCount.Size = new System.Drawing.Size(23, 12);
            this.lbl_recordCount.TabIndex = 101;
            this.lbl_recordCount.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(631, 530);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 102;
            this.label1.Text = "当前页:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(711, 530);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 103;
            this.label2.Text = "总页数:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(802, 530);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 104;
            this.label3.Text = "总记录数:";
            // 
            // RevitFamilyManagerFormDL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_recordCount);
            this.Controls.Add(this.lbl_pageCount);
            this.Controls.Add(this.lbl_currentPage);
            this.Controls.Add(this.lbl_endPage);
            this.Controls.Add(this.lbl_nextPage);
            this.Controls.Add(this.lbl_pervPage);
            this.Controls.Add(this.lbl_firstPage);
            this.Controls.Add(this.lbl_instandXls);
            this.Controls.Add(this.btn_xlsOK);
            this.Controls.Add(this.btn_defaultXls);
            this.Controls.Add(this.txt_defaultXls);
            this.Controls.Add(this.btn_winminiz);
            this.Controls.Add(this.btn_winclose);
            this.Controls.Add(this.btn_loadRfa0);
            this.Controls.Add(this.btn_config);
            this.Controls.Add(this.lvw_ParaListSelectedFamily);
            this.Controls.Add(this.btn_ViewFamilyPara);
            this.Controls.Add(this.btn_justLoadRfa);
            this.Controls.Add(this.btn_loadRfa);
            this.Controls.Add(this.btn_Download);
            this.Controls.Add(this.lvw_SelectedFamilies);
            this.Controls.Add(this.btn_DeleteFromListBox);
            this.Controls.Add(this.btn_AddToListBox);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.txt_Search);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tvw_Family);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RevitFamilyManagerFormDL";
            this.Text = "SEPD族管理平台V0.5 by 上海电力设计院；科技信息部；周昊天，吕嘉文，徐经纬";
            this.Load += new System.EventHandler(this.RevitFamilyPlatformForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RevitFamilyManagerFormDL_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvw_Family;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.TextBox txt_Search;
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
        private System.Windows.Forms.Button btn_DeleteFromListBox;
        private System.Windows.Forms.Button btn_AddToListBox;
        private System.Windows.Forms.Button btn_justLoadRfa;
        private System.Windows.Forms.Button btn_loadRfa;
        public System.Windows.Forms.Button btn_Download;
        private System.Windows.Forms.Button btn_ViewFamilyPara;
        public System.Windows.Forms.ListView lvw_ParaListSelectedFamily;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        public System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btn_config;
        private System.Windows.Forms.Button btn_loadRfa0;
        private System.Windows.Forms.Button btn_winminiz;
        private System.Windows.Forms.Button btn_winclose;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.Button btn_defaultXls;
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
        private System.Windows.Forms.Button btn_xlsOK;
        private System.Windows.Forms.TextBox txt_defaultXls;
        private System.Windows.Forms.Label lbl_instandXls;
        private System.Windows.Forms.Label lbl_firstPage;
        private System.Windows.Forms.Label lbl_pervPage;
        private System.Windows.Forms.Label lbl_nextPage;
        private System.Windows.Forms.Label lbl_endPage;
        private System.Windows.Forms.Label lbl_currentPage;
        private System.Windows.Forms.Label lbl_pageCount;
        private System.Windows.Forms.Label lbl_recordCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}