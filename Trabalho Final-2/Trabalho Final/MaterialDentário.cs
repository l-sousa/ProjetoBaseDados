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

        private List<string> materiais = new List<string>();

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
            const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";
            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {

                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD
                SqlDataAdapter da = new SqlDataAdapter("SELECT material FROM PROJETO.MATERIAL_DENTARIO", SqlConn);
                //--- Converte resultado para uma Tabela
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    materiais.Add(dr.Field<string>("material"));
                }
                //--- Preenche o DataGrid com a Tabela
                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }

        }

        private void ShowMaterial()
        {

            const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";
            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {

                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD
                SqlDataAdapter da = new SqlDataAdapter("SELECT PROJETO.FORNECEDOR.nome,material,PROJETO.FORNECEDOR.endereco,PROJETO.FORNECEDOR.contacto FROM PROJETO.CLINICA join PROJETO.MATERIAL_DENTARIO on PROJETO.MATERIAL_DENTARIO.nif_clinica = PROJETO.CLINICA.nif join PROJETO.FORNECEDOR on nif_fornecedor = PROJETO.FORNECEDOR.nif", SqlConn);
                //--- Converte resultado para uma Tabela
                DataTable dt = new DataTable();
                da.Fill(dt);
                //--- Preenche o DataGrid com a Tabela
                dataGridView1.DataSource = dt;
                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }
        }

        private void Fornecedor_Load(object sender, EventArgs e)
        {
            ShowMaterial();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            int nif_clinica;
            string nome = textBox1.Text;
            if(nome=="" || nome.Length > 100)
            {
                System.Windows.Forms.MessageBox.Show("Material Inválido!");
                textBox1.Text = "";
                return;
            }

            if (materiais.Contains(nome))
            {
                System.Windows.Forms.MessageBox.Show("Material já existe!");
                textBox1.Text = "";
                return;
            }


            const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";
            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {

                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD
                SqlDataAdapter da = new SqlDataAdapter("SELECT nif from PROJETO.CLINICA", SqlConn);

                DataTable ds = new DataTable();

                da.Fill(ds);

                nif_clinica = Convert.ToInt32(ds.Rows[0]["nif"]);

                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }
            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {

                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD


                using (SqlCommand cmd = new SqlCommand("INSERT INTO PROJETO.MATERIAL_DENTARIO(nif_clinica, material) VALUES (" + nif_clinica + ",\'" + nome + "\')", SqlConn))
                {
                    int rowsAdded = cmd.ExecuteNonQuery();
                    if (rowsAdded > 0)
                        System.Windows.Forms.MessageBox.Show("Material adicionado!");
                    else
                        System.Windows.Forms.MessageBox.Show("Nao foi possível adicionar novo !");

                }


                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }

            //mostrar o material atualizado

            ShowMaterial();

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

