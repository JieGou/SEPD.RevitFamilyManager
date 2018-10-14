namespace SEPD.RevitFamilyManager
{
    partial class configForm
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
            this.txt_cfgsqlip = new System.Windows.Forms.TextBox();
            this.txt_cfgftppw = new System.Windows.Forms.TextBox();
            this.txt_cfgftpuser = new System.Windows.Forms.TextBox();
            this.txt_cfgsqldb = new System.Windows.Forms.TextBox();
            this.txt_cfgsqlpw = new System.Windows.Forms.TextBox();
            this.txt_cfgsqluser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_cfgtempfolder = new System.Windows.Forms.TextBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_selectFolder = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_cfgftpip = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_cfgsqlip
            // 
            this.txt_cfgsqlip.Location = new System.Drawing.Point(176, 83);
            this.txt_cfgsqlip.Name = "txt_cfgsqlip";
            this.txt_cfgsqlip.Size = new System.Drawing.Size(367, 21);
            this.txt_cfgsqlip.TabIndex = 0;
            // 
            // txt_cfgftppw
            // 
            this.txt_cfgftppw.Location = new System.Drawing.Point(176, 321);
            this.txt_cfgftppw.Name = "txt_cfgftppw";
            this.txt_cfgftppw.PasswordChar = '*';
            this.txt_cfgftppw.Size = new System.Drawing.Size(367, 21);
            this.txt_cfgftppw.TabIndex = 1;
            // 
            // txt_cfgftpuser
            // 
            this.txt_cfgftpuser.Location = new System.Drawing.Point(176, 294);
            this.txt_cfgftpuser.Name = "txt_cfgftpuser";
            this.txt_cfgftpuser.Size = new System.Drawing.Size(367, 21);
            this.txt_cfgftpuser.TabIndex = 2;
            // 
            // txt_cfgsqldb
            // 
            this.txt_cfgsqldb.Location = new System.Drawing.Point(176, 177);
            this.txt_cfgsqldb.Name = "txt_cfgsqldb";
            this.txt_cfgsqldb.Size = new System.Drawing.Size(367, 21);
            this.txt_cfgsqldb.TabIndex = 3;
            // 
            // txt_cfgsqlpw
            // 
            this.txt_cfgsqlpw.Location = new System.Drawing.Point(176, 150);
            this.txt_cfgsqlpw.Name = "txt_cfgsqlpw";
            this.txt_cfgsqlpw.PasswordChar = '*';
            this.txt_cfgsqlpw.Size = new System.Drawing.Size(367, 21);
            this.txt_cfgsqlpw.TabIndex = 4;
            // 
            // txt_cfgsqluser
            // 
            this.txt_cfgsqluser.Location = new System.Drawing.Point(176, 123);
            this.txt_cfgsqluser.Name = "txt_cfgsqluser";
            this.txt_cfgsqluser.Size = new System.Drawing.Size(367, 21);
            this.txt_cfgsqluser.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "SQL服务器IP及端口：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "SQL用户：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "SQL密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "SQLDB名称：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(90, 297);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "FTP用户：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(90, 324);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "FTP密码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(87, 401);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "设置临时文件夹：";
            // 
            // txt_cfgtempfolder
            // 
            this.txt_cfgtempfolder.Location = new System.Drawing.Point(194, 398);
            this.txt_cfgtempfolder.Name = "txt_cfgtempfolder";
            this.txt_cfgtempfolder.Size = new System.Drawing.Size(277, 21);
            this.txt_cfgtempfolder.TabIndex = 13;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(92, 461);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(122, 67);
            this.btn_OK.TabIndex = 14;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(349, 461);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(122, 67);
            this.btn_Cancel.TabIndex = 15;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_selectFolder
            // 
            this.btn_selectFolder.Location = new System.Drawing.Point(495, 396);
            this.btn_selectFolder.Name = "btn_selectFolder";
            this.btn_selectFolder.Size = new System.Drawing.Size(75, 23);
            this.btn_selectFolder.TabIndex = 16;
            this.btn_selectFolder.Text = "...";
            this.btn_selectFolder.UseVisualStyleBackColor = true;
            this.btn_selectFolder.Click += new System.EventHandler(this.btn_selectFolder_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(92, 245);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "FTP服务器IP：";
            // 
            // txt_cfgftpip
            // 
            this.txt_cfgftpip.Location = new System.Drawing.Point(176, 242);
            this.txt_cfgftpip.Name = "txt_cfgftpip";
            this.txt_cfgftpip.Size = new System.Drawing.Size(367, 21);
            this.txt_cfgftpip.TabIndex = 18;
            // 
            // configForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 572);
            this.Controls.Add(this.txt_cfgftpip);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn_selectFolder);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.txt_cfgtempfolder);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_cfgsqluser);
            this.Controls.Add(this.txt_cfgsqlpw);
            this.Controls.Add(this.txt_cfgsqldb);
            this.Controls.Add(this.txt_cfgftpuser);
            this.Controls.Add(this.txt_cfgftppw);
            this.Controls.Add(this.txt_cfgsqlip);
            this.Name = "configForm";
            this.Text = "configForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_cfgsqlip;
        private System.Windows.Forms.TextBox txt_cfgftppw;
        private System.Windows.Forms.TextBox txt_cfgftpuser;
        private System.Windows.Forms.TextBox txt_cfgsqldb;
        private System.Windows.Forms.TextBox txt_cfgsqlpw;
        private System.Windows.Forms.TextBox txt_cfgsqluser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_cfgtempfolder;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_selectFolder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_cfgftpip;
    }
}