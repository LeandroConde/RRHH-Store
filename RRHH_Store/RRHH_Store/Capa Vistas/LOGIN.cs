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
using RRHH_Store.Capa_Datos;
using RRHH_Store.Capa_Negocios;

namespace RRHH_Store
{
    public partial class Login : Form
    {
        AdministradorClass ad = new AdministradorClass();
        public Login()
        {
            InitializeComponent();
        }

        private void picBoxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void picBoxMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void picBoxMaximizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            picBoxMaximizar.Visible = false;
            picBoxRestaurar.Visible = true;
        }

        private void picBoxRestaurar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            picBoxRestaurar.Visible = false;
            picBoxMaximizar.Visible = true;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            IList<Administrador> listaAd = new List<Administrador>();
            listaAd = ad.GetAllAdministrador();
            int bandera = 0;

            foreach (var item in listaAd)
            {
                if (txtUsuario.Text == item.usuario && txtPassword.Text == item.contraseña)
                {
                    MessageBox.Show("Ingreso correcto.");
                    bandera = 1;
                }
            }

            if (bandera == 1)
            {
                this.Hide();
                Form Login = new MenuPrincipal();
                Login.Show();
            }
            else
            {
                MessageBox.Show("Usuario o clave incorrecta.");
            }

 
        }

     
    }
}
