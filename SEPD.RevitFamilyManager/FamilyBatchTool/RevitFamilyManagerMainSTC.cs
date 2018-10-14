
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
using Autodesk.Revit.UI.Selection;
namespace SEPD.RevitFamilyManager
{

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]

    class RevitFamilyManagerMainSTC : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            DataTable dt = new DataTable("familyInstance");
            dt.Columns.Add("实例名称", typeof(String));
            dt.Columns.Add("选定参数值", typeof(String));
            Document Revit_Doc = commandData.Application.ActiveUIDocument.Document;

            //获取文件中所有元素
            FilteredElementCollector all_Elements = new FilteredElementCollector(Revit_Doc);
            //建立族类型过滤器（其中FamilySymbol可做替换）
            ElementClassFilter class_Filter = new ElementClassFilter(typeof(FamilyInstance));
            //建立类别过滤器（其中OST_Lights可做替换，例如OST_StackedWalls）
            ElementCategoryFilter cat_Filter = new ElementCategoryFilter(BuiltInCategory.OST_ElectricalFixtures);

            all_Elements = all_Elements.WherePasses(class_Filter).WherePasses(cat_Filter);

            FamilyInstance fi = null;
            List<FamilyInstance> filteredList = new List<FamilyInstance>();
 
            var filteresd = from e in all_Elements
                            //where e.Name.Substring(0, 3) == "IBS"
                            where e.Name != null
                            select e as FamilyInstance;
            filteredList = filteresd.ToList<FamilyInstance>();

    
            //for (int ii =0; ii<filteredList.Count() ;ii++)
            //{
            //    dt.Rows.Add();
            //    dt.Rows[ii]["实例名称"] = filteredList[ii].Name;

            //    Parameter para = filteredList[ii].LookupParameter("长度");
            //    if (para != null)
            //    {
            //        string parav = para.AsValueString();
            //        dt.Rows[ii]["选定参数值"] = parav;
            //    }
            //}

            RevitFamilyManagerFormSTC RevitFamilyManagerFormSTC = new RevitFamilyManagerFormSTC();
            //RevitFamilyManagerFormSTC.fidt = dt;
            RevitFamilyManagerFormSTC.filteredList = filteredList;
            RevitFamilyManagerFormSTC.ShowDialog();
  
            return Result.Succeeded;
        }
    }
}
