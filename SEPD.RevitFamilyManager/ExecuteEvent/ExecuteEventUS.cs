


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

    public class ExecuteEventUS : IExternalEventHandler 
    {

        public DataSet openFamilyParaDataSet = null;


        public string familyFilePath = null;
        //public string xlsFilePath = null;
        public string XlsFilePath = null;
        public string xlsFilePathLog = @"C:\ProgramData\Autodesk\Revit\Addins\2016\xlsFilePath.txt";

        public List<string> familyFilePaths = new List<string>();

        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;

            File.WriteAllText(xlsFilePathLog, string.Empty);
            //FileStream fs = new FileStream(xlsFilePathLog, FileMode.Append, FileAccess.Write);
            //StreamWriter sw = new StreamWriter(fs);
            //sw.Write("\n");
            //sw.Close();
            //fs.Close();

            try
            {
                MyFamilyLoadOptions option = new MyFamilyLoadOptions();

                for (int k = 0; k < familyFilePaths.Count(); k++)
                {
                    Transaction openFamily = new Transaction(doc, "openFamily");
                    openFamily.Start();
                    //MessageBox.Show(familyFilePaths[k] );
                    Document rfadoc = app.Application.OpenDocumentFile(familyFilePaths[k]);
                    //openFamily.Commit();
                    //settingFamilyParamenters(rfadoc, familyFilePaths[k], parameterFilePaths[k]);
                    //获得族内参数保存为dataset
 
                    XlsFilePath = Creating_Excel_From_DataTable(gettingFamilyParamenters(rfadoc), familyFilePaths[k]);
                    rfadoc.Close();
                    openFamily.Commit();
                    //Family Family = rfadoc.LoadFamily(doc, option);
                }
            }
            catch (Exception eeffef)
            { MessageBox.Show(eeffef.ToString()); }

        }
        public DataTable gettingFamilyParamenters(Document rfadoc)
        {
            DataTable openFamilyParaDataTable = new DataTable("familyParameterTable");
            //创建带列名的列
            openFamilyParaDataTable.Columns.Add("paraname", typeof(String));
            openFamilyParaDataTable.Columns.Add("paravalue", typeof(String));
            openFamilyParaDataTable.Columns.Add("paratag", typeof(String));

            if (rfadoc.IsFamilyDocument)
            {
                FamilyManager familyMgr = rfadoc.FamilyManager;
                FamilyType currentType = familyMgr.CurrentType;
                //Element elerfadoc = rfadoc as Element;
                //ParameterSet de = familyMgr.Parameters;

                //获取参数集
                FamilyParameterSet rfadocParas = rfadoc.FamilyManager.Parameters;
                List<FamilyParameter> rfadocParasList = new List<FamilyParameter>();
                // 接下来将获取的值放入DATASET
                int countNum = 0;
                foreach (FamilyParameter rfadocPara in rfadocParas)
                {
                    rfadocParasList.Add(rfadocPara);
                }
                for (int ii = 0; ii < rfadocParasList.Count(); ii++)
                {

                    if (currentType == null)
                    {
                        MessageBox.Show("NO LI");
                        break;
                    }


                    string rfadocParaName = rfadocParasList[ii].Definition.Name;
                    if (rfadocParaName != null)
                    {

                        string rfadocParaValue = currentType.AsString(rfadocParasList[ii]);
                        bool rfadocParaTag = rfadocParasList[ii].IsInstance;
                        openFamilyParaDataTable.Rows.Add();

                        openFamilyParaDataTable.Rows[countNum]["paraname"] = rfadocParaName;
                        openFamilyParaDataTable.Rows[countNum]["paravalue"] = rfadocParaValue;
                        if (rfadocParaTag == true)
                        {
                            openFamilyParaDataTable.Rows[countNum]["paratag"] = "是";
                        }
                        else
                        {
                            openFamilyParaDataTable.Rows[countNum]["paratag"] = "否";
                        }


                    }
                    else
                    { continue; }
                    countNum++;

                }

            }
            return openFamilyParaDataTable;
        }

        public string Creating_Excel_From_DataTable(DataTable dta, string FilePath)
        {
            HSSFWorkbook workbook2003 = new HSSFWorkbook(); //新建xls工作簿  
            workbook2003.CreateSheet("Sheet1");  //新建1个Sheet工作表
            HSSFSheet SheetOne = (HSSFSheet)workbook2003.GetSheet("Sheet1"); //获取名称为Sheet1的工作表  

            for (int i = 0; i < dta.Rows.Count; i++)
            {
                IRow row = SheetOne.CreateRow(i);
                row.CreateCell(0).SetCellValue(dta.Rows[i][0].ToString());
                row.CreateCell(1).SetCellValue(dta.Rows[i][1].ToString());
                row.CreateCell(2).SetCellValue(dta.Rows[i][2].ToString());
            }
            string saveAsPath = FilePath.Replace(".rfa", "") + ".xls";

            FileStream file2003 = new FileStream(saveAsPath, FileMode.Create);
            workbook2003.Write(file2003);
            file2003.Close();
            workbook2003.Close();
            //eeeeee = saveAsPath;

            //string xlsFilePath = @"C:\ProgramData\Autodesk\Revit\Addins\2016\xlsFilePath.txt";
 
            FileStream fs = new FileStream(xlsFilePathLog, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            if (saveAsPath!= null)
            {
                sw.Write(saveAsPath + "\n");
            }
            sw.Close();
            fs.Close();

            return saveAsPath;
        }

        public string GetName()
        {
            return "ceshssi";
        }

    }
}
