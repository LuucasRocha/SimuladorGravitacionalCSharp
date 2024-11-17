using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorGravitacional
{
    public class Corpo
    {
        public string Nome { get; set; }
        public double Massa { get; set; }
        public double Raio { get; set; }
        public double Densidade { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double VelX { get; set; }
        public double VelY { get; set; }

        private const double G = 6.67430e-11; // Constante gravitacional

        public Corpo(string nome, double massa, double raio, double posX, double posY, double velX, double velY)
        {
            Nome = nome;
            Massa = massa;
            Raio = raio;
            Densidade = Massa / (4 / 3 * Math.PI * Math.Pow(raio, 3));
            PosX = posX;
            PosY = posY;
            VelX = velX;
            VelY = velY;
        }

        // Método para calcular a força gravitacional entre dois corpos
        public double[] CalcularForcaGravitacional(Corpo outro)
        {
            double distX = outro.PosX - PosX;
            double distY = outro.PosY - PosY;
            double distancia = Math.Sqrt(distX * distX + distY * distY);

            if (distancia == 0) return new double[] { 0, 0 }; // Evitar divisão por zero

            double forca = G * (Massa * outro.Massa) / (distancia * distancia);

            // Direção da força
            double forcaX = forca * (distX / distancia);
            double forcaY = forca * (distY / distancia);

            return new double[] { forcaX, forcaY };
        }

        // Método para atualizar a posição do corpo
        public void AtualizarPosicao(double tempo, double acelX, double acelY)
        {
            PosX += VelX * tempo + 0.5 * acelX * tempo * tempo;
            PosY += VelY * tempo + 0.5 * acelY * tempo * tempo;
            VelX += acelX * tempo;
            VelY += acelY * tempo;
        }

        // Sobrecarga do operador "+" para colisões
        public static Corpo operator +(Corpo c1, Corpo c2)
        {
            double novaMassa = c1.Massa + c2.Massa;
            double novoRaio = Math.Pow(Math.Pow(c1.Raio, 3) + Math.Pow(c2.Raio, 3), 1.0 / 3.0);
            double novaVelX = (c1.VelX * c1.Massa + c2.VelX * c2.Massa) / novaMassa;
            double novaVelY = (c1.VelY * c1.Massa + c2.VelY * c2.Massa) / novaMassa;

            return new Corpo(c1.Nome + "+" + c2.Nome, novaMassa, novoRaio, c1.PosX, c1.PosY, novaVelX, novaVelY);
        }
    }

}
