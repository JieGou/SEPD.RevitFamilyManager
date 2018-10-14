using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEPD.RevitFamilyManager
{
    public partial class RevitFamilyManagerFormEP : Form
    {
        public List<string> listElementNames = new List<string>();
        public List<string> listFamilyNames = new List<string>();
         public DataTable dt = new DataTable();
        ExcelHelper ExcelHelper = new ExcelHelper();
        string xPath = null;
        public bool clearsymbol = false;
        public bool clearpara = false;

        //创建datatable
        public DataTable dtListView = new DataTable("Table_Excel");

        public RevitFamilyManagerFormEP()
        {
            InitializeComponent();
        }

        private void RevitFamilyManagerFormEP_Load(object sender, EventArgs e)
        {
            this.lvw_eleSelXls.Items.Clear();
            for (int i = 0; i < listElementNames.Count(); i++)
            {
                var item = new ListViewItem();
                item.SubItems.Add(listElementNames[i]);
                item.SubItems.Add("...");
                item.SubItems.Add("...");
                item.SubItems.Add("...");
                item.SubItems[0].Text = listElementNames[i];
                item.SubItems[1].Text = "...";
                item.SubItems[2].Text = "...";
                item.SubItems[3].Text = listFamilyNames[i]; 
                lvw_eleSelXls.Items.Add(item);
            }
        }

        private void lvw_eleSelXls_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.lvw_eleSelXls.FullRowSelect = true;
            if (lvw_eleSelXls.SelectedItems.Count == 0)
            { return; }
            else if (this.lvw_eleSelXls.SelectedItems[0].SubItems[2].Text != "...")
            {
                xPath = this.lvw_eleSelXls.SelectedItems[0].SubItems[1].Text;
                dt = ExcelHelper.Reading_Excel_Information(xPath);
            }

            this.dgv_parameter.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.dgv_parameter.Rows.Add();

                this.dgv_parameter.Rows[i].Cells["dgv_pName"].Value = dt.Rows[i]["paraname"].ToString();
                this.dgv_parameter.Rows[i].Cells["dgv_pValue"].Value = dt.Rows[i]["paravalue"].ToString();
                this.dgv_parameter.Rows[i].Cells["dgv_pIns"].Value = dt.Rows[i]["paratag"].ToString();
                this.dgv_parameter.Rows[i].Cells["dgv_pGroup"].Value = dt.Rows[i]["paragroup"].ToString();
                this.dgv_parameter.Rows[i].Cells["dgv_pType"].Value = dt.Rows[i]["paratype"].ToString();

            }

        }

        private void btn_selectLocalXls_Click(object sender, EventArgs e)
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
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    string pPath = openFileDialog.FileName.ToString();//获取用户选择的文件路径

                    lvw_eleSelXls.BeginUpdate();
                    this.lvw_eleSelXls.SelectedItems[0].SubItems[1].Text = pPath;
                    this.lvw_eleSelXls.SelectedItems[0].SubItems[2].Text = Path.GetFileName(pPath);
                    lvw_eleSelXls.EndUpdate();

                    //
                    dt = ExcelHelper.Reading_Excel_Information(pPath);
                    this.dgv_parameter.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.dgv_parameter.Rows.Add();
                        this.dgv_parameter.Rows[i].Cells["dgv_pName"].Value = dt.Rows[i]["paraname"].ToString();
                        this.dgv_parameter.Rows[i].Cells["dgv_pValue"].Value = dt.Rows[i]["paravalue"].ToString();
                        this.dgv_parameter.Rows[i].Cells["dgv_pIns"].Value = dt.Rows[i]["paratag"].ToString();
                        this.dgv_parameter.Rows[i].Cells["dgv_pGroup"].Value = dt.Rows[i]["paragroup"].ToString();
                        this.dgv_parameter.Rows[i].Cells["dgv_pType"].Value = dt.Rows[i]["paratype"].ToString();
                    }


                }



            }
            catch
            { }
        }

        private void btn_delectSelected_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvw_eleSelXls.SelectedItems)
            {
                lvw_eleSelXls.Items.RemoveAt(lvi.Index);
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

                string saveAsPath = ExcelHelper.Creating_Excel_Information(dta, xPath.Replace(".xls", "") + ".rfa");

            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            //创建带列名的列
            dtListView.Columns.Add("eleName", typeof(String));
            dtListView.Columns.Add("eleXPath", typeof(String));
            dtListView.Columns.Add("elepName", typeof(String));
            dtListView.Columns.Add("elefName",typeof(String));

            for (int i = 0; i < lvw_eleSelXls.Items.Count ; i++)
            {
                dtListView.Rows.Add();
                dtListView.Rows[i][0] = lvw_eleSelXls.Items[i].SubItems[0].Text;
                dtListView.Rows[i][1] = lvw_eleSelXls.Items[i].SubItems[1].Text;
                dtListView.Rows[i][2] = lvw_eleSelXls.Items[i].SubItems[2].Text;
                dtListView.Rows[i][3] = lvw_eleSelXls.Items[i].SubItems[3].Text;
            }

            if (ckb_clearsymbol.CheckState == CheckState.Checked)
            {
                clearsymbol = true;
            }
            if (ckb_clearpara.CheckState == CheckState.Checked)
            {
                clearpara = true;
            }

            this.Close();
        }
    }
}
