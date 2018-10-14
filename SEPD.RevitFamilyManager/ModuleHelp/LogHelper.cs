/*
 *  文件名（File Name）：             Class1.cs
 *  
 *  作者（Author）：                  徐经纬
 * 
 *  日期（Create Date）：             2018.1.9
 * 
 *  功能描述（Assisttion Description）：1、制定文件头部注释格式；
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
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace SEPD.Cable.Create.App_Code
{
    /// <summary>
    /// 操作日志记录类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 操作日志记录
        /// </summary>
        /// <param name="opType">需要记录的信息</param>
        public static void LogMonitor(string opType)
        {
            string opDate = null;
            string opIP = null;
            string currentUser = null;
            string userVerifyFile = @"C:\ProgramData\Autodesk\Revit\Addins\2016\NearestUserName.txt";
            try
            {

                if (File.Exists(userVerifyFile))
                {
                    StreamReader sr = new StreamReader(userVerifyFile, Encoding.Default);
                    string line = sr.ReadLine();
                    string[] VerifyInfo = line.Split('/');
                    currentUser = VerifyInfo[0]; 
                }
                else
                {
                    currentUser = "无工号";
                }

                //本机当前使用的IPv4地址
                IPAddress ipAddr = Dns.Resolve(Dns.GetHostName()).AddressList[0]; //获得当前IP地址
                opIP = ipAddr.ToString();

                //当前操作时间
                opDate = DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString();
                
                //写入数据库
                DatabaseConnection databaseConnection=new DatabaseConnection();
                databaseConnection.Open();

                string opSQL = "INSERT INTO table_Log_cable (log_OpDate,log_OpIP,log_OpType,log_OpUser) VALUES ('" + opDate +
                               "','" + opIP + "','" + opType + "','" + currentUser + "')";

                int i = databaseConnection.GetDataExecuteScalar(opSQL);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
