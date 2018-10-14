using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEPD.RevitFamilyManager
{
    public partial class selectStandardForm : Form
    {
        public string downloadFamilyLoc = null;
        public string sqlConnectionLocation = null;
        public string finalFileName = null;
        List<string> standards = new List<string>();
        public selectStandardForm()
        {
            InitializeComponent();

            //查找列出所有已存在标准
            SqlConnection conStandard = new SqlConnection(sqlConnectionLocation);
            SqlDataAdapter sdaStandard = new SqlDataAdapter();
            SqlCommand cmdStandard = new SqlCommand("SELECT st_Standard  FORM table_Standard");
            sdaStandard.SelectCommand = cmdStandard;
            DataSet dsStandard = new DataSet();
            sdaStandard.Fill(dsStandard, "standard");
            DataTable dtStandard = dsStandard.Tables[0];
 
            for (int i = 0; i < dtStandard.Rows.Count; i++)
            {
                standards.Add(dtStandard.Rows[i][0].ToString() ); 
            }
            standards = standards.Distinct().ToList();
            foreach (string standard in standards)
            {
                cmb_standard.Items.Add(standard);
            }
 
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {

            
            string fileNameCat = null;
            string fileNameLevel = null;
            string fileNameType = null;
            string fileNameProd = null;

            SqlConnection conInfo = new SqlConnection(sqlConnectionLocation);
            SqlDataAdapter sdaInfo = new SqlDataAdapter();
            SqlCommand cmdInfo = new SqlCommand("SELECT * FORM table_p_Family WHERE f_Path = '" + downloadFamilyLoc + "'");
            sdaInfo.SelectCommand = cmdInfo;
            DataSet dsInfo = new DataSet();
            sdaInfo.Fill(dsInfo, "Info");
            DataTable dtInfo = dsInfo.Tables[0];

            fileNameCat = dtInfo.Rows[0]["f_Cat"].ToString();//CODEname  代码
            fileNameLevel = dtInfo.Rows[0]["f_ValLevel"].ToString();
            fileNameType = dtInfo.Rows[0]["f_DeviceType"].ToString();
            fileNameProd = dtInfo.Rows[0]["f_manufacturer"].ToString();

            cmdInfo = new SqlCommand("SELECT st_Code FORM st_Standard WHERE st_Name = '"+ fileNameCat + "'"+ " AND st_Standard = '"+cmb_standard.Text + " AND st_Part = '名称'");
            sdaInfo.SelectCommand = cmdInfo;
            sdaInfo.Fill(dsInfo, "InfoCat");
            dtInfo = dsInfo.Tables[0];
            if (dtInfo.Rows[0][0] != null)
                fileNameCat = dtInfo.Rows[0][0].ToString();

            cmdInfo = new SqlCommand("SELECT st_Code FORM st_Standard WHERE st_Name = '" + fileNameLevel + "'" + " AND st_Standard = '" + cmb_standard.Text + " AND st_Part = '电压等级'");
            sdaInfo.SelectCommand = cmdInfo;
            sdaInfo.Fill(dsInfo, "InfoLevel");
            dtInfo = dsInfo.Tables[0];
            if (dtInfo.Rows[0][0] != null)
                fileNameLevel = dtInfo.Rows[0][0].ToString();

            cmdInfo = new SqlCommand("SELECT st_Code FORM st_Standard WHERE st_Name = '" + fileNameLevel + "'" + " AND st_Standard = '" + cmb_standard.Text + " AND st_Part = '型号'");
            sdaInfo.SelectCommand = cmdInfo;
            sdaInfo.Fill(dsInfo, "InfoLevel");
            dtInfo = dsInfo.Tables[0];
            if (dtInfo.Rows[0][0] != null)
                fileNameType = dtInfo.Rows[0][0].ToString(); 
           

            cmdInfo = new SqlCommand("SELECT st_Code FORM st_Standard WHERE st_Name = '" + fileNameLevel + "'" + " AND st_Standard = '" + cmb_standard.Text + " AND st_Part = '厂家'");
            sdaInfo.SelectCommand = cmdInfo;
            sdaInfo.Fill(dsInfo, "InfoLevel");
            dtInfo = dsInfo.Tables[0];
            if (dtInfo.Rows[0][0] != null)
                fileNameProd = dtInfo.Rows[0][0].ToString();

            finalFileName = fileNameCat + "_" + fileNameLevel + "_" + fileNameType + "_" + fileNameProd;

            this.Close();
        }
    }
}
