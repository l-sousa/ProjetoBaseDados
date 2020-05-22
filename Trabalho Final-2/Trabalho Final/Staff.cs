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

namespace Trabalho_Final
{
    
    public partial class Staff : UserControl
    {
        private static Staff _instance;

        private Hashtable nifs_staff = getnifs();

        public static Hashtable getnifs()
        {
            const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";
            try

            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string cmdst = "SELECT nif,especialidade FROM PROJETO.DENTISTA";

                SqlDataAdapter adap = new SqlDataAdapter(cmdst, con);

                DataTable ds = new DataTable();

                adap.Fill(ds);



                Hashtable nifs_dentista = new Hashtable();

                foreach (DataRow dr in ds.Rows)
                {
                    nifs_dentista.Add(dr.Field<int>("nif"), dr.Field<string>("especialidade"));

                }

                if (con.State == ConnectionState.Open)
                    con.Close();

                return nifs_dentista;
            }

            catch (Exception pp)

            {
                System.Windows.Forms.MessageBox.Show("Imposssível conectar!");
                return null;
            }
        }
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

            // VALIDAÇÃO DAS CAIXAS DE TEXTO
            string nome_box = textBox1.Text;

            int box_contacto;
            bool sucess = Int32.TryParse(textBox6.Text, out box_contacto);
            if(!sucess)
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
            if (!sucess || nifs_staff.ContainsKey(box_nif))
            {
                MessageBox.Show("Nif inválido");
                textBox4.Text = "";
                return;
            }

            string box_endereco = textBox3.Text;

            double box_salario;
            sucess = Double.TryParse(textBox7.Text, out box_salario);
            if (!sucess)
            {
                MessageBox.Show("Nif inválido");
                textBox7.Text = "";
                return;
            }

            // CRIAR CÓDIGO NECESSÁRIO PARA O INSERT
            /*vamos precisar de fazer dois inserts, na tabela de STAFF e na tabela de PESSOA    */

            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {
                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD

                // INSERT 

                string staff_query = "INSERT INTO PROJETO.STAFF(nif, salario, nif_clinica) VALUES (" + nif_clinica + ",\'" + nome + "\')";
                using (SqlCommand cmd = new SqlCommand(, SqlConn))
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

