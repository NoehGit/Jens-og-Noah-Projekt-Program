using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jens_og_Noah_Projekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        // ASCII APP :
        public static double Brightness(Color c)
        {
            return (0.2126 * c.R + 0.7152 * c.G + 0.0722 * c.B);
        }

        private void inputSelectBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string inputFilename = openFileDialog1.FileName;
                inputTextbox.Text = inputFilename;

                progressBar1.Value = 0;

                if (inputTextbox.Text != "" && outputTextbox.Text != "")
                {
                    goBtn.Enabled= true;
                }

                try
                {
                    var image = new Bitmap(inputFilename);
                    int pixelAmount = image.Height * image.Width;
                    progressBar1.Maximum = pixelAmount;
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Den valgte fil er ikke et billede, prøv igen");
                    inputTextbox.Clear();
                    goBtn.Enabled = false;
                    return;
                }
            }
        }

        private void outputSelectBtn_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = "c:\\";
            DialogResult result = openFileDialog2.ShowDialog();
            if (result == DialogResult.OK)
            {
                string outputFilename = openFileDialog2.FileName;
                outputTextbox.Text = outputFilename;
                progressBar1.Value=0;
            }
            if (inputTextbox.Text != "" && outputTextbox.Text != "")
            {
                goBtn.Enabled= true;
            }
        }

        private void goBtn_Click(object sender, EventArgs e)
        {
            string imageLocation = inputTextbox.Text;
            string outputLocation = outputTextbox.Text;
            string AsciiColor = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/|()1{}[]?-_+~<>i!lI;:,^'. ";

            var img = new Bitmap(imageLocation);

            using (var wrtr = new StreamWriter(outputLocation))
            {
                for (var y = 0; y < img.Height; y++)
                {
                    for (var x = 0; x < img.Width; x++)
                    {
                        var color = img.GetPixel(x, y);
                        var brightness = Brightness(color);
                        var idx = brightness / 255 * (AsciiColor.Length - 1);
                        var pxl = AsciiColor[AsciiColor.Length - (int)Math.Round(idx) - 1];
                        wrtr.Write(pxl);
                        progressBar1.Step = 1;
                        progressBar1.PerformStep();
                    }
                    wrtr.WriteLine();
                }
            }
            showBtn.Enabled= true;
        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            string path = outputTextbox.Text;
            System.Diagnostics.Process.Start(path);
        }

        // END OF ASCII APP

        // TIC TAC TOE APP :

        String[] gameBoard = new string[9];
        int currentTurn = 0;

        public String returnSymbol(int turn)
        {
            if (turn % 2 == 0)
            {
                return "O";
            }
            else
            {
                return "X";
            }
        }

        public void checkForWinner()
        {
            for (int i=0; i<8; i++)
            {
                String combination = "";

                switch (i)
                {
                    case 0:
                        combination= gameBoard[0] + gameBoard[4] + gameBoard[8];
                        break;
                    case 1:
                        combination = gameBoard[2] + gameBoard[4] + gameBoard[6];
                        break;
                    case 2:
                        combination = gameBoard[0] + gameBoard[1] + gameBoard[2];
                        break;
                    case 3:
                        combination = gameBoard[3] + gameBoard[4] + gameBoard[5];
                        break;
                    case 4:
                        combination = gameBoard[6] + gameBoard[7] + gameBoard[8];
                        break;
                    case 5:
                        combination = gameBoard[0] + gameBoard[3] + gameBoard[6];
                        break;
                    case 6:
                        combination = gameBoard[2] + gameBoard[4] + gameBoard[7];
                        break;
                    case 7:
                        combination = gameBoard[3] + gameBoard[5] + gameBoard[8];
                        break;
                }

                if (combination.Equals("OOO"))
                {
                    reset();
                    MessageBox.Show("O har vundet!", "Vinderen er fundet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        public void reset()
        {
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            button7.Text = "";
            button8.Text = "";
            button9.Text = "";

            gameBoard = new string[9];
            currentTurn = 0;
        }

        public void checkDraw()
        {
            int counter = 0;
            for (int i=0; i<gameBoard.Length; i++)
            {
                if (gameBoard[i] != null)
                {
                    counter++;
                }
                if (counter== 9)
                {
                    reset();
                    MessageBox.Show("Spillet er uafgjort", "Vinderen er ikke fundet?!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                checkDraw();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[0] = returnSymbol(currentTurn);
            button1.Text = gameBoard[0];
            checkForWinner();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[1] = returnSymbol(currentTurn);
            button2.Text = gameBoard[1];
            checkForWinner();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[2] = returnSymbol(currentTurn);
            button3.Text = gameBoard[2];
            checkForWinner();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[3] = returnSymbol(currentTurn);
            button4.Text = gameBoard[3];
            checkForWinner();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[4] = returnSymbol(currentTurn);
            button5.Text = gameBoard[4];
            checkForWinner();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[5] = returnSymbol(currentTurn);
            button6.Text = gameBoard[5];
            checkForWinner();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[6] = returnSymbol(currentTurn);
            button7.Text = gameBoard[6];
            checkForWinner();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[7] = returnSymbol(currentTurn);
            button8.Text = gameBoard[7];
            checkForWinner();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[8] = returnSymbol(currentTurn);
            button9.Text = gameBoard[8];
            checkForWinner();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}
