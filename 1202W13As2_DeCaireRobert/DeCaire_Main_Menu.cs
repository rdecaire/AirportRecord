﻿using System;
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
        DeCaire_Airport_API airport = new DeCaire_Airport_API();
        internal static List<DeCaire_Airport_Report> reportList = new List<DeCaire_Airport_Report>();
        
        public DeCaire_Main_Menu()
        {
            
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new DeCaire_New_Record().Show();
        }

        private void DeCaire_Main_Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new DeCaire_Report_Display(reportList).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            CallList searchReturn = airport.SearchAirCode(searchText);
            new DeCaire_Search_Result(searchReturn).Show();
        }
    }
}
