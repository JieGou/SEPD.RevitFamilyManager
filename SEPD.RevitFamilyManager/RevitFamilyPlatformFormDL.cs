
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
using System.Runtime.InteropServices;

//using WinForm = System.Windows.Forms;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Data.SqlClient;
using System.Net;
using SEPD.CommunicationModule;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using SEPD.RevitFamilyManager;

namespace SEPD.RevitFamilyManager
{
    public partial class RevitFamilyManagerFormDL : System.Windows.Forms.Form
    {
        LogMonitor LogMonitor = new LogMonitor();

        ExecuteEvent Exc = null;
        ExternalEvent eventHandler = null;

        #region 全窗口拖动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void RevitFamilyManagerFormDL_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x0112, 0xF012, 0);
        }
        #endregion

        string serverIP = "10.193.217.38";
        string ftpConnectionLocation = "ftp://10.193.217.38/";
        string sqlConnectionLocation = "server=10.193.217.38,1433;uid=sa;Password=SanWei2209;database=DB_Family_Library";
        string ftpUserName = "Administrator";
        string ftpPassword = "LvJW2209";
        string sqlUserName = "sa";
        string sqlPassword = "SanWei2209";
        string sqlDatabase = "DB_Family_Library";
        string xlsLocation = @"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\parameter_excel\";
        string downloadLocation = @"C:\RFA_TMP\downloadLocation\";

        //配置文件检测
        public void configFile()
        {
            List<string> configList = new List<string>();
            // 检测本地config文件是否存在
            string defaultConfigFilePath = @"C:\ProgramData\Autodesk\Revit\Addins\2016\SEPD.RevitFamilyManager.config";
            if (!File.Exists(defaultConfigFilePath))
            {
            }
            else
            {
                //dsConnectionInfo = XmlHelper.xmlFileReader(defaultConfigFilePath);
                //预留读取配置文件代码
                StreamReader sr = new StreamReader(defaultConfigFilePath, Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    configList.Add(line.ToString());
                }

                serverIP = configList[0];
                sqlUserName = configList[1];
                sqlPassword = configList[2];
                sqlDatabase = configList[3];
                sqlConnectionLocation = "server=" + serverIP + ";uid=" + sqlUserName + ";Password=" + sqlPassword + ";database=" + sqlDatabase;

                ftpConnectionLocation = configList[4];
                ftpUserName = configList[5];
                ftpPassword = configList[6];

                xlsLocation = configList[7];

            }
        }


        //控件属性


        //所有类型
        public List<string> allFamilyTypes = new List<string>();

        public string localParaLoc = null;
        public List<string> localParaLocs = new List<string>();

        public string localPicPath = null;

        public string openFamilyName = null;
        public string openConfigParaName = null;

        public string familyPath = null;
        public string familyName = null;
        public string familyHash = null;
        public string familyType = null;
        public string familyPro = null;

        public string ParaPath = null;
        public string ParaName = null;

        //已确定参数值xls的位置及名称
        public string configParaPath = null;
        public string configParaName = null;

        public string saveAsParaPath = null;
        public string downloadFileName = null;
        public string downloadXlsName = null;
        public string downloadFilePath = null;
        public string downloadXlsPath = null;

        public string LoadTag = null;

        public List<string> downloadFamilyLocs = new List<string>();
        public List<string> downloadXlsLocs = new List<string>();

        public List<string> downloadFamilyLocsA = new List<string>();
        public List<string> downloadXlsLocsA = new List<string>();

        public DataTable dgvDataTable = null;

        public RevitFamilyManagerFormDL()
        {
            InitializeComponent();

            //电压等级初始化
            //cmb_ValLevel.Items.Add("1000kV");
            //cmb_ValLevel.Items.Add("500kV");
            //cmb_ValLevel.Items.Add("220kV");
            //cmb_ValLevel.Items.Add("110kV");
            //cmb_ValLevel.Items.Add("66kV");
            //cmb_ValLevel.Items.Add("35kV");
            //cmb_ValLevel.Items.Add("10kV");
            //cmb_ValLevel.Items.Add("0.4kV");
            //cmb_ValLevel.Items.Add("其他");

            //显示树状图
            this.SetRoot();
            this.SetRoot2();
            List<string> xlsFileNames = GetDirFile.FTPGetFile(ftpConnectionLocation + "DB_Family_Library_FTP/precast/", ftpUserName, ftpPassword);
            //foreach (var xls in xlsFileNames)
            //{ cmb_Precast.Items.Add(xls); }

            //显示参数表列名
            #region listView 列
            //lvw_ParaList.Columns.Add("参数条目", 100);
            //lvw_ParaList.Columns.Add("参数值", 100);
            //lvw_ParaList.Columns.Add("是否实例", 100);
            //lvw_ParaList.View = System.Windows.Forms.View.Details;
            //lvw_ParaListSelectedFamily.View = System.Windows.Forms.View.Details;
            //var item = new ListViewItem();
            ////item.SubItems.Add();
            //lvw_ParaList.Items.Add(item);
            #endregion

            if (allFamilyTypes == null)
            {
                MessageBox.Show("!!!!!!!!!!!!!");
            }
            //else
            //{
            //    foreach (var aft in allFamilyTypes)
            //    {
            //        cmb_allFamilyTypes.Items.Add(aft);
            //    }
            //}

        }

        private void SetRoot()
        {
            //FTPConnection FTPConnection = new FTPConnection();
            List<string> getFtpFolder = new List<string>();
            List<string> getFtpFile = new List<string>();

            string rootPath = ftpConnectionLocation + "DB_Family_Library_FTP/";

            getFtpFolder = GetDirFile.FTPGetDirctory(rootPath, ftpUserName, ftpPassword);
            getFtpFile = GetDirFile.FTPGetFile(rootPath, ftpUserName, ftpPassword);
            //foreach (var jdjd in getFtpFolder)
            //{ 
            //    Console.WriteLine(jdjd);
            //    MessageBox.Show(jdjd);
            //}
            List<TreeNode> tnodes = new List<TreeNode>();
            List<string> pathsss = new List<string>();

            getFtpFolder.Remove("paracast");
            getFtpFolder.Remove("pics");
            getFtpFolder.Remove("precast");

            foreach (string ProDirName in getFtpFolder)
            {
                TreeNode tnode = new TreeNode(ProDirName);
                tnode.Name = ProDirName;
                tnode.Tag = tnode.Name;
                tnodes.Add(tnode);
                pathsss.Add(rootPath + ProDirName + "/");
                this.tvw_Family.Nodes.Add(tnode);
            }
            SetBrench(pathsss, tnodes);
        }

