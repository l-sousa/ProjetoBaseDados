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
            if (nome_box.Length >= 100 || nome_box.Length < 1)
            {
                MessageBox.Show("Nome inválido");
                textBox1.Text = "";
                return;
            }

            int box_contacto;
            bool sucess = Int32.TryParse(textBox6.Text, out box_contacto);
            if (!sucess || box_contacto.ToString().Length!=9 || !box_contacto.ToString().StartsWith("91"))
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
            if (box_endereco.Length>=100 || box_endereco.Length<1)
            {
                MessageBox.Show("Endereço inválido");
                textBox3.Text = "";
                return;
            }

            double box_salario;
            sucess = Double.TryParse(textBox7.Text, out box_salario);
            if (sucess && box_salario.ToString().IndexOf(".") != -1)
            {
                MessageBox.Show(((box_salario.ToString().Substring(box_salario.ToString().IndexOf('.'))).Length - 1 <= 2).ToString());
                if ((box_salario.ToString().Length > 9 || textBox7.Text == "") && (box_salario.ToString().Substring(box_salario.ToString().IndexOf('.'))).Length - 1 > 2)
                {
                    System.Windows.Forms.MessageBox.Show("O campo \"Salário\" deve ter no máximo tamanho 8!");
                    textBox7.Text = "";
                    return;
                }
            }
            else
            {
                if (box_salario.ToString().Length > 6 || textBox7.Text == "")
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
                profissao = comboBox1.Text.Trim();
                if (profissao.Equals("DENTISTA"))
                {
                    especialidade = comboBox2.Text.Trim();
                    sucess = Int32.TryParse(textBox9.Text, out numero_ordem);
                    if (!sucess)
                    {
                        MessageBox.Show("Numero ordem inválido");
                        return;
                    }
                }
                else
                {
                    especialidade = DBNull.Value.ToString();
                    numero_ordem = -99999;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Selecione uma profissão!");
                return;
            }

            MessageBox.Show(numero_ordem.ToString());

            //Inserção da staff
            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {
                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD

                // INSERT 
                //, @idade int, @endereco varchar(100), @salario decimal(8,2), @profissao varchar(100) 

                SqlConnection conn = new SqlConnection(connectionString);
                string query = "PROJETO.insert_staff";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("especialidade", especialidade);
                cmd.Parameters.AddWithValue("numero_ordem", numero_ordem);
                cmd.Parameters.AddWithValue("nome", nome_box);
                cmd.Parameters.AddWithValue("contacto", box_contacto);
                cmd.Parameters.AddWithValue("nif", box_nif);
                cmd.Parameters.AddWithValue("idade", box_idade);
                cmd.Parameters.AddWithValue("endereco", box_endereco);
                cmd.Parameters.AddWithValue("salario", box_salario);
                cmd.Parameters.AddWithValue("profissao", profissao);
                cmd.Parameters.Add("@retval", SqlDbType.Int);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                int retval = (int)cmd.Parameters["@retval"].Value;
                conn.Close();

                if(retval == -1)
                {
                    MessageBox.Show("Nif já existente!");
                    textBox4.Text = "";
                    return;
                }
                else if(retval == 0)
                {
                    MessageBox.Show("Número da ordem ja existente!");
                    textBox9.Text = "";
                    return;
                }
                else
                {
                    MessageBox.Show("Adicionado!");
                    label8.Visible = false;
                    label9.Visible = false;
                    label10.Visible = false;
                    label11.Visible = false;
                    textBox9.Visible = false;
                    comboBox2.Visible = false;
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
            textBox7.Text = "";
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
                    textBox9.Visible = true;
                    comboBox2.Visible = true;

                }
                else
                {
                    label8.Visible = false;
                    label9.Visible = false;
                    label10.Visible = false;
                    label11.Visible = false;
                    textBox9.Visible = false;
                    comboBox2.Visible = false;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Selecione uma profissão!");
                return;
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                using (SqlConnection SqlConn = new SqlConnection(connectionString))
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    //--- Pesquisa na BD

                    int nif;
                    bool success = Int32.TryParse(dataGridView1.SelectedRows[0].Cells["nif"].Value.ToString(), out nif);
                    if (!success)
                    {
                        System.Windows.Forms.MessageBox.Show("Não foi possível obter valor de 'código'!");
                        return;
                    }

                    SqlConnection conn = new SqlConnection(connectionString);
                    string query = "PROJETO.remove_staff";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("nif", nif);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();


                    if (SqlConn.State == ConnectionState.Open)
                        SqlConn.Close();
                }

                MessageBox.Show("Pessoa do staff eliminada com sucesso!");

            }
            else
            {
                MessageBox.Show("Tem de selecionar um membro do staff para ser possível removê-lo!");
                return;
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string nome_box = textBox1.Text;
                if(nome_box == "")
                {
                    nome_box = dataGridView1.SelectedRows[0].Cells["nome"].Value.ToString();
                }
                else if(nome_box.Length >= 100 || nome_box.Length < 1)
                {
                    MessageBox.Show("Nome inválido");
                    textBox1.Text = "";
                    return;
                }

                int box_contacto;
                bool sucess = Int32.TryParse(textBox6.Text, out box_contacto);
                if (box_contacto.ToString() == "")
                {
                    Int32.TryParse(dataGridView1.SelectedRows[0].Cells["contacto"].Value.ToString(),out box_contacto);
                }
                else if (!sucess || box_contacto.ToString().Length != 9 || !box_contacto.ToString().StartsWith("91"))
                {
                    MessageBox.Show("Contacto inválido");
                    textBox6.Text = "";
                    return;
                }

                int box_idade;
                sucess = Int32.TryParse(textBox5.Text, out box_idade);
                if (box_idade.ToString() == "")
                {
                    Int32.TryParse(dataGridView1.SelectedRows[0].Cells["idade"].Value.ToString(), out box_idade);
                }
                if (!sucess)
                {
                    MessageBox.Show("Idade inválida");
                    textBox5.Text = "";
                }

                int box_nif;
                if(textBox4.Text == "")
                {
                    Int32.TryParse(dataGridView1.SelectedRows[0].Cells["nif"].Value.ToString(), out box_nif);
                }
                else
                {
                    MessageBox.Show("O Nif não pode ser alterado!");
                    return;
                }

                string box_endereco = textBox3.Text;
                if (box_endereco.ToString() == "")
                {
                    box_endereco = dataGridView1.SelectedRows[0].Cells["endereco"].Value.ToString();
                }
                else if (box_endereco.Length >= 100 || box_endereco.Length < 1)
                {
                    MessageBox.Show("Endereço inválido");
                    textBox3.Text = "";
                    return;
                }

                double box_salario;
                sucess = Double.TryParse(textBox7.Text, out box_salario);
                if (box_salario.ToString() == "")
                {
                    Double.TryParse(dataGridView1.SelectedRows[0].Cells["salario"].Value.ToString(), out box_salario);
                }
                else if (sucess && box_salario.ToString().IndexOf(".") != -1)
                {
                    MessageBox.Show(((box_salario.ToString().Substring(box_salario.ToString().IndexOf('.'))).Length - 1 <= 2).ToString());
                    if ((box_salario.ToString().Length > 9 || textBox7.Text == "") && (box_salario.ToString().Substring(box_salario.ToString().IndexOf('.'))).Length - 1 > 2)
                    {
                        System.Windows.Forms.MessageBox.Show("O campo \"Salário\" deve ter no máximo tamanho 8!");
                        textBox7.Text = "";
                        return;
                    }
                }
                else
                {
                    if (box_salario.ToString().Length > 6 || textBox7.Text == "")
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
                    profissao = comboBox1.Text.Trim();
                    if (profissao.Equals("DENTISTA"))
                    {
                        especialidade = dataGridView1.SelectedRows[0].Cells["especialidade"].Value.ToString();
                        comboBox2.SelectedItem = especialidade.ToUpper();
                        if (textBox9.Text == "")
                        {
                            Int32.TryParse(dataGridView1.SelectedRows[0].Cells["numero_ordem"].Value.ToString(), out numero_ordem);
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("O número de ordem não pode ser alterado!");
                            textBox9.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        especialidade = DBNull.Value.ToString();
                        numero_ordem = -99999;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Selecione uma profissão para poder alterar uma pessoa do staff!");
                    return;
                }

                using (SqlConnection SqlConn = new SqlConnection(connectionString))
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    //--- Pesquisa na BD

                    SqlConnection conn = new SqlConnection(connectionString);
                    string query = "PROJETO.update_staff";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("especialidade", especialidade);
                    cmd.Parameters.AddWithValue("numero_ordem", numero_ordem);
                    cmd.Parameters.AddWithValue("nome", nome_box);
                    cmd.Parameters.AddWithValue("contacto", box_contacto);
                    cmd.Parameters.AddWithValue("nif", box_nif);
                    cmd.Parameters.AddWithValue("idade", box_idade);
                    cmd.Parameters.AddWithValue("endereco", box_endereco);
                    cmd.Parameters.AddWithValue("salario", box_salario);
                    cmd.Parameters.AddWithValue("profissao", profissao);
                    cmd.Parameters.Add("@retval", SqlDbType.Int);
                    cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    int retval = (int)cmd.Parameters["@retval"].Value;
                    conn.Close();

                    /* Mudar o return
                    if (retval == -1)
                    {
                        MessageBox.Show("Nif já existente!");
                        textBox4.Text = "";
                        return;
                    }
                    else if (retval == 0)
                    {
                        MessageBox.Show("Número da ordem ja existente!");
                        textBox9.Text = "";
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Adicionado!");
                        label8.Visible = false;
                        label9.Visible = false;
                        label10.Visible = false;
                        label11.Visible = false;
                        textBox9.Visible = false;
                        comboBox2.Visible = false;
                    }*/

                    if (SqlConn.State == ConnectionState.Open)
                        SqlConn.Close();
                }

            }
            else
            {
                MessageBox.Show("Tem de selecionar um membro do staff para ser possível alterá-lo!");
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

