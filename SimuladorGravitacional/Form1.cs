using SimuladorGravitacional;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SimuladorGravitacional;


public partial class Form1 : Form
{
    private Graphics g; // Objeto gr�fico para desenhar na tela
    private Universo2D U, Uinicial; // Universos (atual e inicial)
    int numCorpos; // N�mero de corpos no universo
    int numInterac; // N�mero de intera��es
    int numTempoInterac; // Tempo entre as intera��es

    // Construtor do formul�rio
    public Form1()
    {
        InitializeComponent(); // Inicializa os componentes do formul�rio
    }

    // M�todo acionado ao clicar no bot�o 2 (inicializa��o do universo)
    private void button2_Click(object sender, EventArgs e)
    {
        int xMax, yMax, mMin, mMax;

        numCorpos = Convert.ToInt32(qtdCorpos.Text); // Obt�m o n�mero de corpos
        U = new Universo2D(); // Cria um novo universo

        // Configura os par�metros do universo
        xMax = Convert.ToInt32(valXMax.Text);
        yMax = Convert.ToInt32(valYMax.Text);
        mMin = Convert.ToInt32(masMin.Text);
        mMax = Convert.ToInt32(masMax.Text);
        // Carrega os corpos no universo com os par�metros especificados
        U.carregaCorpos(numCorpos, 0, xMax, 0, yMax, mMin, mMax);
        // Cria uma c�pia do universo inicial
        Uinicial = new Universo2D();
        Uinicial.copiaUniverso(U);
        // Atualiza a tela
        Form1.ActiveForm.Refresh();
    }

