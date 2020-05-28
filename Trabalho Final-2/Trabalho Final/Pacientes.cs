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
                    string query = "PROJETO.procurar_pacientes";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("procura",search);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();


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
                if (box_contacto.ToString() == "")
                {
                    Int32.TryParse(dataGridView1.CurrentRow.Cells["contacto"].Value.ToString(), out box_contacto);
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
                    Int32.TryParse(dataGridView1.CurrentRow.Cells["idade"].Value.ToString(), out box_idade);
                }
                if (!sucess)
                {
                    MessageBox.Show("Idade inválida");
                    textBox5.Text = "";
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
                    string query = "PROJETO.update_pacientes";
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
                MessageBox.Show("Tem de selecionar o nome de um paciente para ser possível alterá-lo!");
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
