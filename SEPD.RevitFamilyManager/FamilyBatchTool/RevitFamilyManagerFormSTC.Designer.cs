namespace SEPD.RevitFamilyManager
{
    partial class RevitFamilyManagerFormSTC
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
            this.dgv_FamilyAndXls = new System.Windows.Forms.DataGridView();
            this.btn_flash = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_condition = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_all = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_all = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_paracond = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FamilyAndXls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_FamilyAndXls
            // 
            this.dgv_FamilyAndXls.AllowUserToOrderColumns = true;
            this.dgv_FamilyAndXls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FamilyAndXls.Location = new System.Drawing.Point(56, 295);
            this.dgv_FamilyAndXls.Name = "dgv_FamilyAndXls";
            this.dgv_FamilyAndXls.RowTemplate.Height = 23;
            this.dgv_FamilyAndXls.Size = new System.Drawing.Size(451, 121);
            this.dgv_FamilyAndXls.TabIndex = 5;
            // 
            // btn_flash
            // 
            this.btn_flash.Location = new System.Drawing.Point(56, 87);
            this.btn_flash.Name = "btn_flash";
            this.btn_flash.Size = new System.Drawing.Size(118, 43);
            this.btn_flash.TabIndex = 6;
            this.btn_flash.Text = "数据刷新";
            this.btn_flash.UseVisualStyleBackColor = true;
            this.btn_flash.Click += new System.EventHandler(this.btn_flash_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(56, 146);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(451, 126);
            this.dataGridView1.TabIndex = 7;
            // 
            // txt_condition
            // 
            this.txt_condition.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_condition.Location = new System.Drawing.Point(135, 11);
            this.txt_condition.Name = "txt_condition";
            this.txt_condition.Size = new System.Drawing.Size(407, 31);
            this.txt_condition.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "条件筛选框：";
            // 
            // lbl_all
            // 
            this.lbl_all.AutoSize = true;
            this.lbl_all.Location = new System.Drawing.Point(133, 580);
            this.lbl_all.Name = "lbl_all";
            this.lbl_all.Size = new System.Drawing.Size(41, 12);
            this.lbl_all.TabIndex = 10;
            this.lbl_all.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 580);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "计算结果：";
            // 
            // txt_all
            // 
            this.txt_all.Location = new System.Drawing.Point(203, 577);
            this.txt_all.Name = "txt_all";
            this.txt_all.Size = new System.Drawing.Size(100, 21);
            this.txt_all.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "参数筛选框：";
            // 
            // txt_paracond
            // 
            this.txt_paracond.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_paracond.Location = new System.Drawing.Point(135, 48);
            this.txt_paracond.Name = "txt_paracond";
            this.txt_paracond.Size = new System.Drawing.Size(407, 31);
            this.txt_paracond.TabIndex = 13;
            this.txt_paracond.Text = "长度";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(56, 437);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(451, 121);
            this.dataGridView2.TabIndex = 15;
            // 
            // RevitFamilyManagerFormSTC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 622);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_paracond);
            this.Controls.Add(this.txt_all);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_all);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_condition);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_flash);
            this.Controls.Add(this.dgv_FamilyAndXls);
            this.Name = "RevitFamilyManagerFormSTC";
            this.Text = "RevitFamilyManagerFormSTC";
            this.Load += new System.EventHandler(this.RevitFamilyManagerFormSTC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FamilyAndXls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_FamilyAndXls;
        private System.Windows.Forms.Button btn_flash;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_condition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_all;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_all;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_paracond;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}