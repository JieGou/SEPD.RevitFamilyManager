/*
 *  文件名（File Name）：             Class1.cs
 *  
 *  作者（Author）：                  徐经纬
 * 
 *  日期（Create Date）：             2018.1.9
 * 
 *  功能描述（Function Description）：1、制定文件头部注释格式；
 *                                    2、增加修改记录部分的格式。
 * 
 *  修改记录（Revision Histroy）：
 *      RH1：
 *          修改作者：                徐经纬
 *          修改日期：                2018.1.9
 *          修改理由：                1、制定文件头部注释格式；
 *                                    2、增加修改记录部分的格式。
 * 
 *      RH2：
 *          修改作者：                徐经纬
 *          修改日期：                2018.1.9
 *          修改理由：                1、制定文件头部注释格式；
 *                                    2、增加修改记录部分的格式。
 * 
 *  备注（Remarks）：                 可自行添加备注信息
 * 
 */


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
    public class  ExecuteEvent : IExternalEventHandler
    {
        public string LoadTag = null;
        public List<string> familyFilePaths = new List<string>();
        public List<string> parameterFilePaths = new List<string>();

        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;

            if (LoadTag == "0")
            {
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
                        settingFamilyParamenters(rfadoc, familyFilePaths[k], parameterFilePaths[k]);
                        openFamily.Commit();
                        Family Family = rfadoc.LoadFamily(doc, option);
                    }
                }
                catch (Exception eeffef)
                { MessageBox.Show(eeffef.ToString()); }

                #region 进程解放部分  (已经验证revit.exe进程占用，由于单线程原因，解除占用只能关闭revit)
                //Process.GetProcesses();
                //foreach (var ffp in familyFilePaths)
                //{
                //    string fileName = ffp;//要检查被那个进程占用的文件  

                //    Process tool = new Process();
                //    tool.StartInfo.FileName = @"C:\ProgramData\Autodesk\Revit\Addins\2016\handle.exe";
                //    tool.StartInfo.Arguments = fileName + " /accepteula";
                //    tool.StartInfo.UseShellExecute = false;
                //    tool.StartInfo.RedirectStandardOutput = true;
                //    tool.Start();
                //    tool.WaitForExit();
                //    string outputTool = tool.StandardOutput.ReadToEnd();

                //    string matchPattern = @"(?<=\s+pid:\s+)\b(\d+)\b(?=\s+)";
                //    foreach (Match match in Regex.Matches(outputTool, matchPattern))
                //    {
                //        MessageBox.Show(int.Parse(match.Value).ToString());
                //        Process.GetProcessById(int.Parse(match.Value)).Kill();
                //    }
                //}
                #endregion

                try
                {
                    foreach (var ffp in familyFilePaths)
                    {
                        if (ffp != null)
                        {
                            File.Delete(ffp);
                        }
                    }

                    foreach (var pfp in parameterFilePaths)
                    {
                        if (pfp != null)
                        {
                            File.Delete(pfp);
                        }
                    }
                }
                catch (Exception defza)
                {
                    MessageBox.Show("所选族已载入成功");
                }
            }
            else if (LoadTag == "1")//载入并附条目
            {
                try
                {
                    MyFamilyLoadOptions option = new MyFamilyLoadOptions();

                    for (int k = 0; k < familyFilePaths.Count(); k++)
                    {
                        Transaction openFamily = new Transaction(doc, "openFamily");
                        openFamily.Start();
                        //MessageBox.Show(familyFilePaths[k]);
                 
                        Document rfadoc = app.Application.OpenDocumentFile(familyFilePaths[k]);
                        //openFamily.Commit();
                        //settingFamilyParamenters(rfadoc, familyFilePaths[k], parameterFilePaths[k]);
                        openFamily.Commit();
                        Family Family = rfadoc.LoadFamily(doc, option);
                    }
                }
                catch (Exception eeffef)
                { MessageBox.Show(eeffef.ToString()); }

                try
                {
                    foreach (var ffp in familyFilePaths)
                    {
                        if (ffp != null)
                        {
                            File.Delete(ffp);
                        }
                    }

                    foreach (var pfp in parameterFilePaths)
                    {
                        if (pfp != null)
                        {
                            File.Delete(pfp);
                        }
                    }
                }
                catch (Exception defza)
                {
                    MessageBox.Show("所选族已载入成功");
                }
            }
            else if (LoadTag == "2")//载入附条目并填写值
            {
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
                        settingFamilyParamentersValue(rfadoc, familyFilePaths[k], parameterFilePaths[k]);
                        openFamily.Commit();
                        Family Family = rfadoc.LoadFamily(doc, option);
                    }
                }
                catch (Exception eeffef)
                { MessageBox.Show(eeffef.ToString()); }

                try
                {
                    foreach (var ffp in familyFilePaths)
                    {
                        if (ffp != null)
                        {
                            File.Delete(ffp);
                        }
                    }

                    foreach (var pfp in parameterFilePaths)
                    {
                        if (pfp != null)
                        {
                            File.Delete(pfp);
                        }
                    }
                }
                catch (Exception defza)
                {
                    MessageBox.Show("所选族已载入成功");
                }
            }
            //throw new NotImplementedException();
        }

        public void settingFamilyParamenters(Document doc, string familyFilePath, string parameterFilePath)
        {
            //读取xls文件
            ExcelHelper ExcelHelper = new ExcelHelper();
            DataTable dt = ExcelHelper.Reading_Excel_Information(parameterFilePath);

            //获取参数集
            FamilyParameterSet rfadocParas = doc.FamilyManager.Parameters;
            List<string> rfadocParasListName = new List<string>();
            foreach (FamilyParameter rfadocPara in rfadocParas)
            {
                rfadocParasListName.Add(rfadocPara.Definition.Name);
            }

            #region
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List<string> ls_ParameterNames = new List<string>();
                //ls_ParameterNames.Add(dt.Rows[i][0].ToString);
            }
            #endregion

            #region 族参数操作1
            FamilyManager familyMgr = doc.FamilyManager;

            //清空族内所有类型 仅保留默认族类型
            int typesizes = familyMgr.Types.Size;
            if (familyMgr.Types.Size > 1 && familyMgr.Types.Size != 0)
            {
                for (int typenumber = 0; typenumber < typesizes - 1; typenumber++)
                {
                    if (familyMgr.CurrentType != null)
                    {
                        Transaction DeleteType = new Transaction(doc, "DeleteType");
                        DeleteType.Start();
                        familyMgr.DeleteCurrentType();
                        DeleteType.Commit();
                    }
                }
            }

            //清空族内所有电气工程参数条目
            foreach (FamilyParameter fp in familyMgr.Parameters)
            {
                if (fp.Definition.ParameterGroup == BuiltInParameterGroup.PG_ELECTRICAL)
                {
                    Transaction RemoveParameter = new Transaction(doc, "RemoveParameter");
                    RemoveParameter.Start();
                    familyMgr.RemoveParameter(fp);
                    RemoveParameter.Commit();
                }
            }

            //开始添加

            Transaction addParameter = new Transaction(doc, "AddParameters");
            addParameter.Start();

            List<string> paraNames = new List<string>();
            List<bool> isInstances = new List<bool>();
            List<string> paravalues = new List<string>();
            //设置族参数的类别和类型
            BuiltInParameterGroup paraGroup = BuiltInParameterGroup.PG_ELECTRICAL;
            BuiltInParameterGroup paraGroupEx = BuiltInParameterGroup.PG_GENERAL;
            ParameterType paraType = ParameterType.Text;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string paraName = dt.Rows[i]["paraname"].ToString();
                paraNames.Add(paraName);

                //设置族参数为实例参数
                bool isInstance = true;
                if (dt.Rows[i]["paratag"].ToString() == "是")
                {
                    isInstance = true;
                }
                else
                {
                    isInstance = false;
                }
                isInstances.Add(isInstance);

                paravalues.Add(dt.Rows[i]["paravalue"].ToString());
            }

            ////获取参数集
            //FamilyParameterSet rfadocParas = doc.FamilyManager.Parameters;
            //List<string> rfadocParasListName = new List<string>();
            //foreach (FamilyParameter rfadocPara in rfadocParas)
            //{
            //    rfadocParasListName.Add(rfadocPara.Definition.Name);
            //}
 
            for (int k = 0; k < paraNames.Count(); k++)
            {
                int tag = 0;
                if (paraNames[k].Contains("M_") || paraNames[k].Contains("D_") || paraNames[k].Contains("设计-") || paraNames[k].Contains("管理-"))
                {
                    FamilyParameter newParameter = familyMgr.AddParameter(paraNames[k], paraGroup, paraType, isInstances[k]);
                }
                else
                {
                    foreach (var fpln in rfadocParasListName)
                    {
                        if (paraNames[k] == fpln)
                        { tag = 1; }
                    }
                    if (tag == 1 )
                    {
                        continue;
                    }
                    else
                    { FamilyParameter newParameter = familyMgr.AddParameter(paraNames[k], paraGroupEx, paraType, isInstances[k]); }
                }
                //创建族参数(每个参数两秒)
                //familyMgr.Set(newParameter, paravalues[k]);
 
            }

            SaveOptions opt = new SaveOptions();
            //doc.Save(opt);
            //doc.SaveAs(@"D:\"+);
            //doc.Close();
            addParameter.Commit();
            #endregion

        }

        public void settingFamilyParamentersValue(Document doc, string familyFilePath, string parameterFilePath)
        {
            //读取xls文件
            ExcelHelper ExcelHelper = new ExcelHelper();
            DataTable dt = ExcelHelper.Reading_Excel_Information(parameterFilePath);

            //获取参数集
            FamilyParameterSet rfadocParas = doc.FamilyManager.Parameters;
            List<string> rfadocParasListName = new List<string>();
            foreach (FamilyParameter rfadocPara in rfadocParas)
            {
                rfadocParasListName.Add(rfadocPara.Definition.Name);
            }

            #region
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List<string> ls_ParameterNames = new List<string>();
                //ls_ParameterNames.Add(dt.Rows[i][0].ToString);
            }
            #endregion

            #region 族参数操作1
            FamilyManager familyMgr = doc.FamilyManager;

            //清空族内所有类型 仅保留默认族类型
            int typesizes = familyMgr.Types.Size;
            if (familyMgr.Types.Size > 1 && familyMgr.Types.Size != 0)
            {
                for (int typenumber = 0; typenumber < typesizes - 1; typenumber++)
                {
                    if (familyMgr.CurrentType != null)
                    {
                        Transaction DeleteType = new Transaction(doc, "DeleteType");
                        DeleteType.Start();
                        familyMgr.DeleteCurrentType();
                        DeleteType.Commit();
                    }
                }
            }

            //清空族内所有参数条目
            foreach (FamilyParameter fp in familyMgr.Parameters)
            {
                if (fp.Definition.ParameterGroup == BuiltInParameterGroup.PG_ELECTRICAL)
                {
                    Transaction RemoveParameter = new Transaction(doc, "RemoveParameter");
                    RemoveParameter.Start();
                    familyMgr.RemoveParameter(fp);
                    RemoveParameter.Commit();
                }
            }

            //开始添加

            Transaction addParameter = new Transaction(doc, "AddParameters");
            addParameter.Start();

            List<string> paraNames = new List<string>();
            List<bool> isInstances = new List<bool>();
            List<string> paravalues = new List<string>();
            //设置族参数的类别和类型
            BuiltInParameterGroup paraGroup = BuiltInParameterGroup.PG_ELECTRICAL;
            BuiltInParameterGroup paraGroupEx = BuiltInParameterGroup.PG_GENERAL;
            ParameterType paraType = ParameterType.Text;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string paraName = dt.Rows[i]["paraname"].ToString();
                paraNames.Add(paraName);

                //设置族参数为实例参数
                bool isInstance = true;
                if (dt.Rows[i]["paratag"].ToString() == "是")
                {
                    isInstance = true;
                }
                else
                {
                    isInstance = false;
                }
                isInstances.Add(isInstance);

                paravalues.Add(dt.Rows[i]["paravalue"].ToString());

            }

            for (int k = 0; k < paraNames.Count(); k++)
            {
                int tag = 0;
                if (paraNames[k].Contains("M_") || paraNames[k].Contains("D_") || paraNames[k].Contains("设计-") || paraNames[k].Contains("管理-"))
                {
                    FamilyParameter newParameter = familyMgr.AddParameter(paraNames[k], paraGroup, paraType, isInstances[k]);
                    //创建族参数(每个参数两秒)
                    familyMgr.Set(newParameter, paravalues[k]);
                }
                else
                {
                    foreach (var fpln in rfadocParasListName)
                    {
                        if (paraNames[k] == fpln)
                        { tag = 1; }
                    }
                    if (tag == 1)
                    {
                        continue;
                    }
                    else
                    { FamilyParameter newParameter = familyMgr.AddParameter(paraNames[k], paraGroupEx, paraType, isInstances[k]); }
                }

            }
            SaveOptions opt = new SaveOptions();
            //doc.Save(opt);
            //doc.SaveAs(@"D:\"+);
            //doc.Close();
            addParameter.Commit();
            #endregion
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

    }
     
}
