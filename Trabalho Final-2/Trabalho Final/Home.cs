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
        private DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(@"Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p5g4; user=p5g4; password=TiagoLucas2000");
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

        private void btnProcuraConsulta_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            string search = textBox1.Text.Trim();
            var split = search.Split('/');

                //TODO pesquisa dinamica por exemplo */*/2022 devolve todas as consultas de 2022
            if (search == "")
            {
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
                string[] dataspt = search.Split('/');;
                string procura = dataspt[2].ToString() + "-" + dataspt[1].ToString() + "-" + dataspt[0].ToString();
                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT * FROM PROJETO.CONSULTA WHERE dataa=@procura";
                cmd1.Parameters.AddWithValue("procura", procura);
                cmd1.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                dt.Clear();
                da.Fill(dt);
                sqlcon.Close();
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
