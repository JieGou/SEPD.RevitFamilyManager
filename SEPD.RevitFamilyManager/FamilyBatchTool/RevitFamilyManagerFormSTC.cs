using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;

namespace SEPD.RevitFamilyManager
{
    public partial class RevitFamilyManagerFormSTC : System.Windows.Forms.Form
    {
        public DataTable fidt = new DataTable("familyInstance");
        public DataTable codt = new DataTable("familyInstance_paras");
        public List<FamilyInstance> filteredList = new List<FamilyInstance>();

        public RevitFamilyManagerFormSTC()
        {
            InitializeComponent();
   
        }
 
        private void btn_flash_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.Rows.Clear();
            //this.dgv_FamilyAndXls.Rows.Clear();
            this.dataGridView1.DataSource = null;
            //codt = null;

            string SearchCondition = txt_condition.Text;    //实例名称筛选条件
            string paraCondition = txt_paracond.Text;       //参数筛选条件
            string[] SearchConditions = null;
            string[] paraConditions = null;

            #region 分割查找条件
            //分割实例名称查找条件
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
            //参数筛选条件
            if (paraCondition.Contains(','))
            {
                paraConditions = paraCondition.Split(',');
            }
            else if (paraCondition.Contains('，'))
            {
                paraConditions = paraCondition.Split('，');
            }
            else
            {
                paraConditions = paraCondition.Split(' ');
            }

            #endregion

            //根据条件初始化dataset的列名
            codt.Columns.Add("实例名称", typeof(String));          
            foreach (string cond in paraConditions)
            {
                codt.Columns.Add(cond, typeof(String));
            }

            //显示实例名称筛选的项
            //int codtCount = 0;
            for (int k = 0; k < fidt.Rows.Count; k++)
            {
                int cont = 0;
                foreach (string cond in SearchConditions)
                {
                    if (fidt.Rows[k]["实例名称"].ToString().Contains(cond))
                    {
                        cont++;
                    }
                    else
                    {
                        cont = 0;
                        break;
                    }
                }

                if (cont != 0)
                {
                    //codt.Rows.Add(fidt.Rows[k].ItemArray);

                    //DataRow row = codt.NewRow();
                    //row.ItemArray = fidt.Rows[k].ItemArray ;
                    //fidt.Rows.InsertAt(row, codtCount);
                    //codtCount++;
                    //codtCount++;
                    //codt.Rows.Add();
                    //codt.Rows[codtCount]["实例名称"] = fidt.Rows[k];
                    //codt.Rows[codtCount]["选定参数值"] = fidt.Rows[k];

                    //int index = this.dgv_FamilyAndXls.Rows.Add();
                    //this.dgv_FamilyAndXls.Rows[index].Cells["dgv_fName"].Value = fidt.Rows[k]["实例名称"];
                    //this.dgv_FamilyAndXls.Rows[index].Cells["dgv_fPath"].Value = fidt.Rows[k]["选定参数值"];
                    //this.dgv_FamilyAndXls.DataSource = fidt;

                    continue;
                }
                else if(cont == 0)
                {
                    fidt.Rows[k].Delete();
                    continue;
                }
            }
            fidt.AcceptChanges();

            int indexCodt = 0;
            for (int j = 0; j < fidt.Rows.Count; j++)
            {
                for (int ii = 0; ii < filteredList.Count(); ii++)
                {
                    
                    if (fidt.Rows[j]["实例名称"].ToString() == filteredList[ii].Name)
                    {
                        codt.Rows.Add();
                        codt.Rows[indexCodt]["实例名称"]= filteredList[ii].Name;

                        foreach (string cond in paraConditions)
                        {
                            Parameter para = filteredList[ii].LookupParameter(cond);
                            if (para != null)
                            {
                                string parav = para.AsValueString();
                                codt.Rows[indexCodt][cond] = parav;
                            }
                            else
                            {
                                //codt.Rows[indexCodt][cond] = "0";
                            }
                        }
                        indexCodt++;
                    }
                }
            }

            //this.dgv_FamilyAndXls.DataSource = codt;
            this.dataGridView1.DataSource = codt;

            //foreach (string cond in paraConditions)
            //{
            //    this.listView1.Columns.Add(cond,50);
            //    var item = new ListViewItem();
            //    item.SubItems.Add(cond);
            //    item.SubItems[0].Text = dgvColumnSum(dataGridView1, cond).ToString();
            //    listView1.Items.Add(item);
            //}
            dataGridView2.Rows.Clear();
            foreach (string cond in paraConditions)
            {
                this.dataGridView2.Columns.Add(cond,cond);
                this.dataGridView2.Rows[0].Cells[cond].Value = dgvColumnSum(dataGridView1, cond).ToString();
            }

