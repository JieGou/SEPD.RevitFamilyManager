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
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEPD.RevitFamilyManager.DataClass
{
    class MyMeans
    {
        #region  全局变量
        public static string loginID = "";          //定义全局变量 记录当前登录的用户编号
        public static string loginName = "";        //定义全局变量 记录当前登录的用户名
        //定义静态全局变量，记录“基础信息”各窗体中的表名 sql语句 及 要添加和修改的字段名
        public static string meanSQL = "",meanTable = "", meanField = "";
        //定义一个sqlconnection类型的静态公共变量MyCon用于判断数据库是否连接成功
        public static SqlConnection myCon;
        //定义sql server 连接字符串 用户使用时 将datasource改为自己的sql server 服务器名
        public static string myStrSqlCon = "Data Source = 10.193.217.38 ;Database = db_FamilyManager ; User id =sa ;PWD=SanWei2209 ";
        //public static string myStrSqlCon = "server = 10.193.217.38,1433;uid=sa;Password=SanWei2209;database=db_FamilyManager";
        public static int loginN = 0;
        public static string allSql = "SELECT * FROM tb_Cate";
        #endregion

        #region 数据库自定义方法
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection getCon()
        {
            myCon = new SqlConnection(myStrSqlCon);
            myCon.Open();
            return myCon;
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void closeCon()
        {
            if (myCon.State ==  ConnectionState.Open)
            {
                myCon.Close();
                myCon.Dispose();
            }
        }
        /// <summary>
        /// 只读获取信息
        /// </summary>
        /// <param name="sqlStr">执行的sql语句</param>
        /// <returns></returns>
        public SqlDataReader getRead(string sqlStr)
        {
            getCon();
            SqlCommand myRead = myCon.CreateCommand();
            myRead.CommandText = sqlStr;
            SqlDataReader myReader = myRead.ExecuteReader();
            return myReader;
        }
        /// <summary>
        /// 增删改操作
        /// </summary>
        /// <param name="sqlStr">执行的sql语句</param>
        public void getSqlCom(string sqlStr )
        {
            getCon();
            SqlCommand sqlCom = new SqlCommand(sqlStr,myCon);
            sqlCom.ExecuteNonQuery();
            sqlCom.Dispose();
            closeCon();
        }
        /// <summary>
        /// 返回弱类型dataset  常用于读操作
        /// </summary>
        /// <param name="sqlStr">执行的sql语句</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet getDataSet(string sqlStr, string tableName)
        {
            getCon();
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            SqlCommand cmdSC = new SqlCommand(sqlStr);
            sqlDA.SelectCommand = cmdSC;
            DataSet myDataSet = new DataSet();
            sqlDA.Fill(myDataSet,tableName);
            closeCon();
            return myDataSet;
        }
        /// <summary>
        /// 增删改操作数据库
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public int getInt(string sqlStr)
        {
            getCon();
            SqlCommand cmdEdit = new SqlCommand();
            cmdEdit.Connection = myCon;
            cmdEdit.CommandText = sqlStr;
            cmdEdit.CommandType = CommandType.Text;
            int arrayNum = Convert.ToInt32(cmdEdit.ExecuteNonQuery());
            return arrayNum;
        }
        #endregion
    }
}
