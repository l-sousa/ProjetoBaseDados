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
    public partial class Pagamento : UserControl
    {
        const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";
        private static Pagamento _instance;
        public static Pagamento Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Pagamento();
                return _instance;
            }
        }
        public Pagamento()
        {
            InitializeComponent();
        }

        private void Pagamento_Load(object sender, EventArgs e)
        {
            showPagamentos("SELECT * FROM PROJETO.PAGA");
        }


        private void showPagamentos(string query)
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

        private void button1_Click(object sender, EventArgs e)
        {

            // VALIDAÇÃO DAS CAIXAS DE TEXTO
            int paciente_box; 
            Int32.TryParse(textBox2.Text,out paciente_box);

            int rececionista_box;
            Int32.TryParse(textBox3.Text, out rececionista_box);

            int consulta_box;
            Int32.TryParse(textBox4.Text, out consulta_box);

            int checkdentista_box;
            Int32.TryParse(textBox5.Text, out checkdentista_box);



            //Inserção de Paciente
            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {
                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD

                // INSERT 

                SqlConnection conn = new SqlConnection(connectionString);
                string query = "PROJETO.insert_pagamento";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nif_paciente", paciente_box);
                cmd.Parameters.AddWithValue("nif_rececionista", rececionista_box);
                cmd.Parameters.AddWithValue("codigo_consulta", consulta_box);
                cmd.Parameters.AddWithValue("codigo_check", checkdentista_box);
                cmd.Parameters.Add("@retval", SqlDbType.Int);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                int retval = (int)cmd.Parameters["@retval"].Value;
                conn.Close();


                if (retval == -1)
                {
                    MessageBox.Show("Nif do paciente não existe!");
                    textBox2.Text = "";
                    return;
                }
                else if(retval == -2)
                {
                    MessageBox.Show("Nif do rececionista não existe!");
                    textBox3.Text = "";
                    return;
                }
                else if(retval == -3)
                {
                    MessageBox.Show("Consulta não existe!");
                    textBox4.Text = "";
                    return;
                }
                else if (retval == -4)
                {
                    MessageBox.Show("Cheque dentista não existe!");
                    textBox5.Text = "";
                    return;
                }
                else if(retval == -5)
                {
                    MessageBox.Show("Consulta já foi paga!");
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    return;
                }
                else if(retval == -6)
                {
                    MessageBox.Show("Cheque dentista já foi utilizado!");
                    textBox5.Text = "";
                    return;
                }
                else if (retval == 0)
                {
                    MessageBox.Show("Nao foi possível inserir paciente!");
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    return;
                }
                else
                {
                    MessageBox.Show("Adicionado!");
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox4.Text = "";
                }

                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }

            //limpar as text box todas
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            //fazer load do datagrid de novo
            showPagamentos("SELECT * FROM PROJETO.PAGA");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                using (SqlConnection SqlConn = new SqlConnection(connectionString))
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    //--- Pesquisa na BD

                    int paciente;
                    bool success = Int32.TryParse(dataGridView1.SelectedRows[0].Cells["nif_paciente"].Value.ToString(), out paciente);
                    if (!success)
                    {
                        System.Windows.Forms.MessageBox.Show("Não foi possível obter valor do 'nif' do paciente!");
                        return;
                    }

                    int rececionista;
                    success = Int32.TryParse(dataGridView1.SelectedRows[0].Cells["nif_rececionista"].Value.ToString(), out rececionista);
                    if (!success)
                    {
                        System.Windows.Forms.MessageBox.Show("Não foi possível obter valor do 'nif' do rececionista!");
                        return;
                    }

                    int codigo;
                    success = Int32.TryParse(dataGridView1.SelectedRows[0].Cells["codigo_consulta"].Value.ToString(), out codigo);
                    if (!success)
                    {
                        System.Windows.Forms.MessageBox.Show("Não foi possível obter o código da consulta!");
                        return;
                    }

                    int cheque;
                    success = Int32.TryParse(dataGridView1.SelectedRows[0].Cells["codigo_check"].Value.ToString(), out cheque);
                    if (!success)
                    {
                        System.Windows.Forms.MessageBox.Show("Não foi possível obter o código do cheque dentista!");
                        return;
                    }

                    SqlConnection conn = new SqlConnection(connectionString);
                    string query = "PROJETO.remove_pagamento";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("codigo_consulta", codigo);
                    cmd.Parameters.Add("@retval", SqlDbType.Int);
                    cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    int retval = (int)cmd.Parameters["@retval"].Value;
                    conn.Close();

                    if (retval == 0)
                    {
                        MessageBox.Show("Não foi possível remover!");
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Pagamento removido!");
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }

                    if (SqlConn.State == ConnectionState.Open)
                        SqlConn.Close();
                }

            }
            else
            {
                MessageBox.Show("Tem de selecionar um pagamento para ser possível removê-lo!");
                return;
            }
        }

        private void Procurar_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text.Trim();

            //TODO pesquisa dinamica por exemplo */*/2022 devolve todas as consultas de 2022
            if (search == "")
            {
                showPagamentos("SELECT * FROM PROJETO.PAGA");
                return;
            }
            else
            {

                showPagamentos("SELECT * FROM PROJETO.PAGA WHERE codigo_consulta=" + search + ";");
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
