using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorGravitacional
{
    public class Universo
    {
        public List<Corpo> Corpos { get; private set; }
        public double TempoEntreIteracoes { get; private set; }
        public int Iteracoes { get; private set; }

        public Universo(double tempoEntreIteracoes, int iteracoes)
        {
            Corpos = new List<Corpo>();
            TempoEntreIteracoes = tempoEntreIteracoes;
            Iteracoes = iteracoes;
        }

        // Adiciona um corpo ao universo
        public void AdicionarCorpo(Corpo corpo)
        {
            Corpos.Add(corpo);
        }

        // Método para calcular as interações entre todos os corpos
        public void Atualizar()
        {
            // Usar computação paralela para otimizar os cálculos
            Parallel.ForEach(Corpos, corpo =>
            {
                double acelX = 0;
                double acelY = 0;

                foreach (var outro in Corpos)
                {
                    if (corpo != outro)
                    {
                        double[] forca = corpo.CalcularForcaGravitacional(outro);
                        acelX += forca[0] / corpo.Massa;
                        acelY += forca[1] / corpo.Massa;
                    }
                }

                // Atualizar a posição e velocidade de cada corpo
                corpo.AtualizarPosicao(TempoEntreIteracoes, acelX, acelY);
            });

            // Tratar colisões após atualizar as posições
            TratarColisoes();
        }

        // Método para tratar colisões
        private void TratarColisoes()
        {
            for (int i = 0; i < Corpos.Count; i++)
            {
                for (int j = i + 1; j < Corpos.Count; j++)
                {
                    Corpo corpo1 = Corpos[i];
                    Corpo corpo2 = Corpos[j];

                    double distancia = Math.Sqrt(Math.Pow(corpo2.PosX - corpo1.PosX, 2) + Math.Pow(corpo2.PosY - corpo1.PosY, 2));
                    if (distancia <= (corpo1.Raio + corpo2.Raio))
                    {
                        // Combinar os corpos caso haja colisão
                        Corpo novoCorpo = corpo1 + corpo2;
                        Corpos[i] = novoCorpo;
                        Corpos.RemoveAt(j);
                        j--; // Ajustar o índice devido à remoção
                    }
                }
            }
        }

        // Método para gravar o estado do universo em um arquivo texto
        public void GravarEstado(string caminho)
        {
            using (StreamWriter sw = new StreamWriter(caminho))
            {
                sw.WriteLine($"{Corpos.Count};{Iteracoes};{TempoEntreIteracoes}");
                foreach (var corpo in Corpos)
                {
                    sw.WriteLine($"{corpo.Nome};{corpo.Massa};{corpo.Raio};{corpo.PosX};{corpo.PosY};{corpo.VelX};{corpo.VelY}");
                }
            }
        }

        // Método para carregar o estado do universo de um arquivo texto
        public static Universo CarregarEstado(string caminho)
        {
            using (StreamReader sr = new StreamReader(caminho))
            {
                string primeiraLinha = sr.ReadLine();
                string[] dados = primeiraLinha.Split(';');

                int quantidadeCorpos = int.Parse(dados[0]);
                int iteracoes = int.Parse(dados[1]);
                double tempoEntreIteracoes = double.Parse(dados[2]);

                Universo universo = new Universo(tempoEntreIteracoes, iteracoes);

                for (int i = 0; i < quantidadeCorpos; i++)
                {
                    string linha = sr.ReadLine();
                    string[] atributos = linha.Split(';');

                    string nome = atributos[0];
                    double massa = double.Parse(atributos[1]);
                    double raio = double.Parse(atributos[2]);
                    double posX = double.Parse(atributos[3]);
                    double posY = double.Parse(atributos[4]);
                    double velX = double.Parse(atributos[5]);
                    double velY = double.Parse(atributos[6]);

                    Corpo corpo = new Corpo(nome, massa, raio, posX, posY, velX, velY);
                    universo.AdicionarCorpo(corpo);
                }

                return universo;
            }
        }
    }
}
