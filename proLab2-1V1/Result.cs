﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proLab2_1V1
{
    public partial class Result : Form
    {
        public Result(Character c)
        {
            InitializeComponent();
            label2.Text = label2.Text + "  " + c.getName();
        }

        private void Result_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Application.Exit();
            this.Close();  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
