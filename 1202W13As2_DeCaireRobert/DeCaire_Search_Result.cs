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
    public partial class DeCaire_Search_Result : Form
    {
        public DeCaire_Search_Result(CallList searchList)
        {
            InitializeComponent();
            string output = "Search Results\r\n\r\n";
            foreach (Call call in searchList)
            {
                if (!string.IsNullOrEmpty(call.code))
                {
                    output += "Code: " + call.code + "\r\n";
                    output += "Airport: " + call.name + "\r\n";
                    output += "Location: " + call.location + "\r\n";
                    output += "\r\n";
                }
            }
            textBox1.Text = output;
            textBox1.Select(0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeCaire_Search_Result_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close the report?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
