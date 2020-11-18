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

        private void newChessBroard()
        {
            playerNum = 1;
            label1.Text = "当前棋手:玩家1";
            rowNum = (groupBox1.Height - 12) / 36;
            columnNum = (groupBox1.Width - 12) / 36;
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < columnNum; j++)
                {
                    groupBox1.Controls.Remove(Btn[i, j]);
                }
            }
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < columnNum; j++)
                {
                    Btn[i, j] = new chessBtn
                    {
                        btnPosX = i+1,
                        btnPosY = j+1,
                        Location = new Point(12 + 36 * j, 12 + 36 * i),
                        Size = new Size(30, 30),
                        Text = "",
                        TabStop = false,
                    };
                    groupBox1.Controls.Add(Btn[i, j]);
                    Btn[i, j].Click += new EventHandler(Btn_Click);
                }
            }
            for (int i = 1; i <= rowNum; i++)
            {
                for (int j = 1; j <= columnNum; j++)
                {
                    playerNameTmp[i, j] = 0;
                }
            }
        }

        private void ifPlayerWin()
        {
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < columnNum; j++)
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

            if(ifWin) label1.Text = "玩家" + playerNum.ToString() + "胜利!!";
            else
            {
                if (playerNum == playerCou) playerNum = 1;
                else playerNum++;
                label1.Text = "当前棋手:玩家" + playerNum.ToString();
            } 
        }

        private void 单机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OfflineConfig offlineConfig = new OfflineConfig();
            offlineConfig.Show();
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("默认双人五子棋，五子连一线者获胜\n玩家人数与获胜子数可在设置中自定义\n拖动放大窗体后点击新游戏即可扩大棋盘","帮助",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("作者 : 荆棘Justin\n邮箱 : i@jsun969.cn\n开源地址 : https://github.com/jsun969/xChes","关于");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Width = Width - 39;
            newChessBroard();
            playerCou = 2;
            winChessNum = 5;
            playerColor[1] = "Red";
            playerColor[2] = "Blue";
            playerColor[3] = "Pink";
            playerColor[4] = "Green";
            playerColor[5] = "Yellow";
            playerColor[6] = "Cyan";
            playerColor[7] = "Orange";
            playerColor[8] = "Lime";
            playerColor[9] = "Purple";
        }
    }
}
