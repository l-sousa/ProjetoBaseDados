using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Collections;

namespace Trabalho_Final
{
    public partial class Consultas : UserControl
    {
        private Hashtable nifs_dentista = getnifs();

        public static Hashtable getnifs()
        {
            const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";
            try

            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string cmdst = "SELECT nif,especialidade FROM PROJETO.DENTISTA";

                SqlDataAdapter adap = new SqlDataAdapter(cmdst, con);

                DataTable ds = new DataTable();

                adap.Fill(ds);



                Hashtable nifs_dentista = new Hashtable();

                foreach (DataRow dr in ds.Rows)
                {
                    nifs_dentista.Add(dr.Field<int>("nif"), dr.Field<string>("especialidade"));

                }

                if (con.State == ConnectionState.Open)
                    con.Close();

                return nifs_dentista;
            }

            catch (Exception pp)

            {
                System.Windows.Forms.MessageBox.Show("Imposssível conectar!");
                return null;
            }
        }

        

        private static Consultas _instance;
        public static Consultas Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Consultas();
                return _instance;
            }
        }

        public Consultas()
        {
            InitializeComponent();
        }

        private void show_consultas()
        {
            const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";

            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {

                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PROJETO.CONSULTA", SqlConn);
                //--- Converte resultado para uma Tabela
                DataTable dt = new DataTable();
                da.Fill(dt);
                //--- Preenche o DataGrid com a Tabela
                dataGridView1.DataSource = dt;
                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }
        }

        private void Consultas_Load(object sender, EventArgs e)
        {
            const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";

            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {

                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PROJETO.CONSULTA", SqlConn);
                //--- Converte resultado para uma Tabela
                DataTable dt = new DataTable();
                da.Fill(dt);
                //--- Preenche o DataGrid com a Tabela
                dataGridView1.DataSource = dt;
                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";
            try

            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                    con.Open();



                string search = textBox7.Text.Trim();
                var split = search.Split('/');
                string cmdst;

                //TODO pesquisa dinamica por exemplo */*/2022 devolve todas as consultas de 2022
                if (search == "")
                    cmdst = "select * from PROJETO.CONSULTA ";
                else
                {

                    string[] dataspt = search.Split('/'); ;
                    string procura = dataspt[2].ToString() + "-" + dataspt[1].ToString() + "-" + dataspt[0].ToString();
                    cmdst = "select * from PROJETO.CONSULTA where  dataa='" + procura + "'";
                    textBox7.Text = "";
                }


                SqlDataAdapter adap = new SqlDataAdapter(cmdst, con);

                DataTable ds = new DataTable();


                adap.Fill(ds);

                dataGridView1.DataSource = ds;

                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            catch (Exception pp)

            {
                System.Windows.Forms.MessageBox.Show("Impossível conectar!");
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";

            try
            {
                int codigo = Int32.Parse(textBox1.Text.Trim());
                double preco = Double.Parse(textBox6.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                int duracao = Int32.Parse(textBox5.Text.Trim());
                string tipo = textBox4.Text.Trim();
                int dentista = Int32.Parse(textBox3.Text.Trim());

                string specifier = "G";

                

                try

                {
                    SqlConnection con = new SqlConnection(connectionString);
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string cmdst = "SELECT codigo FROM PROJETO.CONSULTA";

                    SqlDataAdapter adap = new SqlDataAdapter(cmdst, con);

                    DataTable ds = new DataTable();

                    adap.Fill(ds);

                    List<int> cons_codes = new List<int>(); ;

                    foreach (DataRow dr in ds.Rows)
                    {
                        cons_codes.Add(dr.Field<int>("codigo"));
                    }

                    if (cons_codes.Contains(codigo))
                    {
                        System.Windows.Forms.MessageBox.Show("O código no campo \"Código\" já existe!");
                        textBox1.Text = "";
                        return;
                    }

                    if (con.State == ConnectionState.Open)
                        con.Close();
                }

                catch (Exception pp)

                {
                    System.Windows.Forms.MessageBox.Show("Impossível conectar!");
                }

               

                    if (!nifs_dentista.ContainsKey(dentista))
                    {
                        System.Windows.Forms.MessageBox.Show("O nif no campo \"Dentista\" não existe!");
                        textBox3.Text = "";
                        return;
                    }

                if(preco.ToString().IndexOf(',') != -1) { 
                    if ((preco.ToString(specifier).Length > 6 || textBox6.Text=="") && (preco.ToString().Substring(preco.ToString().IndexOf(','))).Length > 2) 
                    {
                        System.Windows.Forms.MessageBox.Show("O campo \"Preço\" deve ter no máximo tamanho 6!");
                        textBox6.Text = "";
                        return;
                    }
                }
                else
                {
                    if (preco.ToString(specifier).Length > 3 || textBox6.Text == "")
                    {
                        System.Windows.Forms.MessageBox.Show("O campo \"Preço\" deve ter no máximo tamanho 5 em que 2 tem de ser decimais!");
                        textBox6.Text = "";
                        return;
                    }
                }
                string[] tipos_consulta = {"cirurgia", "periodontologia", "dentisteria", "ortodontia","implantologia", "medicina dentaria preventiva", "odontopediatria", "proteses" ,"medicina dentária preventiva"};
                if (!tipos_consulta.Contains(tipo.ToLower()))
                {
                    System.Windows.Forms.MessageBox.Show("O tipo de consulta no campo \"Tipo\" não existe!");
                    return;
                }

                if (textBox5.Text == "")
                {
                    System.Windows.Forms.MessageBox.Show("Insira a duração da consulta!");
                    return;
                }

                DateTime dDate;

                if (DateTime.TryParse(textBox2.Text, out dDate))
                {
                    String.Format("{0:d/MM/yyyy}", dDate);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Data inválida!");
                    return;
                }

            }
            catch(Exception erro)
            {
                System.Windows.Forms.MessageBox.Show("Um dos campos não foi bem introduzido!");
                return;
            }


            string sql = null;



            // Prepare a proper parameterized query 
            
            // Create the connection (and be sure to dispose it at the end)
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {

                    string[] dataspt = textBox2.Text.Split('/'); ;
                    string procura = dataspt[2].ToString() + "-" + dataspt[1].ToString() + "-" + dataspt[0].ToString();
                    string type = textBox4.Text.ToLower();
                    if (String.Equals(type, "medicina dentaria preventiva") || String.Equals(type, "medicina dentária preventiva"))
                        type = "Medicina Dentária Preventiva";
                    else
                        type = type.First().ToString().ToUpper() + type.Substring(1);
                    sql = "insert into PROJETO.CONSULTA (codigo, preco, tipo, duracao,nif_dentista, dataa) values(" + textBox1.Text + "," + textBox6.Text + "," + "\'" + type + "\'" + "," + textBox5.Text + "," + textBox3.Text + "," + "\'" + procura + "\'" + ")";

                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        int rowsAdded = cmd.ExecuteNonQuery();
                        if (rowsAdded > 0)
                            System.Windows.Forms.MessageBox.Show("Consulta adicionada!");
                        else
                            System.Windows.Forms.MessageBox.Show("Nao foi possível inserir consulta!");

                    }
                    show_consultas();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Impossível conectar!");
                    return;
                }
            }


        }

        private void textBox4_Click(object sender, EventArgs e)
        {

                int nif = Int32.Parse(textBox3.Text);

                if (!nifs_dentista.ContainsKey(nif))
                    return;
                else
                {
                    textBox4.Text = (string)nifs_dentista[nif];
                }

        }


       
        private void btnAlterar_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedCells.Count == 1)
            {
                int codigo = Convert.ToInt32(dataGridView1.CurrentRow.Cells["codigo"].Value.ToString());
                double preco = Double.Parse(dataGridView1.CurrentRow.Cells["preco"].Value.ToString());
                string tipo = dataGridView1.CurrentRow.Cells["tipo"].Value.ToString();
                int duracao = Convert.ToInt32(dataGridView1.CurrentRow.Cells["duracao"].Value.ToString());
                int nif_dentista = Convert.ToInt32(dataGridView1.CurrentRow.Cells["nif_dentista"].Value.ToString());
                string data = dataGridView1.CurrentRow.Cells["dataa"].Value.ToString();
                
                data = data.Substring(0,data.Length - 8);
                string[] dataspt = data.Split('/');
                string procura = dataspt[2].ToString() + "-" + dataspt[1].ToString() + "-" + dataspt[0].ToString();
                data = procura;


                //Verificar se o codigo foi alterado
                if (textBox1.Text != "")
                {
                    System.Windows.Forms.MessageBox.Show("O código da consulta não pode ser alterado!");
                    textBox1.Text = "";
                    return;
                }

                bool success;

                double valor;

                //verificação do preço
                success = Double.TryParse(textBox6.Text, out valor);

                if (textBox6.Text != "")
                {
                    if (valor.ToString().IndexOf(",") != -1) {
                        
                        if (success && (valor.ToString().Length <= 6 && valor.ToString().Length > 0) && (valor.ToString().Substring(valor.ToString().IndexOf(','))).Length <= 2)
                        {

                            preco = valor;
                        }

                        else
                        {
                            System.Windows.Forms.MessageBox.Show("O preço tem de ter no máximo 5 dígigos em que no máximo 2 são casas decimais!");
                            return;
                        }
                    }
                    else
                    {
                        if (success && (valor.ToString().Length <= 3 && valor.ToString().Length > 0))
                        {
                            preco = valor;
                        }

                        else
                        {
                            System.Windows.Forms.MessageBox.Show("O preço tem de ter no máximo 5 dígigos em que no máximo 2 são casas decimais!");
                            return;
                        }
                    }
                }
            

                //verificação da duração

                int valor1;

                success = int.TryParse(textBox5.Text, out valor1);

                if (textBox5.Text != "")
                {
                    if (success && valor > 0)
                    {
                        duracao = valor1;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("A duração(em minutos) tem de ser maior do que 0!");
                        return;
                    }
                }

                //verificação do nif do dentista

                int valor2;

                success = int.TryParse(textBox3.Text, out valor2);

                if (textBox3.Text != "")
                {
                    if (success && valor2.ToString().Length == 9 && nifs_dentista.ContainsKey(valor2))
                    {
                        nif_dentista = valor2;
                        textBox4.Text = (string)nifs_dentista[valor2];
                        tipo = textBox4.Text;

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("O nif do dentista não existe!");
                        return;
                    }
                }

                //verificação da data

                DateTime dDate;

                if (textBox2.Text != "")
                {
                    if (DateTime.TryParse(textBox2.Text, out dDate))
                    {   
                        data = String.Format("{0:d/MM/yyyy}", dDate); 
                        dataspt = data.Split('/'); ;
                        procura = dataspt[2].ToString() + "-" + dataspt[1].ToString() + "-" + dataspt[0].ToString();
                        data = procura;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Data inválida!");
                        return;
                    }
                }


                //Fazer alteração na base de dados
                const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";

                using (SqlConnection SqlConn = new SqlConnection(connectionString))
                {

                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    //--- Pesquisa na BD
                    string sql = "UPDATE PROJETO.CONSULTA SET preco = " + preco + ", tipo = \'" + tipo + "\', duracao = " + duracao + ", nif_dentista = " + nif_dentista + ", dataa = \'" + data + "\' WHERE codigo = " + codigo + ";";

                    using (SqlCommand cmd = new SqlCommand(sql, SqlConn))
                    {
                        int rowsAdded = cmd.ExecuteNonQuery();
                        if (rowsAdded > 0)
                            System.Windows.Forms.MessageBox.Show("Consulta alterada!");
                        else
                            System.Windows.Forms.MessageBox.Show("Nao foi possível alterar consulta!");

                    }

                    if (SqlConn.State == ConnectionState.Open)
                        SqlConn.Close();
                }

                //Preencher a grid com as consultas atualizadas

                show_consultas();

                //dar clear aos campos todos
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox1.Text = "";


            }

            else if(dataGridView1.SelectedCells.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Para alterar é preciso selecionar um código de uma consulta!");
                return;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Apenas uma consulta pode ser alterada de cada vez!");
                return;
            }

        }
        // APAGAR
        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";

                using (SqlConnection SqlConn = new SqlConnection(connectionString))
                {

                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    int codigo;
                    int value;
                    bool success = Int32.TryParse(dataGridView1.SelectedRows[0].Cells["codigo"].Value.ToString(), out value);
                    if(success)
                    {
                        codigo = value;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Não foi possível obter valor de 'código'!");
                        return;
                    }
                    //--- Pesquisa na BD
                    string sql = "DELETE FROM PROJETO.CONSULTA WHERE codigo=" + codigo;

                    using (SqlCommand cmd = new SqlCommand(sql, SqlConn))
                    {
                        int rowsAdded = cmd.ExecuteNonQuery();
                        if (rowsAdded > 0)
                            System.Windows.Forms.MessageBox.Show("Consulta removida!");
                        else
                            System.Windows.Forms.MessageBox.Show("Nao foi possível remover consulta!");

                    }
                    show_consultas();
                    if (SqlConn.State == ConnectionState.Open)
                        SqlConn.Close();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Apenas uma consulta pode ser removida de cada vez!");
                return;
            }

        }





        /*
        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Top = 100;
            dataGridView1.Width = this.Width - (2 * dataGridView1.Left);
            dataGridView1.Height = this.Height - (dataGridView1.Top + dataGridView1.Left);
        }
        */
    }
}
