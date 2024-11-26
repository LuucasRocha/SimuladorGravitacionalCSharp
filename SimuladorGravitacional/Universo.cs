using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SimuladorGravitacional;

namespace SimuladorGravitacional;

class Universo2D
{   // Constantes do sistema
    // Força -> medida em N
    // Massa -> medida em Kg
    // Distância -> medida em m
    // Constante gravitacional (G)
    private double G = 6.67408 * Math.Pow(10, -11.0);
    // Lista de corpos no universo.
    // A classe ObservableCollection permite que mudanças (adições, remoções ou atualizações) sejam refletidas automaticamente
    // em interfaces gráficas que estão vinculadas a esta coleção, facilitando a exibição e sincronização de dados.
    private ObservableCollection<Corpo> lstCorpos;

    public Universo2D()
    {
        // Inicializa a coleção de corpos como uma ObservableCollection.
        lstCorpos = new ObservableCollection<Corpo>();
    }

    // Retorna o corpo na posição indicada, ou null se a posição for inválida
    public Corpo getCorpo(int pos)
    {
        if ((pos >= 0) && (pos < lstCorpos.Count()))
        {
            return lstCorpos.ElementAt(pos);
        }
        else
        {
            return null;
        }
    }

    // Retorna toda a coleção de corpos
    public ObservableCollection<Corpo> getCorpo()
    {
        return lstCorpos;
    }

    // Define ou substitui um corpo em uma posição específica na lista
    public void setCorpo(Corpo cp, int pos)
    {
        //Caso a posição de inserção seja dentro da região dos corpos, substitui o corpo na posição
        if (pos < lstCorpos.Count())
        {
            lstCorpos.ElementAt(pos).copiaCorpo(cp);
        }
        else // Caso contrário, insere o corpo no final da região dos corpos
        {
            lstCorpos.Add(cp);
        }
    }

    // Retorna a quantidade de corpos no universo
    public int qtdCorpos()
    {
        return lstCorpos.Count();
    }

    // Calcula a distância entre dois corpos no plano 2D
    public double distancia(Corpo c1, Corpo c2)
    {
        double b, c;

        b = c1.getPosY() - c2.getPosY();
        c = c1.getPosX() - c2.getPosX();

        // Teorema de Pitágoras para calcular a distância
        return Math.Sqrt(Math.Pow(b, 2) + Math.Pow(c, 2));
    }

    // Calcula a força gravitacional entre dois corpos e atualiza as forças de ambos
    private void forcaG(Corpo c1, Corpo c2)
    {
        double hipotenusa = distancia(c2, c1);
        // Calcula os componentes adjacente e oposto em relação à força
        double catetoAdjacenteC1 = c2.getPosY() - c1.getPosY();
        double catetoOpostoC1 = c2.getPosX() - c1.getPosX();
        // Fórmula da força gravitacional
        double forca = G * ((c1.getMassa() * c2.getMassa()) / (Math.Pow(hipotenusa, 2)));
        // Divide a força nos eixos X e Y
        double forcaY = catetoAdjacenteC1 * forca / hipotenusa;
        double forcaX = catetoOpostoC1 * forca / hipotenusa;
        // Atualiza as forças dos corpos 
        c1.setForcaX(c1.getForcaX() + forcaX);
        c1.setForcaY(c1.getForcaY() + forcaY);
        c2.setForcaX(c2.getForcaX() - forcaX);
        c2.setForcaY(c2.getForcaY() - forcaY);
    }

    // Verifica se há colisão entre dois corpos
    private bool colisao(Corpo c1, Corpo c2)
    {
        // Compara a distância entre os corpos com a soma dos raios
        if ((distancia(c1, c2)) <= (c1.getRaio() + c2.getRaio()))
        {
            // Fusão dos corpos em um único objeto
            Corpo corpoResultante = c1 + c2;

            // Atualiza o corpo dominante e marca o outro como inválido
            if (c1.getMassa() >= c2.getMassa())
            {
                c1.copiaCorpo(corpoResultante);
                c2.setValido(false);
            }
            else
            {
                c2.copiaCorpo(corpoResultante);
                c1.setValido(false);
            }

            return true;
        }
        return false;
    }

    // Carrega corpos com posições e massas aleatórias dentro de intervalos fornecidos
    public void carregaCorpos(int numCorpos, int xIni, int xFim, int yIni, int yFim, int masIni, int masFim)
    {
        int i;
        string nome;
        int massa;

        Random rd = new Random();

        for (i = 0; i < numCorpos; i++)
        {
            nome = "cp" + i;
            massa = rd.Next(masIni, masFim);
            // Adiciona novos corpos à lista
            lstCorpos.Add(new Corpo(nome, massa,
                                          rd.Next(xIni, xFim), rd.Next(yIni, yFim), 0,
                                          0, 0, 0, rd.Next(1, 255))); // Raio
        }
    }

