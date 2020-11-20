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
    public partial class Settings : Form
    {
        public Settings()
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
            trackBar1.Value = int.Parse(textBox1.Text);
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
            trackBar2.Value = int.Parse(textBox2.Text);
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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar2.Value.ToString();
        }
    }
}
