using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xChess
{
    public partial class OfflineConfig : Form
    {
        public OfflineConfig()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out int text);
            if (text < 2)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox2.Text, out int text);
            if (text < 5)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void OfflineConfig_Load(object sender, EventArgs e)
        {
            textBox1.Text = Form1.playerCou.ToString();
            textBox2.Text = Form1.winChessNum.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.playerCou = int.Parse(textBox1.Text);
            Form1.winChessNum = int.Parse(textBox2.Text);
            Close();
        }
    }
}