    // Realiza as interações entre corpos para um número específico de segundos
    public void interacaoCorpos(int qtdSegundos)
    {
        zeraForcas();

        // Computação paralela para cálculo de forças gravitacionais entre os corpos 
        Parallel.For(0, qtdCorpos() - 1, i =>
        {
            for (int j = i + 1; j < qtdCorpos(); j++)
            {
                forcaG(lstCorpos[i], lstCorpos[j]);
            }
        });

        // Computação paralela para cálculo de posições e velocidades, 
        // Atualiza posições e velocidades em paralelo
        Parallel.For(0, qtdCorpos(), i =>
        {
            calculaVelPosCorpos(qtdSegundos, lstCorpos[i]);
        });

        // Tratamento de colisões em sequência (não pode ser paralelizado devido a alterações na lista)
        bool teveColisao = false;
        for (int i = 0; i < qtdCorpos() - 1; i++)
        {
            for (int j = i + 1; j < qtdCorpos(); j++)
            {
                if (colisao(lstCorpos[i], lstCorpos[j]))
                {
                    teveColisao = true;
                }
            }
        }

        if (teveColisao)
        {
            OrganizaUniverso(); // Remove corpos inválidos
        }
    }

    // Realiza interações entre os corpos ao longo de um número definido de passos (qtdInteracoes),
    // simulando o movimento e as interações gravitacionais. Ele inclui a força gravitacional, atualização das posições e velocidades, e o tratamento de colisões.
    public void interacaoCorpos(int qtdInteracoes, int qtdSegundos)
    {
        // Laço principal para executar múltiplas iterações do universo
        while (qtdInteracoes > 0)
        {
            bool teveColisao = false; // Para indicar se houve colisões nesta interação
            zeraForcas(); // Reseta todas as forças aplicadas nos corpos antes de calcular as interações
            int i = 0;

            // Loop para calcular forças gravitacionais e atualizar posições e velocidades em cada corpo do universo
            for (i = 0; i < qtdCorpos() - 1; i++)
            {
                for (int j = i + 1; j < qtdCorpos(); j++)
                {
                    forcaG(lstCorpos[i], lstCorpos[j]); // Calcula a força gravitacional entre dois corpos
                }

                // Calcula a velocidade e a posição final de cada corpo no Universo
                calculaVelPosCorpos(qtdSegundos, lstCorpos[i]);
            }
            // Calcula a velocidade e a posição final do último corpo no Universo
            calculaVelPosCorpos(qtdSegundos, lstCorpos[i]);

            // Trata as colisões entre os corpos, comparando cada par
            for (i = 0; i < qtdCorpos() - 1; i++)
            {
                for (int j = i + 1; j < qtdCorpos(); j++)
                {
                    if (colisao(lstCorpos[i], lstCorpos[j]))
                    {
                        teveColisao = true; // Marca que houve uma colisão
                    }
                }
            }

            if (teveColisao) // Remove corpos inválidos (resultantes de colisões)
            {
                OrganizaUniverso();
            }

            qtdInteracoes--; // Decrementa o contador de iterações
        } 
    }

    // copia o estado de outro universo (u) para o universo atual, criando novas instâncias dos corpos.
    public void copiaUniverso(Universo2D u)
    {
        // Inicializa uma nova coleção para armazenar os corpos do universo copiado
        lstCorpos = new ObservableCollection<Corpo>();
        Corpo cp;
        // Itera sobre todos os corpos no universo fonte
        for (int i = 0; i < u.qtdCorpos(); i++)
        {
            // Cria uma nova instância de Corpo com os mesmos atributos do corpo original
            cp = new Corpo(u.getCorpo(i).getNome(),
                           u.getCorpo(i).getMassa(),
                           u.getCorpo(i).getPosX(),
                           u.getCorpo(i).getPosY(),
                           u.getCorpo(i).getPosZ(),
                           u.getCorpo(i).getVelX(),
                           u.getCorpo(i).getVelY(),
                           u.getCorpo(i).getVelZ(),
                           u.getCorpo(i).getDensidade());
            this.setCorpo(cp, i);
        }
    }

    // Zera todas as forças dos corpos para evitar acúmulos incorretos
    private void zeraForcas()
    {
        for (int i = 0; i < qtdCorpos(); i++)
        {
            // Zera as forças da interação
            lstCorpos.ElementAt(i).setForcaX(0);
            lstCorpos.ElementAt(i).setForcaY(0);
            lstCorpos.ElementAt(i).setForcaZ(0);
        }
    }

    // Calcula a nova posição e velocidade de um corpo baseado em sua aceleração
    private void calculaVelPosCorpos(int qtdSegundos, Corpo c1)
    {
        double acelX;
        double acelY;  

        acelX = c1.getForcaX() / c1.getMassa();
        acelY = c1.getForcaY() / c1.getMassa();

        c1.setPosX(c1.getPosX() + (c1.getVelX() * qtdSegundos) + (acelX * Math.Pow(qtdSegundos, 2) / 2));
        c1.setVelX(c1.getVelX() + (acelX * qtdSegundos));

        c1.setPosY(c1.getPosY() + (c1.getVelY() * qtdSegundos) + (acelY * Math.Pow(qtdSegundos, 2) / 2));
        c1.setVelY(c1.getVelY() + (acelY * qtdSegundos));

    }

    // Remove corpos marcados como inválidos da lista
    private void OrganizaUniverso()
    {
        int i;
        for (i = 0; i < qtdCorpos(); i++)
        {
            if (!lstCorpos.ElementAt(i).getValido())
            {
                lstCorpos.RemoveAt(i);
            }
        }
    }
}