    // M�todo acionado ao clicar no bot�o 1 (execu��o das intera��es)
    private void button1_Click(object sender, EventArgs e)
    {
        numInterac = Convert.ToInt32(qtdInterac.Text); // N�mero de intera��es
        numTempoInterac = Convert.ToInt32(qtdTempoInterac.Text); // Tempo entre as intera��es


        // Se o radioButton1 (Atualiza Tela) estiver selecionado
        if (radioButton1.Checked) //Atualiza Tela
        {
            for (int i = 0; i <= numInterac; i++)
            {
                U.interacaoCorpos(numTempoInterac); // Realiza a intera��o dos corpos



                // Plota os corpos a cada 100 intera��es
                if ((i % 100 == 0) && (Form1.ActiveForm != null))
                {
                    Form1.ActiveForm.Refresh(); // Atualiza a tela
                }
            }
        }
        else if (radioButton2.Checked) // Se o radioButton2 (Background) estiver selecionado
        {
            for (int i = 0; i <= numInterac; i++)
            {
                U.interacaoCorpos(numTempoInterac);
               
            }

            // Plota os corpos ao final das intera��es
            if (Form1.ActiveForm != null)
            {
                Form1.ActiveForm.Refresh();
            }
        }
        else if (radioButton3.Checked) //Para Arquivo
        {
            string texto;
            Corpo cp;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog(); // Dialog para salvar arquivo
            saveFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
            saveFileDialog1.Title = "Salvar arquivo";
            saveFileDialog1.ShowDialog();

            // Se o nome do arquivo n�o for vazio, abre o arquivo para salvar
            if (saveFileDialog1.FileName != "")
            {
                
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);

                // Grava a quantidade total de corpos no Universo e a quantidade de intera��es
                sw.WriteLine(U.qtdCorpos() + ";" + numInterac);

                // Faz as intera��es
                for (int i = 0; i <= numInterac; i++)
                {
                    U.interacaoCorpos(numTempoInterac);
                   

                    // A cada 10 intera��es, grava a situa��o dos corpos no arquivo
                    if (i % 10 == 0)
                    {
                        texto = "** Interacao " + i + " ************";
                        sw.WriteLine(texto);

                        for (int j = 0; j < U.qtdCorpos(); j++)
                        {
                            cp = U.getCorpo(j);
                            if (cp != null)
                            {
                                texto = cp.getNome() + ";"
                                        + cp.getMassa() + ";"
                                        + cp.getPosX() + ";"
                                        + cp.getPosY() + ";"
                                        + cp.getPosZ() + ";"
                                        + cp.getVelX() + ";"
                                        + cp.getVelY() + ";"
                                        + cp.getVelZ() + ";"
                                        + cp.getDensidade();

                                sw.WriteLine(texto); // Grava os dados do corpo
                            }
                        }
                    }
                }

                sw.Close();
                fs.Close();
            }
        }
    }

    // M�todo de desenho e atualiza��o da tela do universo
    private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs pe)
    {
        Corpo cp;
        float prop = 1, propX = 1, propY = 1;
        float deslocX = 0;
        float deslocY = 0;
        float maxX = 0, W = 0, H = 0, maxY = 0;
        double posX = 0, posY = 0;
        int qtdCp;

        if (Form1.ActiveForm != null)
        {
            // Ajusta os valores de X e Y m�ximos caso o usu�rio ainda n�o tenha definido
            if (valXMax.Text == "")
            {
                valXMax.Text = Form1.ActiveForm.Size.Width.ToString();
                valYMax.Text = Form1.ActiveForm.Size.Height.ToString();
            }
            // Calcula a largura e altura da tela
            W = Form1.ActiveForm.Size.Width - 50;
            H = Form1.ActiveForm.Size.Height - 50;
        }

        if (U != null)
        {
            g = pe.Graphics;
            qtdCp = U.qtdCorpos();

            // Calcula a propor��o e o deslocamento para ajustar a visualiza��o

            for (int i = 0; i < qtdCp; i++)
            {
                cp = U.getCorpo(i);
                if (cp != null)
                {
                    posX = cp.getPosX();
                    posY = cp.getPosY();

                    // Busca os menores valores de X e Y para Deslocamento
                    if (posX < deslocX)
                    {
                        deslocX = (float)posX;
                    }
                    if (posY < deslocY)
                    {
                        deslocY = (float)posY;
                    }

                    // Busca os maiores valores de X e Y para Propor��o
                    if (posY > maxY)
                    {
                        maxY = (float)posY;
                    }
                    if (posX > maxX)
                    {
                        maxX = (float)posX;
                    }
                }
            }
            // Calcula a propor��o dependendo dos valores m�ximos e m�nimos de X e Y
            if ((maxX - deslocX) > W)
            {
                propX = (maxX - deslocX) / W;
            }
            if ((maxY - deslocY) > H)
            {
                propY = (maxY - deslocY) / H;
            }

            if (propX > propY)
            {
                prop = propX;
            }
            else
            {
                prop = propY;
            }
            // Exibe a propor��o na tela
            txtProporcao.Text = (1 / prop).ToString();
            qtdCorposAtual.Text = qtdCp.ToString();

            // Desenha o corpo 
            for (int i = 0; i < qtdCp; i++)
            {
                cp = U.getCorpo(i);
                if (cp != null)
                {
                    posX = cp.getPosX() - deslocX;
                    posY = cp.getPosY() - deslocY;

                    // Desenha o corpo como uma elipse
                    g.DrawEllipse(new Pen(Color.FromArgb((int)cp.getDensidade(), 0, 0)),
                        (float)(posX - cp.getRaio()) / prop,
                        (float)(posY - cp.getRaio()) / prop,
                        (float)(cp.getRaio() * 2) / prop,
                        (float)(cp.getRaio() * 2) / prop);

                    // Desenha as for�as em X e Y como barras
                    g.DrawLine(new Pen(Color.FromArgb(0, 0, 255)),
                        (float)(posX) / prop,
                        (float)(posY) / prop,
                        (float)(posX + (cp.getForcaX() * 50)) / prop,
                        (float)(posY) / prop);
                    g.DrawLine(new Pen(Color.FromArgb(0, 0, 255)),
                        (float)posX / prop,
                        (float)posY / prop,
                        (float)(posX) / prop,
                        (float)(posY + (cp.getForcaY() * 50)) / prop);
                }
            }
        }
    }

    private void btn_grava_Click(object sender, EventArgs e)
    {
        Corpo cp;
        int i;
        string texto;

        if (U != null)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
            saveFileDialog1.Title = "Salvar arquivo";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);

                sw.WriteLine(U.qtdCorpos());
                for (i = 0; i < U.qtdCorpos(); i++)
                {
                    cp = U.getCorpo(i);
                    if (cp != null)
                    {
                        texto = cp.getNome() + ";"
                              + cp.getMassa() + ";"
                              + cp.getPosX() + ";"
                              + cp.getPosY() + ";"
                              + cp.getPosZ() + ";"
                              + cp.getVelX() + ";"
                              + cp.getVelY() + ";"
                              + cp.getVelZ() + ";"
                              + cp.getDensidade();

                        sw.WriteLine(texto);
                    }
                }

                sw.Close();
                fs.Close();
            }
        }
        else // N�o ha corpos no Universo
        {
            MessageBox.Show("N�o h� corpos no Universo a serem salvos", "Aten��o");
        }
    }

    private void btn_grava_ini_Click(object sender, EventArgs e)
    {
        Corpo cp;
        int i;
        string texto;
        // Verifica se o universo inicial Uinicial cont�m corpos
        if (Uinicial != null)
        {
            // Exibe o di�logo para o usu�rio escolher onde salvar o arquivo
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
            saveFileDialog1.Title = "Salvar arquivo";
            saveFileDialog1.ShowDialog();

            // Se o nome do arquivo n�o for vazio, abre o arquivo para salvar
            if (saveFileDialog1.FileName != "")
            {
                // Cria o arquivo para salvar
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                // Salva a quantidade de corpos no universo inicial
                sw.WriteLine(Uinicial.qtdCorpos());
                // Itera por todos os corpos e grava as informa��es no arquivo
                for (i = 0; i < Uinicial.qtdCorpos(); i++)
                {
                    cp = Uinicial.getCorpo(i);
                    if (cp != null)
                    {
                        // Monta a string com as informa��es do corpo
                        texto = cp.getNome() + ";"
                              + cp.getMassa() + ";"
                              + cp.getPosX() + ";"
                              + cp.getPosY() + ";"
                              + cp.getPosZ() + ";"
                              + cp.getVelX() + ";"
                              + cp.getVelY() + ";"
                              + cp.getVelZ() + ";"
                              + cp.getDensidade();
                        // Escreve as informa��es do corpo no arquivo
                        sw.WriteLine(texto);
                    }
                }
                // Fecha o arquivo ap�s salvar
                sw.Close();
                fs.Close();
            }
        }
        else // N�o ha corpos no Universo
        {
            MessageBox.Show("N�o h� corpos no Universo a serem salvos", "Aten��o");
        }

    }

    private void btn_carrega_Click(object sender, EventArgs e)
    {
        string texto;
        int controle;
        Corpo cp;
        // Exibe o di�logo para o usu�rio escolher um arquivo para carregar
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        openFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
        openFileDialog1.Title = "Abrir arquivo";
        // Se o usu�rio escolher um arquivo, carrega os dados
        if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
            controle = 0;
            // L� linha por linha at� o final do arquivo
            while (!sr.EndOfStream)
            {
                texto = sr.ReadLine();
                if (controle != 0)
                {
                    // Se n�o for a primeira linha, extrai os dados de cada corpo
                    string[] valores = texto.Split(';');
                    // Cria um novo corpo com os valores extra�dos e adiciona ao universo
                    cp = new Corpo(valores[0],
                                   Convert.ToDouble(valores[1]),
                                   Convert.ToDouble(valores[2]),
                                   Convert.ToDouble(valores[3]),
                                   Convert.ToDouble(valores[4]),
                                   Convert.ToDouble(valores[5]),
                                   Convert.ToDouble(valores[6]),
                                   Convert.ToDouble(valores[7]),
                                   Convert.ToDouble(valores[8]));
                    U.setCorpo(cp, controle - 1); // Adiciona o corpo ao universo
                }
                else
                {
                    // Se for a primeira linha, define o n�mero de corpos
                    qtdCorpos.Text = texto;

                    numCorpos = Convert.ToInt32(qtdCorpos.Text);
                    U = new Universo2D(); // Cria um novo universo
                }
                controle++;
            }
            // Fecha o arquivo ap�s a leitura
            sr.Close();


            // Cria uma c�pia do universo carregado
            Uinicial = new Universo2D();
            Uinicial.copiaUniverso(U);
            // Atualiza a tela do formul�rio
            Form1.ActiveForm.Refresh();
        }
    }

    private void radioButton3_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }
}