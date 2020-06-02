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
        const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";

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
            Material();
        }

        private void Material()
        {

            ShowMaterial("SELECT PROJETO.FORNECEDOR.nome,material,PROJETO.FORNECEDOR.endereco,PROJETO.FORNECEDOR.contacto FROM PROJETO.CLINICA join PROJETO.MATERIAL_DENTARIO on PROJETO.MATERIAL_DENTARIO.nif_clinica = PROJETO.CLINICA.nif join PROJETO.FORNECEDOR on nif_fornecedor = PROJETO.FORNECEDOR.nif");

        }

        private void ShowMaterial(string query)
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

        private void Material_Load(object sender, EventArgs e)
        {
            ShowMaterial("SELECT PROJETO.FORNECEDOR.nome,material,PROJETO.FORNECEDOR.endereco,PROJETO.FORNECEDOR.contacto FROM PROJETO.CLINICA join PROJETO.MATERIAL_DENTARIO on PROJETO.MATERIAL_DENTARIO.nif_clinica = PROJETO.CLINICA.nif join PROJETO.FORNECEDOR on nif_fornecedor = PROJETO.FORNECEDOR.nif");
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            string nome = textBox1.Text;
            if(nome=="" || nome.Length > 100)
            {
                System.Windows.Forms.MessageBox.Show("Material Inválido!");
                textBox1.Text = "";
                return;
            }


            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {
                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD

                SqlConnection conn = new SqlConnection(connectionString);
                string query = "PROJETO.insert_material";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("material", nome);
                cmd.Parameters.Add("@retval", SqlDbType.Int);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                int retval = (int)cmd.Parameters["@retval"].Value;
                conn.Close();

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

                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }

            //mostrar o material atualizado

            ShowMaterial("SELECT PROJETO.FORNECEDOR.nome,material,PROJETO.FORNECEDOR.endereco,PROJETO.FORNECEDOR.contacto FROM PROJETO.CLINICA join PROJETO.MATERIAL_DENTARIO on PROJETO.MATERIAL_DENTARIO.nif_clinica = PROJETO.CLINICA.nif join PROJETO.FORNECEDOR on nif_fornecedor = PROJETO.FORNECEDOR.nif");

            //apagar o que esta escrito na caixa de texto

            textBox1.Text = "";


        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";

            string search = textBox2.Text.Trim();
            string cmdst;

            //TODO pesquisa dinamica por exemplo */*/2022 devolve todas as consultas de 2022
            if (search == "")
            {
                cmdst = "SELECT PROJETO.FORNECEDOR.nome,material,PROJETO.FORNECEDOR.endereco,PROJETO.FORNECEDOR.contacto FROM PROJETO.CLINICA join PROJETO.MATERIAL_DENTARIO on PROJETO.MATERIAL_DENTARIO.nif_clinica = PROJETO.CLINICA.nif join PROJETO.FORNECEDOR on nif_fornecedor = PROJETO.FORNECEDOR.nif";
                textBox2.Text = "";
            }
            else
            {
                cmdst = "SELECT PROJETO.FORNECEDOR.nome,material,PROJETO.FORNECEDOR.endereco,PROJETO.FORNECEDOR.contacto FROM PROJETO.CLINICA join PROJETO.MATERIAL_DENTARIO on PROJETO.MATERIAL_DENTARIO.nif_clinica = PROJETO.CLINICA.nif join PROJETO.FORNECEDOR on nif_fornecedor = PROJETO.FORNECEDOR.nif WHERE PROJETO.MATERIAL_DENTARIO.material LIKE \'%" + search + "%\'";
                textBox2.Text = "";
            }

            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {

                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD
                SqlDataAdapter da = new SqlDataAdapter(cmdst, SqlConn);
                //--- Converte resultado para uma Tabela
                DataTable dt = new DataTable();
                da.Fill(dt);
                //--- Preenche o DataGrid com a Tabela
                dataGridView1.DataSource = dt;
                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }

        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 1)
            {
                using (SqlConnection SqlConn = new SqlConnection(connectionString))
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    //--- Pesquisa na BD

                    string material = dataGridView1.CurrentRow.Cells["material"].Value.ToString();

                    SqlConnection conn = new SqlConnection(connectionString);
                    string query = "PROJETO.remove_material";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("material", material);
                    cmd.Parameters.Add("@retval", SqlDbType.Int);
                    cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    int retval = (int)cmd.Parameters["@retval"].Value;
                    conn.Close();
                    /*
                    if (retval == -1)
                    {
                        MessageBox.Show("Material já existente na clínica!");
                        textBox1.Text = "";
                        return;
                    }
                    else if (retval == 0)
                    {
                        MessageBox.Show("Erro ao adicionar material!");
                        textBox1.Text = "";
                        return;
                    }
                    */

                    if (SqlConn.State == ConnectionState.Open)
                        SqlConn.Close();
                }


                ShowMaterial("SELECT PROJETO.FORNECEDOR.nome,material,PROJETO.FORNECEDOR.endereco,PROJETO.FORNECEDOR.contacto FROM PROJETO.CLINICA join PROJETO.MATERIAL_DENTARIO on PROJETO.MATERIAL_DENTARIO.nif_clinica = PROJETO.CLINICA.nif join PROJETO.FORNECEDOR on nif_fornecedor = PROJETO.FORNECEDOR.nif");

            }
            else
            {
                MessageBox.Show("Tem de selecionar um paciente para ser possível removê-lo!");
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

