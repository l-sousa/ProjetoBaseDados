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
        private DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(@"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000");
        private static Staff _instance;
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
            dataGridView1.DataSource = dt;
            sqlcon.Open();
            SqlCommand cmd1 = sqlcon.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.NIF=PROJETO.STAFF.NIF";
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
            if (Todos.Checked)
            {
                if (search == "")
                {
                    sqlcon.Open();
                    SqlCommand cmd1 = sqlcon.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.NIF=PROJETO.STAFF.NIF";
                    cmd1.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    dt.Clear();
                    da.Fill(dt);
                    sqlcon.Close();
                }
                else
                {
                    sqlcon.Open();
                    SqlCommand cmd1 = sqlcon.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN PROJETO.PESSOA ON PROJETO.PESSOA.nif = PROJETO.STAFF.nif WHERE nome LIKE @search";
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

                    textBox2.Text = "";
                }
            }
            else if (Dentista.Checked)
            {
                if (search == "")
                {
                    sqlcon.Open();
                    SqlCommand cmd1 = sqlcon.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome,especialidade,numero_ordem FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.DENTISTA ON PROJETO.STAFF.nif=PROJETO.DENTISTA.nif";
                    cmd1.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    dt.Clear();
                    da.Fill(dt);
                    sqlcon.Close();
                }
                else
                {
                    sqlcon.Open();
                    SqlCommand cmd1 = sqlcon.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome,especialidade,numero_ordem FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.DENTISTA ON PROJETO.STAFF.nif=PROJETO.DENTISTA.nif WHERE nome LIKE @search";
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

                    textBox2.Text = "";
                }
            }
            else if (Rececionista.Checked)
            {
                if (search == "")
                {
                    sqlcon.Open();
                    SqlCommand cmd1 = sqlcon.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.RECECIONISTA ON PROJETO.STAFF.nif=PROJETO.RECECIONISTA.nif";
                    cmd1.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    dt.Clear();
                    da.Fill(dt);
                    sqlcon.Close();
                }
                else
                {
                    sqlcon.Open();
                    SqlCommand cmd1 = sqlcon.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.RECECIONISTA ON PROJETO.STAFF.nif=PROJETO.RECECIONISTA.nif WHERE nome LIKE @search";
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

                    textBox2.Text = "";
                }
            }
            else if (Assistente.Checked)
            {
                if (search == "")
                {
                    sqlcon.Open();
                    SqlCommand cmd1 = sqlcon.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.ASSISTENTE ON PROJETO.STAFF.nif=PROJETO.ASSISTENTE.nif";
                    cmd1.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    dt.Clear();
                    da.Fill(dt);
                    sqlcon.Close();
                }
                else
                {
                    sqlcon.Open();
                    SqlCommand cmd1 = sqlcon.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.ASSISTENTE ON PROJETO.STAFF.nif=PROJETO.ASSISTENTE.nif WHERE nome LIKE @search";
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

                    textBox2.Text = "";
                }
            }
            else
            {
                if (search == "")
                {
                    sqlcon.Open();
                    SqlCommand cmd1 = sqlcon.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif";
                    cmd1.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    dt.Clear();
                    da.Fill(dt);
                    sqlcon.Close();
                }
                else
                {
                    sqlcon.Open();
                    SqlCommand cmd1 = sqlcon.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN PROJETO.PESSOA ON PROJETO.PESSOA.nif = PROJETO.STAFF.nif WHERE nome LIKE @search";
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

                    textBox2.Text = "";
                }
            }

        }

        private void Filtro_Todos(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            if (Todos.Checked)
            {
                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
            }

        }

        private void Dentista_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            if (Dentista.Checked)
            {
                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome,especialidade,numero_ordem FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.DENTISTA ON PROJETO.STAFF.nif=PROJETO.DENTISTA.nif";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
            }
        }
        private void Assistente_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            if (Assistente.Checked)
            {
                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.ASSISTENTE ON PROJETO.STAFF.nif=PROJETO.ASSISTENTE.nif";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
            }
        }

        private void Rececionista_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            if (Rececionista.Checked)
            {
                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.nif=PROJETO.STAFF.nif JOIN PROJETO.RECECIONISTA ON PROJETO.STAFF.nif=PROJETO.RECECIONISTA.nif";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
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


            // INSERT  

            string query = "PROJETO.insert_staff";
            SqlCommand cmd = new SqlCommand(query, sqlcon);
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
            sqlcon.Open();
            cmd.ExecuteNonQuery();
            int retval = (int)cmd.Parameters["@retval"].Value;
            sqlcon.Close();

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

            //limpar as text box todas
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox9.Text = "";

            //fazer load do datagrid de novo
            sqlcon.Open();
            SqlCommand cmd1 = sqlcon.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.NIF=PROJETO.STAFF.NIF";
            cmd1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            dt.Clear();
            da.Fill(dt);
            sqlcon.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
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
            dataGridView1.DataSource = dt;
            if (dataGridView1.SelectedCells.Count == 1)
            {
                int nif;
                bool success = Int32.TryParse(dataGridView1.CurrentRow.Cells["nif"].Value.ToString(), out nif);
                if (!success)
                {
                    System.Windows.Forms.MessageBox.Show("Não foi possível obter valor de 'código'!");
                    return;
                }

                string query = "PROJETO.remove_staff";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nif", nif);
                cmd.Parameters.Add("@retval", SqlDbType.Int);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                int retval = (int)cmd.Parameters["@retval"].Value;
                sqlcon.Close();

                

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
            dataGridView1.DataSource = dt;
            if (dataGridView1.SelectedCells.Count == 1)
            {
                string nome_box = textBox1.Text;
                if(nome_box == "")
                {
                    nome_box = dataGridView1.CurrentRow.Cells["nome"].Value.ToString();
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
                    Int32.TryParse(dataGridView1.CurrentRow.Cells["contacto"].Value.ToString(),out box_contacto);
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
                if(textBox4.Text == "")
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

                double box_salario;
                sucess = Double.TryParse(textBox7.Text, out box_salario);
                if (box_salario.ToString() == "")
                {
                    Double.TryParse(dataGridView1.CurrentRow.Cells["salario"].Value.ToString(), out box_salario);
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
                        especialidade = dataGridView1.CurrentRow.Cells["especialidade"].Value.ToString();
                        comboBox2.SelectedItem = especialidade.ToUpper();
                        if (textBox9.Text == "")
                        {
                            Int32.TryParse(dataGridView1.CurrentRow.Cells["numero_ordem"].Value.ToString(), out numero_ordem);
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

                string query = "PROJETO.update_staff";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
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
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                int retval = (int)cmd.Parameters["@retval"].Value;
                sqlcon.Close();

                if (retval == -1)
                {
                    MessageBox.Show("Profissão não coincide com o membro da staff selecionado!");
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    label8.Visible = false;
                    label9.Visible = false;
                    label10.Visible = false;
                    label11.Visible = false;
                    textBox9.Visible = false;
                    comboBox2.Visible = false;
                    textBox9.Text = "";
                    return;
                }
                else if (retval == 0)
                {
                    MessageBox.Show("Não foi possível alterar!");
                    textBox1.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox9.Text = "";
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    label8.Visible = false;
                    label9.Visible = false;
                    label10.Visible = false;
                    label11.Visible = false;
                    textBox9.Visible = false;
                    comboBox2.Visible = false;
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
                    textBox1.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox9.Text = "";
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                }

                //fazer load do datagrid de novo
                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT PROJETO.STAFF.nif,endereco,contacto,idade,nome FROM PROJETO.STAFF JOIN  PROJETO.PESSOA ON PROJETO.PESSOA.NIF=PROJETO.STAFF.NIF";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
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

