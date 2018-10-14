using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace SEPD.RevitFamilyManager
{
    public partial class configForm : Form
    {
        private List<string> configList = new List<string>();
        public configForm()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            configList.Add(txt_cfgsqlip.Text);
            configList.Add(txt_cfgsqluser.Text);
            configList.Add(txt_cfgsqlpw.Text);
            configList.Add(txt_cfgftpip.Text);
            configList.Add(txt_cfgftpuser.Text);
            configList.Add(txt_cfgftppw.Text);
            configList.Add(txt_cfgtempfolder.Text);
 
            string defaultConfigFilePath = @"C:\ProgramData\Autodesk\Revit\Addins\2016\SEPD.RevitFamilyManager.config";
            if (!File.Exists(defaultConfigFilePath))
            {  
            }
            else
            {
                FileStream fs = new FileStream(defaultConfigFilePath, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                foreach (string cfgList in configList)
                {
                    sw.Write(cfgList);
                }
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }

            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_selectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择Txt所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                txt_cfgtempfolder.Text = dialog.SelectedPath;
            }
        }
    }
}
