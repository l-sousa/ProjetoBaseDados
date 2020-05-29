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
using System.Runtime.InteropServices.WindowsRuntime;

namespace Trabalho_Final
{
    public partial class Pacientes : UserControl
    {
        const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";

        private static Pacientes _instance;
        public static Pacientes Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Pacientes();
                return _instance;
            }
        }
        public Pacientes()
        {
            InitializeComponent();
        }

        private void Pacientes_Load(object sender, EventArgs e)
        {
            showPacientes("SELECT PROJETO.PESSOA.nif,endereco,contacto,idade,nome FROM PROJETO.PACIENTE JOIN PROJETO.PESSOA ON PROJETO.PACIENTE.NIF = PROJETO.PESSOA.NIF");
        }

        private void showPacientes(string query)
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
                using (SqlConnection SqlConn = new SqlConnection(connectionString))
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    //--- Pesquisa na BD

                    String search = textBox2.Text.Trim();

                    SqlConnection conn = new SqlConnection(connectionString);
                    string query = "PROJETO.search_paciente";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("nome",search);
                    cmd.Parameters.Add("@retval", SqlDbType.Int);
                    cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    int retval = (int)cmd.Parameters["@retval"].Value;
                    conn.Close();
                    
                    
                    if(retval  == 0)
                    {
                        MessageBox.Show("Não foi possível filtrar a pesquisa!");
                        textBox2.Text = "";
                        return;
                    }
                    if (SqlConn.State == ConnectionState.Open)
                        SqlConn.Close();
                }

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 1)
            {
                string nome_box = textBox1.Text;
                if (nome_box == "")
                {
                    nome_box = dataGridView1.CurrentRow.Cells["nome"].Value.ToString();
                }
                else if (nome_box.Length >= 100 || nome_box.Length < 1)
                {
                    MessageBox.Show("Nome inválido");
                    textBox1.Text = "";
                    return;
                }

                int box_contacto;
                bool sucess = Int32.TryParse(textBox6.Text, out box_contacto);
                if (textBox6.Text == "")
                {
                    Int32.TryParse(dataGridView1.CurrentRow.Cells["contacto"].Value.ToString(), out box_contacto);
                }
                else if (!sucess || box_contacto.ToString().Length != 9 || !box_contacto.ToString().StartsWith("91"))
                {
                    MessageBox.Show("Contacto inválido!");
                    textBox6.Text = "";
                    return;
                }

                int box_idade;
                sucess = Int32.TryParse(textBox5.Text, out box_idade);
                if (textBox5.Text.Trim() == "")
                {
                    Int32.TryParse(dataGridView1.CurrentRow.Cells["idade"].Value.ToString(), out box_idade);
                }
                else if (!sucess)
                {
                    MessageBox.Show("Idade inválida");
                    textBox5.Text = "";
                    return;
                }

                int box_nif;
                if (textBox4.Text == "")
                {
                    Int32.TryParse(dataGridView1.CurrentRow.Cells["nif"].Value.ToString(), out box_nif);
                }
                else
                {
                    MessageBox.Show("O Nif não pode ser alterado!");
                    return;
                }

                string box_endereco = textBox3.Text;
                if (box_endereco.ToString() == "")
                {
                    box_endereco = dataGridView1.CurrentRow.Cells["endereco"].Value.ToString();
                }
                else if (box_endereco.Length >= 100 || box_endereco.Length < 1)
                {
                    MessageBox.Show("Endereço inválido");
                    textBox3.Text = "";
                    return;
                }

                

                using (SqlConnection SqlConn = new SqlConnection(connectionString))
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    //--- Pesquisa na BD

                    SqlConnection conn = new SqlConnection(connectionString);
                    string query = "PROJETO.update_paciente";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("nome", nome_box);
                    cmd.Parameters.AddWithValue("contacto", box_contacto);
                    cmd.Parameters.AddWithValue("nif", box_nif);
                    cmd.Parameters.AddWithValue("idade", box_idade);
                    cmd.Parameters.AddWithValue("endereco", box_endereco);
                    cmd.Parameters.Add("@retval", SqlDbType.Int);
                    cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    int retval = (int)cmd.Parameters["@retval"].Value;
                    conn.Close();

                    if (retval == 0)
                    {
                        MessageBox.Show("Não foi possível alterar!");
                        textBox1.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Paciente alterado!");
                        textBox1.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                    }

                    if (SqlConn.State == ConnectionState.Open)
                        SqlConn.Close();
                }

                //Load do datagrid de novo
                showPacientes("SELECT PROJETO.PESSOA.nif,endereco,contacto,idade,nome FROM PROJETO.PACIENTE JOIN PROJETO.PESSOA ON PROJETO.PACIENTE.NIF = PROJETO.PESSOA.NIF");

            }
            else
            {
                MessageBox.Show("Tem de selecionar o nome de um paciente para ser possível alterá-lo!");
                return;
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
                }


                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }

            // VALIDAÇÃO DAS CAIXAS DE TEXTO
            string nome_box = textBox1.Text;
            if (nome_box.Length >= 100 || nome_box.Length < 1)
            {
                MessageBox.Show("Nome inválido");
                textBox1.Text = "";
                return;
            }

            int box_contacto;
            bool sucess = Int32.TryParse(textBox6.Text, out box_contacto);
            if (!sucess || box_contacto.ToString().Length != 9 || !box_contacto.ToString().StartsWith("91"))
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
                return;
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
            if (box_endereco.Length >= 100 || box_endereco.Length < 1)
            {
                MessageBox.Show("Endereço inválido");
                textBox3.Text = "";
                return;
            }

            //Inserção de Paciente
            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {
                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD

                // INSERT 

                SqlConnection conn = new SqlConnection(connectionString);
                string query = "PROJETO.insert_paciente";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", nome_box);
                cmd.Parameters.AddWithValue("contacto", box_contacto);
                cmd.Parameters.AddWithValue("nif", box_nif);
                cmd.Parameters.AddWithValue("idade", box_idade);
                cmd.Parameters.AddWithValue("endereco", box_endereco);
                cmd.Parameters.Add("@retval", SqlDbType.Int);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                int retval = (int)cmd.Parameters["@retval"].Value;
                conn.Close();

                if (retval == -1)
                {
                    MessageBox.Show("Nif já existente!");
                    textBox4.Text = "";
                    return;
                }
                else if(retval == 0){
                    MessageBox.Show("Nao foi possível inserir paciente!");
                    textBox1.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    return;
                }
                else
                {
                    MessageBox.Show("Adicionado!");
                    textBox1.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                }

                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }

            //limpar as text box todas
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            //fazer load do datagrid de novo
            showPacientes("SELECT PROJETO.PESSOA.nif,endereco,contacto,idade,nome FROM PROJETO.PACIENTE JOIN PROJETO.PESSOA ON PROJETO.PACIENTE.NIF = PROJETO.PESSOA.NIF");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // VALIDAÇÃO DAS CAIXAS DE TEXTO
            int box_codigo;
            bool success = Int32.TryParse(textBox7.Text,out box_codigo);
            if (!success)
            {
                MessageBox.Show("Código inválido");
                textBox7.Text = "";
                return;
            }

            string search = textBox9.Text;
            string box_validade;
            if (search == "")
            {
                MessageBox.Show("A data tem de ser preenchida!");
                textBox9.Text = "";
                return;
            }
            else
            {
                string[] dataspt = search.Split('/'); ;
                box_validade = dataspt[2].ToString() + "-" + dataspt[1].ToString() + "-" + dataspt[0].ToString();
            }

            double box_desconto;
            success = Double.TryParse(textBox8.Text, out box_desconto);

            if (success && box_desconto.ToString().IndexOf(',') != -1)
            {
                if ((box_desconto.ToString().Length > 6 || textBox8.Text == "") && (box_desconto.ToString().Substring(box_desconto.ToString().IndexOf(','))).Length - 1 > 2)
                {
                    System.Windows.Forms.MessageBox.Show("O campo \"desconto\" deve ter no máximo tamanho 5 em que 2 tem de ser decimais!");
                    textBox8.Text = "";
                    return;
                }
            }
            else
            {
                if (box_desconto.ToString().Length > 5 || textBox8.Text == "")
                {
                    System.Windows.Forms.MessageBox.Show("O campo \"desconto\" deve ter no máximo tamanho 5!");
                    textBox8.Text = "";
                    return;
                }
            }


            //Inserção de Cheque dentista
            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {
                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD

                // INSERT 

                SqlConnection conn = new SqlConnection(connectionString);
                string query = "PROJETO.insert_check_dentista";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codigo", box_codigo);
                cmd.Parameters.AddWithValue("valor", box_desconto);
                cmd.Parameters.AddWithValue("validade", box_validade);
                cmd.Parameters.Add("@retval", SqlDbType.Int);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                int retval = (int)cmd.Parameters["@retval"].Value;
                conn.Close();

                if (retval == -1)
                {
                    MessageBox.Show("Código já existente!");
                    textBox7.Text = "";
                    return;
                }
                else if (retval == 0)
                {
                    MessageBox.Show("Não foi possível adicionar cheque!");
                    textBox9.Text = "";
                    return;
                }
                else
                {
                    MessageBox.Show("Cheque dentista adicionado!");
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                }

                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }

            //limpar as text box todas
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
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
