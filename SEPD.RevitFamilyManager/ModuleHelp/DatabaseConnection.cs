/*
 *  文件名（File Name）：               DatabaseConnection.cs
 *  
 *  作者（Author）：                    徐经纬
 * 
 *  日期（Create Date）：               2018.4.17
 * 
 *  功能描述（Assisttion Description）：1、数据库的连接与关闭；
 *                                      2、数据库数据的读取与操作。
 * 
 *  修改记录（Revision Histroy）：      无
 * 
 *  备注（Remarks）：                   无
 * 
 */

using System;
using System.Data.SqlClient;

namespace SEPD.Cable.Create.App_Code
{
    class DatabaseConnection
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private SqlConnection sqlConnection;

        /// <summary>
        /// 数据库命令
        /// </summary>
        private SqlCommand sqlCommand;

        /// <summary>
        /// 数据库数据流
        /// </summary>
        private SqlDataReader sqlDataReader;

        /// <summary>
        /// 连接数据库
        /// </summary>
        public DatabaseConnection()
        {
            string connection = "Data Source=10.193.217.38;Database=DB_Family_Library;Uid=Sa;Pwd=SanWei2209";
            sqlConnection = new SqlConnection(connection);
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        public void Open()
        {
            sqlConnection.Open();
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void Close()
        {
            sqlConnection.Close();
        }

        /// <summary>
        /// 获取读取的数据流
        /// </summary>
        /// <param name="sentence">SQL语句</param>
        /// <returns>数据流</returns>
        public SqlDataReader GetDataReader(string sentence)
        {
            sqlCommand = new SqlCommand(sentence, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();

            return sqlDataReader;
        }

        /// <summary>
        /// 获取命令执行影响行数的结果
        /// </summary>
        /// <param name="sentence">SQL语句</param>
        /// <returns>数据流</returns>
        public int GetDataReaderResult(string sentence)
        {
            sqlCommand = new SqlCommand(sentence, sqlConnection);
            int i = sqlCommand.ExecuteNonQuery();

            return i;
        }

        /// <summary>
        /// 获取命令执行第一行第一列的结果
        /// </summary>
        /// <param name="sentence">SQL语句</param>
        /// <returns>受影响的行数</returns>
        public int GetDataExecuteScalar(string sentence)
        {
            sqlCommand = new SqlCommand(sentence, sqlConnection);
            int i = Convert.ToInt32(sqlCommand.ExecuteScalar());

            return i;
        }

    }
}
