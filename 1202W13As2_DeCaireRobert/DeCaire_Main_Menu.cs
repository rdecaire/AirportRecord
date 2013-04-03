using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1202W13As2_DeCaireRobert
{
    public partial class DeCaire_Main_Menu : Form
    {
        public static List<DeCaire_Airport_Report> reportList = new List<DeCaire_Airport_Report>();
        public DeCaire_Main_Menu()
        {
            
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }
    }
}
