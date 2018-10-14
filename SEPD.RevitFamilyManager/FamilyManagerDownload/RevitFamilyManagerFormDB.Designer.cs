namespace SEPD.RevitFamilyManager
{
    partial class RevitFamilyManagerFormDB
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RevitFamilyManagerFormDB));
            this.tvw_Family = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_DeleteFromListBox = new System.Windows.Forms.Button();
            this.btn_AddToListBox = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.btn_loadRfa0 = new System.Windows.Forms.Button();
            this.btn_justLoadRfa = new System.Windows.Forms.Button();
            this.btn_loadRfa = new System.Windows.Forms.Button();
            this.btn_Download = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_SearchII = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tvw_Family
            // 
            this.tvw_Family.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvw_Family.Location = new System.Drawing.Point(12, 35);
            this.tvw_Family.Name = "tvw_Family";
            this.tvw_Family.Size = new System.Drawing.Size(184, 539);
            this.tvw_Family.TabIndex = 3;
            this.tvw_Family.MouseCaptureChanged += new System.EventHandler(this.tvw_Family_MouseCaptureChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(219, 108);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(452, 237);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(219, 352);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(559, 222);
            this.dataGridView2.TabIndex = 5;
            // 
            // txt_Search
            // 
            this.txt_Search.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Search.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_Search.Location = new System.Drawing.Point(219, 35);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(452, 23);
            this.txt_Search.TabIndex = 37;
            this.txt_Search.Text = "查找条件使用逗号或空格分隔";
            // 
            // btn_Search
            // 
            this.btn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Search.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Search.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Search.Location = new System.Drawing.Point(219, 67);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(97, 35);
            this.btn_Search.TabIndex = 38;
            this.btn_Search.Text = "首次查询";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_DeleteFromListBox
            // 
            this.btn_DeleteFromListBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_DeleteFromListBox.BackgroundImage")));
            this.btn_DeleteFromListBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_DeleteFromListBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DeleteFromListBox.Location = new System.Drawing.Point(680, 287);
            this.btn_DeleteFromListBox.Name = "btn_DeleteFromListBox";
            this.btn_DeleteFromListBox.Size = new System.Drawing.Size(98, 58);
            this.btn_DeleteFromListBox.TabIndex = 43;
            this.btn_DeleteFromListBox.UseVisualStyleBackColor = true;
            this.btn_DeleteFromListBox.Click += new System.EventHandler(this.btn_DeleteFromListBox_Click);
            // 
            // btn_AddToListBox
            // 
            this.btn_AddToListBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_AddToListBox.BackgroundImage")));
            this.btn_AddToListBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AddToListBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_AddToListBox.Location = new System.Drawing.Point(680, 108);
            this.btn_AddToListBox.Name = "btn_AddToListBox";
            this.btn_AddToListBox.Size = new System.Drawing.Size(98, 58);
            this.btn_AddToListBox.TabIndex = 42;
            this.btn_AddToListBox.UseVisualStyleBackColor = true;
            this.btn_AddToListBox.Click += new System.EventHandler(this.btn_AddToListBox_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(784, 108);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.Size = new System.Drawing.Size(342, 237);
            this.dataGridView3.TabIndex = 44;
            this.dataGridView3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellClick);
            this.dataGridView3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView3_KeyDown);
            // 
            // btn_loadRfa0
            // 
            this.btn_loadRfa0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_loadRfa0.BackgroundImage")));
            this.btn_loadRfa0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_loadRfa0.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_loadRfa0.Location = new System.Drawing.Point(958, 352);
            this.btn_loadRfa0.Name = "btn_loadRfa0";
            this.btn_loadRfa0.Size = new System.Drawing.Size(81, 80);
            this.btn_loadRfa0.TabIndex = 54;
            this.btn_loadRfa0.UseVisualStyleBackColor = true;
            this.btn_loadRfa0.Click += new System.EventHandler(this.btn_loadRfa0_Click);
            // 
            // btn_justLoadRfa
            // 
            this.btn_justLoadRfa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_justLoadRfa.BackgroundImage")));
            this.btn_justLoadRfa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_justLoadRfa.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_justLoadRfa.Location = new System.Drawing.Point(871, 351);
            this.btn_justLoadRfa.Name = "btn_justLoadRfa";
            this.btn_justLoadRfa.Size = new System.Drawing.Size(81, 80);
            this.btn_justLoadRfa.TabIndex = 53;
            this.btn_justLoadRfa.UseVisualStyleBackColor = true;
            this.btn_justLoadRfa.Click += new System.EventHandler(this.btn_justLoadRfa_Click);
            // 
            // btn_loadRfa
            // 
            this.btn_loadRfa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_loadRfa.BackgroundImage")));
            this.btn_loadRfa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_loadRfa.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_loadRfa.Location = new System.Drawing.Point(1045, 352);
            this.btn_loadRfa.Name = "btn_loadRfa";
            this.btn_loadRfa.Size = new System.Drawing.Size(81, 80);
            this.btn_loadRfa.TabIndex = 52;
            this.btn_loadRfa.UseVisualStyleBackColor = true;
            this.btn_loadRfa.Click += new System.EventHandler(this.btn_loadRfa_Click);
            // 
            // btn_Download
            // 
            this.btn_Download.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Download.BackgroundImage")));
            this.btn_Download.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Download.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Download.Location = new System.Drawing.Point(784, 351);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(81, 80);
            this.btn_Download.TabIndex = 51;
            this.btn_Download.UseVisualStyleBackColor = true;
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(680, 182);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(98, 90);
            this.pictureBox1.TabIndex = 77;
            this.pictureBox1.TabStop = false;
            // 
            // btn_SearchII
            // 
            this.btn_SearchII.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_SearchII.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SearchII.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_SearchII.Location = new System.Drawing.Point(341, 67);
            this.btn_SearchII.Name = "btn_SearchII";
            this.btn_SearchII.Size = new System.Drawing.Size(169, 35);
            this.btn_SearchII.TabIndex = 78;
            this.btn_SearchII.Text = "根据当前结果再次查询";
            this.btn_SearchII.UseVisualStyleBackColor = true;
            this.btn_SearchII.Click += new System.EventHandler(this.btn_SearchII_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(780, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 139);
            this.label1.TabIndex = 79;
            this.label1.Text = "此处仅下载原始rfa文件及属性表格，不再Revit内进行其他操作。";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(871, 435);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 126);
            this.label2.TabIndex = 81;
            this.label2.Text = "此处仅将实体模型族载入当前打开的Revit工程中。";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(954, 435);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 139);
            this.label3.TabIndex = 82;
            this.label3.Text = "此处将族模型与属性表中参数条目整合并，载入当前打开的Revit工程。";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(1041, 435);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 163);
            this.label4.TabIndex = 83;
            this.label4.Text = "此处将族模型有属性表中参数条目与值合并入族，并载入当前打开的Revit工程。";
            // 
            // RevitFamilyManagerFormDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 607);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_SearchII);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_loadRfa0);
            this.Controls.Add(this.btn_justLoadRfa);
            this.Controls.Add(this.btn_loadRfa);
            this.Controls.Add(this.btn_Download);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.btn_DeleteFromListBox);
            this.Controls.Add(this.btn_AddToListBox);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.txt_Search);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tvw_Family);
            this.Name = "RevitFamilyManagerFormDB";
            this.Text = "SEPD族管理平台 by上海电力设计院；科技信息部；周昊天，吕嘉文，徐经纬";
            this.Load += new System.EventHandler(this.RevitFamilyManagerFormDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvw_Family;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_DeleteFromListBox;
        private System.Windows.Forms.Button btn_AddToListBox;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button btn_loadRfa0;
        private System.Windows.Forms.Button btn_justLoadRfa;
        private System.Windows.Forms.Button btn_loadRfa;
        public System.Windows.Forms.Button btn_Download;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_SearchII;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}