        private void SetBrench(List<string> newPaths, List<TreeNode> xnode)
        {
            List<TreeNode> tnodes = new List<TreeNode>();
            List<string> pathsss = new List<string>();

            for (int i = 0; i < newPaths.Count() || i < xnode.Count(); i++)
            //for (int i = 0; i <3; i++)
            {
                List<string> getNewFolder = new List<string>();
                List<string> getNewFile = new List<string>();
                getNewFolder = GetDirFile.FTPGetDirctory(newPaths[i], ftpUserName, ftpPassword);
                getNewFile = GetDirFile.FTPGetFile(newPaths[i], ftpUserName, ftpPassword);
                if (getNewFolder != null)
                {
                    foreach (string gnf in getNewFolder)
                    {
                        allFamilyTypes.Add(gnf);
                    }
                }

                if (getNewFolder == null)
                {

                    break;
                }
                else if (getNewFolder != null)
                {
                    getNewFolder.Remove("paracast");
                    getNewFolder.Remove("pics");
                    getNewFolder.Remove("precast");
                    foreach (string TypeDirName in getNewFolder)
                    {
                        TreeNode tnode = new TreeNode(TypeDirName);
                        tnode.Name = TypeDirName;
                        tnode.Tag = tnode.Name;
                        pathsss.Add(newPaths[i] + TypeDirName + "/");
                        tnodes.Add(tnode);

                        xnode[i].Nodes.Add(tnode);
                        //allFamilyTypes.Add();
                    }
                    SetBrench(pathsss, tnodes);
                }

            }
        }

        

