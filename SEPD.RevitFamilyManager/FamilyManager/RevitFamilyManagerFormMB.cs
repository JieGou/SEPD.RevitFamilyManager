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
using System.Collections;

namespace SEPD.RevitFamilyManager
{
    public partial class RevitFamilyManagerFormMB : System.Windows.Forms.Form
    {

        #region 全窗口拖动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void RevitFamilyManagerFormMB_MouseDown(object sender, MouseEventArgs e)
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

        public RevitFamilyManagerFormMB()
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

                familyPro = tn.Parent.Name;
                familyType = tn.Name;
                List<string> fileList = GetDirFile.FTPGetFile(ftpConnectionLocation + "DB_Family_Library_FTP/" + tn.Parent.Name + "/" + tn.Name + "/", ftpUserName, ftpPassword);

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
            //this.dataGridView1.Rows.Clear();
            string readPath = "SELECT * FROM table_p_Family WHERE f_Cat ='" + familyType + "'";
            DataSet ds = SqlCompose.ExecuteSqlQuery(readPath);
            DataTable dtTree = new DataTable();
            dtTree = ds.Tables[0];
            dgvDataTable = dtTree;
            dtp = dtTree;

            dataGridView1.DataSource = dtTree;
            DataUB.dataTempTable = dtTree;
            dgvDataTable = dtTree;
            dtp = dtTree;

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
            dataGridView1.Columns["f_ParaLocation"].HeaderText = "属性表路径";
            dataGridView1.Columns["f_ConfigParaLocation"].HeaderText = "表路径";
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

        private void btn_add_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {

        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {

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
            { }

            if (tn != null && tn.Parent != null)
            {

                familyPro = tn.Parent.ToString().Replace("TreeNode:", "").Replace(" ", "");
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
                //for (int i = 0; i < lvw_SelectedFamilies.Items.Count; i++)
                //{
                //    ToBeUpdates.Add(lvw_SelectedFamilies.Items[i].SubItems[8].Text);
                //}

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

        }

        private void RevitFamilyManagerFormMB_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btn_Search_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ExcelHelper ExcelHelper = new ExcelHelper();
                var ftp = new FtpHelper0(serverIP, ftpUserName, ftpPassword);

                int row = e.RowIndex;
                int col = e.ColumnIndex;
                //显示缩略图
                ftp.DownloadFile(this.dataGridView2.Rows[row].Cells["f_Pic"].Value.ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                //ftp.DownloadFile(DataUB.dataTempTable.Rows[row]["f_Pic"].ToString().Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");
                string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(this.dataGridView2.Rows[row].Cells["f_Pic"].Value.ToString());
                //string iconPath = xlsLocation + @"parameter_perview\" + Path.GetFileName(DataUB.dataTempTable.Rows[row]["f_Pic"].ToString());
                // MessageBox.Show(iconPath.ToString());
                pictureBox1.BackgroundImage = Image.FromFile(iconPath);
                //显示属性表
                string ParaLoc = this.dataGridView2.Rows[row].Cells["f_ConfigParaLocation"].Value.ToString();
                ftp.DownloadFile(ParaLoc.Replace(ftpConnectionLocation, "").Replace("/", "\\"), xlsLocation + @"parameter_perview\");

                //string localParasPath = DataUB.dataTempTable.Rows[row]["familyParass"].ToString();
                string localParasPath = this.dataGridView2.Rows[row].Cells["f_ConfigParaLocation"].Value.ToString();
                string xlsName = Path.GetFileName(ParaLoc);
                string localParaLoc = xlsLocation + @"parameter_perview\" + xlsName;
                DataTable dtpara = ExcelHelper.Reading_Excel_Information(localParaLoc);
                this.dataGridView4.DataSource = dtpara;

                dataGridView4.Columns["paraname"].HeaderText = "属性条目";
                dataGridView4.Columns["paravalue"].HeaderText = "属性值";
                dataGridView4.Columns["paratag"].HeaderText = "是否实例";
                dataGridView4.Columns["paragroup"].HeaderText = "属性分族";
                dataGridView4.Columns["paratype"].HeaderText = "属性类型";

            }
            catch (Exception req)
            {
                //MessageBox.Show(req.ToString());
            }

           
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
                this.dataGridView3.DataSource = dtpara;

                //txt_defaultXls.Text = localParasPath;
                dataGridView3.Columns["paraname"].HeaderText = "属性条目";
                dataGridView3.Columns["paravalue"].HeaderText = "属性值";
                dataGridView3.Columns["paratag"].HeaderText = "是否实例";
                dataGridView3.Columns["paragroup"].HeaderText = "属性分族";
                dataGridView3.Columns["paratype"].HeaderText = "属性类型";
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
                    this.dataGridView3.DataSource = dtpara;

                


                }
                catch (Exception req)
                { }

                #region  回车键快速选择

                #endregion

            }

        }

