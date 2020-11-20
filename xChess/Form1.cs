using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace xChess
{
    public partial class Form1 : Form
    {
        private chessBtn[,] Btn = new chessBtn[1005, 1005];
        private int rowNum,columnNum;
        public static int playerCou;
        public static string[] playerColor = new string[15];
        private int playerNum;
        public static int winChessNum;
        private int[,] playerNameTmp = new int[1005,1005];
        private bool ifWin;
        public Form1()
        {
            InitializeComponent();
        }

        public void newChessBroard()
        {
            playerNum = 1;
            label1.Text = "当前棋手:玩家1";
            label2.BackColor = Color.FromName(playerColor[1]);
            rowNum = (groupBox1.Height - 12) / 36;
            columnNum = (groupBox1.Width - 12) / 36;
            for (int i = 1; i <= rowNum; i++)
            {
                for (int j = 1; j <= columnNum; j++)
                {
                    groupBox1.Controls.Remove(Btn[i, j]);
                }
            }
            for (int i = 1; i <= rowNum; i++)
            {
                for (int j = 1; j <= columnNum; j++)
                {
                    Btn[i, j] = new chessBtn
                    {
                        btnPosX = i+1,
                        btnPosY = j+1,
                        Location = new Point(12 + 36 * (j-1), 12 + 36 * (i-1)),
                        Size = new Size(30, 30),
                        Text = "",
                        TabStop = false,
                    };
                    groupBox1.Controls.Add(Btn[i, j]);
                    Btn[i, j].Click += new EventHandler(Btn_Click);
                    playerNameTmp[i, j] = 0;
                }
            }
        }

        private void ifPlayerWin()
        {
            for (int i = 1; i <= rowNum; i++)
            {
                for (int j = 1; j <= columnNum; j++)
                {
                    Btn[i, j].Enabled = false;
                }
            }
            ifWin = true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            label1.Width = Width-39;
            groupBox1.Width = Width - 42;
            groupBox1.Height = Height - 156;
        }

        private void 新游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ifWin = false;
            newChessBroard();
        }


        private void Btn_Click(object sender, EventArgs e)
        {
            chessBtn chessBt = (chessBtn)sender;
            chessBt.Enabled = false;
            chessBt.BackColor = Color.FromName(playerColor[playerNum]);
            playerNameTmp[chessBt.btnPosX, chessBt.btnPosY] = playerNum;

            int cou = 0;
            int i,j;
            //行 
            for (i = chessBt.btnPosY + 1; playerNameTmp[chessBt.btnPosX,i] == playerNum; i++)
            {
                cou++;
            }
            for (i = chessBt.btnPosY - 1; playerNameTmp[chessBt.btnPosX, i] == playerNum; i--)
            {
                cou++;
            }
            if (cou == winChessNum-1) ifPlayerWin();
            //列 
            cou = 0;
            for (i = chessBt.btnPosX + 1; playerNameTmp[i, chessBt.btnPosY] == playerNum; i++)
            {
                cou++;
            }
            for (i = chessBt.btnPosX - 1; playerNameTmp[i, chessBt.btnPosY] == playerNum; i--)
            {
                cou++;
            }
            if (cou == winChessNum - 1) ifPlayerWin();
            //右对角线
            cou = 0;
            i = chessBt.btnPosX + 1;
            j = chessBt.btnPosY + 1;
            while (playerNameTmp[i, j] == playerNum)
            {
                cou++;
                i++;
                j++;
            }
            i = chessBt.btnPosX - 1;
            j = chessBt.btnPosY - 1;
            while (playerNameTmp[i, j] == playerNum)
            {
                cou++;
                i--;
                j--;
            }
            if (cou == winChessNum - 1) ifPlayerWin();
            //左对角线
            cou = 0;
            i = chessBt.btnPosX - 1;
            j = chessBt.btnPosY + 1;
            while (playerNameTmp[i, j] == playerNum)
            {
                cou++;
                i--;
                j++;
            }
            i = chessBt.btnPosX + 1;
            j = chessBt.btnPosY - 1;
            while (playerNameTmp[i, j] == playerNum)
            {
                cou++;
                i++;
                j--;
            }
            if (cou == winChessNum - 1) ifPlayerWin();

            if (ifWin)
            {
                label2.BackColor = Color.FromName(playerColor[playerNum]);
                label1.Text = "玩家" + playerNum.ToString() + "胜利!!";
            }
            else
            {
                if (playerNum == playerCou) playerNum = 1;
                else playerNum++;
                label1.Text = "当前棋手:玩家" + playerNum.ToString();
                label2.BackColor = Color.FromName(playerColor[playerNum]);
            } 
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.playerCou = playerCou;
            Properties.Settings.Default.winChessNum = winChessNum;
            Properties.Settings.Default.formWidth = Width;
            Properties.Settings.Default.formHeight = Height;
            if (WindowState == FormWindowState.Maximized) Properties.Settings.Default.ifMaximize = true;
            else Properties.Settings.Default.ifMaximize = false;
            Properties.Settings.Default.Save();
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings offlineConfig = new Settings();
            offlineConfig.Show();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "导出",
                Filter = "xChess存档|*.xchs",
                DefaultExt = ".xchs",
                FileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss_ffff") + ".xchs"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string outputData = Height.ToString() + "\n" + Width.ToString() + "\n" + playerCou.ToString() + "\n" + winChessNum.ToString();

                File.WriteAllText(sfd.FileName, Convert.ToBase64String(Encoding.UTF8.GetBytes(outputData)));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            playerCou = Properties.Settings.Default.playerCou;
            winChessNum = Properties.Settings.Default.winChessNum;
            Width = Properties.Settings.Default.formWidth;
            Height = Properties.Settings.Default.formHeight;
            if(Properties.Settings.Default.ifMaximize) WindowState = FormWindowState.Maximized;
            playerColor[1] = "Red";
            playerColor[2] = "Blue";
            playerColor[3] = "Pink";
            playerColor[4] = "Green";
            playerColor[5] = "Yellow";
            playerColor[6] = "Cyan";
            playerColor[7] = "Orange";
            playerColor[8] = "Lime";
            playerColor[9] = "Purple";
            label1.Width = Width - 39;
            newChessBroard();
        }
    }
}
