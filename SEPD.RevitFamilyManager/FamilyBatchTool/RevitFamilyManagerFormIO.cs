using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;

using NPOI;
using NPOI.XSSF;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

using System.IO;

//using WinForm = System.Windows.Forms;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Data.SqlClient;
using System.Net;
using SEPD.CommunicationModule;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using System.Runtime.InteropServices;
using System.Threading;

namespace SEPD.RevitFamilyManager
{
    public partial class RevitFamilyManagerFormIO : System.Windows.Forms.Form
    {
        public RevitFamilyManagerFormIO()
        {
            InitializeComponent();
        }
        

        public List<string> fPathList = new List<string>();
        public List<string> fNameList = new List<string>();
        public DataTable familyInfo = new DataTable("familyInformation");
        ExcelHelper ExcelHelper = new ExcelHelper();

        ExecuteEventIO ExecuteEventIO = null;
        ExternalEvent ExternalEventIO = null;
        string xPath = null;

        private void RevitFamilyManagerFormGP_Load(object sender, EventArgs e)
        {
            ExecuteEventIO = new ExecuteEventIO();
            ExternalEventIO = ExternalEvent.Create(ExecuteEventIO);
             
            familyInfo.Columns.Add("fPath", typeof(String));
            familyInfo.Columns.Add("xPath", typeof(String));

        }

        /// <summary>
        /// 批量选择族文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SelectFamily_Click(object sender, EventArgs e)
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
            catch (Exception eee )
            { MessageBox.Show(eee.ToString()); }
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            ExecuteEventIO.familyFilePaths = fPathList;
            ExternalEventIO.Raise();
        }

        private void btn_flash_Click(object sender, EventArgs e)
        {
            string xlsFilePathLog = @"C:\ProgramData\Autodesk\Revit\Addins\2016\xlsFilePath.txt";

            if (File.Exists(xlsFilePathLog))
            {
                StreamReader sr = new StreamReader(xlsFilePathLog, Encoding.GetEncoding("UTF-8"));

                string[] line = File.ReadAllLines(xlsFilePathLog);
                for (int i = 0; i < line.Length; i++)
                {
                    familyInfo.Rows.Add();
                    familyInfo.Rows[i]["fPath"] = fPathList[i];
                    familyInfo.Rows[i]["xPath"] = line[i];
                }
                //MessageBox.Show(familyInfo.Rows.Count.ToString());
                sr.Close();
            }
            else
            {
                MessageBox.Show("no path");
            }

            //对dgv窗口刷新显示 族名族路径和表名表路径
            this.dgv_FamilyAndXls.Rows.Clear();
            for (int i = 0; i < familyInfo.Rows.Count; i++)
            {
                int index = this.dgv_FamilyAndXls.Rows.Add();
                this.dgv_FamilyAndXls.Rows[index].Cells["dgv_fName"].Value = Path.GetFileName(familyInfo.Rows[i]["fPath"].ToString());
                this.dgv_FamilyAndXls.Rows[index].Cells["dgv_fPath"].Value = familyInfo.Rows[i]["fPath"].ToString();
                this.dgv_FamilyAndXls.Rows[index].Cells["dgv_xName"].Value = Path.GetFileName(familyInfo.Rows[i]["xPath"].ToString());
                this.dgv_FamilyAndXls.Rows[index].Cells["dgv_xPath"].Value = familyInfo.Rows[i]["xPath"].ToString();
            }
            //对lvw窗口刷新显示 族名族路径和表名表路径
            lvw_FamilyAndXls.FullRowSelect = true;
            lvw_FamilyAndXls.Items.Clear();
            for (int i = 0; i < familyInfo.Rows.Count; i++)
            {
                var item = new ListViewItem();
                item.SubItems.Add(Path.GetFileName(familyInfo.Rows[i]["fPath"].ToString()));
                item.SubItems.Add(familyInfo.Rows[i]["fPath"].ToString());
                item.SubItems.Add(Path.GetFileName(familyInfo.Rows[i]["xPath"].ToString()));
                item.SubItems.Add(familyInfo.Rows[i]["xPath"].ToString());

                item.SubItems[0].Text= Path.GetFileName(familyInfo.Rows[i]["fPath"].ToString());
                item.SubItems[1].Text = familyInfo.Rows[i]["fPath"].ToString();
                item.SubItems[2].Text = Path.GetFileName(familyInfo.Rows[i]["xPath"].ToString());
                item.SubItems[3].Text = familyInfo.Rows[i]["xPath"].ToString();
                lvw_FamilyAndXls.Items.Add(item);
            }

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            lvw_FamilyAndXls.Items.Clear();
            dgv_FamilyAndXls.Rows.Clear();
            fPathList.Clear();
        }

