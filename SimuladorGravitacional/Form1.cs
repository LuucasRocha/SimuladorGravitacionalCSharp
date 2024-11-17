using System.Windows.Forms;

namespace SimuladorGravitacional
{
    public partial class Form1 : Form
    {
        private Universo universo;
        private System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();

            // Inicializar o Universo com algumas configura��es iniciais
            universo = new Universo(0.01, 1000); // Tempo entre itera��es e n�mero de itera��es

            // Adicionar alguns corpos de exemplo (podemos modificar isso mais tarde)
            universo.AdicionarCorpo(new Corpo("Corpo1", 5.972e24, 6371, 100, 100, 0, 0)); // Terra
            universo.AdicionarCorpo(new Corpo("Corpo2", 7.348e22, 1737, 200, 200, 0, 0)); // Lua

            // Configurar o Timer para atualizar a simula��o periodicamente
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 16; // Aproximadamente 60 FPS
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // M�todo que ser� chamado em cada tick do timer
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Atualizar a simula��o
            universo.Atualizar();

            // Redesenhar o universo
            pictureBox1.Invalidate(); // Solicita que o PictureBox seja redesenhado
        }

        // M�todo que desenha os corpos no PictureBox
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White); // Limpa o fundo (cor branca representando o "universo")

            // Desenhar os corpos
            foreach (var corpo in universo.Corpos)
            {
                float posX = (float)corpo.PosX;
                float posY = (float)corpo.PosY;
                float raio = (float)corpo.Raio;

                // Desenhar cada corpo como um c�rculo
                g.FillEllipse(Brushes.Black, posX - raio, posY - raio, raio * 2, raio * 2);
            }
        }
    }
}