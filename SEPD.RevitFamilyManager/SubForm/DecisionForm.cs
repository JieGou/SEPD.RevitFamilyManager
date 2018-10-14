using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEPD.RevitFamilyPlatform
{
    public partial class DecisionForm : Form
    {
        public bool Continue = true;
        public DecisionForm(DataTable dt)
        {
            InitializeComponent();

            try
            {
                this.lsb_Info.Items.Clear();
                this.lsb_Info.Items.Add("*");
                //this.lsb_Info.Items.Add(dt.Rows[0]["f_Name"].ToString());
                //MessageBox.Show(dt.Rows.Count.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string exsistFamilyInfo = dt.Rows[i]["f_Name"].ToString() + "; " + dt.Rows[i]["f_UploadDate"].ToString() + "; " + dt.Rows[i]["f_ValLevel"].ToString() + "; " + dt.Rows[i]["f_DeviceType"].ToString() + "; " + dt.Rows[i]["f_manufacturer"].ToString();
                    //MessageBox.Show(exsistFamilyInfo);
                    this.lsb_Info.Items.Add(exsistFamilyInfo);
                }
            }
            catch (Exception eerre)
            { }

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Continue = false;
            this.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Continue = true;
            this.Close();
        }

        private void btn_Cancel_Click_1(object sender, EventArgs e)
        {
            Continue = false;
            this.Close();
        }

        private void DecisionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
