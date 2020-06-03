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
        private DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(@"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000");

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
            dataGridView1.DataSource = dt;
            sqlcon.Open();
            SqlCommand cmd1 = sqlcon.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT PROJETO.PESSOA.nif,endereco,contacto,idade,nome FROM PROJETO.PACIENTE JOIN PROJETO.PESSOA ON PROJETO.PACIENTE.NIF = PROJETO.PESSOA.NIF";
            cmd1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            dt.Clear();
            da.Fill(dt);
            sqlcon.Close();
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            string search = textBox2.Text.Trim();

            //TODO pesquisa dinamica por exemplo */*/2022 devolve todas as consultas de 2022
            if (search == "")
            {
                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT PROJETO.PESSOA.nif,endereco,contacto,idade,nome FROM PROJETO.PACIENTE JOIN PROJETO.PESSOA ON PROJETO.PACIENTE.NIF = PROJETO.PESSOA.NIF";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
                return;
             }    
            else
            {

                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT PROJETO.PESSOA.nif,endereco,contacto,idade,nome FROM PROJETO.PACIENTE JOIN PROJETO.PESSOA ON PROJETO.PACIENTE.NIF = PROJETO.PESSOA.NIF WHERE PROJETO.PESSOA.NOME LIKE @search";
                SqlParameter p = new SqlParameter("@search", SqlDbType.Char, 255)
                {
                    Value = "%" + search + "%"
                };
                cmd1.Parameters.AddWithValue("search", search);
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
                return;
            }

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
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
                else if (!sucess || box_contacto.ToString().Length != 9)
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

                //--- Pesquisa na BD

                string query = "PROJETO.update_paciente";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", nome_box);
                cmd.Parameters.AddWithValue("contacto", box_contacto);
                cmd.Parameters.AddWithValue("nif", box_nif);
                cmd.Parameters.AddWithValue("idade", box_idade);
                cmd.Parameters.AddWithValue("endereco", box_endereco);
                cmd.Parameters.Add("@retval", SqlDbType.Int);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                int retval = (int)cmd.Parameters["@retval"].Value;
                sqlcon.Close();

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

                //Load do datagrid de novo
                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT PROJETO.PESSOA.nif,endereco,contacto,idade,nome FROM PROJETO.PACIENTE JOIN PROJETO.PESSOA ON PROJETO.PACIENTE.NIF = PROJETO.PESSOA.NIF";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
                textBox1.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
            else
            {
                MessageBox.Show("Tem de selecionar o nome de um paciente para ser possível alterá-lo!");
                return;
            }
        }
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            //Buscar o nif da clinica
            int nif_clinica;

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = sqlcon;

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT PROJETO.get_nif_clinica()";
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
                nif_clinica = Int32.Parse(cmd.ExecuteScalar().ToString());
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
            if (!sucess || box_contacto.ToString().Length != 9)
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
            // INSERT 

            string query = "PROJETO.insert_paciente";
            SqlCommand cmd1 = new SqlCommand(query, sqlcon);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("nome", nome_box);
            cmd1.Parameters.AddWithValue("contacto", box_contacto);
            cmd1.Parameters.AddWithValue("nif", box_nif);
            cmd1.Parameters.AddWithValue("idade", box_idade);
            cmd1.Parameters.AddWithValue("endereco", box_endereco);
            cmd1.Parameters.Add("@retval", SqlDbType.Int);
            cmd1.Parameters["@retval"].Direction = ParameterDirection.Output;
            sqlcon.Open();
            cmd.ExecuteNonQuery();
            int retval = (int)cmd.Parameters["@retval"].Value;
            sqlcon.Close();

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

            //limpar as text box todas
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            //fazer load do datagrid de novo
            sqlcon.Open();
            SqlCommand cmd2 = sqlcon.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT PROJETO.PESSOA.nif,endereco,contacto,idade,nome FROM PROJETO.PACIENTE JOIN PROJETO.PESSOA ON PROJETO.PACIENTE.NIF = PROJETO.PESSOA.NIF";
            cmd2.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            dt.Clear();
            da.Fill(dt);
            sqlcon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
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
            success = Double.TryParse(textBox8.Text.ToString().Replace('.',','), out box_desconto);

            if (success && box_desconto.ToString().IndexOf(',') != -1)
            {
                if ((box_desconto.ToString().Length > 6 || textBox8.Text == "") && (box_desconto.ToString().Substring(box_desconto.ToString().IndexOf(','))).Length - 1 > 2)
                {
                    System.Windows.Forms.MessageBox.Show("O campo \"desconto\" deve ter no máximo tamanho 5 em que 2 tem de ser decimais!");
                    textBox8.Text = "";
                    return;
                }
            }
            else if(success)
            {
                if (box_desconto.ToString().Length > 5 || textBox8.Text == "")
                {
                    System.Windows.Forms.MessageBox.Show("O campo \"desconto\" deve ter no máximo tamanho 5!");
                    textBox8.Text = "";
                    return;
                }
            }
            else
            {
                MessageBox.Show("Desconto inválido!");
                textBox8.Text = "";
                return;
            }

            // INSERT 

            string query = "PROJETO.insert_check_dentista";
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codigo", box_codigo);
            cmd.Parameters.AddWithValue("valor", box_desconto);
            cmd.Parameters.AddWithValue("validade", box_validade);
            cmd.Parameters.Add("@retval", SqlDbType.Int);
            cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
            sqlcon.Open();
            cmd.ExecuteNonQuery();
            int retval = (int)cmd.Parameters["@retval"].Value;
            sqlcon.Close();

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
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            if (dataGridView1.SelectedCells.Count == 1)
            {

                int nif;
                bool success = Int32.TryParse(dataGridView1.CurrentRow.Cells["nif"].Value.ToString(), out nif);
                if (!success)
                {
                    System.Windows.Forms.MessageBox.Show("Não foi possível obter valor do 'nif'!");
                    return;
                }

                string query = "PROJETO.remove_paciente";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nif", nif);
                cmd.Parameters.Add("@retval", SqlDbType.Int);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                int retval = (int)cmd.Parameters["@retval"].Value;
                sqlcon.Close();


                if (retval == 0)
                {
                    MessageBox.Show("Não foi possível remover paciente!");
                    return;
                }
                else
                {
                    MessageBox.Show("Paciente removido!");
                }

                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT PROJETO.PESSOA.nif,endereco,contacto,idade,nome FROM PROJETO.PACIENTE JOIN PROJETO.PESSOA ON PROJETO.PACIENTE.NIF = PROJETO.PESSOA.NIF";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();

            }
            else
            {
                MessageBox.Show("Tem de selecionar um e apenas um paciente para ser possível removê-lo!");
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
