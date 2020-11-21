using System;
using System.Windows.Forms;

namespace xChess
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void OfflineConfig_Load(object sender, EventArgs e)
        {
            textBox1.Text = Form1.playerCnt.ToString();
            textBox2.Text = Form1.winChessNum.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.playerCnt = int.Parse(textBox1.Text);
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
