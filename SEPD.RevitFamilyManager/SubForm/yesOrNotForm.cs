using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEPD.RevitFamilyManager
{
    public partial class yesOrNotForm : Form
    {
        public bool judgeFlag = false;
        public yesOrNotForm()
        {
            InitializeComponent();
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            judgeFlag = false;
            this.Close();
        }

        private void btn_yes_Click(object sender, EventArgs e)
        {
            judgeFlag = true;
            this.Close();
        }
    }
}
