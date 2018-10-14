

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
using System.Windows.Media.Imaging;
using Model;


using System.Collections.ObjectModel;

using System.Reflection;

//using SEPD.Cable.Create.App_Code;
//using SEPD.Cable.Create.Models;

namespace SEPD.RevitFamilyManager
{
    [TransactionAttribute(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class RevitFamilyManagerRibbon : IExternalApplication
    {
        //string GenDllPath = Assembly.GetExecutingAssembly().Location;
        string GenDllPath = @"C:\ProgramData\Autodesk\Revit\Addins\2016\";
        public Result OnShutdown(UIControlledApplication application)
        {
            return 0;
            //throw new NotImplementedException();
        }

        public Result OnStartup(UIControlledApplication application)
        {
            DotNet.Utilities.LogHelper.LogSearch();
            //string plugin_Revit_Family_Platform = GenDllPath + "SEPD.RevitFamilyManager.dll";
            string plugin_Revit_Family_Platform = Assembly.GetExecutingAssembly().Location;
            //string HelpPath = GenDllPath + "SepdHelper.dll";
            string HelpPath = Assembly.GetExecutingAssembly().Location;

            //MessageBox.Show(plugin_Revit_Family_Platform +"\n\n" +HelpPath);
            //-----------------------------------------------------------------------------------------------//
            //添加一个新的选项卡
            application.CreateRibbonTab("SEPD通用功能s");
            //添加新的信息交互ribbon面板

            RibbonPanel creatInfoFamilyPlatform = application.CreateRibbonPanel("SEPD通用功能s", "族库管理");
            RibbonPanel creatInfoFamilyBatch = application.CreateRibbonPanel("SEPD通用功能s", "批量工具");
            //RibbonPanel creatInfoTransformer = application.CreateRibbonPanel("SEPD通用功能", "信息交互");
            RibbonPanel creatInfoTransformerHelp = application.CreateRibbonPanel("SEPD通用功能s", "快速帮助");
            //----------------------------------------------------------------------------------------------//
            #region 创建一个为族库上传平台的按钮
            PushButtonData CreateRevitManagerUploadData = new PushButtonData("1", "族库上传管理", plugin_Revit_Family_Platform, "SEPD.RevitFamilyManager.RevitFamilyManagerMainUB");
            PushButton CreateRevitManagerUploadButton = creatInfoFamilyPlatform.AddItem(CreateRevitManagerUploadData) as PushButton;
            //CreateRevitManagerUploadButton.LargeImage = new BitmapImage(new Uri(@"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\ICONpm\ICON-RevitFamilyPlatform.png"));
            CreateRevitManagerUploadButton.LargeImage = Func.ChangeBitmapToImageSource(Resource1.族管理上传ICO);
            //creatInfoFamilyPlatform.AddItem(CreateRevitManagerUploadData);
            //creatInfoFamilyPlatform.AddSeparator();
            CreateRevitManagerUploadButton.ToolTip = "族库上传管理";
            CreateRevitManagerUploadButton.LongDescription = "进入族库上传平台";
            #endregion
            //----------------------------------------------------------------------------------------------//
            #region 创建一个为族库下载平台的按钮
            PushButtonData CreateRevitManagerDownloadData = new PushButtonData("2", "族库下载管理", plugin_Revit_Family_Platform, "SEPD.RevitFamilyManager.RevitFamilyManagerMainDB");
            PushButton CreateRevitManagerDownloadButton = creatInfoFamilyPlatform.AddItem(CreateRevitManagerDownloadData) as PushButton;
            //CreateRevitManagerDownloadButton.LargeImage = new BitmapImage(new Uri(@"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\ICONpm\ICON-RevitFamilyPlatform.png"));
            CreateRevitManagerDownloadButton.LargeImage = Func.ChangeBitmapToImageSource(Resource1.族管理下载ICO);
            //creatInfoFamilyPlatform.AddItem(CreateRevitManagerDownloadData);
            //creatInfoFamilyPlatform.AddSeparator();
            CreateRevitManagerDownloadButton.ToolTip = "族库下载管理";
            CreateRevitManagerDownloadButton.LongDescription = "进入族库下载平台";
            #endregion
            //-----------------------------------------------------------------------------------------------//
            #region 创建一个为族库删改管理平台的按钮
            PushButtonData CreateRevitManagerRfaInfoChange = new PushButtonData("3", "族库删改管理", plugin_Revit_Family_Platform, "SEPD.RevitFamilyManager.RevitFamilyManagerMainMB");
            PushButton CreateRevitManagerRfaInfoChangeButton = creatInfoFamilyPlatform.AddItem(CreateRevitManagerRfaInfoChange) as PushButton;
            //CreateRevitManagerUploadButton.LargeImage = new BitmapImage(new Uri(@"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\ICONpm\ICON-RevitFamilyPlatform.png"));
            //CreateRevitManagerDownloadButton.LargeImage = new BitmapImage(new Uri(str + "\\SepdBuliding\\ICONpm" + "\\ICON-RevitFamilyBacthAlter.png", UriKind.RelativeOrAbsolute));
            //CreateRevitManagerBatchChangeButton.LargeImage = Func.ChangeBitmapToImageSource(Resource1.族管理上传ICO);
            CreateRevitManagerRfaInfoChangeButton.LargeImage = Func.ChangeBitmapToImageSource(Resource1.族库删改管理);
            CreateRevitManagerRfaInfoChangeButton.ToolTip = "实例图元属性批量修改";
            CreateRevitManagerRfaInfoChangeButton.LongDescription = "实例图元属性从excel批量修改";
            #endregion
            //-----------------------------------------------------------------------------------------------//
            #region 创建一个为族库批量上传平台的按钮
            //PushButtonData CreateRevitManagerBatchUploadData = new PushButtonData("3", "族库批量上传", plugin_Revit_Family_Platform, "SEPD.RevitFamilyManager.RevitFamilyManagerMainULS");
            //PushButton CreateRevitManagerBatchUploadButton = creatInfoFamilyBatch.AddItem(CreateRevitManagerBatchUploadData) as PushButton;
            ////CreateRevitManagerUploadButton.LargeImage = new BitmapImage(new Uri(@"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\ICONpm\ICON-RevitFamilyPlatform.png"));
            //CreateRevitManagerBatchUploadButton.LargeImage = Func.ChangeBitmapToImageSource(Resource1.族批量上传);
            ////creatInfoFamilyPlatform.AddItem(CreateRevitManagerUploadData);
            ////creatInfoFamilyPlatform.AddSeparator();
            //CreateRevitManagerBatchUploadButton.ToolTip = "族库批量上传";
            //CreateRevitManagerBatchUploadButton.LongDescription = "进入族库批量上传平台";
            #endregion
            //----------------------------------------------------------------------------------------------//
            #region 创建一个为属性批量提取的按钮
            PushButtonData CreateRevitManagerBatchGrub = new PushButtonData("4", "属性批量提取", plugin_Revit_Family_Platform, "SEPD.RevitFamilyManager.RevitFamilyManagerMainIO");
            PushButton CreateRevitManagerBatchGrubButton = creatInfoFamilyBatch.AddItem(CreateRevitManagerBatchGrub) as PushButton;
            //CreateRevitManagerUploadButton.LargeImage = new BitmapImage(new Uri(@"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\ICONpm\ICON-RevitFamilyPlatform.png"));
            CreateRevitManagerBatchGrubButton.LargeImage = Func.ChangeBitmapToImageSource(Resource1.族属性批量提取);
            //creatInfoFamilyPlatform.AddItem(CreateRevitManagerUploadData);
            //creatInfoFamilyPlatform.AddSeparator();
            CreateRevitManagerBatchGrubButton.ToolTip = "属性批量提取";
            CreateRevitManagerBatchGrubButton.LongDescription = "从rfa文件中进行属性批量提取生产excel表格";
            #endregion
            //----------------------------------------------------------------------------------------------//
            #region 创建一个为属性批量修改的按钮
            PushButtonData CreateRevitManagerBatchChange = new PushButtonData("5", "属性批量修改", plugin_Revit_Family_Platform, "SEPD.RevitFamilyManager.RevitFamilyManagerMainEP");
            PushButton CreateRevitManagerBatchChangeButton = creatInfoFamilyBatch.AddItem(CreateRevitManagerBatchChange) as PushButton;
            //CreateRevitManagerUploadButton.LargeImage = new BitmapImage(new Uri(@"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\ICONpm\ICON-RevitFamilyPlatform.png"));
            CreateRevitManagerBatchChangeButton.LargeImage = Func.ChangeBitmapToImageSource(Resource1.属性批量修改);
            //creatInfoFamilyPlatform.AddItem(CreateRevitManagerUploadData);
            //creatInfoFamilyPlatform.AddSeparator();
            CreateRevitManagerBatchChangeButton.ToolTip = "实例图元属性批量修改";
            CreateRevitManagerBatchChangeButton.LongDescription = "实例图元属性从excel批量修改";
            #endregion
            //----------------------------------------------------------------------------------------------//
        
            #region 创建一个IFC文件批量打开保存的按钮
            PushButtonData CreateIFCtoRVT = new PushButtonData("7", "IFC批量转换", plugin_Revit_Family_Platform, "SEPD.IFCtoRVT.IFCtoRVTMain");
            PushButton CreateIFCtoRVTButton = creatInfoFamilyBatch.AddItem(CreateIFCtoRVT) as PushButton;
            //CreateRevitManagerUploadButton.LargeImage = new BitmapImage(new Uri(@"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\ICONpm\ICON-RevitFamilyPlatform.png"));
            //CreateRevitManagerDownloadButton.LargeImage = new BitmapImage(new Uri(str + "\\SepdBuliding\\ICONpm" + "\\ICON-RevitFamilyBacthAlter.png", UriKind.RelativeOrAbsolute));
            //CreateRevitManagerBatchChangeButton.LargeImage = Func.ChangeBitmapToImageSource(Resource1.族管理上传ICO);
            CreateIFCtoRVTButton.LargeImage = Func.ChangeBitmapToImageSource(Resource1.IFC文件转RVT文件);
            CreateIFCtoRVTButton.ToolTip = "IFC文件转RVT文件";
            CreateIFCtoRVTButton.LongDescription = "IFC文件转RVT文件";
            #endregion
            //-----------------------------------------------------------------------------------------------//
            #region 创建一个参数统计的按钮
            PushButtonData CreateSuperCount = new PushButtonData("8", "参数统计", plugin_Revit_Family_Platform, "SEPD.RevitFamilyManager.RevitFamilyManagerMainSTC");
            PushButton CreateSuperCountButton = creatInfoFamilyBatch.AddItem(CreateSuperCount) as PushButton;
            //CreateRevitManagerUploadButton.LargeImage = new BitmapImage(new Uri(@"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\ICONpm\ICON-RevitFamilyPlatform.png"));
            //CreateRevitManagerDownloadButton.LargeImage = new BitmapImage(new Uri(str + "\\SepdBuliding\\ICONpm" + "\\ICON-RevitFamilyBacthAlter.png", UriKind.RelativeOrAbsolute));
            //CreateRevitManagerBatchChangeButton.LargeImage = Func.ChangeBitmapToImageSource(Resource1.族管理上传ICO);
            CreateSuperCountButton.LargeImage = Func.ChangeBitmapToImageSource(Resource1.参数统计);
            CreateSuperCountButton.ToolTip = "参数统计";
            CreateSuperCountButton.LongDescription = "参数统计";
            #endregion
            //-----------------------------------------------------------------------------------------------//
            #region 创建一个为帮助的按钮
            PushButtonData CreatHelpData = new PushButtonData("1", "帮助", HelpPath, "SepdHelper.Helper");
            PushButton CreatHelpButton = creatInfoTransformerHelp.AddItem(CreatHelpData) as PushButton;
            //CreatHelpButton.LargeImage = new BitmapImage(new Uri(@"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\ICONpm\帮助说明文档.png", UriKind.RelativeOrAbsolute));
            CreatHelpButton.LargeImage = Func.ChangeBitmapToImageSource(Resource1.帮助说明文档);
            //creatInfoTransformerHelp.AddItem(CreatHelpData);
            //creatInfoTransformerHelp.AddSeparator();
            CreatHelpButton.ToolTip = "帮助";
            CreatHelpButton.LongDescription = "点击按钮进行帮助查看";
            #endregion

            return Result.Succeeded;
        }
    }
}