        public DataTable dtSearchResult = null;
        private void btn_Search_Click_2(object sender, EventArgs e)
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
                    dtSearchResult.ImportRow(dgvDataTable.Rows[i]);
                }
                else
                { continue; }
            }

            dataGridView1.DataSource = dtSearchResult;

            //dataGridView1.Columns["f_Name"].HeaderText = "族名称";
            //dataGridView1.Columns["f_Path"].HeaderText = "文件路径";
            //dataGridView1.Columns["f_Pic"].HeaderText = "缩略图";
            //dataGridView1.Columns["f_Value"].HeaderText = "文件大小";
            //dataGridView1.Columns["f_Pro"].HeaderText = "所属专业";
            //dataGridView1.Columns["f_ValLevel"].HeaderText = "电压等级";
            //dataGridView1.Columns["f_Cat"].HeaderText = "类型";
            //dataGridView1.Columns["f_Hash"].HeaderText = "哈希";
            //dataGridView1.Columns["f_UploadDate"].HeaderText = "上传日期";
            //dataGridView1.Columns["f_ParaLocation"].HeaderText = "属性表路径";
            //dataGridView1.Columns["f_ConfigParaLocation"].HeaderText = "表路径";
            //dataGridView1.Columns["f_manufacturer"].HeaderText = "生产厂家";
            //dataGridView1.Columns["f_Source"].HeaderText = "族来源";
            //dataGridView1.Columns["f_DeviceType"].HeaderText = "型号";
            //dataGridView1.Columns["f_Standard"].HeaderText = "依据标准";

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
        public DataTable dtBox = null;
        private void btn_add_Click_1(object sender, EventArgs e)
        {

            bool dict = false;
            int rowIndexCurrent = dataGridView1.CurrentCell.RowIndex;  //当前行索引
            if (dataGridView1.SelectedRows.Count >= 2)
            {
                for (int k = 0; k < dataGridView1.SelectedRows.Count; k++)
                {
                    for (int l = 0; l < DataUB.dataTempTable.Rows.Count; l++)
                    {
                        if (dataGridView1.SelectedRows[k].Cells[0].Value.ToString() == DataUB.dataTempTable.Rows[l][0].ToString())
                        {
                            if (dataGridView2.RowCount == 0)
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
                            //此处需要查重
                            string[] cn = new string[dtBox.Columns.Count];
                            for (int i = 0; i < dtBox.Columns.Count; i++)
                            { cn[i] = dtBox.Columns[i].ToString(); }
                            //dtBox = dtBox.DefaultView.ToTable(true, new string[] { "f_Hash", });
                            dtBox = dtBox.DefaultView.ToTable(true, cn);
                            dataGridView2.DataSource = dtBox;
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

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value != null && dataGridView1.Rows[rowIndexCurrent].Cells[0].Value != null)
                {
                    if (dataGridView2.Rows[i].Cells[0].Value.ToString() == dataGridView1.Rows[rowIndexCurrent].Cells[0].Value.ToString())
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
                if (dataGridView2.RowCount == 0)
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
                dataGridView2.DataSource = dtBox;
                DataUB.dataForDGV2 = dtBox;
            }

            dataGridView2.Columns["f_Uid"].HeaderText = "索引";
            dataGridView2.Columns["f_Name"].HeaderText = "族名称";
            dataGridView2.Columns["f_Path"].HeaderText = "文件路径";
            dataGridView2.Columns["f_Pic"].HeaderText = "缩略图";
            dataGridView2.Columns["f_Value"].HeaderText = "文件大小";
            dataGridView2.Columns["f_Pro"].HeaderText = "所属专业";
            dataGridView2.Columns["f_ValLevel"].HeaderText = "电压等级";
            dataGridView2.Columns["f_Cat"].HeaderText = "类型";
            dataGridView2.Columns["f_Hash"].HeaderText = "哈希";
            dataGridView2.Columns["f_UploadDate"].HeaderText = "上传日期";
            dataGridView2.Columns["f_ParaLocation"].HeaderText = "表路径";
            dataGridView2.Columns["f_ConfigParaLocation"].HeaderText = "属性表路径";
            dataGridView2.Columns["f_manufacturer"].HeaderText = "生产厂家";
            dataGridView2.Columns["f_Source"].HeaderText = "族来源";
            dataGridView2.Columns["f_DeviceType"].HeaderText = "型号";
            dataGridView2.Columns["f_Standard"].HeaderText = "依据标准";
 
            dataGridView2.Columns["f_Path"].Visible = false;
            dataGridView2.Columns["f_Pic"].Visible = false;
            dataGridView2.Columns["f_ParaLocation"].Visible = false;
            dataGridView2.Columns["f_ConfigParaLocation"].Visible = false;

            dataGridView2.Columns["f_Uid"].ReadOnly = true;
            dataGridView2.Columns["f_Hash"].ReadOnly = true;


        }

        private void btn_del_Click_1(object sender, EventArgs e)
        {
            int rowIndexCurrent = dataGridView2.CurrentCell.RowIndex;  //当前行索引
            List<string> rowNums = new List<string>();
            if (dtBox != null && dataGridView2.SelectedRows.Count >= 1)
            {
                for (int k = 0; k < dataGridView2.SelectedRows.Count; k++)
                {
                    for (int i = 0; i < dtBox.Rows.Count; i++)
                    {
                        //if (dataGridView2.Rows[rowIndexCurrent].Cells[0].Value.ToString() == dtBox.Rows[i][0].ToString())
                        if (dataGridView2.SelectedRows[k].Cells[0].Value.ToString() == dtBox.Rows[i][0].ToString())
                        {
                            //dataGridView3.Rows.RemoveAt(rowIndexCurrent);
                            rowNums.Add(dtBox.Rows[i][0].ToString()); //获取符合要求的行索引号
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
            dataGridView2.DataSource = dtBox;



        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
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

            int dgvRows = dataGridView2.RowCount;
            if (dgvRows > 0)
            {

                #region 上下键自动选择
                int row = dataGridView2.CurrentRow.Index;
                int roww = dataGridView2.Rows.Count - 1;

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
                    this.dataGridView4.DataSource = dtpara;
                }
                catch (Exception req)
                { }

                #region  回车键快速选择

                #endregion

            }
        }

        private void btn_Delete_Click_1(object sender, EventArgs e)
        {

            yesOrNotForm yesOrNotForm = new yesOrNotForm();
            yesOrNotForm.ShowDialog();
            if (yesOrNotForm.judgeFlag == false)
            { }
            else
            {

                int rowIndexCurrent = dataGridView2.CurrentCell.RowIndex;  //当前行索引
                                                                           //string hash = this.dataGridView2.Rows[rowIndexCurrent].Cells["f_Hash"].Value.ToString();
                                                                           //SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
                                                                           //string exesql = "DELETE FROM table_p_Family WHERE f_Hash='" + hash + "'";
                                                                           //DataSet ds = SqlCompose.ExecuteSqlQuery(exesql);
                ArrayList strSqlList = new ArrayList();
                SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
                List<string> rowNums = new List<string>();
                if (dtBox != null && dataGridView2.SelectedRows.Count >= 1)
                {
                    for (int k = 0; k < dataGridView2.SelectedRows.Count; k++)
                    {
                        for (int i = 0; i < dtBox.Rows.Count; i++)
                        {
                            //if (dataGridView2.Rows[rowIndexCurrent].Cells[0].Value.ToString() == dtBox.Rows[i][0].ToString())
                            if (dataGridView2.SelectedRows[k].Cells[0].Value.ToString() == dtBox.Rows[i][0].ToString())
                            {
                                string hash = this.dataGridView2.Rows[k].Cells["f_Hash"].Value.ToString();
                                string cat = this.dataGridView2.Rows[k].Cells["f_Cat"].Value.ToString();
                                string exesql = "DELETE FROM table_p_Family WHERE f_Hash='" + hash + "' AND f_Cat='" + cat + "'";
                                //MessageBox.Show(exesql);
                                //SqlCompose.ExecuteSqlNonQuery(exesql);
                                strSqlList.Add(exesql);
                                rowNums.Add(dtBox.Rows[i][0].ToString());
                                //dtBox.Rows[i].Delete();
                                //dtBox.AcceptChanges();
                            }
                        }
                    }
                }
                bool bdd = SqlCompose.ExecuteSqlNonQuery(strSqlList);
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
                dataGridView2.DataSource = dtBox;
            }

            #region 实现删除功能 的原始代码

            //int rowIndexCurrent = dataGridView2.CurrentCell.RowIndex;  //当前行索引
            //                                                           //string hash = this.dataGridView2.Rows[rowIndexCurrent].Cells["f_Hash"].Value.ToString();
            //                                                           //SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
            //                                                           //string exesql = "DELETE FROM table_p_Family WHERE f_Hash='" + hash + "'";
            //                                                           //DataSet ds = SqlCompose.ExecuteSqlQuery(exesql);
            //ArrayList strSqlList = new ArrayList();
            //SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
            //List<string> rowNums = new List<string>();
            //if (dtBox != null && dataGridView2.SelectedRows.Count >= 1)
            //{
            //    for (int k = 0; k < dataGridView2.SelectedRows.Count; k++)
            //    {
            //        for (int i = 0; i < dtBox.Rows.Count; i++)
            //        {
            //            //if (dataGridView2.Rows[rowIndexCurrent].Cells[0].Value.ToString() == dtBox.Rows[i][0].ToString())
            //            if (dataGridView2.SelectedRows[k].Cells[0].Value.ToString() == dtBox.Rows[i][0].ToString())
            //            {
            //                string hash = this.dataGridView2.Rows[k].Cells["f_Hash"].Value.ToString();
            //                string cat = this.dataGridView2.Rows[k].Cells["f_Cat"].Value.ToString();
            //                string exesql = "DELETE FROM table_p_Family WHERE f_Hash='" + hash + "' AND f_Cat='" + cat + "'";
            //                //MessageBox.Show(exesql);
            //                //SqlCompose.ExecuteSqlNonQuery(exesql);
            //                strSqlList.Add(exesql);
            //                rowNums.Add(dtBox.Rows[i][0].ToString());
            //                //dtBox.Rows[i].Delete();
            //                //dtBox.AcceptChanges();
            //            }
            //        }
            //    }
            //}
            //bool bdd = SqlCompose.ExecuteSqlNonQuery(strSqlList);
            //foreach (string str in rowNums)
            //{
            //    for (int i = dtBox.Rows.Count - 1; i >= 0; i--)
            //    {
            //        if (str == dtBox.Rows[i][0].ToString())
            //        {
            //            dtBox.Rows[i].Delete();
            //            dtBox.AcceptChanges();
            //        }
            //    }
            //}

            //dataGridView2.DataSource = dtBox;



            #endregion
        }

        private void btn_cancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_transform_Click_1(object sender, EventArgs e)
        {
            yesOrNotForm yesOrNotForm = new yesOrNotForm();
            yesOrNotForm.ShowDialog();
            int rowIndexCurrent = dataGridView2.CurrentCell.RowIndex;  //当前行索引
            if (yesOrNotForm.judgeFlag == false)
            { }
            else
            {
                ArrayList strSqlList = new ArrayList();
                SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
                List<string> rowNums = new List<string>();
                if (dtBox != null && dataGridView2.SelectedRows.Count >= 1)
                {
                    for (int k = 0; k < dataGridView2.SelectedRows.Count; k++)
                    {
                        for (int i = 0; i < dtBox.Rows.Count; i++)
                        {
                            //if (dataGridView2.Rows[rowIndexCurrent].Cells[0].Value.ToString() == dtBox.Rows[i][0].ToString())
                            if (dataGridView2.SelectedRows[k].Cells[0].Value.ToString() == dtBox.Rows[i][0].ToString())
                            {
                                string hashs = dataGridView2.SelectedRows[k].Cells["f_Hash"].Value.ToString();
                                string exesql1 = "UPDATE table_p_Family SET f_Pro = '" + lbl_familyPro.Text + "'" + " WHERE f_Hash = '" + hashs + "'";
                                string exesql2 = "UPDATE table_p_Family SET f_Cat = '" + lbl_familyCat.Text + "'" + " WHERE f_Hash = '" + hashs + "'";
                                //dataGridView3.Rows.RemoveAt(rowIndexCurrent);
                                strSqlList.Add(exesql1);
                                strSqlList.Add(exesql2);
                                rowNums.Add(dtBox.Rows[i][0].ToString()); //获取符合要求的行索引号
                            }
                        }
                    }
                }
                bool bdd = SqlCompose.ExecuteSqlNonQuery(strSqlList);
                foreach (string str in rowNums)
                {
                    for (int i = dtBox.Rows.Count - 1; i >= 0; i--)
                    {
                        if (str == dtBox.Rows[i][0].ToString())
                        {
                            dtBox.Rows[i]["f_Pro"] = lbl_familyPro.Text;
                            dtBox.Rows[i]["f_Cat"] = lbl_familyCat.Text;
                        }
                    }
                }
                dataGridView2.DataSource = dtBox;
            }
        }

        private void btn_confChange_Click_1(object sender, EventArgs e)
        {
            SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
            int rowIndexCurrent = dataGridView2.CurrentCell.RowIndex;  //当前行索引
            int colIndexCurrent = dataGridView2.CurrentCell.ColumnIndex;//当前列索引
            string uidNameCurrent = dataGridView2.Rows[rowIndexCurrent].Cells[0].Value.ToString();
            string colNameCurrent = dataGridView2.Columns[colIndexCurrent].Name.ToString();
            string strNameCurrent = dataGridView2.Rows[rowIndexCurrent].Cells[colIndexCurrent].Value.ToString() ;
            string exesql = "UPDATE table_p_Family SET " + colNameCurrent + " = '" + strNameCurrent + "'" + " WHERE f_Uid = '" + uidNameCurrent + "'";
            SqlCompose.ExecuteSqlNonQuery(exesql);
            MessageBox.Show("完成更改");

            //ArrayList strSqlList = new ArrayList();
     
          
            //DataUB.confChangeAF = GetDgvToTable(dataGridView2);

            //this.dataGridView1.DataSource = dtBox;

            //List<string[]> dataCellsLocs = CompareDataTable(DataUB.confChangeBF, DataUB.confChangeAF);
            //foreach (string[] str in dataCellsLocs)
            //{
            //    string Uid = str[0];
            //    string columnName = str[1];
            //    string newCell = this.dataGridView2.Rows[Convert.ToInt16(str[3])].Cells[str[1]].Value.ToString();
            //    MessageBox.Show("fefe::" + newCell.ToString());
            //    string exesql = "UPDATE table_p_Family SET " + columnName + " = '" + newCell + "'" + " WHERE f_Uid = '" + Uid + "'";
            //    strSqlList.Add(exesql);
            //}
            //bool bdd = SqlCompose.ExecuteSqlNonQuery(strSqlList);

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void lbl_familyCat_Click_1(object sender, EventArgs e)
        {

        }

        private void lbl_familyPro_Click_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 绑定DataGridView数据到DataTable
        /// </summary>
        /// <param name="dgv">复制数据的DataGridView</param>
        /// <returns>返回的绑定数据后的DataTable</returns>
        //public DataTable GetDgvToTable(DataGridView dgv)
        //{
        //    DataTable dt = new DataTable();
        //    // 列强制转换
        //    for (int count = 0; count < dgv.Columns.Count; count++)
        //    {
        //        DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
        //        dt.Columns.Add(dc);
        //    }
        //    // 循环行
        //    for (int count = 0; count < dgv.Rows.Count; count++)
        //    {
        //        DataRow dr = dt.NewRow();
        //        for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
        //        {
        //            dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
        //        }
        //        dt.Rows.Add(dr);
        //    }
        //    return dt;
        //}

        /////   <summary>
        /////   比较两个DataTable内容是否相等，先是比数量，数量相等就比内容
        /////   </summary>
        /////   <param   name= "dtA "> </param>
        /////   <param   name= "dtB "> </param>
        //public List<string[]> CompareDataTable(DataTable dtA, DataTable dtB)
        //{
        //    List<int[]> RowAndColumn = new List<int[]>();
        //    List<string[]> RowAndColumnStr = new List<string[]>();
        //    //MessageBox.Show(dtA.Rows.Count.ToString() +"___"+ dtB.Rows.Count.ToString());
        //    if (dtA.Rows.Count == dtB.Rows.Count - 1)
        //    {
        //        //比内容
        //        for (int i = 0; i < dtA.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < dtA.Columns.Count; j++)
        //            {
        //                if (!dtA.Rows[i][j].Equals(dtB.Rows[i][j]))
        //                {
        //                    RowAndColumn.Add(new int[] { i, j });
        //                    MessageBox.Show(dtB.Rows[i]["f_Uid"].ToString() + dtB.Columns[j].ColumnName + i.ToString() + j.ToString());
        //                    RowAndColumnStr.Add(new string[] { dtB.Rows[i]["f_Uid"].ToString(), dtB.Columns[j].ColumnName, i.ToString(), j.ToString() });
        //                }
        //            }
        //        }
        //        return RowAndColumnStr;
        //    }
        //    else
        //    {
        //        MessageBox.Show("行数不符");
        //        return null;
        //    }
        //}
        //public List<string[]> CompareDataGridView(DataGridView dgvA, DataGridView dgvB)
        //{
        //    List<int[]> RowAndColumn = new List<int[]>();
        //    List<string[]> RowAndColumnStr = new List<string[]>();
        //    //MessageBox.Show(dtA.Rows.Count.ToString() +"___"+ dtB.Rows.Count.ToString());
        //    if (dgvA.Rows.Count == dgvB.Rows.Count - 1)
        //    {
        //        //比内容
        //        for (int i = 0; i < dgvA.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < dgvA.Columns.Count; j++)
        //            {
        //                if (dgvA.Rows[i].Cells[j].Value.ToString() != dgvB.Rows[i].Cells[j].Value.ToString())
        //                {
        //                    RowAndColumn.Add(new int[] { i, j });
        //                    MessageBox.Show(dgvB.Rows[i].Cells["f_Uid"].ToString() + dgvB.Columns[j] + i.ToString() + j.ToString());
        //                    RowAndColumnStr.Add(new string[] { dgvB.Rows[i].Cells["f_Uid"].ToString(), dgvB.Columns[j].ToString(), i.ToString(), j.ToString() });
        //                }
        //            }
        //        }
        //        return RowAndColumnStr;
        //    }
        //    else
        //    {
        //        MessageBox.Show("行数不符");
        //        return null;
        //    }



        //}


    }
}
   

 