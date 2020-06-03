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
using System.Security.Permissions;
using System.Collections;

namespace Trabalho_Final
{
    public partial class MaterialDentário : UserControl
    {
        private DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(@"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000");

        private static MaterialDentário _instance;
        public static MaterialDentário Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MaterialDentário();
                return _instance;
            }
        }
        public MaterialDentário()
        {
            InitializeComponent();
        }

        private void Material_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            sqlcon.Open();
            SqlCommand cmd1 = sqlcon.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT PROJETO.FORNECEDOR.nome,material,PROJETO.FORNECEDOR.endereco,PROJETO.FORNECEDOR.contacto FROM PROJETO.CLINICA join PROJETO.MATERIAL_DENTARIO on PROJETO.MATERIAL_DENTARIO.nif_clinica = PROJETO.CLINICA.nif join PROJETO.FORNECEDOR on nif_fornecedor = PROJETO.FORNECEDOR.nif";
            cmd1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            dt.Clear();
            da.Fill(dt);
            sqlcon.Close();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            string nome = textBox1.Text;
            if(nome=="" || nome.Length > 100)
            {
                System.Windows.Forms.MessageBox.Show("Material Inválido!");
                textBox1.Text = "";
                return;
            }

            string query = "PROJETO.insert_material";
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("material", nome);
            cmd.Parameters.Add("@retval", SqlDbType.Int);
            cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
            sqlcon.Open();
            cmd.ExecuteNonQuery();
            int retval = (int)cmd.Parameters["@retval"].Value;
            sqlcon.Close();

            if (retval == -1)
            {
                MessageBox.Show("Material já existente na clínica!");
                textBox1.Text = "";
                return;
            }
            else if(retval == 0)
            {
                MessageBox.Show("Erro ao adicionar material!");
                textBox1.Text = "";
                return;
            }
            else
            {
                MessageBox.Show("Material adicionado!");
                textBox1.Text = "";
            }

            //mostrar o material atualizado

            sqlcon.Open();
            SqlCommand cmd1 = sqlcon.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT PROJETO.FORNECEDOR.nome,material,PROJETO.FORNECEDOR.endereco,PROJETO.FORNECEDOR.contacto FROM PROJETO.CLINICA join PROJETO.MATERIAL_DENTARIO on PROJETO.MATERIAL_DENTARIO.nif_clinica = PROJETO.CLINICA.nif join PROJETO.FORNECEDOR on nif_fornecedor = PROJETO.FORNECEDOR.nif";
            cmd1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            dt.Clear();
            da.Fill(dt);
            sqlcon.Close();

            //apagar o que esta escrito na caixa de texto

            textBox1.Text = "";


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
                cmd1.CommandText = "SELECT PROJETO.FORNECEDOR.nome,material,PROJETO.FORNECEDOR.endereco,PROJETO.FORNECEDOR.contacto FROM PROJETO.CLINICA join PROJETO.MATERIAL_DENTARIO on PROJETO.MATERIAL_DENTARIO.nif_clinica = PROJETO.CLINICA.nif join PROJETO.FORNECEDOR on nif_fornecedor = PROJETO.FORNECEDOR.nif";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
                textBox2.Text = "";
            }
            else
            {
                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT PROJETO.FORNECEDOR.nome,material,PROJETO.FORNECEDOR.endereco,PROJETO.FORNECEDOR.contacto FROM PROJETO.CLINICA join PROJETO.MATERIAL_DENTARIO on PROJETO.MATERIAL_DENTARIO.nif_clinica = PROJETO.CLINICA.nif join PROJETO.FORNECEDOR on nif_fornecedor = PROJETO.FORNECEDOR.nif WHERE PROJETO.MATERIAL_DENTARIO.material LIKE @search";
                SqlParameter p = new SqlParameter("@search", SqlDbType.Char, 255)
                {
                    Value = "%" + search + "%"
                };
                cmd1.Parameters.AddWithValue(p.ParameterName, p.Value);
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
                textBox2.Text = "";
            }

        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            if (dataGridView1.SelectedCells.Count == 1)
            {

                string material = dataGridView1.CurrentRow.Cells["material"].Value.ToString();

                string query = "PROJETO.remove_material";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("material", material);
                cmd.Parameters.Add("@retval", SqlDbType.Int);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                int retval = (int)cmd.Parameters["@retval"].Value;
                sqlcon.Close();

                if (retval == 0)
                {
                    MessageBox.Show("Erro ao adicionar material!");
                    textBox1.Text = "";
                    return;
                }
                else
                {
                    MessageBox.Show("Material removido com sucesso!");
                }

                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT PROJETO.FORNECEDOR.nome,material,PROJETO.FORNECEDOR.endereco,PROJETO.FORNECEDOR.contacto FROM PROJETO.CLINICA join PROJETO.MATERIAL_DENTARIO on PROJETO.MATERIAL_DENTARIO.nif_clinica = PROJETO.CLINICA.nif join PROJETO.FORNECEDOR on nif_fornecedor = PROJETO.FORNECEDOR.nif";
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
                textBox2.Text = "";


            }
            else
            {
                MessageBox.Show("Tem de selecionar um e apenas um material para ser possível removê-lo!");
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

