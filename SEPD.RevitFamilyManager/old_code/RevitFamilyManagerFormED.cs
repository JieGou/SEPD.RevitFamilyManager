
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
    public partial class RevitFamilyManagerFormED : System.Windows.Forms.Form
    {

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

        public int pageSize = 10;      //每页记录数
        public int recordCount = 0;    //总记录数
        public int pageCount = 0;      //总页数
        public int currentPage = 0;    //当前页
        public int lastPageNumCount = 0;
        DataTable dtSource = new DataTable();
        DataTable dtp = new DataTable();

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

        #region  所有类型
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
        #endregion

        
        public RevitFamilyManagerFormED()
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

            string readPath = "SELECT * FROM table_p_Family WHERE f_Cat ='" + familyType + "'";
            DataSet ds = SqlCompose.ExecuteSqlQuery(readPath);
            DataTable dtTree = new DataTable();
            //MessageBox.Show(ds.Tables[1].ToString());
            dtTree = ds.Tables[0];
            dgvDataTable = dtTree;
            dtp = dtTree;
            try
            {
                List<string> picPathList = new List<string>();
                var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);

                lastPageNumCount = dtTree.Rows.Count % pageSize;
                if (lastPageNumCount != 0)
                {
                    pageCount = dtTree.Rows.Count / pageSize + 1;
                }
                else if (lastPageNumCount == 0)
                {
                    pageCount = dtTree.Rows.Count / pageSize;
                }
                //MessageBox.Show(dt.Rows.Count.ToString());
                int startRow = 0;
                int endRow = 0;
                if (pageCount == 0)
                { endRow = lastPageNumCount; }
                else if (pageCount > 0)
                { endRow = pageSize; }

                lbl_pageCount.Text = pageCount.ToString();
                lbl_currentPage.Text = "1";
                lbl_recordCount.Text = dtTree.Rows.Count.ToString();
                //MessageBox.Show(endRow.ToString());
                //MessageBox.Show(dtTree.Rows.Count.ToString());
                for (int i = startRow; i < endRow; i++)
                {
                    int index = this.dataGridView1.Rows.Add();
                    //var dd = dt.Rows[i]["f_Path"];
                    //this.dataGridView1.Rows[i].Cells[0].Value = ;
                    this.dataGridView1.Rows[index].Cells["dgv_FamilyName"].Value = dtTree.Rows[i]["f_Name"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_Version"].Value = "1.0";
                    this.dataGridView1.Rows[index].Cells["dgv_Date"].Value = dtTree.Rows[i]["f_UploadDate"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_Type"].Value = familyType;
                    this.dataGridView1.Rows[index].Cells["dgv_Hash"].Value = dtTree.Rows[i]["f_Hash"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_FamilyLoc"].Value = dtTree.Rows[i]["f_Path"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_ParaLoc"].Value = dtTree.Rows[i]["f_ConfigParaLocation"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_ValLevel"].Value = dtTree.Rows[i]["f_ValLevel"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_Manufacturer"].Value = dtTree.Rows[i]["f_manufacturer"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_Source"].Value = dtTree.Rows[i]["f_Source"].ToString();
                    this.dataGridView1.Rows[index].Cells["dgv_DeviceType"].Value = dtTree.Rows[i]["f_DeviceType"].ToString();

                    try
                    {
                        if (dtTree.Rows[i]["f_Pic"].ToString() != null)
                        {
                            ftp.DownloadFile(dtTree.Rows[i]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                            string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(dtTree.Rows[i]["f_Pic"].ToString());
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
                //MessageBox.Show(eex.ToString());
            }

            #endregion

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
                tvw_Family2.Nodes.Add(treeNode);
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

        private void lbl_firstPage_Click(object sender, EventArgs e)
        {

            currentPage = 1;
            lbl_currentPage.Text = currentPage.ToString();
            int startRow = 0;
            int endRow = 0;
            List<string> picPathList = new List<string>();
            var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);

            //if (currentPage != pageCount)
            //{
            //    startRow = (currentPage - 1) * pageSize + 1;
            //    endRow = (currentPage - 1) * pageSize + pageSize;
            //}
            //else if (currentPage == pageCount)
            //{
            //    startRow = (currentPage - 1) * pageSize + 1;
            //    endRow = (currentPage - 1) * pageSize + lastPageNumCount;
            //}
            if (lastPageNumCount == dtp.Rows.Count)
            {
                endRow = lastPageNumCount;
            }
            else
            {
                endRow = pageSize;
            }
            dataGridView1.Rows.Clear();

            #region datagridview 单页显示
            for (int i = startRow; i < endRow; i++)
            {
                int index = this.dataGridView1.Rows.Add();
                //var dd = dt.Rows[i]["f_Path"];
                //MessageBox.Show(dd.ToString());
                //this.dataGridView1.Rows[i].Cells[0].Value = ;
                this.dataGridView1.Rows[index].Cells["dgv_FamilyName"].Value = dtp.Rows[i]["f_Name"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Version"].Value = "1.0";
                this.dataGridView1.Rows[index].Cells["dgv_Date"].Value = dtp.Rows[i]["f_UploadDate"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Type"].Value = familyType;
                this.dataGridView1.Rows[index].Cells["dgv_Hash"].Value = dtp.Rows[i]["f_Hash"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_FamilyLoc"].Value = dtp.Rows[i]["f_Path"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_ParaLoc"].Value = dtp.Rows[i]["f_ConfigParaLocation"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_ValLevel"].Value = dtp.Rows[i]["f_ValLevel"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Manufacturer"].Value = dtp.Rows[i]["f_manufacturer"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Source"].Value = dtp.Rows[i]["f_Source"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_DeviceType"].Value = dtp.Rows[i]["f_DeviceType"].ToString();

                try
                {
                    if (dtp.Rows[i]["f_Pic"].ToString() != null)
                    {
                        ftp.DownloadFile(dtp.Rows[i]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                        string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(dtp.Rows[i]["f_Pic"].ToString());
                        this.dataGridView1.Rows[index].Cells["dgv_Pics"].Value = Image.FromFile(iconPath);
                    }
                    else
                    { continue; }
                }
                catch (Exception erew)
                { }



            }
            #endregion


        }

        private void lbl_pervPage_Click(object sender, EventArgs e)
        {
            List<string> picPathList = new List<string>();
            var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);
            int startRow = 0;
            int endRow = 0;
            //lbl_currentPage.Text = currentPage.ToString();
            currentPage = Convert.ToInt32(lbl_currentPage.Text);
            //lbl_currentPage.Text = (currentPage).ToString();
            if (currentPage == 1)
            {
                lbl_currentPage.Text = (currentPage).ToString();
            }
            else if (currentPage == 1 && pageCount == 1)
            {
                dataGridView1.Rows.Clear();
                lbl_currentPage.Text = "1";
                startRow = 0;
                endRow = pageSize - 1;
            }
            else if (currentPage > 1)
            {
                dataGridView1.Rows.Clear();
                lbl_currentPage.Text = (currentPage - 1).ToString();
                startRow = (currentPage - 1) * pageSize - pageSize;
                endRow = (currentPage - 1) * pageSize - 1;
            }

            #region datagridview 单页显示
            for (int i = startRow; i < endRow; i++)
            {
                int index = this.dataGridView1.Rows.Add();
                //var dd = dt.Rows[i]["f_Path"];
                //MessageBox.Show(dd.ToString());
                //this.dataGridView1.Rows[i].Cells[0].Value = ;
                this.dataGridView1.Rows[index].Cells["dgv_FamilyName"].Value = dtp.Rows[i]["f_Name"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Version"].Value = "1.0";
                this.dataGridView1.Rows[index].Cells["dgv_Date"].Value = dtp.Rows[i]["f_UploadDate"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Type"].Value = familyType;
                this.dataGridView1.Rows[index].Cells["dgv_Hash"].Value = dtp.Rows[i]["f_Hash"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_FamilyLoc"].Value = dtp.Rows[i]["f_Path"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_ParaLoc"].Value = dtp.Rows[i]["f_ConfigParaLocation"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_ValLevel"].Value = dtp.Rows[i]["f_ValLevel"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Manufacturer"].Value = dtp.Rows[i]["f_manufacturer"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Source"].Value = dtp.Rows[i]["f_Source"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_DeviceType"].Value = dtp.Rows[i]["f_DeviceType"].ToString();

                try
                {
                    if (dtp.Rows[i]["f_Pic"].ToString() != null)
                    {
                        ftp.DownloadFile(dtp.Rows[i]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                        string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(dtp.Rows[i]["f_Pic"].ToString());
                        this.dataGridView1.Rows[index].Cells["dgv_Pics"].Value = Image.FromFile(iconPath);
                    }
                    else
                    { continue; }
                }
                catch (Exception erew)
                { }
            }
            #endregion

        }

        private void lbl_nextPage_Click(object sender, EventArgs e)
        {
            List<string> picPathList = new List<string>();
            var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);
            int startRow = 0;
            int endRow = 0;
            currentPage = Convert.ToInt32(lbl_currentPage.Text);
            //lbl_currentPage.Text = (currentPage).ToString();
            if (currentPage == pageCount)
            {
                lbl_currentPage.Text = (currentPage).ToString();

            }
            else if (currentPage + 1 == pageCount)
            {
                dataGridView1.Rows.Clear();
                lbl_currentPage.Text = (currentPage + 1).ToString();
                startRow = currentPage * pageSize;
                endRow = currentPage * pageSize + lastPageNumCount;

            }
            else if (currentPage + 1 < pageCount)
            {
                dataGridView1.Rows.Clear();
                lbl_currentPage.Text = (currentPage + 1).ToString();
                startRow = currentPage * pageSize;
                endRow = currentPage * pageSize + pageSize;
            }
            //MessageBox.Show(startRow.ToString() +";;;"+endRow.ToString());
            #region datagridview 单页显示
            for (int i = startRow; i < endRow; i++)
            {
                int index = this.dataGridView1.Rows.Add();
                //var dd = dt.Rows[i]["f_Path"];
                //MessageBox.Show(dd.ToString());
                //this.dataGridView1.Rows[i].Cells[0].Value = ;
                this.dataGridView1.Rows[index].Cells["dgv_FamilyName"].Value = dtp.Rows[i]["f_Name"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Version"].Value = "1.0";
                this.dataGridView1.Rows[index].Cells["dgv_Date"].Value = dtp.Rows[i]["f_UploadDate"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Type"].Value = familyType;
                this.dataGridView1.Rows[index].Cells["dgv_Hash"].Value = dtp.Rows[i]["f_Hash"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_FamilyLoc"].Value = dtp.Rows[i]["f_Path"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_ParaLoc"].Value = dtp.Rows[i]["f_ConfigParaLocation"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_ValLevel"].Value = dtp.Rows[i]["f_ValLevel"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Manufacturer"].Value = dtp.Rows[i]["f_manufacturer"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Source"].Value = dtp.Rows[i]["f_Source"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_DeviceType"].Value = dtp.Rows[i]["f_DeviceType"].ToString();

                try
                {
                    if (dtp.Rows[i]["f_Pic"].ToString() != null)
                    {
                        ftp.DownloadFile(dtp.Rows[i]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                        string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(dtp.Rows[i]["f_Pic"].ToString());
                        this.dataGridView1.Rows[index].Cells["dgv_Pics"].Value = Image.FromFile(iconPath);
                    }
                    else
                    { continue; }
                }
                catch (Exception erew)
                { }
            }
            #endregion

        }

        private void lbl_endPage_Click(object sender, EventArgs e)
        {
            List<string> picPathList = new List<string>();
            var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);
            int startRow = 0;
            int endRow = 0;
            lbl_currentPage.Text = pageCount.ToString();
            if (currentPage == pageCount)
            { }
            else if (currentPage < pageCount)
            {
                dataGridView1.Rows.Clear();
                startRow = currentPage * pageSize;
                endRow = currentPage * pageSize + lastPageNumCount;
            }

            #region datagridview 单页显示
            for (int i = startRow; i < endRow; i++)
            {
                int index = this.dataGridView1.Rows.Add();
                //var dd = dt.Rows[i]["f_Path"];
                //MessageBox.Show(dd.ToString());
                //this.dataGridView1.Rows[i].Cells[0].Value = ;
                this.dataGridView1.Rows[index].Cells["dgv_FamilyName"].Value = dtp.Rows[i]["f_Name"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Version"].Value = "1.0";
                this.dataGridView1.Rows[index].Cells["dgv_Date"].Value = dtp.Rows[i]["f_UploadDate"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Type"].Value = familyType;
                this.dataGridView1.Rows[index].Cells["dgv_Hash"].Value = dtp.Rows[i]["f_Hash"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_FamilyLoc"].Value = dtp.Rows[i]["f_Path"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_ParaLoc"].Value = dtp.Rows[i]["f_ConfigParaLocation"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_ValLevel"].Value = dtp.Rows[i]["f_ValLevel"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Manufacturer"].Value = dtp.Rows[i]["f_manufacturer"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_Source"].Value = dtp.Rows[i]["f_Source"].ToString();
                this.dataGridView1.Rows[index].Cells["dgv_DeviceType"].Value = dtp.Rows[i]["f_DeviceType"].ToString();

                try
                {
                    if (dtp.Rows[i]["f_Pic"].ToString() != null)
                    {
                        ftp.DownloadFile(dtp.Rows[i]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                        string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(dtp.Rows[i]["f_Pic"].ToString());
                        this.dataGridView1.Rows[index].Cells["dgv_Pics"].Value = Image.FromFile(iconPath);
                    }
                    else
                    { continue; }
                }
                catch (Exception erew)
                { }


            }
            #endregion


        }

        private void btn_add_Click(object sender, EventArgs e)
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
                        item1.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_Source"].Value.ToString());
                        if (this.dataGridView1.Rows[i].Cells["dgv_defaultXls"].Value != null)
                        {
                            item1.SubItems.Add(this.dataGridView1.Rows[i].Cells["dgv_defaultXls"].Value.ToString());
                        }
                        else
                        {
                            item1.SubItems.Add("002");
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            int Index = 0;
            if (this.lvw_SelectedFamilies.SelectedItems.Count > 0)//判断listview有被选中项  
            {
                Index = this.lvw_SelectedFamilies.SelectedItems[0].Index;//取当前选中项的index,SelectedItems[0]这必须为0         
                lvw_SelectedFamilies.Items[Index].Remove();
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            //dgvDataTable = dataGridView1.DataSource as DataTable;
            //将当前窗口数据传入datatable
            //dgvDataTable = null;
            //for ()
            //{ }
            //dgvDataTable.Rows = dataGridView1.Rows;

            //--------------------------------
            this.dataGridView1.Rows.Clear();
            string SearchCondition = txt_Search.Text;
            string[] SearchConditions = null;

            //进入数据库查询

            SqlConnection conSearch = new SqlConnection(sqlConnectionLocation);
            SqlCommand cmdSearch = new SqlCommand("SELECT * FROM table_p_Family WHERE f_Pro='" + familyPro + "'AND f_Cat ='" + familyType + "'", conSearch);
            SqlDataAdapter sdaSearch = new SqlDataAdapter();
            sdaSearch.SelectCommand = cmdSearch;
            DataSet dsSearch = new DataSet();
            sdaSearch.Fill(dsSearch, "Search");
            //dataGridView1.DataSource = dsSearch.Tables[0];
            dgvDataTable = dsSearch.Tables[0];


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
            DataTable dtr = new DataTable();
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
                        dtr.Rows.Add(dgvDataTable.Rows[i]);
                    }
                    else
                    { continue; }
                }
                dtp = dtr;
                lastPageNumCount = dtr.Rows.Count % pageSize;
                if (lastPageNumCount != 0)
                {
                    pageCount = dtr.Rows.Count / pageSize + 1;
                }
                else if (lastPageNumCount == 0)
                {
                    pageCount = dtr.Rows.Count / pageSize;
                }

                int startRow = 0;
                int endRow = 0;

                for (currentPage = 1; currentPage <= pageCount; currentPage++)
                {
                    if (currentPage != pageCount)
                    {
                        startRow = (currentPage - 1) * pageSize + 1;
                        endRow = (currentPage - 1) * pageSize + pageSize;
                    }
                    else if (currentPage == pageCount)
                    {
                        startRow = (currentPage - 1) * pageSize + 1;
                        endRow = (currentPage - 1) * pageSize + lastPageNumCount;
                    }

                    for (int i = 0; i < pageSize - 1; i++)
                    {
                        int index = this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[index].Cells["dgv_FamilyName"].Value = dtr.Rows[i]["f_Name"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_Version"].Value = "1.0";
                        this.dataGridView1.Rows[index].Cells["dgv_Date"].Value = dtr.Rows[i]["f_UploadDate"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_Type"].Value = familyType;
                        this.dataGridView1.Rows[index].Cells["dgv_Hash"].Value = dtr.Rows[i]["f_Hash"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_FamilyLoc"].Value = dtr.Rows[i]["f_Path"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_ParaLoc"].Value = dtr.Rows[i]["f_ConfigParaLocation"].ToString();

                        this.dataGridView1.Rows[index].Cells["dgv_ValLevel"].Value = dtr.Rows[i]["f_ValLevel"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_Manufacturer"].Value = dtr.Rows[i]["f_manufacturer"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_Source"].Value = dtr.Rows[i]["f_Source"].ToString();
                        this.dataGridView1.Rows[index].Cells["dgv_DeviceType"].Value = dtr.Rows[i]["f_DeviceType"].ToString();

                        try
                        {
                            if (dtr.Rows[i]["f_Pic"].ToString() != null)
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
                }


            }
            catch (Exception dde)
            {
                MessageBox.Show(dde.ToString());
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            yesOrNotForm yesOrNotForm = new yesOrNotForm();
            yesOrNotForm.ShowDialog();

            if (yesOrNotForm.judgeFlag == false)
            { }
            else
            {
                //获取要删除条目的hash
                List<string> ToBeDeletes = new List<string>();
                for (int i = 0; i < lvw_SelectedFamilies.Items.Count; i++)
                {
                    ToBeDeletes.Add(lvw_SelectedFamilies.Items[i].SubItems[8].Text);
                }

                //ftp删除
                //var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);

                //sql删除
                foreach (var ToBeDelete in ToBeDeletes)
                {
                    SqlConnection conDelete = new SqlConnection(sqlConnectionLocation);
                    conDelete.Open();
                    SqlCommand cmdDelete = new SqlCommand();
                    cmdDelete.Connection = conDelete;
                    cmdDelete.CommandText = "DELETE FROM table_p_Family WHERE f_Hash = '" + ToBeDelete + "'";
                    cmdDelete.CommandType = CommandType.Text;
                    int arrayNum = Convert.ToInt32(cmdDelete.ExecuteNonQuery());
                    MessageBox.Show("已删除" + arrayNum + "个族");
                }
            }
 
        }

        private void tvw_Family2_MouseCaptureChanged(object sender, EventArgs e)
        {
            TreeNode tn = new TreeNode();
            try
            {
                foreach (TreeNode x in this.tvw_Family2.SelectedNode.Parent.Nodes)
                {
                    if (x.IsSelected == true)
                    {
                        tn = x;
 
                    }
                }
            }
            catch (Exception exx)
            {  }

            if (tn != null && tn.Parent != null)
            {
   
                familyPro = tn.Parent.ToString().Replace("TreeNode:","").Replace(" ", "");
                familyType = tn.ToString().Replace("TreeNode:", "").Replace(" ", "");

                lbl_familyPro.Text = familyPro;
                lbl_familyCat.Text = familyType;
                //lbl_familyPro.Refresh();
                //lbl_familyCat.Refresh();
            }

        }

        private void lbl_familyPro_Click(object sender, EventArgs e)
        {
            //lbl_familyPro.Text = familyPro;
            //lbl_familyPro.Refresh();
            
        }

        private void lbl_familyCat_Click(object sender, EventArgs e)
        {
            //lbl_familyCat.Text = familyType;
            //lbl_familyCat.Refresh();
        }

        private void btn_transform_Click(object sender, EventArgs e)
        {
            yesOrNotForm yesOrNotForm = new yesOrNotForm();
            yesOrNotForm.ShowDialog();

            if (yesOrNotForm.judgeFlag == false)
            { }
            else
            {
                //获取要转移条目的hash
                List<string> ToBeUpdates = new List<string>();
                for (int i = 0; i < lvw_SelectedFamilies.Items.Count; i++)
                {
                    ToBeUpdates.Add(lvw_SelectedFamilies.Items[i].SubItems[8].Text);
                }

                //ftp转移     虽然文件结构上来说仍在原有专业类型下  但鉴于开发和运行效率 目前没有必要转移
                //var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);

                //sql转移
                foreach (var ToBeUpdate in ToBeUpdates)
                {
                    SqlConnection conUpdate = new SqlConnection(sqlConnectionLocation);
                    conUpdate.Open();
                    SqlCommand cmdUpdate = new SqlCommand();
                    cmdUpdate.Connection = conUpdate;
                    cmdUpdate.CommandText = "UPDATE table_p_Family SET f_Pro = '" + familyPro + "'" + " WHERE f_Hash = '" + ToBeUpdate + "'";
                    cmdUpdate.CommandText = "UPDATE table_p_Family SET f_Cat = '" + familyType + "'" + " WHERE f_Hash = '" + ToBeUpdate + "'";
                    cmdUpdate.CommandType = CommandType.Text;
                    int arrayNum = Convert.ToInt32(cmdUpdate.ExecuteNonQuery());
                    MessageBox.Show("已转移" + arrayNum + "个");
                }
            }
        }

        private void btn_confChange_Click(object sender, EventArgs e)
        {
            yesOrNotForm yesOrNotForm = new yesOrNotForm();
            yesOrNotForm.ShowDialog();

            if (yesOrNotForm.judgeFlag == false)
            { }
            else
            {
                List<string> ToBeChangedHashs = new List<string>();
                for (int i = 0; i < lvw_SelectedFamilies.Items.Count; i++)
                {
                    //MessageBox.Show("0:" + lvw_SelectedFamilies.Items[i].SubItems[0].Text);
                    //MessageBox.Show("1:" + lvw_SelectedFamilies.Items[i].SubItems[1].Text);
                    //MessageBox.Show("2:" + lvw_SelectedFamilies.Items[i].SubItems[2].Text);
                    //MessageBox.Show("3:" + lvw_SelectedFamilies.Items[i].SubItems[3].Text);
                    //MessageBox.Show("4:" + lvw_SelectedFamilies.Items[i].SubItems[4].Text);
                    //MessageBox.Show("5:" + lvw_SelectedFamilies.Items[i].SubItems[5].Text);
                    //MessageBox.Show("6:" + lvw_SelectedFamilies.Items[i].SubItems[6].Text);
                    //MessageBox.Show("7:" + lvw_SelectedFamilies.Items[i].SubItems[7].Text);
                    //MessageBox.Show("8:" + lvw_SelectedFamilies.Items[i].SubItems[8].Text);
                    //MessageBox.Show("9:" + lvw_SelectedFamilies.Items[i].SubItems[9].Text);

                    //从数据库获取原有该条目所有数据
                    string ToBeChangedHash = lvw_SelectedFamilies.Items[i].SubItems[8].Text;
                    ToBeChangedHashs.Add(ToBeChangedHash);
                    SqlConnection conChangeColumn = new SqlConnection(sqlConnectionLocation);
                    SqlDataAdapter sdaChangeColumn = new SqlDataAdapter();
                    SqlCommand cmdChangeColumn = new SqlCommand("SELECT * FROM table_p_Family WHERE f_Hash = '" + ToBeChangedHash + "'", conChangeColumn);
                    sdaChangeColumn.SelectCommand = cmdChangeColumn;
                    DataSet dstChangeColumn = new DataSet();
                    sdaChangeColumn.Fill(dstChangeColumn, "ChangeColumn");
                    DataTable dtChangeColomn = dstChangeColumn.Tables[0];

                    //执行更改操作  名字 电压等级 厂商 设备型号 来源
                    SqlConnection conChange = new SqlConnection(sqlConnectionLocation);
                    conChange.Open();
                    SqlCommand cmdChange = new SqlCommand();
                    cmdChange.Connection = conChange;
                    
                    if (dtChangeColomn.Rows[0]["f_Name"].ToString() != lvw_SelectedFamilies.Items[i].SubItems[1].Text)
                    {
                        cmdChange.CommandText = "UPDATE table_p_Family SET f_Name = '" + lvw_SelectedFamilies.Items[i].SubItems[1].Text + "'" + " WHERE f_Hash = '" + ToBeChangedHash + "'";
                    }
                    if (dtChangeColomn.Rows[0]["f_ValLevel"].ToString() != lvw_SelectedFamilies.Items[i].SubItems[5].Text)
                    {
                        cmdChange.CommandText = "UPDATE table_p_Family SET f_ValLevel = '" + lvw_SelectedFamilies.Items[i].SubItems[5].Text + "'" + " WHERE f_Hash = '" + ToBeChangedHash + "'";
                    } 
                    if (dtChangeColomn.Rows[0]["f_manufacturer"].ToString() != lvw_SelectedFamilies.Items[i].SubItems[6].Text)
                    {
                        cmdChange.CommandText = "UPDATE table_p_Family SET f_manufacturer = '" + lvw_SelectedFamilies.Items[i].SubItems[6].Text + "'" + " WHERE f_Hash = '" + ToBeChangedHash + "'";
                    }           
                    if (dtChangeColomn.Rows[0]["f_DeviceType"].ToString() != lvw_SelectedFamilies.Items[i].SubItems[7].Text)
                    {
                        cmdChange.CommandText = "UPDATE table_p_Family SET f_DeviceType = '" + lvw_SelectedFamilies.Items[i].SubItems[7].Text + "'" + " WHERE f_Hash = '" + ToBeChangedHash + "'";
                    }                
                    if (dtChangeColomn.Rows[0]["f_Source"].ToString() != lvw_SelectedFamilies.Items[i].SubItems[9].Text)
                    {
                        cmdChange.CommandText = "UPDATE table_p_Family SET f_Source = '" + lvw_SelectedFamilies.Items[i].SubItems[9].Text + "'" + " WHERE f_Hash = '" + ToBeChangedHash + "'";
                    }

                    cmdChange.CommandType = CommandType.Text;
                    int arrayNum = Convert.ToInt32(cmdChange.ExecuteNonQuery());
                    MessageBox.Show("已修改" + arrayNum + "个单元");

                }
                //------------------------------------------------------------------------------

                ////获取要修改条目的hash
                //List<string> ToBeChanges = new List<string>();
                //for (int i = 0; i < lvw_SelectedFamilies.Items.Count; i++)
                //{
                //    ToBeChanges.Add(lvw_SelectedFamilies.Items[i].SubItems[8].Text);
                //}
                //foreach (var ToBeChange in ToBeChanges)
                //{
                //    SqlConnection conChangeColumn = new SqlConnection(sqlConnectionLocation);
                //    SqlDataAdapter sdaChangeColumn = new SqlDataAdapter();
                //    SqlCommand cmdChangeColumn = new SqlCommand("SELECT * FROM table_p_Family WHERE f_Hash = '"+ ToBeChange + "'", conChangeColumn);
                //    sdaChangeColumn.SelectCommand = cmdChangeColumn;
                //    DataSet dstChangeColumn = new DataSet();
                //    sdaChangeColumn.Fill(dstChangeColumn, "ChangeColumn");
                //    DataTable dtChangeColomn = dstChangeColumn.Tables[0];

  

                //    //SqlConnection conChange = new SqlConnection(sqlConnectionLocation);
                //    //conChange.Open();
                //    //SqlCommand cmdChange = new SqlCommand();
                //    //cmdChange.Connection = conChange;
                //    //cmdChange.CommandText = "UPDATE table_p_Family SET f_Pro = '" + familyPro + "'" + " WHERE f_Hash = '" + ToBeChange + "'";
                //    //cmdChange.CommandText = "UPDATE table_p_Family SET f_Cat = '" + familyType + "'" + " WHERE f_Hash = '" + ToBeChange + "'";
                //    //cmdChange.CommandType = CommandType.Text;
                //    //int arrayNum = Convert.ToInt32(cmdChange.ExecuteNonQuery());
                //    //MessageBox.Show("已转移" + arrayNum + "个");
                //}

            }
        }

        private void RevitFamilyManagerFormED_Load(object sender, EventArgs e)
        {

        }



        /*
         *  SqlConnection conn = new SqlConnection(sqlConnectionLocation);
            SqlCommand cmd = new SqlCommand("SELECT * FROM table_p_Family",conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataSet dst = new DataSet();
            sda.Fill(dst, "cs");
            dataGridView2.DataSource = dst.Tables[0];
         */

    }


 


}
