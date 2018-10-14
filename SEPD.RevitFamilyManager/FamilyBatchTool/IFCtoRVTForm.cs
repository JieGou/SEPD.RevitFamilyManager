using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace SEPD.IFCtoRVT
{
    public partial class IFCtoRVTForm : Form
    {
        public string ifcFilePath = null;
        public List<string> ifcPaths = new List<string>();
        public DataTable dtIFC = new DataTable("dtIFC");

        public IFCtoRVTForm()
        {
            InitializeComponent();
            dtIFC.Columns.Add("name", typeof(String));
            dtIFC.Columns.Add("path", typeof(String));

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ifcPaths = null;
            this.Close();
        }

        private void btn_selectIFC_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "ifc文件|*.ifc|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;



            if (openFileDialog.ShowDialog() == DialogResult.OK /*&& openFamilyName != null*/)
            {
                dtIFC.Clear();

                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    string ifcPath = openFileDialog.FileNames[i].ToString();//获取用户选择的文件路径
                    string ifcName = ifcPath.Substring(ifcPath.LastIndexOf("\\") + 1);//获取用户选择的文件名称
                    ifcPaths.Add(ifcPath);
                    //dtIFC.Rows[i]["name"] = ifcName;
                    //dtIFC.Rows[i]["path"] = ifcPath;
                }

                //dataGridView1.DataSource = dtIFC;
            }
        }

        private void IFCtoRVTForm_Load(object sender, EventArgs e)
        {

        }
    }
}
