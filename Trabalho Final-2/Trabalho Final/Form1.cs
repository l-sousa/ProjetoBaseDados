using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabalho_Final
{
    public partial class Form1 : Form //MetroFramework.Forms.MetroForm
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (!panel1.Controls.Contains(Consultas.Instance))
            {
                panel1.Controls.Add(Consultas.Instance);
                Consultas.Instance.Dock = DockStyle.Fill;
                Consultas.Instance.BringToFront();

            }
            else
            {
                Consultas.Instance.BringToFront();
            }

            if (!panel1.Controls.Contains(Home.Instance))
            {
                panel1.Controls.Add(Home.Instance);
                Home.Instance.Dock = DockStyle.Fill;
                Home.Instance.BringToFront();

            }
            else
            {
                Home.Instance.BringToFront();
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (!panel1.Controls.Contains(Home.Instance))
            {
                panel1.Controls.Add(Home.Instance);
                Home.Instance.Dock = DockStyle.Fill;
                Home.Instance.BringToFront();

            }
            else
            {
                Home.Instance.BringToFront();
            }
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            if (!panel1.Controls.Contains(Consultas.Instance))
            {
                panel1.Controls.Add(Consultas.Instance);
                Consultas.Instance.Dock = DockStyle.Fill;
                Consultas.Instance.BringToFront();

            }
            else
            {
                Consultas.Instance.BringToFront();
            }
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            if (!panel1.Controls.Contains(Pacientes.Instance))
            {
                panel1.Controls.Add(Pacientes.Instance);
                Pacientes.Instance.Dock = DockStyle.Fill;
                Pacientes.Instance.BringToFront();

            }
            else
            {
                Pacientes.Instance.BringToFront();
            }
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            if (!panel1.Controls.Contains(Staff.Instance))
            {
                panel1.Controls.Add(Staff.Instance);
                Staff.Instance.Dock = DockStyle.Fill;
                Staff.Instance.BringToFront();

            }
            else
            {
                Staff.Instance.BringToFront();
            }
        }

        private void btnFornecedor_Click(object sender, EventArgs e)
        {
            if (!panel1.Controls.Contains(MaterialDentário.Instance))
            {
                panel1.Controls.Add(MaterialDentário.Instance);
                MaterialDentário.Instance.Dock = DockStyle.Fill;
                MaterialDentário.Instance.BringToFront();

            }
            else
            {
                MaterialDentário.Instance.BringToFront();
            }
        }

        private void Pagamento_Click(object sender, EventArgs e)
        {
            if (!panel1.Controls.Contains(Pagamento.Instance))
            {
                panel1.Controls.Add(Pagamento.Instance);
                Pagamento.Instance.Dock = DockStyle.Fill;
                Pagamento.Instance.BringToFront();

            }
            else
            {
                Pagamento.Instance.BringToFront();
            }
        }

    }
}