        private void tvw_Family_MouseCaptureChanged(object sender, EventArgs e)
        {
            TreeNode tn = new TreeNode();

            try
            {
                foreach (TreeNode x in this.tvw_Family.SelectedNode.Parent.Nodes)
                {
                    if (x.IsSelected == true)
                    {
                        tn = x;
                    }
                }
            }
            catch (Exception exx)
            {
                //MessageBox.Show(exx.Message);
            }

            #region listbox显示操作
            //将下属所有文件显示在listbox
            if (tn != null && tn.Parent != null)
            {
                //this.lsb_FamilyList.Items.Clear();
                //MessageBox.Show(tn.Parent.Name);
                //MessageBox.Show(tn.Name);
                //MessageBox.Show(tn.Name);
                familyPro = tn.Parent.Name;
                familyType = tn.Name;
                List<string> fileList = GetDirFile.FTPGetFile(ftpConnectionLocation + "DB_Family_Library_FTP/" + tn.Parent.Name + "/" + tn.Name + "/", ftpUserName, ftpPassword);
                //MessageBox.Show()
                //DirectoryInfo DI = new DirectoryInfo(@"ftp://10.193.217.38/DB_Family_Library_FTP/" + tn.Parent.Name + "/" + tn.Name + "/");
                //MessageBox.Show(@"ftp://10.193.217.38/DB_Family_Library_FTP/" + tn.Parent.Name + "/" + tn.Name + "/");
                try
                {
                    //FileInfo[] files = DI.GetFiles();
                    if (fileList != null)
                    {
                        foreach (var fl in fileList)
                        {
                            //this.lsb_FamilyList.Items.Add(fl);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            #endregion

            #region datagridview显示操作

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            //sql查询
            //sql链接初始化
            SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
            //SqlCompose.Con.Close();
            this.dataGridView1.Rows.Clear();

            string readPath = "SELECT * FROM table_p_Family WHERE f_Path LIKE '%" + @"/DB_Family_Library_FTP/" + familyPro + "/" + familyType + "/%'";
            DataSet ds = SqlCompose.ExecuteSqlQuery(readPath);
            DataTable dt = new DataTable();
            //MessageBox.Show(ds.Tables[1].ToString());
            dt = ds.Tables[0];
            dgvDataTable = dt;
            try
            {
                List<string> picPathList = new List<string>();
                var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);

                //MessageBox.Show(dt.Rows.Count.ToString());

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    int index = this.dataGridView1.Rows.Add();
                    //var dd = dt.Rows[i]["f_Path"];
                    //MessageBox.Show(dd.ToString());
                    //this.dataGridView1.Rows[i].Cells[0].Value = ;
                    this.dataGridView1.Rows[index].Cells["dgv_FamilyName"].Value = dt.Rows[i]["f_Name"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_Version"].Value = "1.0";
                    this.dataGridView1.Rows[index].Cells["dgv_Date"].Value = dt.Rows[i]["f_UploadDate"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_Type"].Value = familyType;
                    this.dataGridView1.Rows[index].Cells["dgv_Hash"].Value = dt.Rows[i]["f_Hash"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_FamilyLoc"].Value = dt.Rows[i]["f_Path"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_ParaLoc"].Value = dt.Rows[i]["f_ConfigParaLocation"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_ValLevel"].Value = dt.Rows[i]["f_ValLevel"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_Manufacturer"].Value = dt.Rows[i]["f_manufacturer"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_Source"].Value = dt.Rows[i]["f_Source"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_DeviceType"].Value = dt.Rows[i]["f_DeviceType"].ToString();

                    try
                    {
                        if (dt.Rows[i]["f_Pic"].ToString() != null)
                        {
                            ftp.DownloadFile(dt.Rows[i]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                            string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(dt.Rows[i]["f_Pic"].ToString());
                            this.dataGridView1.Rows[index].Cells["dgv_Pics"].Value = Image.FromFile(iconPath);
                        }
                        else
                        { continue; }
                    }
                    catch (Exception erew)
                    { }
                }



            }
            catch (Exception eex)
            {
                MessageBox.Show(eex.ToString());
            }

            #endregion

        }

        private void SetRoot2()
        {
            //FTPConnection FTPConnection = new FTPConnection();
            List<string> getFtpFolder = new List<string>();
            List<string> getFtpFile = new List<string>();

            string rootPath = ftpConnectionLocation + "DB_Family_Library_FTP/";

            getFtpFolder = GetDirFile.FTPGetDirctory(rootPath, ftpUserName, ftpPassword);
            getFtpFile = GetDirFile.FTPGetFile(rootPath, ftpUserName, ftpPassword);

            List<TreeNode> tnodes = new List<TreeNode>();   
            List<string> pathsss = new List<string>();

            //foreach (string ProDirName in getFtpFolder)
            //{
            //    if (ProDirName == "paracast" || ProDirName == "pics" || ProDirName == "precast")
            //    {
            //        getFtpFolder.Remove("paracast");
            //        getFtpFolder.Remove("pics");
            //        getFtpFolder.Remove("precast");
            //    }
            //}
            getFtpFolder.Remove("paracast");
            getFtpFolder.Remove("pics");
            getFtpFolder.Remove("precast"); 


            foreach (string ProDirName in getFtpFolder)
            {
                TreeNode tnode = new TreeNode(ProDirName);
                tnode.Name = ProDirName;
                tnode.Tag = tnode.Name;
                tnodes.Add(tnode);
                pathsss.Add(rootPath + ProDirName + "/");
                //this.tvw_Family2.Nodes.Add(tnode);
                //allFamilyTypes.Add(tnode.Name.ToString());
            }
            SetBrench2(pathsss, tnodes);
            //foreach (var gnf in getFtpFile)
            //{
            //    allFamilyTypes.Add(gnf);
            //}

        }

        private void SetBrench2(List<string> newPaths, List<TreeNode> xnode)
        {
            List<TreeNode> tnodes = new List<TreeNode>();
            List<string> pathsss = new List<string>();

            for (int i = 0; i < newPaths.Count() || i < xnode.Count(); i++)
            //for (int i = 0; i <3; i++)
            {
                List<string> getNewFolder = new List<string>();
                List<string> getNewFile = new List<string>();
                getNewFolder = GetDirFile.FTPGetDirctory(newPaths[i], ftpUserName, ftpPassword);
                getNewFile = GetDirFile.FTPGetFile(newPaths[i], ftpUserName, ftpPassword);

               

                if (getNewFolder == null)
                {
                    break;
                }
                else if (getNewFolder != null)
                {
                    //foreach (string ProDirName in getNewFolder)
                    //{
                    //    if (ProDirName == "paracast" || ProDirName == "pics" || ProDirName == "precast")
                    //    {
                    //        getNewFolder.Remove("paracast");
                    //        getNewFolder.Remove("pics");
                    //        getNewFolder.Remove("precast");
                    //    }
                    //}
                    getNewFolder.Remove("paracast");
                    getNewFolder.Remove("pics");
                    getNewFolder.Remove("precast");

                    foreach (string TypeDirName in getNewFolder)
                    {
                        TreeNode tnode = new TreeNode(TypeDirName);
                        tnode.Name = TypeDirName;
                        tnode.Tag = tnode.Name;
                        pathsss.Add(newPaths[i] + TypeDirName + "/");
                        tnodes.Add(tnode);

                        xnode[i].Nodes.Add(tnode);

                    }
                    SetBrench(pathsss, tnodes);
                }

            }
        }


        private void btn_Search_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            string SearchCondition = txt_Search.Text;
            string[] SearchConditions = null;
            //分割查找条件
            if (SearchCondition.Contains(','))
            {
                SearchConditions = SearchCondition.Split(',');
            }
            else if (SearchCondition.Contains('，'))
            {
                SearchConditions = SearchCondition.Split('，');
            }
            else
            {
                SearchConditions = SearchCondition.Split(' ');
            }

            //MessageBox.Show(SearchConditions[1]);

            try
            {

                for (int i = 0; i < dgvDataTable.Rows.Count; i++)
                {
                    int cont = 0;

                    foreach (string cond in SearchConditions)
                    {
                        if (dgvDataTable.Rows[i]["f_Name"].ToString().Contains(cond) || dgvDataTable.Rows[i]["f_ValLevel"].ToString().Contains(cond) || dgvDataTable.Rows[i]["f_manufacturer"].ToString().Contains(cond) || dgvDataTable.Rows[i]["f_DeviceType"].ToString().Contains(cond) || dgvDataTable.Rows[i]["f_Source"].ToString().Contains(cond) || familyType == cond)
                        { cont++; }
                        else
                        {
                            cont = 0;
                            break;
                        }
                    }

                    if (cont != 0)
                    {
                        int index = this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[index].Cells["dgv_FamilyName"].Value = dgvDataTable.Rows[i]["f_Name"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_Version"].Value = "1.0";
                        this.dataGridView1.Rows[index].Cells["dgv_Date"].Value = dgvDataTable.Rows[i]["f_UploadDate"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_Type"].Value = familyType;
                        this.dataGridView1.Rows[index].Cells["dgv_Hash"].Value = dgvDataTable.Rows[i]["f_Hash"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_FamilyLoc"].Value = dgvDataTable.Rows[i]["f_Path"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_ParaLoc"].Value = dgvDataTable.Rows[i]["f_ConfigParaLocation"].ToString();

                        this.dataGridView1.Rows[index].Cells["dgv_ValLevel"].Value = dgvDataTable.Rows[i]["f_ValLevel"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_Manufacturer"].Value = dgvDataTable.Rows[i]["f_manufacturer"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_Source"].Value = dgvDataTable.Rows[i]["f_Source"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_DeviceType"].Value = dgvDataTable.Rows[i]["f_DeviceType"].ToString();

                        try
                        {
                            if (dgvDataTable.Rows[i]["f_Pic"].ToString() != null)
                            {
                                //ftp.DownloadFile(dgvDataTable.Rows[i]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                                string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(dgvDataTable.Rows[i]["f_Pic"].ToString());
                                this.dataGridView1.Rows[index].Cells["dgv_Pics"].Value = Image.FromFile(iconPath);
                            }
                            else
                            { continue; }
                        }
                        catch (Exception erew)
                        { }

                    }
                    else
                    { continue; }
                }


            }
            catch (Exception dde)
            {
                MessageBox.Show(dde.ToString());
            }
        }

        private void btn_ViewFamilyPara_Click(object sender, EventArgs e)
        {
            var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);
            #region 判断复选
            int SelectedCounter = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    SelectedCounter++;
                }
            }
            if (SelectedCounter > 1)
            {
                MessageBox.Show("目前多选状态，显示列表中第一个勾选的族参数");
            }
            else if (SelectedCounter == 0)
            {
                MessageBox.Show("请在列表中勾选一个族进行参数查看");
            }
            #endregion

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    string ParaLoc = this.dataGridView1.Rows[i].Cells["dgv_ParaLoc"].Value.ToString();
                    ftp.DownloadFile(ParaLoc.Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");

                    string xlsName = Path.GetFileName(ParaLoc);
                    localParaLoc = xlsLocation + @"parameter_perview\" + xlsName;
                    localParaLocs.Add(localParaLoc);
                    ExcelHelper ExcelHelper = new ExcelHelper();
                    DataTable dt = null;
                    if (xlsName.Contains("withoutParameter"))
                    {
                        lvw_ParaListSelectedFamily.Items.Clear();
                        MessageBox.Show("当前参看族无附表!");
                    }
                    else
                    {
                        dt = ExcelHelper.Reading_Excel_Information(localParaLoc);
                        try
                        {
                            lvw_ParaListSelectedFamily.Items.Clear();
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                var item = new ListViewItem();

                                if (dt.Rows[j]["paraname"] == null)
                                { break; }
                                else
                                {
                                    item.SubItems.Add(dt.Rows[j]["paraname"].ToString());
                                    item.SubItems.Add(dt.Rows[j]["paravalue"].ToString());
                                    item.SubItems.Add(dt.Rows[j]["paratag"].ToString());
                                    lvw_ParaListSelectedFamily.Items.Add(item);
                                }

                            }

                            //SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);

                            //string readPath = "SELECT * FROM table_p_Family";
                            //DataSet dss = SqlCompose.ExecuteSqlQuery(readPath);
                            //DataTable dts = new DataTable();
                            //dts = dss.Tables[0];

                            //ftp.DownloadFile(dt.Rows[i]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                            //string iconPath = xlsLocation + @"parameter_perview\"+ Path.GetFileName(dt.Rows[i]["f_Pic"].ToString()); 

                        }
                        catch (Exception dde)
                        {
                            MessageBox.Show(dde.ToString());
                        }
                    }
 
                    break;
                }
 
            }

        }

        private void RevitFamilyPlatformForm_Load(object sender, EventArgs e)
        {
            Exc = new ExecuteEvent();
            eventHandler = ExternalEvent.Create(Exc);
            //ExcPic = new ExecuteEventPic();
            //eventHandlerPic = ExternalEvent.Create(ExcPic);
        }

        private void btn_AddToListBox_Click(object sender, EventArgs e)
        {
            List<string> selectedHash = new List<string>();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {


                    if (lvw_SelectedFamilies.Items.Count == 0)
                    {
                        var item1 = new ListViewItem();

                        item1.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_FamilyName"].Value.ToString());
                        item1.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_FamilyLoc"].Value.ToString());
                        item1.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_ParaLoc"].Value.ToString());

                        item1.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_Date"].Value.ToString());
                        item1.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_ValLevel"].Value.ToString());
                        item1.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_Manufacturer"].Value.ToString());
                        item1.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_DeviceType"].Value.ToString());
                        item1.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_Hash"].Value.ToString());
                        if (this.dataGridView1.Rows[i].Cells["dgv_defaultXls"].Value != null)
                        {
                            item1.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_defaultXls"].Value.ToString());
                        }
                        else
                        {
                            item1.SubItems.Add("00");
                        }
                        

                        lvw_SelectedFamilies.Items.Add(item1);
                        continue;
                    }
                    else
                    {
                        int tagk = 0;
                        for (int j = 0; j < lvw_SelectedFamilies.Items.Count; j++)
                        {

                            if (this.dataGridView1.Rows[i].Cells["dgv_Hash"].Value.ToString() == lvw_SelectedFamilies.Items[j].SubItems[8].Text.ToString())
                            {
                                tagk = 1;
                                break;
                            }

                        }
                        if (tagk == 0)
                        {
                            var item2 = new ListViewItem();

                            item2.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_FamilyName"].Value.ToString());
                            item2.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_FamilyLoc"].Value.ToString());
                            item2.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_ParaLoc"].Value.ToString());

                            item2.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_Date"].Value.ToString());
                            item2.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_ValLevel"].Value.ToString());
                            item2.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_Manufacturer"].Value.ToString());
                            item2.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_DeviceType"].Value.ToString());
                            item2.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_Hash"].Value.ToString());
                            if (this.dataGridView1.Rows[i].Cells["dgv_defaultXls"].Value != null)
                            {
                                item2.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_defaultXls"].Value.ToString());
                            }
                            else
                            {
                                item2.SubItems.Add("00");
                            }

                            lvw_SelectedFamilies.Items.Add(item2);
                        }
                    }

                }
            }
        }

        private void btn_DeleteFromListBox_Click(object sender, EventArgs e)
        {
            int Index = 0;
            if (this.lvw_SelectedFamilies.SelectedItems.Count > 0)//判断listview有被选中项  
            {
                Index = this.lvw_SelectedFamilies.SelectedItems[0].Index;//取当前选中项的index,SelectedItems[0]这必须为0         
                lvw_SelectedFamilies.Items[Index].Remove();
            }
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            LogMonitor.logMonitorUpdate(sqlConnectionLocation, "族库平台：仅下载" + lvw_SelectedFamilies.Items.Count.ToString()+"个");

            downloadFamilyLocs.Clear();
            downloadXlsLocs.Clear();

            string savePath = null;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择文件路径";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string SelectedPath = folderBrowserDialog.SelectedPath;
                savePath = SelectedPath;
 
                try
                {

                    //foreach (var sf in lvw_SelectedFamilies.Items )
                    for (int i = 0; i < lvw_SelectedFamilies.Items.Count; i++)
                    {
                        string downloadFamilyLoc = lvw_SelectedFamilies.Items[i].SubItems[2].Text;
                        string downloadXlsLoc = null;
 
                        if (lvw_SelectedFamilies.Items[i].SubItems[9].Text != "00")
                        {
                            downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[9].Text;
                        }
                        else
                        {
                            downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[3].Text;
                        }

                        //MessageBox.Show(downloadFamilyLoc);
                        //MessageBox.Show(downloadXlsLoc);
                        //MessageBox.Show("1:" + lvw_SelectedFamilies.Items[i].SubItems[1].Text);
                        //MessageBox.Show("2:" + lvw_SelectedFamilies.Items[i].SubItems[2].Text);
                        //MessageBox.Show("3:" + lvw_SelectedFamilies.Items[i].SubItems[3].Text);
                        //MessageBox.Show("4:" + lvw_SelectedFamilies.Items[i].SubItems[4].Text);


                        downloadFamilyLocs.Add(downloadFamilyLoc);
                        downloadXlsLocs.Add(downloadXlsLoc);
                    }

                    var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);
                    foreach (var downloadFamilyLoc in downloadFamilyLocs.Distinct().ToList())
                    {
                        //将指定属性表下载到本地文件夹
                        //ftp.DownloadFile(@"DB_Family_Library_FTP\precast\" + xlsName, @"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\parameter_excel\");
                        ftp.DownloadFile(downloadFamilyLoc.Replace(ftpConnectionLocation, "").Replace("/", "\\"), savePath);
                        downloadFileName = Path.GetFileName(downloadFamilyLoc);
                        //MessageBox.Show(downloadFileName);
                    }

                    foreach (var downloadXlsLoc in downloadXlsLocs.Distinct().ToList())
                    {
                        downloadXlsName = Path.GetFileName(downloadXlsLoc);
                        if (downloadXlsLoc.Contains("ftp"))
                        {
                            ftp.DownloadFile(downloadXlsLoc.Replace(ftpConnectionLocation, "").Replace("/", "\\"), savePath);
                        }
                        else
                        {
             
                            File.Copy(downloadXlsLoc, savePath + "\\" + downloadXlsName, true);
                        }
                        //MessageBox.Show(downloadXlsName);
                    }

                    downloadFilePath = savePath + downloadFileName;
                    downloadXlsPath = savePath + downloadXlsName;

                    MessageBox.Show("下载完成！");

                }
                catch (Exception exce)
                { MessageBox.Show(exce.Message); }

            }
            else
            {
                 
            }

