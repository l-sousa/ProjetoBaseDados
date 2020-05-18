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
using System.Text.RegularExpressions;

namespace Trabalho_Final
{
    public partial class Home : UserControl
    {
        private static Home _instance;
        public static Home Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Home();
                return _instance;
            }
        }
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";

            using (SqlConnection SqlConn = new SqlConnection(connectionString))
            {
              
                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                //--- Pesquisa na BD
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PROJETO.CONSULTA", SqlConn);
                //--- Converte resultado para uma Tabela
                DataTable dt = new DataTable();
                da.Fill(dt);
                //--- Preenche o DataGrid com a Tabela
                dataGridView1.DataSource = dt;
                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }
        }

        private void btnProcuraConsulta_Click(object sender, EventArgs e)
        {
            const string connectionString = @"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000";
            try

            {
                SqlConnection con = new SqlConnection(connectionString);
                if(con.State == ConnectionState.Closed)
                    con.Open();


                
                string search = textBox1.Text.Trim();
                var split = search.Split('/');
                string cmdst;

                //TODO pesquisa dinamica por exemplo */*/2022 devolve todas as consultas de 2022
                if (search=="")
                    cmdst = "select * from PROJETO.CONSULTA ";
                else
                {

                    string[] dataspt = search.Split('/');;
                    string procura = dataspt[2].ToString() + "-" + dataspt[1].ToString() + "-" + dataspt[0].ToString();
                    cmdst = "select * from PROJETO.CONSULTA where  dataa='" + procura + "'";
                }
                    

                SqlDataAdapter adap = new SqlDataAdapter(cmdst, con);

                DataTable ds = new DataTable();


                adap.Fill(ds);

                dataGridView1.DataSource = ds;

                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            //TODO verificação
            catch (Exception pp)

            {
                System.Windows.Forms.MessageBox.Show("Unable to connect!");
            }
        }

        private void AdiConsulta_Click(object sender, EventArgs e)
        {

            Consultas.Instance.BringToFront();

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
