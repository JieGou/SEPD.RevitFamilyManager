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
    using SEPD.RevitFamilyPlatform;

    public partial class RevitFamilyManagerFormULS : System.Windows.Forms.Form
    {
        //默认链接参数
        #region  默认链接参数
        string serverIP = "10.193.217.38";
        string ftpConnectionLocation = "ftp://10.193.217.38/";
        string sqlConnectionLocation = "server=10.193.217.38,1433;uid=sa;Password=SanWei2209;database=DB_Family_Library";
        string ftpUserName = "Administrator";
        string ftpPassword = "LvJW2209";
        string sqlUserName = "sa";
        string sqlPassword = "SanWei2209";
        string sqlDatabase = "DB_Family_Library";
        string xlsLocation = @"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\parameter_excel\";
        string downloadLocation = @"C:\RFA_TMP\";
        string defaultParameterXls = @"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\parameter_excel\withoutParameter.xls";

        #endregion
        public string eeeeee = null;

        ExecuteEventUS ExecuteEventUS = null;
        ExternalEvent ExternalEventUS = null;
        public ExternalEvent ExEvent { get; set; }

        private void RevitFamilyManagerFormUL_Load(object sender, EventArgs e)
        { 
            ExecuteEventUS = new ExecuteEventUS();
            ExternalEventUS = ExternalEvent.Create(ExecuteEventUS);

            familyInfo.Columns.Add("familyPathss", typeof(String));
            familyInfo.Columns.Add("familyPicss", typeof(String));
            familyInfo.Columns.Add("familyParass", typeof(String));

        }
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

        //初始化日志监控
        LogMonitor LogMonitor = new LogMonitor();

        

        #region 全窗口拖动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void RevitFamilyManagerFormUL_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x0112, 0xF012, 0);
        }
        #endregion

        //所有类型

        string txt_SelectedXlsPath = null;
        List<Family> searchFamilyList = new List<Family>();

        public DataTable familyInfo = new DataTable("familyInformation");


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

        public string originFamilyPath;
        public string originFamilyName;

        public string modifyFamilyPath;
        public string modifyFamilyName;

        public string ParaPath = null;
        public string ParaName = null;
        public string originParaPath = null;
        public string originParaName = null;

        public string modifyParaPath = null;
        public string modifyParaName = null;

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

        public List<string> originFamilyPaths = new List<string>();
        public List<string> originFamilyNames = new List<string>();
        public List<string> familyPathss = new List<string>();

        public RevitFamilyManagerFormULS()
        {
            InitializeComponent();
            configFile();
            //ExecuteEventU = new ExecuteEventU();
            //ExternalEventU = ExternalEvent.Create(ExecuteEventU);

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

            //显示树状图
            this.SetRoot();
            //this.SetRoot2();
            List<string> xlsFileNames = GetDirFile.FTPGetFile(ftpConnectionLocation + "DB_Family_Library_FTP/precast/", ftpUserName, ftpPassword);
            //foreach (var xls in xlsFileNames)
            //{ cmb_Precast.Items.Add(xls); }
            if (allFamilyTypes == null)
            {
                MessageBox.Show("!!!!!!!!!!!!!");
            }
            else
            {
                foreach (var aft in allFamilyTypes)
                {
                    //cmb_allFamilyTypes.Items.Add(aft);
                }
            }
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
            SetBrench2(pathsss, tnodes);

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
            { }

            if (tn != null && tn.Parent != null)
            {
                familyPro = tn.Parent.Name;
                familyType = tn.Name;

                lbl_ProText.Text = familyPro;
                lbl_TypeText.Text = familyType;

            }
            #region datagridview显示操作

            //sql查询
            //sql链接初始化
            SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
            //SqlCompose.Con.Close();
            //this.dataGridView1.Rows.Clear();

            string readPath = "SELECT * FROM table_p_Family WHERE f_Path LIKE '%" + @"/DB_Family_Library_FTP/" + familyPro + "/" + familyType + "/%'";
            DataSet ds = SqlCompose.ExecuteSqlQuery(readPath);
            DataTable dt = new DataTable();
            //MessageBox.Show(ds.Tables[1].ToString());
            dt = ds.Tables[0];
            dgvDataTable = dt;
            try
            {
                //MessageBox.Show(dt.Rows[1]["f_Path"].ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //int index = this.dataGridView1.Rows.Add();
                    ////var dd = dt.Rows[i]["f_Path"];
                    ////MessageBox.Show(dd.ToString());
                    ////this.dataGridView1.Rows[i].Cells[0].Value = ;
                    //this.dataGridView1.Rows[index].Cells["dgv_FamilyName"].Value = dt.Rows[i]["f_Name"].ToString();
                    //this.dataGridView1.Rows[index].Cells["dgv_Version"].Value = "1.0";
                    //this.dataGridView1.Rows[index].Cells["dgv_Date"].Value = dt.Rows[i]["f_UploadDate"].ToString();
                    //this.dataGridView1.Rows[index].Cells["dgv_Type"].Value = familyType;
                    //this.dataGridView1.Rows[index].Cells["dgv_Hash"].Value = dt.Rows[i]["f_Hash"].ToString();
                    //this.dataGridView1.Rows[index].Cells["dgv_FamilyLoc"].Value = dt.Rows[i]["f_Path"].ToString();
                    //this.dataGridView1.Rows[index].Cells["dgv_ParaLoc"].Value = dt.Rows[i]["f_ConfigParaLocation"].ToString();
                    //this.dataGridView1.Rows[index].Cells["dgv_ValLevel"].Value = dt.Rows[i]["f_ValLevel"].ToString();
                    //this.dataGridView1.Rows[index].Cells["dgv_Manufacturer"].Value = dt.Rows[i]["f_manufacturer"].ToString();
                    //this.dataGridView1.Rows[index].Cells["dgv_Source"].Value = dt.Rows[i]["f_Source"].ToString();

                }



            }
            catch (Exception eex)
            {
                MessageBox.Show(eex.ToString());
            }

            #endregion


        }

        //lbl_ProText.Text = familyPro;
        //    lbl_TypeText.Text = familyType;
        private void btn_openConfigParaPath_Click(object sender, EventArgs e)
        {
            try
            {
                string openFileDialogLastTimeTextfilePath = @"C:\ProgramData\Autodesk\Revit\Addins\2016\openConfigParaPathLastTime.txt";
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

                //ReviewLocalFamily ReviewLocalFamily = new ReviewLocalFamily();
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = lastTimeOpenLocation;
                openFileDialog.Filter = "excel文件|*.xls|所有文件|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK && openFamilyName != null)
                {
                    string pPath = openFileDialog.FileName.ToString();//获取用户选择的文件路径

                    //MessageBox.Show(Path);
                    string pName = pPath.Substring(pPath.LastIndexOf("\\") + 1);//获取用户选择的文件名称

                    originParaPath = pPath;
                    originParaName = pName;

                    //configParaPath = pPath.Replace(pName, openFamilyName);
                    //configParaPath = pPath.Replace(".xls", "").Replace(pName, openFamilyName) + "_" + ReviewLocalFamily.getFileMD5Hash(pPath) + ".xls";
                    //configParaName = pName.Replace(".xls", "").Replace(pName, openFamilyName) + "_" + ReviewLocalFamily.getFileMD5Hash(pPath) + ".xls";
                    //File.Copy(pPath, configParaPath, true);

                    txt_openConfigParaPath.Text = pPath;
                    openConfigParaName = pName;
                }

                FileStream fs2 = new FileStream(openFileDialogLastTimeTextfilePath, FileMode.Create);
                StreamWriter sw2 = new StreamWriter(fs2);
                sw2.Write(originParaPath);
                sw2.Close();
                fs2.Close();


            }
            catch (Exception ette)
            {
                MessageBox.Show("请先选择族\n\n" + ette.ToString());
            }

            //开始读取xls文件内的关键信息//显示所选表内容

            ExcelHelper ExcelHelper = new ExcelHelper();
            DataTable configKeyPara = ExcelHelper.Reading_Excel_Information(originParaPath);
            this.dataGridView2.Rows.Clear();
            try
            {
                #region  提取 XLS 与 Family 内部信息
                for (int l = 0; l < configKeyPara.Rows.Count; l++)
                {
                    if (configKeyPara.Rows[l]["paraname"].ToString().Contains("厂家"))
                    {
                        txt_Manufacturer.Text = configKeyPara.Rows[l]["paravalue"].ToString();
                        break;
                    }
                    else
                    {
                        txt_Manufacturer.Text = "NA";

                    }

                }
                for (int l = 0; l < configKeyPara.Rows.Count; l++)
                {
                    if (configKeyPara.Rows[l]["paraname"].ToString().Contains("型号"))
                    {
                        txt_DeviceType.Text = configKeyPara.Rows[l]["paravalue"].ToString();
                        break;
                    }
                    else
                    {
                        txt_DeviceType.Text = "NA";

                    }

                }
                for (int l = 0; l < configKeyPara.Rows.Count; l++)
                {
                    if (configKeyPara.Rows[l]["paraname"].ToString().Contains("名") && configKeyPara.Rows[l]["paravalue"].ToString() != null)
                    {
                        txt_SelectedFamilyName.Text = configKeyPara.Rows[l]["paravalue"].ToString();
                        break;
                    }
                    else
                    {
                        txt_SelectedFamilyName.Text = openFamilyName.Replace(".rfa", ""); ;

                    }

                }
                for (int l = 0; l < configKeyPara.Rows.Count; l++)
                {
                    if (configKeyPara.Rows[l]["paraname"].ToString().Contains("电压等级"))
                    {
                        cmb_ValLevel.Text = configKeyPara.Rows[l]["paravalue"].ToString();
                        break;
                    }
                    else
                    {
                        cmb_ValLevel.Text = "其他";

                    }

                }
                for (int l = 0; l < configKeyPara.Rows.Count; l++)
                {
                    if (configKeyPara.Rows[l]["paraname"].ToString().Contains("来源"))
                    {
                        cmb_Source.Text = configKeyPara.Rows[l]["paravalue"].ToString();
                        break;
                    }
                    else
                    {
                        cmb_Source.Text = "NA";

                    }

                }
                #endregion   

                for (int i = 0; i < configKeyPara.Rows.Count; i++)
                {
                    int index = this.dataGridView2.Rows.Add();
                    this.dataGridView2.Rows[index].Cells[0].Value = configKeyPara.Rows[i]["paraname"].ToString();
                    this.dataGridView2.Rows[index].Cells[1].Value = configKeyPara.Rows[i]["paravalue"].ToString();
                    this.dataGridView2.Rows[index].Cells[2].Value = configKeyPara.Rows[i]["paratag"].ToString();
                }
            }
            catch (Exception excs)
            {
                MessageBox.Show(excs.ToString());
            }
        }

        private void btn_openFamilyPath_Click(object sender, EventArgs e)
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
                //string fPath = openFileDialog.FileName.ToString();//获取用户选择的文件路径
                //string fName = fPath.Substring(fPath.LastIndexOf("\\") + 1);//获取用户选择的文件名称

                //originFamilyPath = fPath;
                //originFamilyName = fName;
                //txt_SelectedFamilyName.Text = fName.Replace(".rfa", "");

                //foreach (string fn in openFileDialog.FileNames)
                //{ }
                //familyPath = fPath;
                //txt_openFamilyPath.Text = fPath;
                //openFamilyName = fName;
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
                    familyInfo.Rows[i]["familyPathss"] = fPath;

                    //familyPath = fPath.Replace(".rfa", "") + "_" + ReviewLocalFamily.getFileMD5Hash(fPath) + ".rfa";
                    //familyName = fName.Replace(".rfa", "") + "_" + ReviewLocalFamily.getFileMD5Hash(fPath) + ".rfa";
                    //File.Copy(fPath, familyPath, true);

                    familyPath = fPath;
                    txt_openFamilyPath.Text = fPath;
                    openFamilyName = fName;
                    familyPathss.Add(fPath);

                    #region 获得rfa缩略图
                    localPicPath = getFilePics.getFilePic0(fPath);
                    //放入族缩略图路径
                    familyInfo.Rows[i]["familyPicss"] = fPath;
                    //pictureBox1.BackgroundImage = Image.FromFile(localPicPath);
                    #endregion
 
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
            ExecuteEventUS.familyFilePaths = familyPathss;
            //ExecuteEventUS.XlsFilePath = Path.GetDirectoryName(familyPathss[0]) + "\\";
            ExternalEventUS.Raise();
            #endregion


        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            //创建datatable
            DataTable dta = new DataTable("Table_Excel");
            //创建带列名的列
            dta.Columns.Add("paraname", typeof(String));
            dta.Columns.Add("paravalue", typeof(String));
            dta.Columns.Add("paratag", typeof(String));

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value != null)
                {
                    dta.Rows.Add();

                    dta.Rows[i][0] = dataGridView2.Rows[i].Cells[0].Value.ToString();
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
                        dta.Rows[i][2] = "NO";
                    }
                    else
                    {
                        dta.Rows[i][2] = dataGridView2.Rows[i].Cells[2].Value.ToString();
                    }

                }
                else if (dataGridView2.Rows[i].Cells[0].Value != null)
                {
                    continue;
                }

                ExcelHelper excelHelper = new ExcelHelper();
                string saveAsPath = excelHelper.Creating_Excel_Information(dta);
                txt_SelectedXlsPath = Path.GetDirectoryName(txt_openConfigParaPath.Text) + "\\" + Path.GetFileName(txt_openConfigParaPath.Text);
                File.Copy(saveAsPath, txt_SelectedXlsPath, true);
                File.Delete(saveAsPath);
            }

        }

        private void btn_UploadFamily_Click(object sender, EventArgs e)
        {
 
            LogMonitor.logMonitorUpdate(sqlConnectionLocation, "族库平台：上传,专业：" + lbl_ProText.Text.ToString() + ",类型：" + lbl_TypeText.Text.ToString() + "," + txt_SelectedFamilyName.Text.ToString());
            //cmb_allFamilyTypes.Text = lbl_TypeText.Text.ToString();
            //if (cmb_allFamilyTypes.Text.ToString() == null)
            //{ MessageBox.Show("请选择族类型"); }
            if (lbl_ProText.Text.ToString() == "NA" || lbl_TypeText.Text.ToString() == "NA")
            { MessageBox.Show("请选择族类型"); }
            else
            {
                try
                {
                    //sql链接初始化
                    SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
                    ReviewLocalFamily ReviewLocalFamily = new ReviewLocalFamily();
                    string familyHash = ReviewLocalFamily.getFileMD5Hash(familyPath.ToString());


                    //判断库内是否已有相同模型
                    string checkSameFamilySQL = "SELECT * FROM table_p_Family WHERE f_Hash=" + "'" + familyHash + "'";
                    //MessageBox.Show(checkSameFamilySQL);
                    DataSet ds = SqlCompose.ExecuteSqlQuery(checkSameFamilySQL);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    //MessageBox.Show(ds.Tables[0].Rows[0]["f_Name"].ToString());
                    //MessageBox.Show(dt.Rows.Count.ToString());
                    DecisionForm DecisionForm = new DecisionForm(dt);
                    DecisionForm.Continue = true;
                    if (dt.Rows.Count.ToString() != "0")
                    {

                        MessageBox.Show("库中已经存在相同模型文件" + dt.Rows.Count.ToString() + "个");
                        DecisionForm.ShowDialog();
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

                    if (txt_SelectedFamilyName.Text == null && txt_SelectedFamilyName.Text == "NA")
                    {
                        MessageBox.Show("请填写设备名称");
                    }
                    else
                    {
                        //familyName0 = txt_SelectedFamilyName.Text.ToString();
                        upLoadKey++;
                    }
                    if (upLoadKey < 5)
                    { MessageBox.Show("请将信息填写完整，否则无法上传！"); }

                    #endregion

                    #region 判断族名和表名问题
 
                    bool isSameName = true;
                     
                    #endregion

                    
                    for (int k = 0; k < familyInfo.Rows.Count ; k++)
                    {
                        originFamilyPath = familyInfo.Rows[k]["familyPathss"].ToString();
                        originFamilyName = Path.GetFileName(originFamilyPath);
                        originParaPath = familyInfo.Rows[k]["familyParass"].ToString();
                        originParaName = Path.GetFileName(originParaPath);
                        localPicPath = familyInfo.Rows[k]["familyPicss"].ToString();

                        familyName0 = originFamilyName;

                        if (DecisionForm.Continue == false)
                        {
                            //等待其他操作
                        }
                        else if (DecisionForm.Continue == true && upLoadKey >= 5 && isSameName == true)
                        {

                            //FTP操作  需确定上传至文件服务器路径
                            var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);
                            //string tagetFtpDir = @"DB_Family_Library_FTP\" + familyPro + "\\" + familyType + "\\";
                            string tagetFtpDir = @"DB_Family_Library_FTP\" + familyPro + "\\" + lbl_TypeText.Text.ToString() + "\\";
                            //MessageBox.Show(familyPro);
                            //MessageBox.Show(familyType);
                            string tagetFtpDirPara = @"DB_Family_Library_FTP\paracast\";
                            //处理族文件名称路径
                            modifyFamilyPath = originFamilyPath.Replace(".rfa", "") + "_" + ReviewLocalFamily.getFileMD5Hash(originFamilyPath) + ".rfa";
                            modifyFamilyName = originFamilyName.Replace(".rfa", "") + "_" + ReviewLocalFamily.getFileMD5Hash(originFamilyPath) + ".rfa";
                            File.Copy(originFamilyPath, modifyFamilyPath, true);
                            //MessageBox.Show("族路径已处理" + modifyFamilyPath.ToString());

                            //处理表文件名称路径

                            //modifyParaPath = originParaPath.Replace(".rfa", "") + "_" + ReviewLocalFamily.getFileMD5Hash(originParaPath) + ".rfa";
                            //modifyParaName = originParaName.Replace(".rfa", "") + "_" + ReviewLocalFamily.getFileMD5Hash(originParaName) + ".rfa";
                            modifyParaPath = originParaPath.Replace(".xls", "") + ReviewLocalFamily.getFileMD5Hash(originParaPath) + ".xls";
                            modifyParaName = originParaName.Replace(".xls", "") + ReviewLocalFamily.getFileMD5Hash(originParaPath) + ".xls";
                            //modifyParaPath = originParaPath.Replace(originParaName, originFamilyName);
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
                            //清除临时哈文件项
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
                catch (Exception efc)
                { MessageBox.Show(efc.ToString()); }
            }
        }

        private void lbl_ProText_Click(object sender, EventArgs e)
        {

        }

        private void txt_DeviceType_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_winclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_winminiz_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_winmaxiz_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btn_defaultXls_Click(object sender, EventArgs e)
        {
            txt_openConfigParaPath.Text = @"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\parameter_excel\withoutParameter.xls";
            dataGridView2.Rows.Clear();

            if (txt_openConfigParaPath.Text.ToString() == defaultParameterXls)
            {
                originParaPath = txt_openConfigParaPath.Text;
                openConfigParaName = txt_openConfigParaPath.Text.Substring(originParaPath.LastIndexOf("\\") + 1);
                originParaName = txt_openConfigParaPath.Text.Substring(originParaPath.LastIndexOf("\\") + 1);
            }
        }

        private void btn_checkpara_Click(object sender, EventArgs e)
        {
            string xlsFilePathLog = @"C:\ProgramData\Autodesk\Revit\Addins\2016\xlsFilePath.txt";
 
            if (File.Exists(xlsFilePathLog))
            {
                StreamReader sr = new StreamReader(xlsFilePathLog, Encoding.GetEncoding("UTF-8"));
 
                //for (int ik = 0; ; ik++)
                //{
                //    MessageBox.Show(sr.ReadLine());
                //    if (sr.ReadLine() != "" && sr.ReadLine() != null)
                //    {  
                //        familyInfo.Rows[ik]["familyParass"] = sr.ReadLine().ToString();
                //    }
                //    else
                //    {
                //        break;
                //    }
                //}
                //for ( int eo = 0; eo < familyInfo.Rows.Count; eo++)
                //{
                //    MessageBox.Show(familyInfo.Rows[eo]["familyParass"].ToString());
                //}

                string[] line = File.ReadAllLines(xlsFilePathLog);
                for (int i = 0; i < line.Length ;i++)
                {
                    MessageBox.Show(line[i]);
                    familyInfo.Rows[i]["familyParass"] = line[i];
                }

                MessageBox.Show(familyInfo.Rows.Count.ToString());
                sr.Close();
            }
            else
            {
                MessageBox.Show("no path");
            }

            //txt_openConfigParaPath.Text = ExecuteEventU.XlsFilePath;
            //originParaPath = txt_openConfigParaPath.Text;
            //originParaName = txt_openConfigParaPath.Text.Substring(originParaPath.LastIndexOf("\\") + 1);
            //MessageBox.Show(originParaPath);
            //开始读取xls文件内的关键信息//显示所选表内容

            //ExcelHelper ExcelHelper = new ExcelHelper();
            //DataTable configKeyPara = ExcelHelper.Reading_Excel_Information(originParaPath);

        }
    }

}