            //double sum = 0;
            //int indexR = this.dgv_FamilyAndXls.Rows.Add();
            //foreach (string cond in SearchConditions)
            //{
            //    decimal sumd = dgvColumnSum(dataGridView1, cond);
            //    this.dgv_FamilyAndXls.Rows[indexR].Cells[cond].Value = sumd.ToString();
            //}

            //for (int j = 0; j < codt.Rows.Count; j++)
            //{
            //    if (codt.Rows[j]["fiNamex"] != null)
            //    {
            //        sum = sum + Convert.ToDouble( codt.Rows[j]["fiNamex"] );
            //        sum = sum + Convert.ToDouble(this.dgv_FamilyAndXls.Rows[j].Cells["dgv_fPath"].Value);
            //    }
            //}

            //for (int j = 0; j < dgv_FamilyAndXls.Rows.Count; j++)
            //{
            //    if (this.dgv_FamilyAndXls.Rows[j].Cells["dgv_fPath"] != null)
            //    {
            //        //sum = sum + Convert.ToDouble(codt.Rows[j]["fiNamex"]);
            //        sum = sum + Convert.ToDouble(this.dgv_FamilyAndXls.Rows[j].Cells["dgv_fPath"].Value);
            //    }
            //}

            //lbl_all.Text = sum.ToString();
            //txt_all.Text = sum.ToString();

        }

        private void RevitFamilyManagerFormSTC_Load(object sender, EventArgs e)
        {

            //this.dgv_FamilyAndXls.Rows.Clear();
            //for (int i = 0; i < fidt.Rows.Count; i++)
            //{
            //    int index = this.dgv_FamilyAndXls.Rows.Add();
            //    this.dgv_FamilyAndXls.Rows[index].Cells["dgv_fName"].Value = fidt.Rows[i]["实例名称"];
            //    this.dgv_FamilyAndXls.Rows[index].Cells["dgv_fPath"].Value = fidt.Rows[i]["选定参数值"];
            //}

            //this.dataGridView1.Rows.Clear();
            //BindingSource bs = new BindingSource();
            //bs.DataSource = fidt;
            //this.dataGridView1.DataSource = bs;
            //this.dataGridView1.DataSource = fidt;

            fidt.Columns.Add("实例名称", typeof(String));
            //dt.Columns.Add("选定参数值", typeof(String));

            for (int ii = 0; ii < filteredList.Count(); ii++)
            {
                fidt.Rows.Add();
                fidt.Rows[ii]["实例名称"] = filteredList[ii].Name;

                //Parameter para = filteredList[ii].LookupParameter("长度");
                //if (para != null)
                //{
                //    string parav = para.AsValueString();
                //    dt.Rows[ii]["选定参数值"] = parav;
                //}
            }
            //this.dataGridView1.DataSource = fidt;
            this.dgv_FamilyAndXls.DataSource = fidt;

        }

        /// <summary>
        /// 计算dgv所有列的和
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="columindex"></param>
        /// <returns></returns>
        private static Decimal dgvColumnSum(DataGridView dgv , int columnIndex)
        {
            Decimal totalp = 0;
            decimal Dtotal = 0;
            if (dgv.Rows.Count > 0)
            {
                for (int i = 0; i < dgv.Rows.Count -1 ;i++)
                {


                    if (dgv.Rows[i].Cells[columnIndex].Value.ToString() == "" || dgv.Rows[i].Cells[columnIndex].Value.ToString() == " ")
                    {
                        totalp += 0;
                    }
                    else
                    {
                        totalp += Convert.ToDecimal(dgv.Rows[i].Cells[columnIndex].Value.ToString());

                    }
 
                }
                Dtotal = totalp;
            }

            return Dtotal;
        }
        private static Decimal dgvColumnSum(DataGridView dgv, string columnName)
        {
            Decimal totalp = 0;
            decimal Dtotal = 0;
            if (dgv.Rows.Count > 0)
            {
                for (int i = 0; i < dgv.Rows.Count - 1; i++)
                {

                    if (dgv.Rows[i].Cells[columnName].Value.ToString() == "" || dgv.Rows[i].Cells[columnName].Value.ToString() == " ")
                    {
                        totalp += 0;
                    }
                    else
                    {
                        totalp += Convert.ToDecimal(dgv.Rows[i].Cells[columnName].Value.ToString());

                    }


                }
                Dtotal = totalp;
            }
            return Dtotal;
        }


    }
}
