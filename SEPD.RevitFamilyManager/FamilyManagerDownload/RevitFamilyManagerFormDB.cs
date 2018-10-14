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
    public partial class RevitFamilyManagerFormDB : System.Windows.Forms.Form
    {
        public RevitFamilyManagerFormDB()
        {
            InitializeComponent();
 
            //显示树状图
            this.SetRoot();
            this.SetRoot2();
            //this.tvw_Family2 = this.tvw_Family;

            this.SetTreeRoot();

            List<string> xlsFileNames = GetDirFile.FTPGetFile(ftpConnectionLocation + "DB_Family_Library_FTP/precast/", ftpUserName, ftpPassword);

            if (allFamilyTypes == null)
            {
                MessageBox.Show("!!!!!!!!!!!!!");
            }
        }


        LogMonitor LogMonitor = new LogMonitor();

        ExecuteEvent Exc = null;
        ExternalEvent eventHandler = null;

        #region 全窗口拖动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void RevitFamilyManagerFormDB_MouseDown(object sender, MouseEventArgs e)
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

        public int pageSize = 10;      //每页记录数
        public int recordCount = 0;    //总记录数
        public int pageCount = 0;      //总页数
        public int currentPage = 0;    //当前页
        public int lastPageNumCount = 0;
        DataTable dtSource = new DataTable();
        DataTable dtp = new DataTable();



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

        //------------------------------------------------------
        #region 树状结构1
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
                //this.tvw_Family2.Nodes.Add(tnode);
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
            if (tn != null && tn.Parent != null)
            {
                familyPro = tn.Parent.Name;
                familyType = tn.Name;

                //lbl_ProText.Text = familyPro;
                //lbl_TypeText.Text = familyType;

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
            //this.dataGridView1.Rows.Clear();

            //string readPath = "SELECT * FROM table_p_Family WHERE f_Path LIKE '%" + @"/DB_Family_Library_FTP/" + familyPro + "/" + familyType + "/%'";
            string readPath = "SELECT * FROM table_p_Family WHERE f_Cat ='" + familyType + "'";
            DataSet ds = SqlCompose.ExecuteSqlQuery(readPath);
            DataTable dtTree = new DataTable();
            //MessageBox.Show(ds.Tables[1].ToString());
            dtTree = ds.Tables[0];

            dataGridView1.DataSource = dtTree;
            DataUB.dataTempTable = dtTree;
            dgvDataTable = dtTree;
            dtp = dtTree;




            //try
            //{
            //    List<string> picPathList = new List<string>();
            //    var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);

            //    lastPageNumCount = dtTree.Rows.Count % pageSize;
            //    if (lastPageNumCount != 0)
            //    {
            //        pageCount = dtTree.Rows.Count / pageSize + 1;
            //    }
            //    else if (lastPageNumCount == 0)
            //    {
            //        pageCount = dtTree.Rows.Count / pageSize;
            //    }
            //    //MessageBox.Show(dt.Rows.Count.ToString());
            //    int startRow = 0;
            //    int endRow = 0;
            //    if (pageCount == 0)
            //    { endRow = lastPageNumCount; }
            //    else if (pageCount > 0)
            //    { endRow = pageSize; }

            //    lbl_pageCount.Text = pageCount.ToString();
            //    lbl_currentPage.Text = "1";
            //    lbl_recordCount.Text = dtTree.Rows.Count.ToString();

            //    for (int i = startRow; i < endRow; i++)
            //    {
            //        int index = this.dataGridView1.Rows.Add();
            //        //var dd = dt.Rows[i]["f_Path"];
            //        //this.dataGridView1.Rows[i].Cells[0].Value = ;
            //        this.dataGridView1.Rows[index].Cells["dgv_FamilyName"].Value = dtTree.Rows[i]["f_Name"].ToString();
            //        this.dataGridView1.Rows[index].Cells["dgv_Version"].Value = "1.0";
            //        this.dataGridView1.Rows[index].Cells["dgv_Date"].Value = dtTree.Rows[i]["f_UploadDate"].ToString();
            //        this.dataGridView1.Rows[index].Cells["dgv_Type"].Value = familyType;
            //        this.dataGridView1.Rows[index].Cells["dgv_Hash"].Value = dtTree.Rows[i]["f_Hash"].ToString();
            //        this.dataGridView1.Rows[index].Cells["dgv_FamilyLoc"].Value = dtTree.Rows[i]["f_Path"].ToString();
            //        this.dataGridView1.Rows[index].Cells["dgv_ParaLoc"].Value = dtTree.Rows[i]["f_ConfigParaLocation"].ToString();
            //        this.dataGridView1.Rows[index].Cells["dgv_ValLevel"].Value = dtTree.Rows[i]["f_ValLevel"].ToString();
            //        this.dataGridView1.Rows[index].Cells["dgv_Manufacturer"].Value = dtTree.Rows[i]["f_manufacturer"].ToString();
            //        this.dataGridView1.Rows[index].Cells["dgv_Source"].Value = dtTree.Rows[i]["f_Source"].ToString();
            //        this.dataGridView1.Rows[index].Cells["dgv_DeviceType"].Value = dtTree.Rows[i]["f_DeviceType"].ToString();

            //        try
            //        {
            //            if (dtTree.Rows[i]["f_Pic"].ToString() != null)
            //            {
            //                ftp.DownloadFile(dtTree.Rows[i]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
            //                string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(dtTree.Rows[i]["f_Pic"].ToString());
            //                this.dataGridView1.Rows[index].Cells["dgv_Pics"].Value = Image.FromFile(iconPath);
            //            }
            //            else
            //            { continue; }
            //        }
            //        catch (Exception erew)
            //        { }
            //    }



            //}
            //catch (Exception eex)
            //{
            //    //MessageBox.Show(eex.ToString());
            //}

            #endregion
            dataGridView1.Columns["f_Uid"].HeaderText = "索引";
            dataGridView1.Columns["f_Name"].HeaderText = "族名称";
            dataGridView1.Columns["f_Path"].HeaderText = "文件路径";
            dataGridView1.Columns["f_Pic"].HeaderText = "缩略图";
            dataGridView1.Columns["f_Value"].HeaderText = "文件大小";
            dataGridView1.Columns["f_Pro"].HeaderText = "所属专业";
            dataGridView1.Columns["f_ValLevel"].HeaderText = "电压等级";
            dataGridView1.Columns["f_Cat"].HeaderText = "类型";
            dataGridView1.Columns["f_Hash"].HeaderText = "哈希";
            dataGridView1.Columns["f_UploadDate"].HeaderText = "上传日期";
            dataGridView1.Columns["f_ParaLocation"].HeaderText = "表路径";
            dataGridView1.Columns["f_ConfigParaLocation"].HeaderText = "属性表路径";
            dataGridView1.Columns["f_manufacturer"].HeaderText = "生产厂家";
            dataGridView1.Columns["f_Source"].HeaderText = "族来源";
            dataGridView1.Columns["f_DeviceType"].HeaderText = "型号";
            dataGridView1.Columns["f_Standard"].HeaderText = "依据标准";


            dataGridView1.Columns["f_Path"].Visible = false;
            dataGridView1.Columns["f_Pic"].Visible = false;
            dataGridView1.Columns["f_ParaLocation"].Visible = false;
            dataGridView1.Columns["f_ConfigParaLocation"].Visible = false;

            dataGridView1.Columns["f_Uid"].ReadOnly = true;
            dataGridView1.Columns["f_Hash"].ReadOnly = true;

        }
        #endregion
        //------------------------------------------------------
        #region 树状结构2
        private void SetTreeRoot()
        {
            //tvw_Family2.LabelEdit = true;

            //FTPConnection FTPConnection = new FTPConnection();
            List<string> getFtpFolder = new List<string>();
            List<string> getFtpFile = new List<string>();
            List<string> getFtpFolderX = new List<string>();

            string rootPath = ftpConnectionLocation + "DB_Family_Library_FTP/";

            getFtpFolder = GetDirFile.FTPGetDirctory(rootPath, ftpUserName, ftpPassword);
            getFtpFile = GetDirFile.FTPGetFile(rootPath, ftpUserName, ftpPassword);

            getFtpFolder.Remove("paracast");
            getFtpFolder.Remove("pics");
            getFtpFolder.Remove("precast");

            foreach (string porName in getFtpFolder.Distinct())
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = porName;
                //tvw_Family2.Nodes.Add(treeNode);
                getFtpFolderX = GetDirFile.FTPGetDirctory(rootPath + porName + "/", ftpUserName, ftpPassword);
                foreach (string catName in getFtpFolderX)
                {
                    TreeNode treeNodeX = new TreeNode();
                    treeNodeX.Text = catName;
                    treeNode.Nodes.Add(treeNodeX);
                }
                //this.tvw_Family2.Nodes.Add(treeNode);
            }

        }
        #endregion
        //------------------------------------------------------

        private void RevitFamilyManagerFormDB_Load(object sender, EventArgs e)
        {

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.ReadOnly = true;
            Exc = new ExecuteEvent();
            eventHandler = ExternalEvent.Create(Exc);

        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                ExcelHelper ExcelHelper = new ExcelHelper();
                var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);

                int row = e.RowIndex;
                int col = e.ColumnIndex;

                //显示缩略图图
                ftp.DownloadFile(DataUB.dataTempTable.Rows[row]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataTempTable.Rows[row]["f_Pic"].ToString());
                pictureBox1.BackgroundImage = Image.FromFile(iconPath);
                //显示属性表
                string ParaLoc = DataUB.dataTempTable.Rows[row]["f_ConfigParaLocation"].ToString();
                ftp.DownloadFile(ParaLoc.Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");

                //string localParasPath = DataUB.dataTempTable.Rows[row]["familyParass"].ToString();
                string xlsName = Path.GetFileName(ParaLoc);
                string localParaLoc = xlsLocation + @"parameter_perview\" + xlsName;
                DataTable dtpara = ExcelHelper.Reading_Excel_Information(localParaLoc);
                this.dataGridView2.DataSource = dtpara;

                //dataGridView2.Columns["paraname"].HeaderText = "属性条目";
                //dataGridView2.Columns["paravalue"].HeaderText = "属性值";
                //dataGridView2.Columns["paratag"].HeaderText = "是否实例";
                //dataGridView2.Columns["paragroup"].HeaderText = "属性分族";
                //dataGridView2.Columns["paratype"].HeaderText = "属性类型";
                //txt_defaultXls.Text = localParasPath;
            }
            catch (Exception req)
            { }

          

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

            ExcelHelper ExcelHelper = new ExcelHelper();
            var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);

            string iconftp = null;
            string iconorg = null;
            string paraorg = null;
            string paraftp = null;
            string xlsName = null;
            string localParaLoc = null;

            int dgvRows = dataGridView1.RowCount;
            if (dgvRows > 0)
            {

                #region 上下键自动选择
                int row = dataGridView1.CurrentRow.Index;
                int roww = dataGridView1.Rows.Count - 1;

                if (e.KeyCode == Keys.Down)
                {
                    if (row == roww)
                    {
                        iconftp = DataUB.dataTempTable.Rows[row]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\");
                        iconorg = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataTempTable.Rows[row]["f_Pic"].ToString());
                        paraorg = DataUB.dataTempTable.Rows[row]["f_ConfigParaLocation"].ToString();
                    }
                    else
                    {
                        iconftp = DataUB.dataTempTable.Rows[row + 1]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\");
                        iconorg = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataTempTable.Rows[row + 1]["f_Pic"].ToString());
                        paraorg = DataUB.dataTempTable.Rows[row + 1]["f_ConfigParaLocation"].ToString();
                    }

                }

                if (e.KeyCode == Keys.Up)
                {
                    if (row - 1 < 0)
                    {
                        iconftp = DataUB.dataTempTable.Rows[row]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\");
                        iconorg = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataTempTable.Rows[row]["f_Pic"].ToString());
                        paraorg = DataUB.dataTempTable.Rows[row]["f_ConfigParaLocation"].ToString();
                    }
                    else
                    {
                        iconftp = DataUB.dataTempTable.Rows[row - 1]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\");
                        iconorg = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataTempTable.Rows[row - 1]["f_Pic"].ToString());
                        paraorg = DataUB.dataTempTable.Rows[row - 1]["f_ConfigParaLocation"].ToString();
                    }
                }
                #endregion

                try
                {

                    ftp.DownloadFile(iconftp, xlsLocation + @"parameter_perview\");
                    //string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataTempTable.Rows[row]["f_Pic"].ToString());
                    pictureBox1.BackgroundImage = Image.FromFile(iconorg);

                    //显示属性表
                    paraftp = paraorg.Replace(ftpConnectionLocation, "").Replace("/", "\\");
                    ftp.DownloadFile(paraftp, xlsLocation + @"parameter_perview\");
                    xlsName = Path.GetFileName(paraorg);
                    localParaLoc = xlsLocation + @"parameter_perview\" + xlsName;
                    DataTable dtpara = ExcelHelper.Reading_Excel_Information(localParaLoc);
                    this.dataGridView2.DataSource = dtpara;
                }
                catch (Exception req)
                { }

                #region  回车键快速选择
                //if (e.KeyCode == Keys.Enter)
                //{
                //    bool dict = false;
                //    int rowIndexCurrent = dataGridView1.CurrentCell.RowIndex;  //当前行索引
                //                                                               //for (int k = 0;k < dataGridView1.SelectedRows.Count ; k++)
                //                                                               //{

                //    //}

                //    for (int i = 0; i < dataGridView3.RowCount; i++)
                //    {
                //        if (dataGridView3.Rows[i].Cells[0].Value != null && dataGridView1.Rows[rowIndexCurrent].Cells[0].Value != null)
                //        {
                //            if (dataGridView3.Rows[i].Cells[0].Value.ToString() == dataGridView1.Rows[rowIndexCurrent].Cells[0].Value.ToString())
                //            {
                //                dict = true;
                //                break;
                //            }
                //        }
                //    }

                //    if (dict == true)
                //    { }
                //    else
                //    {
                //        if (dataGridView3.RowCount == 0)
                //        {
                //            dtBox = DataUB.dataTempTable.Copy();
                //            dtBox.Rows.Clear();
                //            dtBox.ImportRow(DataUB.dataTempTable.Rows[rowIndexCurrent]);
                //            dataGridView3.DataSource = dtBox;
                //        }
                //        else
                //        {
                //            dtBox.ImportRow(DataUB.dataTempTable.Rows[rowIndexCurrent]);
                //            dataGridView3.DataSource = dtBox;
                //        }
                //    }
                //}

                #endregion

            }


        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        public DataTable dtBox = null;

        private void btn_AddToListBox_Click(object sender, EventArgs e)
        {
            
            bool dict = false;
            int rowIndexCurrent = dataGridView1.CurrentCell.RowIndex;  //当前行索引
            if (dataGridView1.SelectedRows.Count>=2)
            {
                for (int k = 0; k < dataGridView1.SelectedRows.Count; k++)
                {
                    for (int l = 0 ; l < DataUB.dataTempTable.Rows.Count ; l++)
                    {
                        if (dataGridView1.SelectedRows[k].Cells[0].Value.ToString() == DataUB.dataTempTable.Rows[l][0].ToString())
                        {
                            if (dataGridView3.RowCount == 0)
                            {
                                dtBox = DataUB.dataTempTable.Copy();
                                dtBox.Rows.Clear();
                                //dtBox.ImportRow(DataUB.dataTempTable.Rows[l]);
                                //dataGridView3.DataSource = dtBox;
                            }
                            else
                            {
                                //dtBox.ImportRow(DataUB.dataTempTable.Rows[l]);
                                //dataGridView3.DataSource = dtBox;
                            }
                            dtBox.ImportRow(DataUB.dataTempTable.Rows[l]);
                            dataGridView3.DataSource = dtBox;
                        }
                    }
                    DataGridViewRow row = dataGridView1.Rows[k];
                    //if (row.Selected)
                    //{
                    //    dtBox.ImportRow(row as DataRow);
                    //    dataGridView3.Rows();
                    //}
                }
            }
         
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                if (dataGridView3.Rows[i].Cells[0].Value != null && dataGridView1.Rows[rowIndexCurrent].Cells[0].Value !=null)
                {
                    if (dataGridView3.Rows[i].Cells[0].Value.ToString() == dataGridView1.Rows[rowIndexCurrent].Cells[0].Value.ToString())
                    {
                        dict = true;
                        break;
                    }
                } 
            }
 
            if (dict == true)
            { }
            else
            {
                if (dataGridView3.RowCount == 0)
                {
                    dtBox = DataUB.dataTempTable.Copy();
                    dtBox.Rows.Clear();
                    //dtBox.ImportRow(DataUB.dataTempTable.Rows[rowIndexCurrent]);
                    //dataGridView3.DataSource = dtBox;
                }
                else
                {
                    //dtBox.ImportRow(DataUB.dataTempTable.Rows[rowIndexCurrent]);
                    //dataGridView3.DataSource = dtBox;
                }
                dtBox.ImportRow(DataUB.dataTempTable.Rows[rowIndexCurrent]);
            }
            //此处需要查重
            string[] cn = new string[dtBox.Columns.Count];
            for (int i = 0; i < dtBox.Columns.Count; i++)
            { cn[i] = dtBox.Columns[i].ToString(); }
            //dtBox = dtBox.DefaultView.ToTable(true, new string[] { "f_Hash", });
            dtBox = dtBox.DefaultView.ToTable(true, cn);
            dataGridView3.DataSource = dtBox;

            dataGridView3.Columns["f_Uid"].HeaderText = "索引";
            dataGridView3.Columns["f_Name"].HeaderText = "族名称";
            dataGridView3.Columns["f_Path"].HeaderText = "文件路径";
            dataGridView3.Columns["f_Pic"].HeaderText = "缩略图";
            dataGridView3.Columns["f_Value"].HeaderText = "文件大小";
            dataGridView3.Columns["f_Pro"].HeaderText = "所属专业";
            dataGridView3.Columns["f_ValLevel"].HeaderText = "电压等级";
            dataGridView3.Columns["f_Cat"].HeaderText = "类型";
            dataGridView3.Columns["f_Hash"].HeaderText = "哈希";
            dataGridView3.Columns["f_UploadDate"].HeaderText = "上传日期";
            dataGridView3.Columns["f_ParaLocation"].HeaderText = "表路径";
            dataGridView3.Columns["f_ConfigParaLocation"].HeaderText = "属性表路径";
            dataGridView3.Columns["f_manufacturer"].HeaderText = "生产厂家";
            dataGridView3.Columns["f_Source"].HeaderText = "族来源";
            dataGridView3.Columns["f_DeviceType"].HeaderText = "型号";
            dataGridView3.Columns["f_Standard"].HeaderText = "依据标准"; 

            dataGridView3.Columns["f_Path"].Visible = false;
            dataGridView3.Columns["f_Pic"].Visible = false;
            dataGridView3.Columns["f_ParaLocation"].Visible = false;
            dataGridView3.Columns["f_ConfigParaLocation"].Visible = false;

            dataGridView3.Columns["f_Uid"].ReadOnly = true;
            dataGridView3.Columns["f_Hash"].ReadOnly = true;

        }

        private void btn_DeleteFromListBox_Click(object sender, EventArgs e)
        {
 
            int rowIndexCurrent = dataGridView3.CurrentCell.RowIndex;  //当前行索引
            List<string> rowNums = new List<string>();
            if (dtBox != null && dataGridView3.SelectedRows.Count >= 1)
            {
                for (int k = 0; k < dataGridView3.SelectedRows.Count; k++)
                {
                    for (int i = 0; i < dtBox.Rows.Count; i++)
                    {
                        //if (dataGridView2.Rows[rowIndexCurrent].Cells[0].Value.ToString() == dtBox.Rows[i][0].ToString())
                        if (dataGridView3.SelectedRows[k].Cells[0].Value.ToString() == dtBox.Rows[i][0].ToString())
                        {
                            //dataGridView3.Rows.RemoveAt(rowIndexCurrent);
                            rowNums.Add(dtBox.Rows[i][0].ToString());
                            //dtBox.Rows[i].Delete();
                            //dtBox.AcceptChanges();
                            //break;
                        }
                    }
                }
            }

            foreach (string str in rowNums)
            {
                for (int i = dtBox.Rows.Count - 1; i >= 0; i--)
                {
                    if (str == dtBox.Rows[i][0].ToString())
                    {
                        dtBox.Rows[i].Delete();
                        dtBox.AcceptChanges();
                    }
                }
            }

            //dtBox.AcceptChanges();
            //dtBox  = dataGridView3.DataSource as DataTable;
            dataGridView3.DataSource = dtBox;

        }
 
        public DataTable dtSearchResult = null;
        private void btn_Search_Click(object sender, EventArgs e)
        {
            DataTable dgvDataTable = DataUB.dataTempTable;
            dtSearchResult = dgvDataTable.Copy();
            dtSearchResult.Rows.Clear();
 
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
            for (int i = 0 ; i < dgvDataTable.Rows.Count ; i++)
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
                    dtSearchResult.ImportRow(dgvDataTable.Rows[i]);
                }
                else
                { continue; }
            }

            dataGridView1.DataSource = dtSearchResult;

        }

        private void btn_SearchII_Click(object sender, EventArgs e)
        {
            DataTable dtSearchResult2 = null;
            dtSearchResult2 = dtSearchResult.Copy();
            dtSearchResult2.Rows.Clear();

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
            for (int i = 0; i < dtSearchResult.Rows.Count; i++)
            {
                int cont = 0;
                foreach (string cond in SearchConditions)
                {
                    if (dtSearchResult.Rows[i]["f_Name"].ToString().Contains(cond) || dtSearchResult.Rows[i]["f_ValLevel"].ToString().Contains(cond) || dtSearchResult.Rows[i]["f_manufacturer"].ToString().Contains(cond) || dtSearchResult.Rows[i]["f_DeviceType"].ToString().Contains(cond) || dtSearchResult.Rows[i]["f_Source"].ToString().Contains(cond) || familyType == cond)
                    { cont++; }
                    else
                    {
                        cont = 0;
                        break;
                    }
                }
                if (cont != 0)
                {
                    dtSearchResult2.ImportRow(dtSearchResult.Rows[i]);
                }
                else
                { continue; }
            }

            dataGridView1.DataSource = dtSearchResult2;

            //dtSearchResult = null;
            //dtSearchResult = dtSearchResult2.Copy();
            //dtSearchResult.Rows.Clear();
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            //LogMonitor.logMonitorUpdate(sqlConnectionLocation, "族库平台：仅下载" + lvw_SelectedFamilies.Items.Count.ToString() + "个");
             
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
                    for (int i = 0; i < dtBox.Rows.Count; i++)
                    {
                        string downloadFamilyLoc = dtBox.Rows[i]["f_Path"].ToString();
                        string downloadXlsLoc = dtBox.Rows[i]["f_ConfigParaLocation"].ToString();
 
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
            //LogMonitor.logMonitorUpdate(sqlConnectionLocation, "族库平台：下载并载入" + lvw_SelectedFamilies.Items.Count.ToString() + "个");

            try
            {
                downloadFamilyLocsA.Clear();
                downloadXlsLocsA.Clear();
                downloadFamilyLocs.Clear();
                downloadXlsLocs.Clear();
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int i = 0; i < dtBox.Rows.Count; i++)
                {
                    string downloadXlsLoc = dtBox.Rows[i]["f_ConfigParaLocation"].ToString();           
                    string downloadFamilyLoc = dtBox.Rows[i]["f_Path"].ToString();

                    //MessageBox.Show(lvw_SelectedFamilies.Items[i].SubItems[1].Text);
                    //string downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[3].Text;

                    //if (lvw_SelectedFamilies.Items[i].SubItems[9].Text != "00")
                    //{
                    //    downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[9].Text;
                    //}
                    //else
                    //{
                    //    downloadXlsLoc = lvw_SelectedFamilies.Items[i].SubItems[3].Text;
                    //}

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

        private void btn_loadRfa0_Click(object sender, EventArgs e)
        {
            //LogMonitor.logMonitorUpdate(sqlConnectionLocation, "族库平台：下载附表并载入" + lvw_SelectedFamilies.Items.Count.ToString() + "个");

            try
            {
                downloadFamilyLocsA.Clear();
                downloadXlsLocsA.Clear();
                downloadFamilyLocs.Clear();
                downloadXlsLocs.Clear();
                for (int i = 0; i < dtBox.Rows.Count; i++)
                {
                    string downloadFamilyLoc = dtBox.Rows[i]["f_Path"].ToString();
                    string downloadXlsLoc = dtBox.Rows[i]["f_ConfigParaLocation"].ToString();
       

                    downloadFamilyLocs.Add(downloadFamilyLoc);
                    downloadXlsLocs.Add(downloadXlsLoc);
                }

                var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);
 

                for (int kk = 0; kk < downloadFamilyLocs.Distinct().ToList().Count(); kk++)
                {
                    
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

        private void btn_loadRfa_Click(object sender, EventArgs e)
        {
             
            try
            {
                downloadFamilyLocsA.Clear();
                downloadXlsLocsA.Clear();
                downloadFamilyLocs.Clear();
                downloadXlsLocs.Clear();
                for (int i = 0; i < dtBox.Rows.Count; i++)
                {
                    string downloadFamilyLoc = dtBox.Rows[i]["f_Path"].ToString();
                    string downloadXlsLoc = dtBox.Rows[i]["f_ConfigParaLocation"].ToString();
                    downloadFamilyLocs.Add(downloadFamilyLoc);
                    downloadXlsLocs.Add(downloadXlsLoc);
                }
                var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);
 
                for (int kk = 0; kk < downloadFamilyLocs.Distinct().ToList().Count(); kk++)
                {
                    downloadFileName = Path.GetFileName(downloadFamilyLocs.Distinct().ToList()[kk]);
                    ftp.DownloadFile(downloadFamilyLocs.Distinct().ToList()[kk].Replace(ftpConnectionLocation, "").Replace("/", "\\"), downloadLocation);

                    downloadFamilyLocsA.Add(downloadLocation + downloadFileName);

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

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                ExcelHelper ExcelHelper = new ExcelHelper();
                var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);

                int row = e.RowIndex;
                int col = e.ColumnIndex;
                //显示缩略图
                ftp.DownloadFile(this.dataGridView3.Rows[row].Cells["f_Pic"].Value.ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                //ftp.DownloadFile(DataUB.dataTempTable.Rows[row]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(this.dataGridView3.Rows[row].Cells["f_Pic"].Value.ToString());
                //string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataTempTable.Rows[row]["f_Pic"].ToString());
                // MessageBox.Show(iconPath.ToString());
                pictureBox1.BackgroundImage = Image.FromFile(iconPath);
                //显示属性表
                string ParaLoc = this.dataGridView3.Rows[row].Cells["f_ConfigParaLocation"].Value.ToString();
                ftp.DownloadFile(ParaLoc.Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");

                //string localParasPath = DataUB.dataTempTable.Rows[row]["familyParass"].ToString();
                string localParasPath = this.dataGridView3.Rows[row].Cells["f_ConfigParaLocation"].Value.ToString();
                string xlsName = Path.GetFileName(ParaLoc);
                string localParaLoc = xlsLocation + @"parameter_perview\" + xlsName;
                DataTable dtpara = ExcelHelper.Reading_Excel_Information(localParaLoc);
                this.dataGridView2.DataSource = dtpara;

                dataGridView2.Columns["paraname"].HeaderText = "属性条目";
                dataGridView2.Columns["paravalue"].HeaderText = "属性值";
                dataGridView2.Columns["paratag"].HeaderText = "是否实例";
                dataGridView2.Columns["paragroup"].HeaderText = "属性分族";
                dataGridView2.Columns["paratype"].HeaderText = "属性类型";

            }
            catch (Exception req)
            {
                //MessageBox.Show(req.ToString());
            }

        }

        private void dataGridView3_KeyDown(object sender, KeyEventArgs e)
        {

            ExcelHelper ExcelHelper = new ExcelHelper();
            var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);
            DataUB.dataForDGV2 = dtBox;
            string iconftp = null;
            string iconorg = null;
            string paraorg = null;
            string paraftp = null;
            string xlsName = null;
            string localParaLoc = null;

            int dgvRows = dataGridView3.RowCount;
            if (dgvRows > 0)
            {

                #region 上下键自动选择
                int row = dataGridView3.CurrentRow.Index;
                int roww = dataGridView3.Rows.Count - 1;

                if (e.KeyCode == Keys.Down)
                {
                    if (row == roww)
                    {
                        iconftp = DataUB.dataForDGV2.Rows[row]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\");
                        iconorg = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataForDGV2.Rows[row]["f_Pic"].ToString());
                        paraorg = DataUB.dataForDGV2.Rows[row]["f_ConfigParaLocation"].ToString();
                    }
                    else
                    {
                        iconftp = DataUB.dataForDGV2.Rows[row + 1]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\");
                        iconorg = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataForDGV2.Rows[row + 1]["f_Pic"].ToString());
                        paraorg = DataUB.dataForDGV2.Rows[row + 1]["f_ConfigParaLocation"].ToString();
                    }

                }

                if (e.KeyCode == Keys.Up)
                {
                    if (row - 1 < 0)
                    {
                        iconftp = DataUB.dataForDGV2.Rows[row]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\");
                        iconorg = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataForDGV2.Rows[row]["f_Pic"].ToString());
                        paraorg = DataUB.dataForDGV2.Rows[row]["f_ConfigParaLocation"].ToString();
                    }
                    else
                    {
                        iconftp = DataUB.dataForDGV2.Rows[row - 1]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\");
                        iconorg = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataForDGV2.Rows[row - 1]["f_Pic"].ToString());
                        paraorg = DataUB.dataForDGV2.Rows[row - 1]["f_ConfigParaLocation"].ToString();
                    }
                }
                #endregion

                try
                {

                    ftp.DownloadFile(iconftp, xlsLocation + @"parameter_perview\");
                    //string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataTempTable.Rows[row]["f_Pic"].ToString());
                    pictureBox1.BackgroundImage = Image.FromFile(iconorg);

                    //显示属性表
                    paraftp = paraorg.Replace(ftpConnectionLocation, "").Replace("/", "\\");
                    ftp.DownloadFile(paraftp, xlsLocation + @"parameter_perview\");
                    xlsName = Path.GetFileName(paraorg);
                    localParaLoc = xlsLocation + @"parameter_perview\" + xlsName;
                    DataTable dtpara = ExcelHelper.Reading_Excel_Information(localParaLoc);
                    this.dataGridView2.DataSource = dtpara;
                }
                catch (Exception req)
                { }

                #region  回车键快速选择

                #endregion

            }
        }
    }
}
