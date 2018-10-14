
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

/// <summary>
/// 批量提取RFA文件的通用属性
/// </summary>
/// 
namespace SEPD.RevitFamilyManager
{

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    
    class RevitFamilyManagerMainIO : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            DotNet.Utilities.LogHelper.LogMonitor("table_Log", "族属性批量抽取功能");
            //提取属性
            RevitFamilyManagerFormIO RevitFamilyManagerFormIO = new RevitFamilyManagerFormIO();
            RevitFamilyManagerFormIO.Show();
 
            return Result.Succeeded;
        }
    }
}
