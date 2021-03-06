﻿

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

namespace SEPD.RevitFamilyManager
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]

    class RevitFamilyManagerMainMB : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string sqlConnectionLocation = "server=10.193.217.38,1433;uid=sa;Password=SanWei2209;database=DB_Family_Library";
            const string defaultLoginPath = @"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding";
            const string defaultTempPath = @"C:\RFA_TMP\";
            DotNet.Utilities.LogHelper.LogMonitor("table_Log", "族库平台管理功能");
            //认证文件确认
            if (configFileX() == null)
            { }
            else
            { sqlConnectionLocation = configFileX(); }

            RevitFamilyManagerFormMB RevitFamilyManagerFormMD = new RevitFamilyManagerFormMB();
            Document doc = commandData.Application.ActiveUIDocument.Document;

            #region 用户权限控制
            //初始化数据库连接
            SqlCompose SqlCompose = new SqlCompose(sqlConnectionLocation);
            //获取当前登录用户
            string userVerifyFile = null;
            string getUserName = null;
            try
            {
                userVerifyFile = @"C:\ProgramData\Autodesk\Revit\Addins\2016\NearestUserName.txt";
                //获取认证文件中的用户名密码  进入验证环节
                StreamReader sr = new StreamReader(userVerifyFile, Encoding.Default);
                string line = sr.ReadLine();
                string[] VerifyInfo = line.Split('/');
                string currentUser = VerifyInfo[0];
                //开始操作sql
                string userVerify = "select * from table_User where u_Name = " + "'" + currentUser + "'";
                DataSet ds = SqlCompose.ExecuteSqlQuery(userVerify);
                getUserName = ds.Tables[0].Rows[0]["u_Name"].ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("无法认证当前用户，无法使用功能。");
                return Result.Succeeded;
            }
            //判断获得用户是否符合要求 若否 则直接结束该方法
            if (getUserName == null)
            {
                MessageBox.Show("当前用户权限限制，无法使用功能。");
                return Result.Succeeded;
            }
            #endregion

            RevitFamilyManagerFormMD.Show();

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