        private void lvw_FamilyAndXls_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.dgv_parameter.Rows.Clear();
            
            DataTable xDataTable = null;
            if (lvw_FamilyAndXls.SelectedItems.Count == 0) { return; }
            else
            {
                //MessageBox.Show(this.lvw_FamilyAndXls.SelectedItems[0].SubItems[3].Text);
                xPath = this.lvw_FamilyAndXls.SelectedItems[0].SubItems[3].Text;
            }
            xDataTable = ExcelHelper.Reading_Excel_Information(xPath);

            for (int i = 0; i < xDataTable.Rows.Count; i++)
            {
                //if (xDataTable.Rows[i]["paraname"].ToString().Length < 1)
                //{ break; }
                //xDataTable;
                dgv_parameter.Rows.Add();
                this.dgv_parameter.Rows[i].Cells["dgv_pName"].Value = xDataTable.Rows[i]["paraname"].ToString();
                this.dgv_parameter.Rows[i].Cells["dgv_pValue"].Value = xDataTable.Rows[i]["paravalue"].ToString();
                this.dgv_parameter.Rows[i].Cells["dgv_pIns"].Value = xDataTable.Rows[i]["paratag"].ToString();
                this.dgv_parameter.Rows[i].Cells["dgv_pGroup"].Value = xDataTable.Rows[i]["paragroup"].ToString();
                this.dgv_parameter.Rows[i].Cells["dgv_pType"].Value = xDataTable.Rows[i]["paratype"].ToString();
            }

        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            //创建datatable
            DataTable dta = new DataTable("Table_Excel");
            //创建带列名的列
            dta.Columns.Add("paraname", typeof(String));
            dta.Columns.Add("paravalue", typeof(String));
            dta.Columns.Add("paratag", typeof(String));
            dta.Columns.Add("paragroup", typeof(String));
            dta.Columns.Add("paratype", typeof(String));

            for (int i = 0; i < dgv_parameter.Rows.Count; i++)
            {
                if (dgv_parameter.Rows[i].Cells[0].Value != null)
                {
                    dta.Rows.Add();

                    dta.Rows[i][0] = dgv_parameter.Rows[i].Cells[0].Value.ToString();
                    if (dgv_parameter.Rows[i].Cells[1].Value == null)
                    {
                        dta.Rows[i][0] = "NA";
                    }
                    else
                    {
                        dta.Rows[i][1] = dgv_parameter.Rows[i].Cells["dgv_pValue"].Value.ToString();
                    }
                    if (dgv_parameter.Rows[i].Cells[2].Value == null)
                    {
                        dta.Rows[i][2] = "NO";
                    }
                    else
                    {
                        dta.Rows[i][2] = dgv_parameter.Rows[i].Cells["dgv_pIns"].Value.ToString();
                    }
                    if (dgv_parameter.Rows[i].Cells[2].Value == null)
                    {
                        dta.Rows[i][3] = "PG_ELECTRICAL";
                    }
                    else
                    {
                        dta.Rows[i][3] = dgv_parameter.Rows[i].Cells["dgv_pGroup"].Value.ToString();
                    }
                    if (dgv_parameter.Rows[i].Cells[2].Value == null)
                    {
                        dta.Rows[i][4] = "Text";
                    }
                    else
                    {
                        dta.Rows[i][4] = dgv_parameter.Rows[i].Cells["dgv_pType"].Value.ToString();
                    }
                }
                else if (dgv_parameter.Rows[i].Cells[0].Value != null)
                {
                    continue;
                }
           
                
                string saveAsPath = ExcelHelper.Creating_Excel_Information(dta,xPath.Replace(".xls","")+".rfa");
                 
                //File.Copy(saveAsPath, txt_SelectedXlsPath, true);
                //File.Delete(saveAsPath);
            }
        }
    }
}
