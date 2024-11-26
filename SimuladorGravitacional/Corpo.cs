using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuladorGravitacional;

// Representa um corpo no simulador gravitacional, contendo propriedades físicas e métodos para manipular esses dados.
public class Corpo
{
    // Propriedades privadas do corpo que definem seu estado físico e identificador.
    private String nome;     // Nome do corpo celeste, usado para identificá-lo.
    private double massa;    // Massa do corpo, essencial para cálculos gravitacionais.
    private double densidade; // Densidade do corpo, utilizada para derivar propriedades como o raio.
    private double posX;
    private double posY;  // Coordenadas da posição do corpo no espaço.
    private double posZ;
    private double velX;  
    private double velY;  // Componentes de velocidade do corpo nos eixos X, Y e Z.
    private double velZ;
    private double forcaX;
    private double forcaY;
    private double forcaZ; // Componentes de força atuando no corpo em cada eixo.
    private bool eValido;  // Indica se o corpo é válido no contexto do simulador.

    // Construtor padrão: inicializa o corpo com valores padrão.
    public Corpo()
    {
        // Inicializa as propriedades com valores neutros ou padrões.
        this.nome = "";
        this.massa = 0;
        this.densidade = 1; // Evita divisão por zero ao calcular o raio.
        this.posX = 0;
        this.posY = 0;
        this.posZ = 0;
        this.velX = 0;
        this.velY = 0;
        this.velZ = 0;
        this.forcaX = 0;
        this.forcaY = 0;
        this.forcaZ = 0;
        this.eValido = true; // O corpo é considerado válido por padrão.
    }

    // Construtor com parâmetros: permite criar um corpo com valores personalizados.
    public Corpo(String n, double m, double pX, double pY, double pZ, double vX, double vY, double vZ, double d)
    {
        // Define as propriedades com base nos valores fornecidos.
        this.eValido = true;
        this.nome = n;
        this.massa = m;
        this.posX = pX;
        this.posY = pY;
        this.posZ = pZ;
        this.velX = vX;
        this.velY = vY;
        this.velZ = vZ;
        this.forcaX = 0;
        this.forcaY = 0;
        this.forcaZ = 0; // Inicialmente sem forças aplicadas.
        this.densidade = d;
    }

    // Métodos de acesso (getters): retornam os valores das propriedades privadas.
    public bool getValido()
    {
        return this.eValido;
    }
    public String getNome()
    {
        return this.nome;
    }
    public double getMassa()
    {
        return this.massa;
    }
    public double getPosX()
    {
        return this.posX;
    }
    public double getPosY()
    {
        return this.posY;
    }
    public double getPosZ()
    {
        return this.posZ;
    }
    public double getVelX()
    {
        return this.velX;
    }
    public double getVelY()
    {
        return this.velY;
    }
    public double getVelZ()
    {
        return this.velZ;
    }
    public double getForcaX()
    {
        return this.forcaX;
    }
    public double getForcaY()
    {
        return this.forcaY;
    }
    public double getForcaZ()
    {
        return this.forcaZ;
    }
    public double getDensidade()
    {
        return this.densidade;
    }
    
    // Calcula o raio aproximado do corpo com base na massa e densidade.
    public double getRaio()
    {
        // Fórmula do raio considerando o corpo como uma esfera homogênea.
        double raio = Math.Pow((3 * Math.PI * this.massa) / (4 * this.densidade), ((double)1 / 3));

        return raio / 5; // Reduzido para melhor visualização no simulador.
    }

    // Métodos de modificação (setters): permitem alterar os valores das propriedades privadas.
    public void setValido(bool v)
    {
        this.eValido = v;
    }
    public void setNome(string nome)
    {
        this.nome = nome;
    }
    public void setMassa(double m)
    {
        this.massa = m;
    }
    public void setPosX(double x)
    {
        this.posX = x;
    }
    public void setPosY(double y)
    {
        this.posY = y;
    }
    public void setPosZ(double z)
    {
        this.posZ = z;
    }
    public void setVelX(double x)
    {
        this.velX = x;
    }
    public void setVelY(double y)
    {
        this.velY = y;
    }
    public void setVelZ(double z)
    {
        this.velZ = z;
    }
    public void setForcaX(double forca)
    {
        this.forcaX = forca;
    }
    public void setForcaY(double forca)
    {
        this.forcaY = forca;
    }
    public void setForcaZ(double forca)
    {
        this.forcaZ = forca;
    }
    public void setDensidade(double dens)
    {
        this.densidade = dens;
    }

    public void copiaCorpo(Corpo cp) // Copia os valores de outro corpo para o corpo atual.
    {
        this.nome = cp.getNome();
        this.massa = cp.getMassa();
        this.posX = cp.getPosX();
        this.posY = cp.getPosY();
        this.posZ = cp.getPosZ();
        this.velX = cp.getVelX();
        this.velY = cp.getVelY();
        this.velZ = cp.getVelZ();
    }

    // Sobrecarga do operador +: combina dois corpos em um único corpo.
    public static Corpo operator +(Corpo c1, Corpo c2)
    {
        // Calcula a nova massa como soma das massas dos corpos.
        double novaMassa = c1.getMassa() + c2.getMassa();
        // Combina os nomes dos corpos.
        string novoNome = c1.getNome() + c2.getNome();
        // Calcula o centro de massa (posições ponderadas pela massa).
        double novaPosX = (c1.getPosX() * c1.getMassa() + c2.getPosX() * c2.getMassa()) / novaMassa;
        double novaPosY = (c1.getPosY() * c1.getMassa() + c2.getPosY() * c2.getMassa()) / novaMassa;

        // Retorna o corpo resultante da fusão.
        return new Corpo(
            novoNome,
            novaMassa,
            novaPosX,
            novaPosY,
            0,    // Z não é considerado neste cálculo específico.
            0,    // Velocidade inicial após fusão.
            0,    // Velocidade inicial após fusão.
            0,    // Velocidade inicial após fusão.
            (c1.getDensidade() + c2.getDensidade()) / 2 // Densidade média.
        );
    }


}
