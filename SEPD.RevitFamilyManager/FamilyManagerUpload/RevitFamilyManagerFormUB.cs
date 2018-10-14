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
    public partial class RevitFamilyManagerFormUB : System.Windows.Forms.Form
    {

        ExecuteEventUB ExecuteEventUB = null;
        ExternalEvent ExternalEventUB = null;
        public ExternalEvent ExEvent { get; set; }
 
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

        public int count = 0;
        public int pageSize = 10;      //每页记录数
        public int recordCount = 0;    //总记录数
        public int pageCount = 0;      //总页数
        public int currentPage = 0;    //当前页
        public int lastPageNumCount = 0;
        DataTable dtSource = new DataTable();
        DataTable dtp = new DataTable();
        public int currentDgvRow = 0;

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


        //所有类型

        string txt_SelectedXlsPath = null;
        List<Family> searchFamilyList = new List<Family>();

        public DataTable familyInfo = new DataTable("familyInformation");
 
 

        public string originFamilyPath;
        public string originFamilyName;

        public string modifyFamilyPath;
        public string modifyFamilyName;
 
        public string originParaPath = null;
        public string originParaName = null;

        public string modifyParaPath = null;
        public string modifyParaName = null;

        //已确定参数值xls的位置及名称
 
        public List<string> originFamilyPaths = new List<string>();
        public List<string> originFamilyNames = new List<string>();
        public List<string> familyPathss = new List<string>();
        public Document docc;

        #endregion


        #region 全窗口拖动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void RevitFamilyManagerFormUB_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x0112, 0xF012, 0);
        }
        #endregion

        public RevitFamilyManagerFormUB(Document Revit_Doc):base()

        {
            docc = Revit_Doc;
        }
        public RevitFamilyManagerFormUB()
        {

            InitializeComponent();

            #region 下拉框初始化
            //电压等级初始化
            cmb_ValLevel.Items.Add("1000kV");
            cmb_ValLevel.Items.Add("500kV");
            cmb_ValLevel.Items.Add("220kV");
            cmb_ValLevel.Items.Add("110kV");
            cmb_ValLevel.Items.Add("66kV");
            cmb_ValLevel.Items.Add("35kV");
            cmb_ValLevel.Items.Add("10kV");
            cmb_ValLevel.Items.Add("0.4kV");
            cmb_ValLevel.Items.Add("其他");
            //
            cmb_Source.Items.Add("Autodesk_Default_Family");
            cmb_Source.Items.Add("科技信息部预设");
            cmb_Source.Items.Add("SEPD-KXB-OLD-VERSION");
            cmb_Source.Items.Add("《输变电工程数字化移交标准》");
            cmb_Source.Items.Add("《城市轨道交通BIM应用系列标准》");
            cmb_Source.Items.Add("其他");
            #endregion
 
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

            if (tn != null && tn.Parent != null)
            {
                familyPro = tn.Parent.Name;
                familyType = tn.Name;

                lbl_ProText.Text = familyPro;
                lbl_TypeText.Text = familyType;

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

            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            ////sql查询
            ////sql链接初始化
            //SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
            ////SqlCompose.Con.Close();
            //this.dataGridView1.Rows.Clear();

            //string readPath = "SELECT * FROM table_p_Family WHERE f_Cat ='" + familyType + "'";
            //DataSet ds = SqlCompose.ExecuteSqlQuery(readPath);
            //DataTable dtTree = new DataTable();
            ////MessageBox.Show(ds.Tables[1].ToString());
            //dtTree = ds.Tables[0];
            //dgvDataTable = dtTree;
            //dtp = dtTree;
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
            //    //MessageBox.Show(endRow.ToString());
            //    //MessageBox.Show(dtTree.Rows.Count.ToString());
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

        private void RevitFamilyManagerFormUB_Load(object sender, EventArgs e)
        {
            ExecuteEventUB = new ExecuteEventUB();
            ExternalEventUB = ExternalEvent.Create(ExecuteEventUB);

            familyInfo.Columns.Add("familyNamess", typeof(String));
            familyInfo.Columns.Add("familyPathss", typeof(String));    
            familyInfo.Columns.Add("familyParass", typeof(String));
            familyInfo.Columns.Add("familyPicss", typeof(String));

        }

        private void btn_selectrfa_Click(object sender, EventArgs e)
        {
            //DataTable familyInfo = new DataTable("familyInformation");
            familyInfo.Clear();

            string openFileDialogLastTimeTextfilePath = @"C:\ProgramData\Autodesk\Revit\Addins\2016\openFamilyPathLastTime.txt";
            string lastTimeOpenLocation = @"c:\";
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
            string fPathO = null;

            if (openFileDialog.ShowDialog() == DialogResult.OK /*&& openFamilyName != null*/)
            {
                
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    familyInfo.Rows.Add();

                    string fPath = openFileDialog.FileNames[i].ToString();//获取用户选择的文件路径
                    string fName = fPath.Substring(fPath.LastIndexOf("\\") + 1);//获取用户选择的文件名称
                    //familyPathss = openFileDialog.FileNames;

                    originFamilyPath = fPath;
                    originFamilyName = fName;
                    originFamilyPaths.Add(originFamilyPath);
                    originFamilyNames.Add(originFamilyName);

                    //放入族路径
                    familyInfo.Rows[i]["familyNamess"] = fName;
                    familyInfo.Rows[i]["familyPathss"] = fPath;
                    #region 获得rfa缩略图  familyInfo.Rows[i]["familyPicss"] = fPath 
                    localPicPath = getFilePics.getFilePic0(fPath);
                    //放入族缩略图路径
                    familyInfo.Rows[i]["familyPicss"] = localPicPath;
                    //pictureBox1.BackgroundImage = Image.FromFile(localPicPath);
                    #endregion

                    //familyPath = fPath.Replace(".rfa", "") + "_" + ReviewLocalFamily.getFileMD5Hash(fPath) + ".rfa";
                    //familyName = fName.Replace(".rfa", "") + "_" + ReviewLocalFamily.getFileMD5Hash(fPath) + ".rfa";
                    //File.Copy(fPath, familyPath, true);

                    familyPath = fPath;
                    //txt_openFamilyPath.Text = fPath;
                    openFamilyName = fName;
                    familyPathss.Add(fPath);

                     DataUB.dataTempTable =   familyInfo;
                    //MessageBox.Show(DataUB.dataTempTable.Rows[0]["familyNamess"].ToString());
                    if (DataUB.dataTempTable == null)
                    {
                        MessageBox.Show("DataUB.dataTempTable == null");
                    }
                    //Document doc = app.ActiveUIDocument.Document;
                    //Document rfadoc = app.Application.OpenDocumentFile(familyFilePaths[k]);
                    //Document Revit_Doc = commandData.Application.ActiveUIDocument.Document;
                    //MessageBox.Show(fPath);
                    //Document rfadoc = commandData.Application.Application.OpenDocumentFile(fPath);
                    //MessageBox.Show(rfadoc.PathName);

                }
            }

            FileStream fs2 = new FileStream(openFileDialogLastTimeTextfilePath, FileMode.Create);
            StreamWriter sw2 = new StreamWriter(fs2);
            sw2.Write(familyPath);
            sw2.Close();
            fs2.Close();


            //获得rfa缩略图    传入族路径familyPath
            //#region  获得缩略图文件

            //localPicPath = getFilePics.getFilePic0(familyPath);
            ////MessageBox.Show(localPicPath.ToString());
            //pictureBox1.BackgroundImage = Image.FromFile(localPicPath);
            //#endregion

            //#region  获得缩略图文件
            //List<string> localPicPaths = new List<string>();
            //foreach (string familyPath in familyPathss)
            //{
            //    localPicPath = getFilePics.getFilePic0(familyPath);
            //    localPicPaths.Add(localPicPath);
            //    pictureBox1.BackgroundImage = Image.FromFile(localPicPath);
            //}
            //#endregion

            #region 检查族文件自带属性
            //开启外挂事务
            ExecuteEventUB.dtUB = familyInfo;
            //ExecuteEventUB.familyFilePaths = familyPathss;
            //ExecuteEventUS.XlsFilePath = Path.GetDirectoryName(familyPathss[0]) + "\\";
            ExternalEventUB.Raise();
            #endregion
 
           
        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DataUB.dataTempTable;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ExcelHelper ExcelHelper = new ExcelHelper();
            //MessageBox.Show(DataUB.dataTempTable.Rows[0]["familyParass"].ToString());
            //MessageBox.Show(DataUB.dataTempTable.Rows[0]["familyPicss"].ToString());
            //获取行列索引
            //第一种方式
            int row = e.RowIndex ;
            int col = e.ColumnIndex ;
            localPicPath = DataUB.dataTempTable.Rows[row]["familyPicss"].ToString();
            lbl_fName.Text = DataUB.dataTempTable.Rows[row]["familyNamess"].ToString();
            //显示缩略图
            pictureBox1.BackgroundImage = Image.FromFile(localPicPath);
            string localParasPath = DataUB.dataTempTable.Rows[row]["familyParass"].ToString();
            
            DataTable dtpara = ExcelHelper.Reading_Excel_Information(localParasPath);
            this.dataGridView2.DataSource = dtpara;

            txt_defaultXls.Text = localParasPath;
 
        }
  
        private void btn_selectxls_Click(object sender, EventArgs e)
        {
            

            DataTable dt = null;
            ExcelHelper ExcelHelper = new ExcelHelper();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "excel文件|*.xls|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string pPath = openFileDialog.FileName.ToString();//获取用户选择的文件路径
                string pName = pPath.Substring(pPath.LastIndexOf("\\") + 1);//获取用户选择的文件名称

 

                txt_defaultXls.Text = pPath;
                MessageBox.Show(txt_defaultXls.Text);

                //---------------------------------------------------------------------------------
                try
                {

                    DataUB.dataTempTable.Rows[currentDgvRow]["familyParass"] = pPath;
                    dt = ExcelHelper.Reading_Excel_Information(pPath);
    
                    dataGridView1.DataSource = DataUB.dataTempTable;
                    dataGridView2.DataSource = dt; 
                }
                catch (Exception dde)
                {
                    MessageBox.Show(dde.ToString());
                }

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
 
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value != null)
                {
                    dta.Rows.Add();

                    dta.Rows[i][0] = dataGridView2.Rows[i].Cells[0].Value.ToString();
                    //MessageBox.Show(dta.Rows[i][0].ToString());
                    if (dataGridView2.Rows[i].Cells[1].Value == null)
                    {
                        dta.Rows[i][0] = "NA";
                    }
                    else
                    {
                        dta.Rows[i][1] = dataGridView2.Rows[i].Cells[1].Value.ToString();
                    }
                    if (dataGridView2.Rows[i].Cells[2].Value == null)
                    {
                        dta.Rows[i][2] = "NA";
                    }
                    else
                    {
                        dta.Rows[i][2] = dataGridView2.Rows[i].Cells[2].Value.ToString();
                    }
                    if (dataGridView2.Rows[i].Cells[3].Value == null)
                    {
                        dta.Rows[i][3] = "NA";
                    }
                    else
                    {
                        dta.Rows[i][3] = dataGridView2.Rows[i].Cells[3].Value.ToString();
                    }
                    if (dataGridView2.Rows[i].Cells[4].Value == null)
                    {
                        dta.Rows[i][4] = "NA";
                    }
                    else
                    {
                        dta.Rows[i][4] = dataGridView2.Rows[i].Cells[4].Value.ToString();
                    }

                }
                else if (dataGridView2.Rows[i].Cells[0].Value != null)
                {
                    continue;
                }

                ExcelHelper excelHelper = new ExcelHelper();
                string saveAsPath = excelHelper.Creating_Excel_Information(dta);
                txt_SelectedXlsPath = Path.GetDirectoryName(DataUB.dataTempTable.Rows[currentDgvRow]["familyParass"].ToString()) + "\\" + Path.GetFileName(DataUB.dataTempTable.Rows[currentDgvRow]["familyParass"].ToString());
                File.Copy(saveAsPath, txt_SelectedXlsPath, true);
                File.Delete(saveAsPath);
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            LogMonitor LogMonitor = new LogMonitor();
            DataTable dtUpload = DataUB.dataTempTable ;
 
            if (lbl_ProText.Text.ToString() == "NA" || lbl_TypeText.Text.ToString() == "NA")
            { MessageBox.Show("请选择族类型"); }

            for (int i = 0 ; i< dtUpload.Rows.Count ; i++)
            {

                LogMonitor.logMonitorUpdate(sqlConnectionLocation, "族库平台：上传,专业：" + lbl_ProText.Text.ToString() + ",类型：" + lbl_TypeText.Text.ToString() + "," + dtUpload.Rows[i]["familyNamess"].ToString());

                //sql链接初始化
                SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
                ReviewLocalFamily ReviewLocalFamily = new ReviewLocalFamily();
                string familyHash = ReviewLocalFamily.getFileMD5Hash(familyPath.ToString());

                //判断库内是否已有相同模型
                string checkSameFamilySQL = "SELECT * FROM table_p_Family WHERE f_Hash=" + "'" + familyHash + "'";

                DataSet ds = SqlCompose.ExecuteSqlQuery(checkSameFamilySQL);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                for (int j = 0 ; j < dt.Rows.Count ; j++)
                {
                    string exsistFamilyInfo = dt.Rows[i]["f_Name"].ToString() + "; " + dt.Rows[i]["f_UploadDate"].ToString() + "; " + dt.Rows[i]["f_ValLevel"].ToString() + "; " + dt.Rows[i]["f_DeviceType"].ToString() + "; " + dt.Rows[i]["f_manufacturer"].ToString();
                    MessageBox.Show(exsistFamilyInfo+"___更新版本");
                }

                #region  判断信息完整性
                int upLoadKey = 0;
                string familyValLevel = null;
                string familyManufacturer = null;
                string familySource = null;
                string familyDeviceType = null;
                string familyName0 = null;

                if (cmb_ValLevel.Text == null)
                {
                    MessageBox.Show("请选择电压等级");
                }
                else
                {
                    familyValLevel = cmb_ValLevel.Text.ToString();
                    upLoadKey++;
                }

                if (txt_Manufacturer.Text == null || txt_Manufacturer.Text == "NA")
                {
                    MessageBox.Show("请填写厂家");
                }
                else
                {
                    familyManufacturer = txt_Manufacturer.Text.ToString();
                    upLoadKey++;
                }

                if (cmb_Source.Text == null || cmb_Source.Text == "NA")
                {
                    MessageBox.Show("请填写需要上传的族来源");
                }
                else
                {
                    familySource = cmb_Source.Text.ToString();
                    upLoadKey++;
                }


                if (txt_DeviceType == null || txt_DeviceType.Text == "NA")
                {
                    MessageBox.Show("请填写设备型号");
                }
                else
                {
                    familyDeviceType = txt_DeviceType.Text.ToString();
                    upLoadKey++;
                }

                familyName0 = dtUpload.Rows[i]["familyNamess"].ToString();
 
                if (upLoadKey < 4)
                { MessageBox.Show("请将信息填写完整，否则无法上传！"); }

                #endregion

                if (upLoadKey >= 4)
                {

                    originFamilyPath = dtUpload.Rows[i]["familyPathss"].ToString();
                    originFamilyName = dtUpload.Rows[i]["familyNamess"].ToString();
                    originParaPath = dtUpload.Rows[i]["familyParass"].ToString();
                    originParaName = Path.GetFileName(dtUpload.Rows[i]["familyParass"].ToString());
                    localPicPath = dtUpload.Rows[i]["familyPicss"].ToString();
 
                    //FTP操作  需确定上传至文件服务器路径
                    var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);
                    string tagetFtpDir = @"DB_Family_Library_FTP\" + lbl_ProText.Text + "\\" + lbl_TypeText.Text + "\\";     
                    string tagetFtpDirPara = @"DB_Family_Library_FTP\paracast\";
                    //处理族文件名称路径
                    modifyFamilyPath = originFamilyPath.Replace(".rfa", "") + "_" + ReviewLocalFamily.getFileMD5Hash(originFamilyPath) + ".rfa";
                    modifyFamilyName = originFamilyName.Replace(".rfa", "") + "_" + ReviewLocalFamily.getFileMD5Hash(originFamilyPath) + ".rfa";
                    File.Copy(originFamilyPath, modifyFamilyPath, true);
                    //MessageBox.Show("族路径已处理" + modifyFamilyPath.ToString());

                    //处理表文件名称路径
 
                    modifyParaPath = originParaPath.Replace(".xls", "").Replace(originParaName, originFamilyName) + "_" + ReviewLocalFamily.getFileMD5Hash(originParaPath) + ".xls";
                    modifyParaName = originParaName.Replace(".xls", "").Replace(originParaName, originFamilyName) + "_" + ReviewLocalFamily.getFileMD5Hash(originParaPath) + ".xls";
                   
                    File.Copy(originParaPath, modifyParaPath, true);
                    //MessageBox.Show("族路径已处理" + modifyParaPath.ToString());

                    //MessageBox.Show("族路径已处理\n\n" + modifyFamilyPath.ToString());
                    //MessageBox.Show("族路径已处理\n\n" + tagetFtpDir.ToString());
                    ftp.UploadFile(modifyFamilyPath.ToString(), tagetFtpDir);
                    ftp.UploadFile(modifyParaPath.ToString(), tagetFtpDirPara);
                    ftp.UploadFile(localPicPath, @"DB_Family_Library_FTP\pics\");
 
                    //sql同步操作

                    //SqlHelper.Con.Close();
                    //获取需填写内容
                    string familyLocation = ftpConnectionLocation + tagetFtpDir.Replace("\\", "/") + modifyFamilyName;

                    string familySize = (ReviewLocalFamily.getFileSize(modifyFamilyPath) / 1024).ToString() + "KB";
                    string familyUploadDate = DateTime.Now.ToString();

                    string familyParaLocation = null;
                    string familyConfigParaLocation = ftpConnectionLocation + "DB_Family_Library_FTP/paracast/" + modifyParaName;
                    string familyPicLocation = ftpConnectionLocation + "DB_Family_Library_FTP/pics/" + Path.GetFileName(localPicPath);
                    //MessageBox.Show(familyPicLocation);

                    //开始sql语句操作
                    bool queryNum = false;
                    //string getFamilySQLSync = "INSERT INTO table_ParameterDefault (pmd_Resource,pmd_Location,pmd_Hash,pmd_Date) VALUES ('Administraror','" + paraLocation + "','" + paraHash + "','" + paraUploadDate + "'" + ");";
                    string getFamilySQLSync = "INSERT INTO table_p_Family "
                        + "(f_Name,f_Path,f_Value,f_Pro,f_ValLevel,f_Cat,f_Hash,f_UploadDate,f_ParaLocation,f_ConfigParaLocation,f_manufacturer,f_Source,f_DeviceType,f_Pic) "
                        + "VALUES ('" + familyName0
                        + "','" + familyLocation
                        + "','" + familySize
                        + "','" + familyPro
                        + "','" + familyValLevel
                        + "','" + familyType
                        + "','" + familyHash
                        + "','" + familyUploadDate
                        + "','" + familyParaLocation
                        + "','" + familyConfigParaLocation
                        + "','" + familyManufacturer
                        + "','" + familySource
                        + "','" + familyDeviceType
                        + "','" + familyPicLocation + "')";
                    //MessageBox.Show(getFamilySQLSync);
                    //SqlHelper.Con.Close();
                    queryNum = SqlCompose.ExecuteSqlNonQuery(getFamilySQLSync);

                    if (queryNum != false)
                    {
                        MessageBox.Show("上传完成");
                    }
                    //清除临时文件项
                    File.Delete(modifyFamilyPath);
                    File.Delete(modifyParaPath);
                    //File.Delete(localPicPath);
                }
                else
                {
                    MessageBox.Show("由于信息填写不全，目前无法上传。");
                }
            }


        }

        private void btn_uploadx_Click(object sender, EventArgs e)
        {
            count = 0;
            LogMonitor LogMonitor = new LogMonitor();
            DataTable dtUpload = DataUB.dataTempTable;

            if (lbl_ProText.Text.ToString() == "NA" || lbl_TypeText.Text.ToString() == "NA")
            { MessageBox.Show("请选择族类型"); }

            for (int i = 0; i < dtUpload.Rows.Count; i++)
            {

                LogMonitor.logMonitorUpdate(sqlConnectionLocation, "族库平台：上传,专业：" + lbl_ProText.Text.ToString() + ",类型：" + lbl_TypeText.Text.ToString() + "," + dtUpload.Rows[i]["familyNamess"].ToString());

                //sql链接初始化
                SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
                ReviewLocalFamily ReviewLocalFamily = new ReviewLocalFamily();
                string familyHash = ReviewLocalFamily.getFileMD5Hash(familyPath.ToString());

                ////判断库内是否已有相同模型
                //string checkSameFamilySQL = "SELECT * FROM table_p_Family WHERE f_Hash=" + "'" + familyHash + "'";

                //DataSet ds = SqlCompose.ExecuteSqlQuery(checkSameFamilySQL);
                //DataTable dt = new DataTable();
                //dt = ds.Tables[0];

                //for (int j = 0; j < dt.Rows.Count; j++)
                //{
                //    string exsistFamilyInfo = dt.Rows[i]["f_Name"].ToString() + "; " + dt.Rows[i]["f_UploadDate"].ToString() + "; " + dt.Rows[i]["f_ValLevel"].ToString() + "; " + dt.Rows[i]["f_DeviceType"].ToString() + "; " + dt.Rows[i]["f_manufacturer"].ToString();
                //    MessageBox.Show(exsistFamilyInfo + "___更新版本");
                //}

                #region  判断信息完整性
                int upLoadKey = 0;
                string familyValLevel = null;
                string familyManufacturer = null;
                string familySource = null;
                string familyDeviceType = null;
                string familyName0 = null;

                if (cmb_ValLevel.Text == null)
                {
                    MessageBox.Show("请选择电压等级");
                }
                else
                {
                    familyValLevel = cmb_ValLevel.Text.ToString();
                    upLoadKey++;
                }

                if (txt_Manufacturer.Text == null || txt_Manufacturer.Text == "NA")
                {
                    MessageBox.Show("请填写厂家");
                }
                else
                {
                    familyManufacturer = txt_Manufacturer.Text.ToString();
                    upLoadKey++;
                }

                if (cmb_Source.Text == null || cmb_Source.Text == "NA")
                {
                    MessageBox.Show("请填写需要上传的族来源");
                }
                else
                {
                    familySource = cmb_Source.Text.ToString();
                    upLoadKey++;
                }


                if (txt_DeviceType == null || txt_DeviceType.Text == "NA")
                {
                    MessageBox.Show("请填写设备型号");
                }
                else
                {
                    familyDeviceType = txt_DeviceType.Text.ToString();
                    upLoadKey++;
                }

                familyName0 = dtUpload.Rows[i]["familyNamess"].ToString();

                if (upLoadKey < 4)
                { MessageBox.Show("请将信息填写完整，否则无法上传！"); }

                #endregion

                if (upLoadKey >= 4)
                {

                    originFamilyPath = dtUpload.Rows[i]["familyPathss"].ToString();
                    originFamilyName = dtUpload.Rows[i]["familyNamess"].ToString();
                    originParaPath = dtUpload.Rows[i]["familyParass"].ToString();
                    originParaName = Path.GetFileName(dtUpload.Rows[i]["familyParass"].ToString());
                    localPicPath = dtUpload.Rows[i]["familyPicss"].ToString();

                    //FTP操作  需确定上传至文件服务器路径
                    var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);
                    string tagetFtpDir = @"DB_Family_Library_FTP\" + lbl_ProText.Text + "\\" + lbl_TypeText.Text + "\\";
                    string tagetFtpDirPara = @"DB_Family_Library_FTP\paracast\";
                    //处理族文件名称路径
                    modifyFamilyPath = originFamilyPath.Replace(".rfa", "") + "_" + ReviewLocalFamily.getFileMD5Hash(originFamilyPath) + ".rfa";
                    modifyFamilyName = originFamilyName.Replace(".rfa", "") + "_" + ReviewLocalFamily.getFileMD5Hash(originFamilyPath) + ".rfa";
                    File.Copy(originFamilyPath, modifyFamilyPath, true);
                    //MessageBox.Show("族路径已处理" + modifyFamilyPath.ToString());

                    //处理表文件名称路径

                    modifyParaPath = originParaPath.Replace(".xls", "").Replace(originParaName, originFamilyName) + "_" + ReviewLocalFamily.getFileMD5Hash(originParaPath) + ".xls";
                    modifyParaName = originParaName.Replace(".xls", "").Replace(originParaName, originFamilyName) + "_" + ReviewLocalFamily.getFileMD5Hash(originParaPath) + ".xls";

                    File.Copy(originParaPath, modifyParaPath, true);
                    //MessageBox.Show("族路径已处理" + modifyParaPath.ToString());

                    //MessageBox.Show("族路径已处理\n\n" + modifyFamilyPath.ToString());
                    //MessageBox.Show("族路径已处理\n\n" + tagetFtpDir.ToString());
                    ftp.UploadFile(modifyFamilyPath.ToString(), tagetFtpDir);
                    ftp.UploadFile(modifyParaPath.ToString(), tagetFtpDirPara);
                    ftp.UploadFile(localPicPath, @"DB_Family_Library_FTP\pics\");

                    //sql同步操作

                    //SqlHelper.Con.Close();
                    //获取需填写内容
                    string familyLocation = ftpConnectionLocation + tagetFtpDir.Replace("\\", "/") + modifyFamilyName;

                    string familySize = (ReviewLocalFamily.getFileSize(modifyFamilyPath) / 1024).ToString() + "KB";
                    string familyUploadDate = DateTime.Now.ToString();

                    string familyParaLocation = null;
                    string familyConfigParaLocation = ftpConnectionLocation + "DB_Family_Library_FTP/paracast/" + modifyParaName;
                    string familyPicLocation = ftpConnectionLocation + "DB_Family_Library_FTP/pics/" + Path.GetFileName(localPicPath);
                    //MessageBox.Show(familyPicLocation);

                    //开始sql语句操作
                    bool queryNum = false;
                    //string getFamilySQLSync = "INSERT INTO table_ParameterDefault (pmd_Resource,pmd_Location,pmd_Hash,pmd_Date) VALUES ('Administraror','" + paraLocation + "','" + paraHash + "','" + paraUploadDate + "'" + ");";
                    string getFamilySQLSync = "INSERT INTO table_p_Family "
                        + "(f_Name,f_Path,f_Value,f_Pro,f_ValLevel,f_Cat,f_Hash,f_UploadDate,f_ParaLocation,f_ConfigParaLocation,f_manufacturer,f_Source,f_DeviceType,f_Pic) "
                        + "VALUES ('" + familyName0
                        + "','" + familyLocation
                        + "','" + familySize
                        + "','" + familyPro
                        + "','" + familyValLevel
                        + "','" + familyType
                        + "','" + familyHash
                        + "','" + familyUploadDate
                        + "','" + familyParaLocation
                        + "','" + familyConfigParaLocation
                        + "','" + familyManufacturer
                        + "','" + familySource
                        + "','" + familyDeviceType
                        + "','" + familyPicLocation + "')";
                    //MessageBox.Show(getFamilySQLSync);
                    //SqlHelper.Con.Close();
                    queryNum = SqlCompose.ExecuteSqlNonQuery(getFamilySQLSync);

                    if (queryNum != false)
                    {
                        count++;
     
                    }
                    //清除临时文件项
                    File.Delete(modifyFamilyPath);
                    File.Delete(modifyParaPath);
                }
                else
                {
                    MessageBox.Show("由于信息填写不全，目前无法上传。");
                }
            }
            MessageBox.Show("完成上传项"+count+"个");
        }
    }
}