            //dgv_Selected.ValueType
        }

        private void btn_justLoadRfa_Click(object sender, EventArgs e)
        {
            LogMonitor.logMonitorUpdate(sqlConnectionLocation, "族库平台：下载并载入" + lvw_SelectedFamilies.Items.Count.ToString() + "个");
            
            try
            {
                downloadFamilyLocsA.Clear();
                downloadXlsLocsA.Clear();
                downloadFamilyLocs.Clear();
                downloadXlsLocs.Clear();
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int i = 0; i < lvw_SelectedFamilies.Items.Count; i++)
                {
                    string downloadXlsLoc = null;
                    //MessageBox.Show(lvw_SelectedFamilies.Items[i].SubItems[1].Text);
                    string downloadFamilyLoc = lvw_SelectedFamilies.Items[i].SubItems[2].Text;
                    //string downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[3].Text;

                    if (lvw_SelectedFamilies.Items[i].SubItems[9].Text != "00")
                    {
                        downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[9].Text;
                    }
                    else
                    {
                        downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[3].Text;
                    }

                    downloadFamilyLocs.Add(downloadFamilyLoc);
                    downloadXlsLocs.Add(downloadXlsLoc);

                }
                
                //downloadFamilyLocsA = downloadFamilyLocs;
                //downloadXlsLocsA = downloadXlsLocs;

                var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);
                foreach (var downloadFamilyLoc in downloadFamilyLocs.Distinct().ToList())
                {
                    //将指定属性表下载到本地文件夹
                    //ftp.DownloadFile(@"DB_Family_Library_FTP\precast\" + xlsName, @"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\parameter_excel\");
                    ftp.DownloadFile(downloadFamilyLoc.Replace(ftpConnectionLocation, "").Replace("/", "\\"), downloadLocation);
                    downloadFileName = Path.GetFileName(downloadFamilyLoc);
                    //MessageBox.Show(downloadLocation + downloadFileName);
                    downloadFamilyLocsA.Add(downloadLocation + downloadFileName);
                }

                foreach (var downloadXlsLoc in downloadXlsLocs.Distinct().ToList())
                {
                    downloadXlsName = Path.GetFileName(downloadXlsLoc);
                    
                    if (downloadXlsLoc.Contains("ftp"))
                    {
                        ftp.DownloadFile(downloadXlsLoc.Replace(ftpConnectionLocation, "").Replace("/", "\\"), downloadLocation);
                    }
                    else
                    {
                        File.Copy(downloadXlsLoc, downloadLocation + downloadXlsName, true);
                    }
                    //MessageBox.Show(downloadXlsName);
                    downloadXlsLocsA.Add(downloadLocation + downloadXlsName);
                }
                downloadFilePath = downloadLocation + downloadFileName;
                downloadXlsPath = downloadLocation + downloadXlsName;

                LoadTag = "1";
                //Exc = new ExecuteEvent();
                Exc.LoadTag = LoadTag;
                Exc.familyFilePaths = downloadFamilyLocsA;
                Exc.parameterFilePaths = downloadXlsLocsA;
                //eventHandler = ExternalEvent.Create(Exc);
                eventHandler.Raise();

            }
            catch (Exception efd)
            { MessageBox.Show(efd.ToString()); }
 
        }

        private void btn_loadRfa_Click(object sender, EventArgs e)
        {
            LogMonitor.logMonitorUpdate(sqlConnectionLocation, "族库平台：下载附表并载入" + lvw_SelectedFamilies.Items.Count.ToString() + "个");
            
            try
            {
                downloadFamilyLocsA.Clear();
                downloadXlsLocsA.Clear();
                downloadFamilyLocs.Clear();
                downloadXlsLocs.Clear();
                for (int i = 0; i < lvw_SelectedFamilies.Items.Count; i++)
                {
                    string downloadFamilyLoc = lvw_SelectedFamilies.Items[i].SubItems[2].Text;
                    //string downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[3].Text;
                    string downloadXlsLoc = null;
                    //MessageBox.Show("KGs：" + downloadFamilyLoc);
                    if (lvw_SelectedFamilies.Items[i].SubItems[9].Text != "00")
                    {
                        downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[9].Text;
                    }
                    else
                    {
                        downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[3].Text;
                    }

                    downloadFamilyLocs.Add(downloadFamilyLoc);
                    downloadXlsLocs.Add(downloadXlsLoc);
                }
 
                
                var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);

                //foreach (var downloadFamilyLoc in downloadFamilyLocs.Distinct().ToList())
                //{

                //    ftp.DownloadFile(downloadFamilyLoc.Replace(ftpConnectionLocation, "").Replace("/", "\\"), downloadLocation);
                //    downloadFileName = Path.GetFileName(downloadFamilyLoc);

                //    downloadFamilyLocsA.Add(downloadLocation + downloadFileName);
                //}

                //foreach (var downloadXlsLoc in downloadXlsLocs.Distinct().ToList())
                //{
                //    ftp.DownloadFile(downloadXlsLoc.Replace(ftpConnectionLocation, "").Replace("/", "\\"), downloadLocation);
                //    downloadXlsName = Path.GetFileName(downloadXlsLoc);

                //    downloadXlsLocsA.Add(downloadLocation + downloadXlsName);
                //}

                for (int kk = 0; kk < downloadFamilyLocs.Distinct().ToList().Count(); kk++)
                {
                    //if (!downloadXlsLocs.Distinct().ToList()[kk].Contains("withoutParameter"))
                    //{ 
                        //
                        downloadFileName = Path.GetFileName(downloadFamilyLocs.Distinct().ToList()[kk]);
                        ftp.DownloadFile(downloadFamilyLocs.Distinct().ToList()[kk].Replace(ftpConnectionLocation, "").Replace("/", "\\"), downloadLocation);
 
                        downloadFamilyLocsA.Add(downloadLocation + downloadFileName);

                        //
                        downloadXlsName = Path.GetFileName(downloadXlsLocs.ToList()[kk]);

                        if (downloadXlsLocs.ToList()[kk].Contains("ftp"))
                        {
                            ftp.DownloadFile(downloadXlsLocs.ToList()[kk].Replace(ftpConnectionLocation, "").Replace("/", "\\"), downloadLocation);
                        }
                        else if (downloadXlsLocs.ToList()[kk].Contains("withoutParameter"))
                        {
                            File.Copy(@"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\parameter_excel\withoutParameter.xls", downloadLocation + downloadXlsName, true);
                        }
                        else
                        {
                            File.Copy(downloadXlsLocs.ToList()[kk], downloadLocation + downloadXlsName, true);
                        }
 
                        downloadXlsLocsA.Add(downloadLocation + downloadXlsName);
                    //  }
                    //else if(downloadXlsLocs.Distinct().ToList()[kk].Contains("withoutParameter"))
                    //{
                    //    MessageBox.Show("当前族\n" + "\n无附表，无法使用该功能\n");
                    //    continue;
                    //}

                }


                downloadFilePath = downloadLocation + downloadFileName;
                downloadXlsPath = downloadLocation + downloadXlsName;

                LoadTag = "2";

                Exc.LoadTag = LoadTag;
                Exc.familyFilePaths = downloadFamilyLocsA;
                Exc.parameterFilePaths = downloadXlsLocsA;

                eventHandler.Raise();

            }
            catch (Exception efd)
            { MessageBox.Show(efd.ToString()); }



        }

        private void btn_config_Click(object sender, EventArgs e)
        {
            configForm configForm = new configForm();
            configForm.ShowDialog();
        }

        private void btn_loadRfa0_Click(object sender, EventArgs e)
        {
            LogMonitor.logMonitorUpdate(sqlConnectionLocation, "族库平台：下载附表并载入" + lvw_SelectedFamilies.Items.Count.ToString() + "个");
            
            try
            {
                downloadFamilyLocsA.Clear();
                downloadXlsLocsA.Clear();
                downloadFamilyLocs.Clear();
                downloadXlsLocs.Clear();
                for (int i = 0; i < lvw_SelectedFamilies.Items.Count; i++)
                {
                    string downloadFamilyLoc = lvw_SelectedFamilies.Items[i].SubItems[2].Text;
                    string downloadXlsLoc = null;
                    //MessageBox.Show("KGs：" + downloadFamilyLoc);
                    if (lvw_SelectedFamilies.Items[i].SubItems[9].Text != "00")
                    {
                        downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[9].Text;
                    }
                    else
                    {
                        downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[3].Text;
                    }

                    downloadFamilyLocs.Add(downloadFamilyLoc);
                    downloadXlsLocs.Add(downloadXlsLoc);
                }
 
                var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);

                //foreach (var downloadFamilyLoc in downloadFamilyLocs.Distinct().ToList())
                //{

                //    ftp.DownloadFile(downloadFamilyLoc.Replace(ftpConnectionLocation, "").Replace("/", "\\"), downloadLocation);
                //    downloadFileName = Path.GetFileName(downloadFamilyLoc);

                //    downloadFamilyLocsA.Add(downloadLocation + downloadFileName);
                //}

                //foreach (var downloadXlsLoc in downloadXlsLocs.Distinct().ToList())
                //{
                //    ftp.DownloadFile(downloadXlsLoc.Replace(ftpConnectionLocation, "").Replace("/", "\\"), downloadLocation);
                //    downloadXlsName = Path.GetFileName(downloadXlsLoc);

                //    downloadXlsLocsA.Add(downloadLocation + downloadXlsName);
                //}


                for (int kk = 0; kk< downloadFamilyLocs.Distinct().ToList().Count(); kk++)
                {
                    //if (!downloadXlsLocs.Distinct().ToList()[kk].Contains("withoutParameter"))
                    //{
                        //
                        downloadFileName = Path.GetFileName(downloadFamilyLocs.Distinct().ToList()[kk]);
      
                        ftp.DownloadFile(downloadFamilyLocs.Distinct().ToList()[kk].Replace(ftpConnectionLocation, "").Replace("/", "\\"), downloadLocation);

                        downloadFamilyLocsA.Add(downloadLocation + downloadFileName);

                        //
                        downloadXlsName = Path.GetFileName(downloadXlsLocs.ToList()[kk]);

                    if (downloadXlsLocs.ToList()[kk].Contains("ftp"))
                    {
                        ftp.DownloadFile(downloadXlsLocs.ToList()[kk].Replace(ftpConnectionLocation, "").Replace("/", "\\"), downloadLocation);
                    }
                    else if (downloadXlsLocs.ToList()[kk].Contains("withoutParameter"))
                    {
                        File.Copy(@"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\parameter_excel\withoutParameter.xls", downloadLocation + downloadXlsName, true);
                    }
                    else
                    {
                        File.Copy(downloadXlsLocs.ToList()[kk], downloadLocation + downloadXlsName, true);
                    }

                        //ftp.DownloadFile(downloadXlsLocs.Distinct().ToList()[kk].Replace(ftpConnectionLocation, "").Replace("/", "\\"), downloadLocation);

                        downloadXlsLocsA.Add(downloadLocation + downloadXlsName);
                    //}
                    //else if(downloadXlsLocs.Distinct().ToList()[kk].Contains("withoutParameter"))
                    //{
                    //    MessageBox.Show("当前族\n" + "\n无附表，无法使用该功能\n");
                    //    continue;
                    //}

                }

                downloadFilePath = downloadLocation + downloadFileName;
                downloadXlsPath = downloadLocation + downloadXlsName;

                LoadTag = "0";

                Exc.LoadTag = LoadTag;
                Exc.familyFilePaths = downloadFamilyLocsA;
                Exc.parameterFilePaths = downloadXlsLocsA;

                eventHandler.Raise();

            }
            catch (Exception efd)
            { MessageBox.Show(efd.ToString()); }

        }

        private void btn_winclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_winmaxiz_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btn_winminiz_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lvw_ParaListSelectedFamily_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_defaultXls_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            ExcelHelper ExcelHelper = new ExcelHelper();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "excel文件|*.xls|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK )
            {
                string pPath = openFileDialog.FileName.ToString();//获取用户选择的文件路径
                string pName = pPath.Substring(pPath.LastIndexOf("\\") + 1);//获取用户选择的文件名称

                //configParaPath = pPath.Replace(pName, openFamilyName);
                //configParaPath = pPath.Replace(".xls", "").Replace(pName, openFamilyName) + "_" + ReviewLocalFamily.getFileMD5Hash(pPath) + ".xls";
                //configParaName = pName.Replace(".xls", "").Replace(pName, openFamilyName) + "_" + ReviewLocalFamily.getFileMD5Hash(pPath) + ".xls";
                //File.Copy(pPath, configParaPath, true);

                txt_defaultXls.Text = pPath;
                MessageBox.Show(txt_defaultXls.Text );
                //---------------------------------------------------------------------------------
                dt = ExcelHelper.Reading_Excel_Information(pPath);
                try
                {
                    lvw_ParaListSelectedFamily.Items.Clear();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        var item = new ListViewItem();

                        if (dt.Rows[j]["paraname"] == null)
                        { break; }
                        else
                        {
                            item.SubItems.Add(dt.Rows[j]["paraname"].ToString());
                            item.SubItems.Add(dt.Rows[j]["paravalue"].ToString());
                            item.SubItems.Add(dt.Rows[j]["paratag"].ToString());
                            lvw_ParaListSelectedFamily.Items.Add(item);
                        }
                    }

                    //SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);

                    //string readPath = "SELECT * FROM table_p_Family";
                    //DataSet dss = SqlCompose.ExecuteSqlQuery(readPath);
                    //DataTable dts = new DataTable();
                    //dts = dss.Tables[0];

                    //ftp.DownloadFile(dt.Rows[i]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                    //string iconPath = xlsLocation + @"parameter_perview\"+ Path.GetFileName(dt.Rows[i]["f_Pic"].ToString()); 

                }
                catch (Exception dde)
                {
                    MessageBox.Show(dde.ToString());
                }

            }
        }

        private void btn_xlsOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    this.dataGridView1.Rows[i].Cells["dgv_defaultXls"].Value = txt_defaultXls.Text;
                }
            }
               
        }
    }


    //public class ExecuteEvent : IExternalEventHandler
    //{
    //    public string LoadTag = null;
    //    public List<string> familyFilePaths = new List<string>();
    //    public List<string> parameterFilePaths = new List<string>();

    //    public void Execute(UIApplication app)
    //    {
    //        Document doc = app.ActiveUIDocument.Document;

    //        if (LoadTag == "0")
    //        {
    //            try
    //            {
    //                MyFamilyLoadOptions option = new MyFamilyLoadOptions();

    //                for (int k = 0; k < familyFilePaths.Count(); k++)
    //                {
    //                    Transaction openFamily = new Transaction(doc, "openFamily");
    //                    openFamily.Start();
    //                    //MessageBox.Show(familyFilePaths[k] );
    //                    Document rfadoc = app.Application.OpenDocumentFile(familyFilePaths[k]);
    //                    //openFamily.Commit();
    //                    settingFamilyParamenters(rfadoc, familyFilePaths[k], parameterFilePaths[k]);
    //                    openFamily.Commit();
    //                    Family Family = rfadoc.LoadFamily(doc, option);
    //                }
    //            }
    //            catch (Exception eeffef)
    //            { MessageBox.Show(eeffef.ToString()); }

    //            #region 进程解放部分  (已经验证revit.exe进程占用，由于单线程原因，解除占用只能关闭revit)
    //            //Process.GetProcesses();
    //            //foreach (var ffp in familyFilePaths)
    //            //{
    //            //    string fileName = ffp;//要检查被那个进程占用的文件  

    //            //    Process tool = new Process();
    //            //    tool.StartInfo.FileName = @"C:\ProgramData\Autodesk\Revit\Addins\2016\handle.exe";
    //            //    tool.StartInfo.Arguments = fileName + " /accepteula";
    //            //    tool.StartInfo.UseShellExecute = false;
    //            //    tool.StartInfo.RedirectStandardOutput = true;
    //            //    tool.Start();
    //            //    tool.WaitForExit();
    //            //    string outputTool = tool.StandardOutput.ReadToEnd();

    //            //    string matchPattern = @"(?<=\s+pid:\s+)\b(\d+)\b(?=\s+)";
    //            //    foreach (Match match in Regex.Matches(outputTool, matchPattern))
    //            //    {
    //            //        MessageBox.Show(int.Parse(match.Value).ToString());
    //            //        Process.GetProcessById(int.Parse(match.Value)).Kill();
    //            //    }
    //            //}
    //            #endregion

    //            try
    //            {
    //                foreach (var ffp in familyFilePaths)
    //                {
    //                    if (ffp != null)
    //                    {
    //                        File.Delete(ffp);
    //                    }
    //                }

    //                foreach (var pfp in parameterFilePaths)
    //                {
    //                    if (pfp != null)
    //                    {
    //                        File.Delete(pfp);
    //                    }
    //                }
    //            }
    //            catch (Exception defza)
    //            {
    //                MessageBox.Show("所选族已载入成功");
    //            }
    //        }
    //        else if (LoadTag == "1")
    //        {
    //            try
    //            {
    //                MyFamilyLoadOptions option = new MyFamilyLoadOptions();

    //                for (int k = 0; k < familyFilePaths.Count(); k++)
    //                {
    //                    Transaction openFamily = new Transaction(doc, "openFamily");
    //                    openFamily.Start();
    //                    MessageBox.Show(familyFilePaths[k]);
    //                    Document rfadoc = app.Application.OpenDocumentFile(familyFilePaths[k] );
    //                    //openFamily.Commit();
    //                    //settingFamilyParamenters(rfadoc, familyFilePaths[k], parameterFilePaths[k]);
    //                    openFamily.Commit();
    //                    Family Family = rfadoc.LoadFamily(doc, option);
    //                }
    //            }
    //            catch (Exception eeffef)
    //            { MessageBox.Show(eeffef.ToString()); }

    //            try
    //            {
    //                foreach (var ffp in familyFilePaths)
    //                {
    //                    if (ffp != null)
    //                    {
    //                        File.Delete(ffp);
    //                    }
    //                }

    //                foreach (var pfp in parameterFilePaths)
    //                {
    //                    if (pfp != null)
    //                    {
    //                        File.Delete(pfp);
    //                    }
    //                }
    //            }
    //            catch (Exception defza)
    //            {
    //                MessageBox.Show("所选族已载入成功");
    //            }
    //        }
    //        else if (LoadTag == "2")
    //        {
    //            try
    //            {
    //                MyFamilyLoadOptions option = new MyFamilyLoadOptions();

    //                for (int k = 0; k < familyFilePaths.Count(); k++)
    //                {
    //                    Transaction openFamily = new Transaction(doc, "openFamily");
    //                    openFamily.Start();
    //                    //MessageBox.Show(familyFilePaths[k] );
    //                    Document rfadoc = app.Application.OpenDocumentFile(familyFilePaths[k]);
    //                    //openFamily.Commit();
    //                    settingFamilyParamentersValue(rfadoc, familyFilePaths[k], parameterFilePaths[k]);
    //                    openFamily.Commit();
    //                    Family Family = rfadoc.LoadFamily(doc, option);
    //                }
    //            }
    //            catch (Exception eeffef)
    //            { MessageBox.Show(eeffef.ToString()); }
 
    //            try
    //            {
    //                foreach (var ffp in familyFilePaths)
    //                {
    //                    if (ffp != null)
    //                    {
    //                        File.Delete(ffp);
    //                    }
    //                }

    //                foreach (var pfp in parameterFilePaths)
    //                {
    //                    if (pfp != null)
    //                    {
    //                        File.Delete(pfp);
    //                    }
    //                }
    //            }
    //            catch (Exception defza)
    //            {
    //                MessageBox.Show("所选族已载入成功"   );
    //            }
    //        }
    //        //throw new NotImplementedException();
    //    }

    //    public void settingFamilyParamenters(Document doc, string familyFilePath, string parameterFilePath)
    //    {
    //        //读取xls文件
    //        ExcelHelper ExcelHelper = new ExcelHelper();
    //        DataTable dt = ExcelHelper.Reading_Excel_Information(parameterFilePath);

    //        #region
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            List<string> ls_ParameterNames = new List<string>();
    //            //ls_ParameterNames.Add(dt.Rows[i][0].ToString);
    //        }
    //        #endregion

    //        #region 族参数操作1
    //        FamilyManager familyMgr = doc.FamilyManager;

    //        //清空族内所有类型 仅保留默认族类型
    //        int typesizes = familyMgr.Types.Size;
    //        if (familyMgr.Types.Size > 1 && familyMgr.Types.Size != 0)
    //        {
    //            for (int typenumber = 0; typenumber < typesizes - 1; typenumber++)
    //            {
    //                if (familyMgr.CurrentType != null)
    //                {
    //                    Transaction DeleteType = new Transaction(doc, "DeleteType");
    //                    DeleteType.Start();
    //                    familyMgr.DeleteCurrentType();
    //                    DeleteType.Commit();
    //                }
    //            }
    //        }

    //        //清空族内所有参数条目
    //        foreach (FamilyParameter fp in familyMgr.Parameters)
    //        {
    //            if (fp.Definition.ParameterGroup == BuiltInParameterGroup.PG_ELECTRICAL)
    //            {
    //                Transaction RemoveParameter = new Transaction(doc, "RemoveParameter");
    //                RemoveParameter.Start();
    //                familyMgr.RemoveParameter(fp);
    //                RemoveParameter.Commit();
    //            }
    //        }

    //        //开始添加

    //        Transaction addParameter = new Transaction(doc, "AddParameters");
    //        addParameter.Start();

    //        List<string> paraNames = new List<string>();
    //        List<bool> isInstances = new List<bool>();
    //        List<string> paravalues = new List<string>();
    //        //设置族参数的类别和类型
    //        BuiltInParameterGroup paraGroup = BuiltInParameterGroup.PG_ELECTRICAL;
    //        ParameterType paraType = ParameterType.Text;

    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            string paraName = dt.Rows[i]["paraname"].ToString();
    //            paraNames.Add(paraName);

    //            //设置族参数为实例参数
    //            bool isInstance = true;
    //            if (dt.Rows[i]["paratag"].ToString() == "是")
    //            {
    //                isInstance = true;
    //            }
    //            else
    //            {
    //                isInstance = false;
    //            }
    //            isInstances.Add(isInstance);

    //            paravalues.Add(dt.Rows[i]["paravalue"].ToString());

    //        }

    //        for (int k = 0; k < paraNames.Count(); k++)
    //        {
 
    //            FamilyParameter newParameter = familyMgr.AddParameter(paraNames[k], paraGroup, paraType, isInstances[k]);
    //            //创建族参数(每个参数两秒)
    //            //familyMgr.Set(newParameter, paravalues[k]);
    //        }

    //        SaveOptions opt = new SaveOptions();
    //        //doc.Save(opt);
    //        //doc.SaveAs(@"D:\"+);
    //        //doc.Close();
    //        addParameter.Commit();
    //        #endregion

    //    }
 
    //    public void settingFamilyParamentersValue(Document doc, string familyFilePath, string parameterFilePath)
    //    {
    //        //读取xls文件
    //        ExcelHelper ExcelHelper = new ExcelHelper();
    //        DataTable dt = ExcelHelper.Reading_Excel_Information(parameterFilePath);

    //        #region
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            List<string> ls_ParameterNames = new List<string>();
    //            //ls_ParameterNames.Add(dt.Rows[i][0].ToString);
    //        }
    //        #endregion

    //        #region 族参数操作1
    //        FamilyManager familyMgr = doc.FamilyManager;

    //        //清空族内所有类型 仅保留默认族类型
    //        int typesizes = familyMgr.Types.Size;
    //        if (familyMgr.Types.Size > 1 && familyMgr.Types.Size != 0)
    //        {
    //            for (int typenumber = 0; typenumber < typesizes - 1; typenumber++)
    //            {
    //                if (familyMgr.CurrentType != null)
    //                {
    //                    Transaction DeleteType = new Transaction(doc, "DeleteType");
    //                    DeleteType.Start();
    //                    familyMgr.DeleteCurrentType();
    //                    DeleteType.Commit();
    //                }
    //            }
    //        }

    //        //清空族内所有参数条目
    //        foreach (FamilyParameter fp in familyMgr.Parameters)
    //        {
    //            if (fp.Definition.ParameterGroup == BuiltInParameterGroup.PG_ELECTRICAL)
    //            {
    //                Transaction RemoveParameter = new Transaction(doc, "RemoveParameter");
    //                RemoveParameter.Start();
    //                familyMgr.RemoveParameter(fp);
    //                RemoveParameter.Commit();
    //            }
    //        }

    //        //开始添加

    //        Transaction addParameter = new Transaction(doc, "AddParameters");
    //        addParameter.Start();

    //        List<string> paraNames = new List<string>();
    //        List<bool> isInstances = new List<bool>();
    //        List<string> paravalues = new List<string>();
    //        //设置族参数的类别和类型
    //        BuiltInParameterGroup paraGroup = BuiltInParameterGroup.PG_ELECTRICAL;
    //        ParameterType paraType = ParameterType.Text;

    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            string paraName = dt.Rows[i]["paraname"].ToString();
    //            paraNames.Add(paraName);

    //            //设置族参数为实例参数
    //            bool isInstance = true;
    //            if (dt.Rows[i]["paratag"].ToString() == "是")
    //            {
    //                isInstance = true;
    //            }
    //            else
    //            {
    //                isInstance = false;
    //            }
    //            isInstances.Add(isInstance);

    //            paravalues.Add(dt.Rows[i]["paravalue"].ToString());

    //        }

    //        for (int k = 0; k < paraNames.Count(); k++)
    //        {

    //            FamilyParameter newParameter = familyMgr.AddParameter(paraNames[k], paraGroup, paraType, isInstances[k]);
    //            //创建族参数(每个参数两秒)
    //            familyMgr.Set(newParameter, paravalues[k]);
    //        }
     
    //        SaveOptions opt = new SaveOptions();
    //        //doc.Save(opt);
    //        //doc.SaveAs(@"D:\"+);
    //        //doc.Close();
    //        addParameter.Commit();
    //        #endregion

    //    }

    //    public string GetName()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

}
