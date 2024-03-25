using System;
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
    public partial class Form1 : Form
    {
    
        public Form1()
        {
            InitializeComponent();
            textBox1.TextChanged += textBox_TextChanged;
            textBox2.TextChanged += textBox_TextChanged;
            DoubleBuffered = true;
        }
        // checking for textbox
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender; // Cast sender to TextBox
            string text = textBox.Text;
            string newText = string.Empty;

            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    newText += c;
                }
            }

            textBox.Text = newText;
            textBox.SelectionStart = textBox.Text.Length;

            if (newText.Length > 4)
            {
                textBox.Text = newText.Substring(0, 4);
                textBox.SelectionStart = 4;
            }
        }
        //start button 
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            button1.Visible = false;
        }
        //map size button
        public void button2_Click(object sender, EventArgs e)
        {

                if (textBox3.Text == null || textBox3.Text == "" || textBox3.Text == " " || textBox3.Text == "   ")
                {
                    Character character = new Character(Convert.ToInt16(textBox1.Text), Convert.ToInt16(textBox2.Text));
                    startGame(character);
                }
                else
                {
                    Character character = new Character(textBox3.Text, Convert.ToInt16(textBox1.Text), Convert.ToInt16(textBox2.Text));
                    startGame(character);
                }
        }

        public void startGame(Character c)
        {
            try
            {
                if (textBox1.Text[0] == '0' || textBox2.Text[0] == '0')
                {
                    throw new Exception();
                }
                if (Convert.ToInt16(textBox1.Text) > 1920 || Convert.ToInt16(textBox1.Text) < 50)
                {
                    label5.Visible = true;
                    textBox1.Text = null;
                }
                else if (Convert.ToInt16(textBox2.Text) > 1080 || Convert.ToInt16(textBox2.Text) < 50)
                {
                    label5.Visible = true;
                    textBox2.Text = null;
                }
                else
                {
                    label5.Visible = false;
                    Grid gridForm = new Grid(Convert.ToInt16(textBox1.Text), Convert.ToInt16(textBox2.Text), c);
                    this.Hide();
                    gridForm.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please Check value of x and y");
            }
             
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
