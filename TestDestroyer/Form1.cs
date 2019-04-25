using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace TestDestroyer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            radioButton2.Checked = false;
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                if (radioButton1.Checked)
                {
                    Commons cm = new Commons();
                    cm.ExecuteCommandNunit(textBox1.Text);
                }
                else
                {
                    Commons cm = new Commons();
                    cm.ExecuteCommandMsTest(textBox1.Text);
                }

                MessageBox.Show("Done!!!");
            }
            else
            {
                MessageBox.Show("Please, choose a testing framework option...");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Commons cm = new Commons();
            string file = cm.ChooseFile();
            textBox1.Text = file;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
        }
    }
}
