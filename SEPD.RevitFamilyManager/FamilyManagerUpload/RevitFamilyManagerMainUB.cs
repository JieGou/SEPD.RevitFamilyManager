
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
using DotNet.Utilities;

namespace SEPD.RevitFamilyManager
{

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]

    class RevitFamilyManagerMainUB : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string sqlConnectionLocation = "server=10.193.217.38,1433;uid=sa;Password=SanWei2209;database=DB_Family_Library";
            const string defaultLoginPath = @"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding";
            const string defaultTempPath = @"C:\RFA_TMP\";

            DotNet.Utilities.LogHelper.LogMonitor("table_Log","族库平台批量上传功能");

            #region 下次开启清空临时空间  
            //下次开启清空临时空间
            try
            {
                if (Directory.Exists(defaultTempPath))
                {
                    Directory.Delete(defaultTempPath, true);
                }
            }
            catch (Exception efe)
            {
            }
            #endregion

            #region 认证文件确认
            //认证文件确认
            if (configFileX() == null)
            { }
            else
            { sqlConnectionLocation = configFileX(); }
            #endregion
            Document Revit_Doc = commandData.Application.ActiveUIDocument.Document;

            RevitFamilyManagerFormUB RevitFamilyManagerFormUB = new RevitFamilyManagerFormUB();
            RevitFamilyManagerFormUB.Show();
            //RevitFamilyManagerFormUL.ExEvent = ExternalEventU;

            return Result.Succeeded;
        
        }

        //配置文件检测
        private string configFileX()
        {
            List<string> configList = new List<string>();
            string sqlConnectionLocation = null;
            // 检测本地config文件是否存在
            string defaultConfigFilePath = @"C:\ProgramData\Autodesk\Revit\Addins\2016\SEPD.RevitFamilyManager.config";
            if (!File.Exists(defaultConfigFilePath))
            {
                return null;
            }
            else
            {
                //dsConnectionInfo = XmlHelper.xmlFileReader(defaultConfigFilePath);
                //预留读取配置文件代码
                StreamReader sr = new StreamReader(defaultConfigFilePath, Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    configList.Add(line.ToString());
                }

                string serverIP = configList[0];
                string sqlUserName = configList[1];
                string sqlPassword = configList[2];
                string sqlDatabase = configList[3];
                sqlConnectionLocation = "server=" + serverIP + ";uid=" + sqlUserName + ";Password=" + sqlPassword + ";database=" + sqlDatabase;

            }
            return sqlConnectionLocation;
        }
    }
}
