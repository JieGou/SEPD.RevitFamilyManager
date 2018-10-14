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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace SEPD.RevitFamilyManager
{
     
    public static class DataUB
    {
        public static DataSet dataTempSet  ;
        public static DataTable dataTempTable;
        public static DataTable dataForDGV2;
        public static DataTable confChangeBF;
        public static DataTable confChangeAF;
        //    familyInfo.Columns.Add("familyNamess", typeof(String));
        //    familyInfo.Columns.Add("familyPathss", typeof(String));    
        //    familyInfo.Columns.Add("familyParass", typeof(String));
        //    familyInfo.Columns.Add("familyPicss", typeof(String));
    }
}
