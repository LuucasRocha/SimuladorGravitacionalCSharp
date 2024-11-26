namespace SimuladorGravitacional;

partial class Form1
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        btn_aleatorio = new Button();
        label2 = new Label();
        qtdCorpos = new TextBox();
        qtdInterac = new TextBox();
        label4 = new Label();
        label5 = new Label();
        qtdTempoInterac = new TextBox();
        btn_executa = new Button();
        btn_grava = new Button();
        btn_carrega = new Button();
        groupBox1 = new GroupBox();
        label10 = new Label();
        qtdCorposAtual = new TextBox();
        groupBox2 = new GroupBox();
        radioButton1 = new RadioButton();
        radioButton2 = new RadioButton();
        radioButton3 = new RadioButton();
        label6 = new Label();
        label3 = new Label();
        valYMax = new TextBox();
        btn_grava_ini = new Button();
        valXMax = new TextBox();
        label8 = new Label();
        label7 = new Label();
        masMax = new TextBox();
        masMin = new TextBox();
        txtProporcao = new TextBox();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        // 
        // btn_aleatorio
        // 
        btn_aleatorio.Location = new Point(6, 78);
        btn_aleatorio.Name = "btn_aleatorio";
        btn_aleatorio.Size = new Size(179, 30);
        btn_aleatorio.TabIndex = 8;
        btn_aleatorio.Text = "GERAR CORPOS";
        btn_aleatorio.UseVisualStyleBackColor = true;
        btn_aleatorio.Click += button2_Click;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(16, -3);
        label2.Name = "label2";
        label2.Size = new Size(71, 15);
        label2.TabIndex = 1;
        label2.Text = "Qtd. Corpos";
        // 
        // qtdCorpos
        // 
        qtdCorpos.Location = new Point(6, 15);
        qtdCorpos.Name = "qtdCorpos";
        qtdCorpos.Size = new Size(100, 23);
        qtdCorpos.TabIndex = 1;
        qtdCorpos.Text = "0";
        // 
        // qtdInterac
        // 
        qtdInterac.Location = new Point(111, 15);
        qtdInterac.Name = "qtdInterac";
        qtdInterac.Size = new Size(100, 23);
        qtdInterac.TabIndex = 2;
        qtdInterac.Text = "0";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(106, 0);
        label4.Name = "label4";
        label4.Size = new Size(94, 15);
        label4.TabIndex = 6;
        label4.Text = "Num. Interações";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(203, 0);
        label5.Name = "label5";
        label5.Size = new Size(147, 15);
        label5.TabIndex = 7;
        label5.Text = "Tempo entre interações (s)";
        // 
        // qtdTempoInterac
        // 
        qtdTempoInterac.Location = new Point(228, 15);
        qtdTempoInterac.Name = "qtdTempoInterac";
        qtdTempoInterac.Size = new Size(100, 23);
        qtdTempoInterac.TabIndex = 3;
        qtdTempoInterac.Text = "0";
        // 
        // btn_executa
        // 
        btn_executa.Location = new Point(6, 113);
        btn_executa.Name = "btn_executa";
        btn_executa.Size = new Size(179, 30);
        btn_executa.TabIndex = 9;
        btn_executa.Text = "EXECUTAR PROCESSO";
        btn_executa.UseVisualStyleBackColor = true;
        btn_executa.Click += button1_Click;
        // 
        // btn_grava
        // 
        btn_grava.Location = new Point(319, 113);
        btn_grava.Name = "btn_grava";
        btn_grava.Size = new Size(182, 27);
        btn_grava.TabIndex = 11;
        btn_grava.Text = "GRAVAR UNIVERSO ATUAL";
        btn_grava.UseVisualStyleBackColor = true;
        btn_grava.Click += btn_grava_Click;
        // 
        // btn_carrega
        // 
        btn_carrega.Location = new Point(191, 94);
        btn_carrega.Name = "btn_carrega";
        btn_carrega.Size = new Size(121, 30);
        btn_carrega.TabIndex = 10;
        btn_carrega.Text = "CARREGAR UNIVERSO";
        btn_carrega.UseVisualStyleBackColor = true;
        btn_carrega.Click += btn_carrega_Click;
        // 
        // groupBox1
        // 
        groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        groupBox1.BackColor = Color.Transparent;
        groupBox1.Controls.Add(label10);
        groupBox1.Controls.Add(qtdCorposAtual);
        groupBox1.Controls.Add(label8);
        groupBox1.Controls.Add(masMax);
        groupBox1.Controls.Add(label7);
        groupBox1.Controls.Add(groupBox2);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(masMin);
        groupBox1.Controls.Add(valYMax);
        groupBox1.Controls.Add(btn_grava_ini);
        groupBox1.Controls.Add(btn_grava);
        groupBox1.Controls.Add(valXMax);
        groupBox1.Controls.Add(btn_carrega);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(btn_executa);
        groupBox1.Controls.Add(qtdTempoInterac);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(qtdInterac);
        groupBox1.Controls.Add(qtdCorpos);
        groupBox1.Controls.Add(btn_aleatorio);
        groupBox1.Location = new Point(847, 530);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(507, 149);
        groupBox1.TabIndex = 12;
        groupBox1.TabStop = false;
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Location = new Point(3, 36);
        label10.Name = "label10";
        label10.Size = new Size(102, 15);
        label10.TabIndex = 22;
        label10.Text = "Corpos no Universo";
        // 
        // qtdCorposAtual
        // 
        qtdCorposAtual.Location = new Point(6, 49);
        qtdCorposAtual.Name = "qtdCorposAtual";
        qtdCorposAtual.Size = new Size(100, 23);
        qtdCorposAtual.TabIndex = 23;
        qtdCorposAtual.Text = "0";
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(radioButton1);
        groupBox2.Controls.Add(radioButton2);
        groupBox2.Controls.Add(radioButton3);
        groupBox2.Location = new Point(223, 139);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(291, 10);
        groupBox2.TabIndex = 14;
        groupBox2.TabStop = false;
        // 
        // radioButton1
        // 
        radioButton1.AutoSize = true;
        radioButton1.Checked = true;
        radioButton1.Location = new Point(193, 12);
        radioButton1.Name = "radioButton1";
        radioButton1.Size = new Size(91, 19);
        radioButton1.TabIndex = 20;
        radioButton1.TabStop = true;
        radioButton1.Text = "Atualiza Tela";
        radioButton1.UseVisualStyleBackColor = true;
        // 
        // radioButton2
        // 
        radioButton2.AutoSize = true;
        radioButton2.Location = new Point(104, 12);
        radioButton2.Name = "radioButton2";
        radioButton2.Size = new Size(89, 19);
        radioButton2.TabIndex = 21;
        radioButton2.TabStop = true;
        radioButton2.Text = "Background";
        radioButton2.UseVisualStyleBackColor = true;
        // 
        // radioButton3
        // 
        radioButton3.AutoSize = true;
        radioButton3.Location = new Point(12, 12);
        radioButton3.Name = "radioButton3";
        radioButton3.Size = new Size(93, 19);
        radioButton3.TabIndex = 13;
        radioButton3.TabStop = true;
        radioButton3.Text = "Para Arquivo";
        radioButton3.UseVisualStyleBackColor = true;
        radioButton3.CheckedChanged += radioButton3_CheckedChanged;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(235, 36);
        label6.Name = "label6";
        label6.Size = new Size(60, 15);
        label6.TabIndex = 13;
        label6.Text = "Y Máximo";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(137, 36);
        label3.Name = "label3";
        label3.Size = new Size(60, 15);
        label3.TabIndex = 13;
        label3.Text = "X Máximo";
        // 
        // valYMax
        // 
        valYMax.Location = new Point(228, 49);
        valYMax.Name = "valYMax";
        valYMax.Size = new Size(100, 23);
        valYMax.TabIndex = 5;
        // 
        // btn_grava_ini
        // 
        btn_grava_ini.Location = new Point(319, 75);
        btn_grava_ini.Name = "btn_grava_ini";
        btn_grava_ini.Size = new Size(182, 32);
        btn_grava_ini.TabIndex = 12;
        btn_grava_ini.Text = "GRAVAR UNIVERSO INICIAL";
        btn_grava_ini.UseVisualStyleBackColor = true;
        btn_grava_ini.Click += btn_grava_ini_Click;
        // 
        // valXMax
        // 
        valXMax.Location = new Point(122, 49);
        valXMax.Name = "valXMax";
        valXMax.Size = new Size(100, 23);
        valXMax.TabIndex = 4;
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Location = new Point(347, 36);
        label8.Name = "label8";
        label8.Size = new Size(109, 15);
        label8.TabIndex = 19;
        label8.Text = "Massa Máxima (kg)";
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(356, 0);
        label7.Name = "label7";
        label7.Size = new Size(108, 15);
        label7.TabIndex = 13;
        label7.Text = "Massa Mínima (kg)";
        // 
        // masMax
        // 
        masMax.Location = new Point(356, 49);
        masMax.Name = "masMax";
        masMax.Size = new Size(100, 23);
        masMax.TabIndex = 7;
        masMax.Text = "450000";
        // 
        // masMin
        // 
        masMin.Location = new Point(356, 15);
        masMin.Name = "masMin";
        masMin.Size = new Size(100, 23);
        masMin.TabIndex = 6;
        masMin.Text = "8000";
        // 
        // txtProporcao
        // 
        txtProporcao.Location = new Point(847, 656);
        txtProporcao.Name = "txtProporcao";
        txtProporcao.Size = new Size(13, 23);
        txtProporcao.TabIndex = 20;
        // 
        // Form1
        // 
        ClientSize = new Size(1366, 691);
        Controls.Add(groupBox1);
        Controls.Add(txtProporcao);
        Name = "Form1";
        Text = "Universo 2D";
        Load += Form1_Load;
        Paint += Form1_Paint;
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion


    private System.Windows.Forms.Button btn_aleatorio;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox qtdCorpos;
    private System.Windows.Forms.TextBox qtdInterac;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox qtdTempoInterac;
    private System.Windows.Forms.Button btn_executa;
    private System.Windows.Forms.Button btn_grava;
    private System.Windows.Forms.Button btn_carrega;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btn_grava_ini;
    private System.Windows.Forms.TextBox valYMax;
    private System.Windows.Forms.TextBox valXMax;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox masMax;
    private System.Windows.Forms.TextBox masMin;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.RadioButton radioButton3;
    private System.Windows.Forms.GroupBox groupBox2;  
    private System.Windows.Forms.TextBox txtProporcao;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox qtdCorposAtual;
}
