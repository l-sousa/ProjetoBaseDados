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
        private DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(@"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000");

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

        private void Consultas_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            sqlcon.Open();
            SqlCommand cmd1 = sqlcon.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT * FROM PROJETO.CONSULTA";
            cmd1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            dt.Clear();
            da.Fill(dt);
            sqlcon.Close();
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;

            string search = textBox7.Text.Trim();
            var split = search.Split('/');
            //TODO pesquisa dinamica por exemplo */*/2022 devolve todas as consultas de 2022
            if (search == "")
            {
                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from PROJETO.CONSULTA";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
            }
            else
            {
                string[] dataspt = search.Split('/'); ;
                string procura = dataspt[2].ToString() + "-" + dataspt[1].ToString() + "-" + dataspt[0].ToString();
                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from PROJETO.CONSULTA WHERE dataa=@procura";
                cmd1.Parameters.AddWithValue("procura", procura);
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
                textBox7.Text = "";

             }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            double preco;
            dataGridView1.DataSource = dt;
            bool success = Double.TryParse(textBox6.Text.Trim().Replace('.',','),out preco);
            if(preco.ToString() == "0")
            {
                MessageBox.Show("Preço da consulta inválido!");
                return;
            }
            if(preco.ToString().IndexOf(',') != -1) { 
                if ((preco.ToString().Length > 6 || textBox6.Text=="") && (preco.ToString().Substring(preco.ToString().IndexOf(','))).Length > 2) 
                {
                    System.Windows.Forms.MessageBox.Show("O campo \"Preço\" deve ter no máximo tamanho 6!");
                    textBox6.Text = "";
                    return;
                }
            }
            else
            {
                if (preco.ToString().Length > 3 || textBox6.Text == "")
                {
                    System.Windows.Forms.MessageBox.Show("O campo \"Preço\" deve ter no máximo tamanho 5 em que 2 tem de ser decimais!");
                    textBox6.Text = "";
                    return;
                }
            }

            int duracao;
            success = Int32.TryParse(textBox5.Text.Trim(),out duracao);

            if (!success || textBox5.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Insira a duração da consulta!");
                return;
            }

            DateTime data_i = dateTimePicker1.Value;
            String data_inc = data_i.ToString("yyyy-MM-dd");

            int dentista;
            success = Int32.TryParse(textBox3.Text.Trim(), out dentista);

            if (!success)
            {
                MessageBox.Show("O nif do dentista não é válido!");
                return;
            }

            // INSERT 
            string query = "PROJETO.insert_consulta";
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("preco", preco);
            cmd.Parameters.AddWithValue("duracao", duracao);
            cmd.Parameters.AddWithValue("nif_dentista", dentista);
            cmd.Parameters.AddWithValue("dataa", data_inc);
            cmd.Parameters.Add("@retval", SqlDbType.Int);
            cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
            sqlcon.Open();
            cmd.ExecuteNonQuery();
            int retval = (int)cmd.Parameters["@retval"].Value;
            sqlcon.Close();


            if (retval == -1)
            {
                MessageBox.Show("Nif de dentista não existe!");
                textBox3.Text = "";
                return;
            }
            else if (retval == 0)
            {
                MessageBox.Show("Nao foi possível inserir consulta!");
                dateTimePicker1.Value = DateTime.Now;
                textBox3.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                return;
            }
            else
            {
                MessageBox.Show("Adicionada a consulta!");
                dateTimePicker1.Value = DateTime.Now;
                textBox3.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }

            //limpar as text box todas
            dateTimePicker1.Value = DateTime.Now;
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            //fazer load do datagrid de novo
            sqlcon.Open();
            SqlCommand cmd1 = sqlcon.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT * FROM PROJETO.CONSULTA";
            cmd1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            dt.Clear();
            da.Fill(dt);
            sqlcon.Close();
        }


       
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            if (dataGridView1.SelectedCells.Count == 1)
            {
                int duracao_box;
                bool success = Int32.TryParse(textBox5.Text, out duracao_box);
                if (!success && duracao_box.ToString() == "")
                {
                    Int32.TryParse(dataGridView1.CurrentRow.Cells["duracao"].Value.ToString(), out duracao_box);
                }
                else if (duracao_box.ToString().Length >= 100 || duracao_box.ToString().Length < 1)
                {
                    MessageBox.Show("Nome inválido!");
                    textBox5.Text = "";
                    return;
                }

                int codigo;
                Int32.TryParse(dataGridView1.CurrentRow.Cells["codigo"].Value.ToString(), out codigo);

                double box_preco;
                success = Double.TryParse(textBox6.Text, out box_preco);

                if (success && box_preco.ToString().IndexOf(',') != -1)
                {
                    if ((box_preco.ToString().Length > 6 || textBox6.Text == "") && (box_preco.ToString().Substring(box_preco.ToString().IndexOf(','))).Length - 1 > 2)
                    {
                        System.Windows.Forms.MessageBox.Show("O campo \"Preço\" deve ter no máximo tamanho 5 em que 2 tem de ser decimais!");
                        textBox6.Text = "";
                        return;
                    }
                }
                else
                {
                    if (box_preco.ToString().Length > 5 || textBox6.Text == "")
                    {
                        System.Windows.Forms.MessageBox.Show("O campo \"Preço\" deve ter no máximo tamanho 5!");
                        textBox6.Text = "";
                        return;
                    }
                }

                int dentista;
                success = Int32.TryParse(textBox3.Text, out dentista);
                if (!success && textBox3.Text == "")
                {
                    Int32.TryParse(dataGridView1.CurrentRow.Cells["nif_dentista"].Value.ToString(), out dentista);
                }
                DateTime data_i = dateTimePicker1.Value;
                String data_inc = data_i.ToString("yyyy-MM-dd");

                string query = "PROJETO.update_consulta";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codigo", codigo);
                cmd.Parameters.AddWithValue("duracao", duracao_box);
                cmd.Parameters.AddWithValue("preco", box_preco);
                cmd.Parameters.AddWithValue("nif_dentista", dentista);
                cmd.Parameters.AddWithValue("dataa", data_inc);
                cmd.Parameters.Add("@retval", SqlDbType.Int);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                int retval = (int)cmd.Parameters["@retval"].Value;
                sqlcon.Close();

                if (retval == -1)
                {
                    MessageBox.Show("Nif de dentista não existe");
                    dateTimePicker1.Value = DateTime.Now;
                    textBox3.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    return;
                }
                else if(retval == 0)
                {
                    MessageBox.Show("Não foi possível alterar!");
                    dateTimePicker1.Value = DateTime.Now;
                    textBox3.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    return;
                }
                else
                {
                    MessageBox.Show("Paciente alterado!");
                    dateTimePicker1.Value = DateTime.Now;
                    textBox3.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                }

                //Load do datagrid de novo
                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT * FROM PROJETO.CONSULTA";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
            }
            else
            {
                MessageBox.Show("Tem de selecionar uma consulta para ser possível alterá-lo!");
                return;
            }

        }
        // APAGAR
        private void btnApagar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            if (dataGridView1.SelectedCells.Count == 1)
            {

                int codigo;
                bool success = Int32.TryParse(dataGridView1.CurrentRow.Cells["codigo"].Value.ToString(), out codigo);
                if (!success)
                {
                    System.Windows.Forms.MessageBox.Show("Não foi possível obter valor do 'código'!");
                    return;
                }

                string query = "PROJETO.remove_consulta";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codigo_consulta", codigo);
                cmd.Parameters.Add("@retval", SqlDbType.Int);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                int retval = (int)cmd.Parameters["@retval"].Value;
                sqlcon.Close();

                if (retval == 0)
                {
                MessageBox.Show("Não foi possível alterar!");
                dateTimePicker1.Value = DateTime.Now;
                textBox3.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                return;
                }
                else
                {
                MessageBox.Show("Consulta eliminada!");
                dateTimePicker1.Value = DateTime.Now;
                textBox3.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                }

                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT * FROM PROJETO.CONSULTA";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();

            }
            else
            {
                MessageBox.Show("Tem de selecionar uma e apenas uma consulta para ser possível apagá-la!");
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
