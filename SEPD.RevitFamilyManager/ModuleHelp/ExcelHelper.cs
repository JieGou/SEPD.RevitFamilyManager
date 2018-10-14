using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data;

using NPOI;
using NPOI.XSSF;
using NPOI.POIFS; 
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace SEPD.RevitFamilyManager
{
    public class ExcelHelper
    {
        const string xlsSavePath = @"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\parameter_excel\";

        #region  将xls文件内容读取到DATATABLE中
        /// <summary>
        /// 将xls文件内容读取到DATATABLE中
        /// </summary>
        /// <param name="path">本地的文件目录</param>
        /// <param name="paraname">属性名</param>
        /// <param name="paravalue">属性值</param>
        /// <param name="paratag">是否实例</param>
        /// <param name="paragroup">属性分组</param>
        /// <param name="paratype">数据类型</param>
        /// <returns></returns>

        public DataTable Reading_Excel_Information(string path)
        {

            //创建datatable
            DataTable dt = new DataTable("Table_Excel");
            //创建带列名的列
            dt.Columns.Add("paraname", typeof(String));
            dt.Columns.Add("paravalue", typeof(String));
            dt.Columns.Add("paratag", typeof(String));
            dt.Columns.Add("paragroup",typeof(String));
            dt.Columns.Add("paratype", typeof(String));

            //开始读取excel
            FileStream fs = File.OpenRead(@path);  //打开EXCEL文件
            {

                IWorkbook wk = null;

                if (path.Contains("xls"))
                {
                    wk = new HSSFWorkbook(fs);      //把文件信息写入wk 
                    //MessageBox.Show("若程序出错，请将EXCEL保存为2007版本以上");
                }
                else if (path.Contains("xlsx"))
                {
                    wk = new XSSFWorkbook(fs);
                    //MessageBox.Show("若程序出错，请将EXCEL保存为2007版本以下");
                }
                else
                {
                    MessageBox.Show("请选择Excel文件");
                }


                //for (int i = 0; i < wk.NumberOfSheets; i++)    //遍历文件中的表总数
                for (int i = 0; i <= 0; i++)    //遍历文件中的表总数
                {
                    ISheet sheet = wk.GetSheetAt(i);      //读取当前表数据

                    for (int j = 0; j <= sheet.LastRowNum; j++)  //当前表总行数
                    {
                        //每次开始遍历表时刷新列表
                        //List_T List_T = new List_T();
                        dt.Rows.Add();

                        IRow row = sheet.GetRow(j);

                        if (row != null)
                        {
                            ICell cell0 = row.GetCell(0);   //读取第一列数据(参数名)
                            if (cell0.ToString().Contains("null"))
                            {
                                break;
                            }
                            else
                            {
             

                                dt.Rows[j]["paraname"] = cell0.ToString();
                            }

                            ICell cell1 = row.GetCell(1);   //读取第二列数据(参数值)
                            if (cell1 != null)
                            {
                                //List_T.parametersValue = cell1.ToString();
                                dt.Rows[j]["paravalue"] = cell1.ToString();
                            }
                            else
                            {
                                dt.Rows[j]["paravalue"] = "NA";
                            }

                            ICell cell2 = row.GetCell(2);   //读取第三列数据(参数值)
                            if (cell2.ToString() == null || cell2.ToString() == "否")
                            {
                                dt.Rows[j]["paratag"] = "否";
                            }
                            else
                            {
                                dt.Rows[j]["paratag"] = "是";
                            }

                            ICell cell3 = row.GetCell(3);   //读取第四列数据(参数值)
                            if (cell3 != null)
                            {
                                dt.Rows[j]["paragroup"] = cell3.ToString();
                            }
                            else
                            {
                                dt.Rows[j]["paragroup"] = "PG_ELECTRICAL";
                            }

                            ICell cell4 = row.GetCell(4);   //读取第五列数据(参数值)
                            if (cell4 != null)
                            {
                                dt.Rows[j]["paratype"] = cell4.ToString();
                            }
                            else
                            {
                                dt.Rows[j]["paratype"] = "Text";
                            }

                        }
 
                    }
 
                }
            }
            return dt;
        }
        #endregion


        #region 将DATATABLE中的数据写入到xls中
        /// <summary>
        /// 将DATATABLE中的数据写入到xls中
        /// </summary>
        /// <param name="dta">表类型 弱类型</param>
        /// <returns></returns>
        /// 
        public string Creating_Excel_Information(DataTable dta)
        {
            HSSFWorkbook workbook2003 = new HSSFWorkbook(); //新建xls工作簿  
            workbook2003.CreateSheet("Sheet1");  //新建1个Sheet工作表
            HSSFSheet SheetOne = (HSSFSheet)workbook2003.GetSheet("Sheet1"); //获取名称为Sheet1的工作表  
            //对工作表先添加行，下标从0开始
            //MessageBox.Show(dta.Rows.Count.ToString());
            //MessageBox.Show(dta.Rows[0][0].ToString());
            
            for (int i = 0; i < dta.Rows.Count;i++)
            {
                IRow row = SheetOne.CreateRow(i);
                //HSSFRow SheetRow = (HSSFRow)SheetOne.GetRow(i);
                //HSSFCell[] SheetCell = new HSSFCell[3];
                //SheetCell[0] = (HSSFCell)SheetRow.CreateCell(0);
                //SheetCell[1] = (HSSFCell)SheetRow.CreateCell(1);
                //SheetCell[2] = (HSSFCell)SheetRow.CreateCell(2);

                //SheetCell[0].SetCellValue(dta.Rows[i][0].ToString());
                //SheetCell[1].SetCellValue(dta.Rows[i][1].ToString());
                //SheetCell[2].SetCellValue(dta.Rows[i][2].ToString());
                //MessageBox.Show(dta.Rows[i][2].ToString());
                row.CreateCell(0).SetCellValue(dta.Rows[i][0].ToString());
                row.CreateCell(1).SetCellValue(dta.Rows[i][1].ToString());
                row.CreateCell(2).SetCellValue(dta.Rows[i][2].ToString());
                row.CreateCell(3).SetCellValue(dta.Rows[i][3].ToString());
                row.CreateCell(4).SetCellValue(dta.Rows[i][4].ToString());

            }

            string saveAsPath = xlsSavePath + dta.Rows[0][1].ToString() + "_" + DateTime.Now.ToString().Replace("/", "_").Replace(":", "_") + "_new.xls";
            //string saveAsPath = xlsSavePath + dta.Rows[0][1].ToString() + "_" + DateTime.Now.ToString().Replace("/", "_").Replace(":", "_") + "_new.xls";
            FileStream file2003 = new FileStream(saveAsPath, FileMode.Create);
            workbook2003.Write(file2003);
            file2003.Close();
            workbook2003.Close();
            return saveAsPath;
        }
        #endregion


        #region 将DATATABLE中的数据写入到xls中 2
        /// <summary>
        /// 将DATATABLE中的数据写入到xls中
        /// </summary>
        /// <param name="dta">表类型 弱类型</param>
        /// <returns></returns>
        /// 
        public string Creating_Excel_Information(DataTable dta,string path)
        {
            HSSFWorkbook workbook2003 = new HSSFWorkbook(); //新建xls工作簿  
            workbook2003.CreateSheet("Sheet1");  //新建1个Sheet工作表
            HSSFSheet SheetOne = (HSSFSheet)workbook2003.GetSheet("Sheet1"); //获取名称为Sheet1的工作表  
            
            //对工作表先添加行，下标从0开始
            for (int i = 0; i < dta.Rows.Count; i++)
            {
                IRow row = SheetOne.CreateRow(i);
 
                row.CreateCell(0).SetCellValue(dta.Rows[i][0].ToString());
                row.CreateCell(1).SetCellValue(dta.Rows[i][1].ToString());
                row.CreateCell(2).SetCellValue(dta.Rows[i][2].ToString());
                row.CreateCell(3).SetCellValue(dta.Rows[i][3].ToString());
                row.CreateCell(4).SetCellValue(dta.Rows[i][4].ToString());

            }

            string saveAsPath = path.Replace(".rfa", "") + ".xls";
            //string saveAsPath = xlsSavePath + dta.Rows[0][1].ToString() + "_" + DateTime.Now.ToString().Replace("/", "_").Replace(":", "_") + "_new.xls";
            FileStream file2003 = new FileStream(saveAsPath, FileMode.Create);
            workbook2003.Write(file2003);
            file2003.Close();
            workbook2003.Close();
            return saveAsPath;
        }
        #endregion


    }
}
