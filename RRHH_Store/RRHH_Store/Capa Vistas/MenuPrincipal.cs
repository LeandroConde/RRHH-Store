using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RRHH_Store.Capa_Vistas;

namespace RRHH_Store.Capa_Vistas
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form filtro = new FiltroPreviaAlta();
            filtro.Show();
        }


        private void picBoxMaximizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            picBoxMaximizar.Visible = false;
            picBoxRestaurar.Visible = true;
        }

        private void picBoxMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void picBoxRestaurar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            picBoxRestaurar.Visible = false;
            picBoxMaximizar.Visible = true;
        }

        private void picBoxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form consulta = new Consulta();
            consulta.Show();
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form pdf = new ConsultaCV_Certificado();
            pdf.Show();
        }


        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form registrarse = new Login();
            registrarse.Show();
        }
    }
}
