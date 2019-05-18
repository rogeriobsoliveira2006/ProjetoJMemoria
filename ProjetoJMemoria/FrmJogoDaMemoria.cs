using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoJMemoria
{
    public partial class FrmJogoDaMemoria : Form
    {
        int movimentos, cliques, cartasEncontradas, tagIndex;
        Image[] img = new Image[12];
        List<string> lista = new List<string>();
        int[] tags = new int[2];

        public FrmJogoDaMemoria()
        {
            InitializeComponent();
            Inicio();
        }

        private void Inicio()
        {
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                tagIndex = int.Parse(string.Format("{0}", item.Tag));
                img[tagIndex] = item.Image;
                item.Image = Properties.Resources.Verso;
                item.Enabled = true;
            }
            Posicoes();
        }

        private void Posicoes()
        {
            int[,] matrizXY = { { 105, 170 }, { 197, 170 }, { 288, 170 }, { 105, 296 }, { 197, 296 }, { 288, 296 }, { 151, 422 }, { 246, 422 }, { 426, 170 }, { 519, 170 }, { 388, 296 }, { 480, 296 }, { 571, 296 }, { 388, 422 }, { 480, 422 }, { 571, 422 }, { 672, 170 }, { 764, 170 }, { 855, 170 }, { 672, 296 }, { 764, 296 }, { 855, 296 }, { 718, 422 }, { 813, 422 } };
            Random rdn = new Random();
            List<string> linhaVerificacao = new List<string>();
            int i = 0;
            int[] coords = new int[2];

            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                do
                {
                    i = rdn.Next(0, matrizXY.GetLength(0));
                    
                    if (!lista.Contains(i.ToString()))
                    {
                        for (int j = 0; j < matrizXY.GetLength(1); j++)
                        {
                            coords[j] = matrizXY[i, j];
                            
                        }
                        item.Location = new Point(coords[0],coords[1]);
                        lista.Add(i.ToString());
                    }

                } while (linhaVerificacao.Contains(i.ToString()));

                linhaVerificacao.Add(i.ToString());

            }
        }

        private void ImagensClick_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            cliques++;

            tagIndex = int.Parse(string.Format("{0}", pic.Tag));
            pic.Image = img[tagIndex];
            pic.Refresh();

            if (cliques == 1)
            {

                tags[0] = int.Parse(string.Format("{0}", pic.Tag));
            }
            else if (cliques == 2)
            {
                movimentos++;
                lblMovimentos.Text = "Movimentos: " + movimentos.ToString();
                tags[1] = int.Parse(string.Format("{0}", pic.Tag));
                Desvirar(ChecagemPares());
            }
        }

        private bool ChecagemPares()
        {
            cliques = 0;

            if (tags[0] == tags[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Desvirar(bool check)
        {
            Thread.Sleep(1000);

            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (int.Parse(string.Format("{0}", item.Tag)) == tags[0] || int.Parse(string.Format("{0}", item.Tag)) == tags[1])
                {
                    if (check)
                    {
                        item.Enabled = false;
                        cartasEncontradas++;
                    }
                    else
                    {
                        item.Image = Properties.Resources.Verso;
                        item.Refresh();
                    }
                }
            }

            FinalJogo();
        }

        private void FinalJogo()
        {
            if (cartasEncontradas == img.Length * 2)
            {
                MessageBox.Show("Parabéns, você terminou o jogo com " + movimentos.ToString() + " movimentos!");
                DialogResult msg =  MessageBox.Show("Deseja continuar o jogo?", "Caixa de pergunta", MessageBoxButtons.YesNo);
                if (msg == DialogResult.Yes)
                {
                    cliques = 0; movimentos = 0; cartasEncontradas = 0;
                    lista.Clear();
                    Inicio();
                }
                else if(msg == DialogResult.No)
                {
                    MessageBox.Show("Obrigado por jogar!");
                    Application.Exit();
                }
            }
        }
    }
}
