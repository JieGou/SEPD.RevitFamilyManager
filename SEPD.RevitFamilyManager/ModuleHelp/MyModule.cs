 

using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPD.RevitFamilyManager.ModuleClass
{
    class MyModule
    {
        #region 公共变量
        //声明
        DataClass.MyMeans myDataClass = new DataClass.MyMeans();
        public static string sqlStr = "";
        public static string searchCondition = "";
        public static string familyID = "";
        #endregion
    }
}
