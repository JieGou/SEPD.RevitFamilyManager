

using Autodesk.Revit.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
//using SEPD.RevitFamilyPlatform;
using SEPD.CommunicationModule;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.WindowsAPICodePack.Shell;
using SEPD.RevitFamilyManager.DataClass;

namespace SEPD.RevitFamilyManager
{
    //[Transaction(TransactionMode.Manual)]
    //[Regeneration(RegenerationOption.Manual)]
    //[Journaling(JournalingMode.UsingCommandData)]

    //class ModuleTestPart : IExternalCommand
    //{
    //    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    //    {

    //        MyMeans MyMeans = new MyMeans();

    //        SqlDataReader sdr =MyMeans.getRead("SELECT * FROM tb_Cate");
    //        while (sdr.Read())
    //        {
    //            MessageBox.Show(sdr[2].ToString());
    //        }



    //        return Result.Succeeded;

    //        throw new NotImplementedException();
    //    }
    //}




    class ModuleTestPart
    {
        public string LoadTag = null;
        public List<string> familyFilePaths = new List<string>();
        public List<string> parameterFilePaths = new List<string>();

        public void ModuleTestPart1(ExternalCommandData commandData)
        {

            Document doc = commandData.Application.ActiveUIDocument.Document;

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
                        Document rfadoc = commandData.Application.Application.OpenDocumentFile(familyFilePaths[k]);
                        //openFamily.Commit();
                        settingFamilyParamenters(rfadoc, familyFilePaths[k], parameterFilePaths[k]);
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

                        Document rfadoc = commandData.Application.Application.OpenDocumentFile(familyFilePaths[k]);
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
                        Document rfadoc = commandData.Application.Application.OpenDocumentFile(familyFilePaths[k]);
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
                    if (tag == 1)
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
