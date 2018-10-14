using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEPD.RevitFamilyManager
{
    public partial class RevitFamilyManagerFormSTF : System.Windows.Forms.Form
    {
        public RevitFamilyManagerFormSTF()
        {
            InitializeComponent();
        }

        public List<string> fPathList = new List<string>();
        public List<string> fNameList = new List<string>();
        public DataTable familyInfo = new DataTable("familyInformation");

        private void btn_selectRfa_Click(object sender, EventArgs e)
        {
            string openFileDialogLastTimeTextfilePath = @"C:\ProgramData\Autodesk\Revit\Addins\2016\openFamilyPathLastTime.txt";
            string lastTimeOpenLocation = @"c:\";
            try
            {
                if (File.Exists(openFileDialogLastTimeTextfilePath))
                {
                    StreamReader sr = new StreamReader(openFileDialogLastTimeTextfilePath, Encoding.Default);
                    string line = sr.ReadLine();
                    lastTimeOpenLocation = line;
                    sr.Close();
                }
                else
                {
                    FileStream fs = new FileStream(openFileDialogLastTimeTextfilePath, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(lastTimeOpenLocation);
                    sw.Close();
                    fs.Close();
                }

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.InitialDirectory = lastTimeOpenLocation;
                openFileDialog.Filter = "rfa文件|*.rfa|所有文件|*.*";
                openFileDialog.RestoreDirectory = true;


                if (openFileDialog.ShowDialog() == DialogResult.OK /*&& openFamilyName != null*/)
                {
                    for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                    {
                        string fPath = openFileDialog.FileNames[i].ToString(); ;//获取用户选择的文件路径
                        string fName = fPath.Substring(fPath.LastIndexOf("\\") + 1);//获取用户选择的文件名称

                        fPathList.Add(fPath);
                        fNameList.Add(fName.Replace(".rfa", ""));
                    }
                }

                //dgv显示族名和族路径
                this.dgv_FamilyAndXls.Rows.Clear();
                for (int i = 0; i < fPathList.Count(); i++)
                {
                    int index = this.dgv_FamilyAndXls.Rows.Add();
                    this.dgv_FamilyAndXls.Rows[index].Cells["dgv_fName"].Value = fNameList[i];
                    this.dgv_FamilyAndXls.Rows[index].Cells["dgv_fPath"].Value = fPathList[i];
                    //this.dgv_FamilyAndXls.Rows[index].Cells["dgv_xName"].Value = dgv_xName;
                    //this.dgv_FamilyAndXls.Rows[index].Cells["dgv_xPath"].Value = dgv_xPath;
                }

                //lvw显示族名和族路径
                this.lvw_FamilyAndXls.Items.Clear();
                for (int i = 0; i < fPathList.Count(); i++)
                {
                    var item = new ListViewItem();
                    item.SubItems.Add(fNameList[i]);
                    //item.SubItems.Add(fPathList[i]);

                    item.SubItems[0].Text = fNameList[i];
                    item.SubItems[1].Text = fPathList[i];

                    lvw_FamilyAndXls.Items.Add(item);
                }
            }
            catch (Exception eee)
            { MessageBox.Show(eee.ToString()); }

        }

        private void btn_changeCat_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
