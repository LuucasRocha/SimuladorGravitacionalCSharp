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

            // Inicializar o Universo com algumas configurações iniciais
            universo = new Universo(0.01, 1000); // Tempo entre iterações e número de iterações

            // Adicionar alguns corpos de exemplo (podemos modificar isso mais tarde)
            universo.AdicionarCorpo(new Corpo("Corpo1", 5.972e24, 6371, 100, 100, 0, 0)); // Terra
            universo.AdicionarCorpo(new Corpo("Corpo2", 7.348e22, 1737, 200, 200, 0, 0)); // Lua

            // Configurar o Timer para atualizar a simulação periodicamente
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 16; // Aproximadamente 60 FPS
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Método que será chamado em cada tick do timer
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Atualizar a simulação
            universo.Atualizar();

            // Redesenhar o universo
            pictureBox1.Invalidate(); // Solicita que o PictureBox seja redesenhado
        }

        // Método que desenha os corpos no PictureBox
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

                // Desenhar cada corpo como um círculo
                g.FillEllipse(Brushes.Black, posX - raio, posY - raio, raio * 2, raio * 2);
            }
        }
    }
}