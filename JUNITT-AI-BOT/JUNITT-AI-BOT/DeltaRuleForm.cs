using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace WindowsFormsApp14
{
    public partial class DeltaRuleForm : MaterialForm
    {
        public DeltaRuleForm()
        {
            InitializeComponent();
            Timer1.Start();
        }

        private void DeltaRuleForm_Load(object sender, EventArgs e)
        {

        }

        private void DeltaRuleForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}

