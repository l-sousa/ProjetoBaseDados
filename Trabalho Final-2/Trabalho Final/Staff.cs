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
using System.Runtime.CompilerServices;
using System.Collections;
using Microsoft.Win32;

namespace Trabalho_Final
{

    public partial class Staff : UserControl
    {
        private static Staff _instance;

        const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";
        public static Staff Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Staff();
                return _instance;
            }
        }
        public Staff()
        {

            InitializeComponent();

        }

        private void Staff_Load(object sender, EventArgs e)
        {

            showStaff("SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.NIF=PROJETO.STAFF.NIF");
        }

        private void showStaff(string query)
        {
            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {

                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD
                SqlDataAdapter da = new SqlDataAdapter(query, SqlConn);
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
            string query;
            string search = textBox2.Text.Trim();
            if (Todos.Checked)
            {
                if (search == "")
                {
                    query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif";
                    showStaff(query);
                }
                else
                {
                    query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN PROJETO.PESSOA ON PROJETO.PESSOA.nif = PROJETO.STAFF.nif WHERE nome LIKE \'%" + search + "%\'";
                    showStaff(query);
                    textBox2.Text = "";
                }
            }
            else if (Dentista.Checked)
            {
                if (search == "")
                {
                    query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome,especialidade,numero_ordem FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.DENTISTA ON PROJETO.STAFF.nif=PROJETO.DENTISTA.nif";
                    showStaff(query);
                }
                else
                {
                    query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome,especialidade,numero_ordem FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.DENTISTA ON PROJETO.STAFF.nif=PROJETO.DENTISTA.nif WHERE nome LIKE \'%" + search + "%\'";
                    showStaff(query);
                    textBox2.Text = "";
                }
            }
            else if (Rececionista.Checked)
            {
                if (search == "")
                {
                    query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.RECECIONISTA ON PROJETO.STAFF.nif=PROJETO.RECECIONISTA.nif";
                    showStaff(query);
                }
                else
                {
                    query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.RECECIONISTA ON PROJETO.STAFF.nif=PROJETO.RECECIONISTA.nif WHERE nome LIKE \'%" + search + "%\'";
                    showStaff(query);
                    textBox2.Text = "";
                }
            }
            else if (Assistente.Checked)
            {
                if (search == "")
                {
                    query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.ASSISTENTE ON PROJETO.STAFF.nif=PROJETO.ASSISTENTE.nif";
                    showStaff(query);
                }
                else
                {
                    query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.ASSISTENTE ON PROJETO.STAFF.nif=PROJETO.ASSISTENTE.nif WHERE nome LIKE \'%" + search + "%\'";
                    showStaff(query);
                    textBox2.Text = "";
                }
            }
            else
            {
                if (search == "")
                {
                    query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif";
                    showStaff(query);
                }
                else
                {
                    query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN PROJETO.PESSOA ON PROJETO.PESSOA.nif = PROJETO.STAFF.nif WHERE nome LIKE \'%" + search + "%\'";
                    showStaff(query);
                    textBox2.Text = "";
                }
            }

        }

        private void Filtro_Todos(object sender, EventArgs e)
        {
            if (Todos.Checked)
            {
                string query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif";
                showStaff(query);
            }

        }

        private void Dentista_CheckedChanged(object sender, EventArgs e)
        {
            if (Dentista.Checked)
            {
                string query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome,especialidade,numero_ordem FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.DENTISTA ON PROJETO.STAFF.nif=PROJETO.DENTISTA.nif";
                showStaff(query);
            }
        }
        private void Assistente_CheckedChanged(object sender, EventArgs e)
        {
            if (Assistente.Checked)
            {
                string query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.ASSISTENTE ON PROJETO.STAFF.nif=PROJETO.ASSISTENTE.nif";
                showStaff(query);
            }
        }

        private void Rececionista_CheckedChanged(object sender, EventArgs e)
        {
            if (Rececionista.Checked)
            {
                string query = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.RECECIONISTA ON PROJETO.STAFF.nif=PROJETO.RECECIONISTA.nif";
                showStaff(query);
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            //Buscar o nif da clinica
            int nif_clinica;
            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {

                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD

                // INSERT 
                //  @especialidade varchar(35), @numero_ordem int, @nif_clinica int, @nome varchar(100), @contacto int, @nif int, @idade int, @endereco varchar(100), @salario decimal(8, 2), @profissao varchar(100) 

                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PROJETO.get_nif_clinica()";
                if (SqlConn.State == ConnectionState.Closed)
                {
                    SqlConn.Open();
                    nif_clinica = Int32.Parse(cmd.ExecuteScalar().ToString());
                    MessageBox.Show("" + nif_clinica);
                }


                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }

            // VALIDAÇÃO DAS CAIXAS DE TEXTO
            string nome_box = textBox1.Text;

            int box_contacto;
            bool sucess = Int32.TryParse(textBox6.Text, out box_contacto);
            if (!sucess)
            {
                MessageBox.Show("Contacto inválido");
                textBox6.Text = "";
                return;
            }

            int box_idade;
            sucess = Int32.TryParse(textBox5.Text, out box_idade);
            if (!sucess)
            {
                MessageBox.Show("Idade inválida");
                textBox5.Text = "";
            }

            int box_nif;
            sucess = Int32.TryParse(textBox4.Text, out box_nif);
            if (!sucess)
            {
                MessageBox.Show("Nif inválido");
                textBox4.Text = "";
                return;
            }

            string box_endereco = textBox3.Text;

            string specifier = "G";

            double box_salario;
            sucess = Double.TryParse(textBox7.Text, out box_salario);
            if (sucess && box_salario.ToString().IndexOf(',') != -1)
            {
                if ((box_salario.ToString(specifier).Length > 9 || textBox7.Text == "") && (box_salario.ToString().Substring(box_salario.ToString().IndexOf(','))).Length > 2)
                {
                    System.Windows.Forms.MessageBox.Show("O campo \"Salário\" deve ter no máximo tamanho 8!");
                    textBox7.Text = "";
                    return;
                }
            }
            else
            {
                if (box_salario.ToString(specifier).Length > 6 || textBox7.Text == "")
                {
                    System.Windows.Forms.MessageBox.Show("O campo \"Salário\" deve ter no máximo tamanho 8 em que 2 tem de ser decimais!");
                    textBox7.Text = "";
                    return;
                }
            }

            string profissao;
            string especialidade;
            int numero_ordem;

            if (comboBox1.SelectedIndex > -1)
            {
                profissao = comboBox1.Text;

                if (profissao.Equals("DENTISTA"))
                {
                    especialidade = textBox8.Text;
                    sucess = Int32.TryParse(textBox9.Text, out numero_ordem);
                    if (!sucess)
                    {
                        MessageBox.Show("Numero ordem inválido");
                        return;
                    }
                }
                else
                {
                    especialidade = null;
                    numero_ordem = -99999;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Selecione uma profissão!");
                return;
            }



            //Inserção da staff
            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {
                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD

                // INSERT 
                string query = "PROJETO.insert_staff(" + especialidade + "," + numero_ordem + "," + nome_box + "," + box_contacto + "," + box_nif + "," + box_idade + "," + box_endereco + "," + box_salario + "," + profissao + ")";
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@retValue", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;

                int retval = (int)cmd.Parameters["@retValue"].Value;
                MessageBox.Show(""+retval);
                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }


            //limpar as text box todas
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";

            //fazer load do datagrid de novo
            showStaff("SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.NIF=PROJETO.STAFF.NIF");

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string profissao;
            if (comboBox1.SelectedIndex > -1)
            {

                profissao = comboBox1.Text.Trim();

                if (profissao.Equals("DENTISTA"))
                {

                    label8.Visible = true;
                    label9.Visible = true;
                    label10.Visible = true;
                    label11.Visible = true;
                    textBox8.Visible = true;
                    textBox9.Visible = true;

                }
                else
                {
                    label8.Visible = false;
                    label9.Visible = false;
                    label10.Visible = false;
                    label11.Visible = false;
                    textBox8.Visible = false;
                    textBox9.Visible = false;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Selecione uma profissão!");
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

