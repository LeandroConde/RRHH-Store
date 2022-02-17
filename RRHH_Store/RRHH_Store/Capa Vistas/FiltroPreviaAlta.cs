using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RRHH_Store.Capa_Datos;
using RRHH_Store.Capa_Negocios;


namespace RRHH_Store.Capa_Vistas
{
    public partial class FiltroPreviaAlta : Form
    {
        #region INSTANCIACIONES
        PersonaClass personaAux = new PersonaClass();
        PostulanteClass postulanteAux = new PostulanteClass();
        PerfilClass perfilAux = new PerfilClass();
        PerfilAcademicoClass academAux = new PerfilAcademicoClass();
        PerfilProfesionalClass profesionalAux = new PerfilProfesionalClass();
        PerfilPsicologicoClass psicologicoAux = new PerfilPsicologicoClass();
        #endregion
        public FiltroPreviaAlta()
        {
            InitializeComponent();
        }


        #region BOTONES DE LA VISTA

        #region ICONOS

        private void pictureBoxRestaurar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            picBoxRestaurar.Visible = false;
            picBoxMaximizar.Visible = true;
        }

        private void pictureBoxMaximizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            picBoxMaximizar.Visible = false;
            picBoxRestaurar.Visible = true;
        }
        private void minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBoxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        private void btnVolverFiltroPreviaAlta_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form FiltroPreviaAlta = new MenuPrincipal();
            FiltroPreviaAlta.Show();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form vistaAlta = new Alta();
            vistaAlta.Show();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form actualizar = new Consulta();
            actualizar.Show();
        }

        #endregion


        private void FiltroPreviaAlta_Load(object sender, EventArgs e)
        {

            #region DISEÑO DE GRILLA
            dtgvFiltro.SelectAll();
            dtgvFiltro.ClearSelection();
            dtgvFiltro.Columns.Add("nombrePostulante", "Nombre");
            dtgvFiltro.Columns.Add("Dni", "Dni");
            dtgvFiltro.Columns.Add("NivelEducativo", "Nivel Educativo Alcanzado");
            dtgvFiltro.Columns.Add("Experiencia", "Empresa para la que trabajó");
            dtgvFiltro.Columns.Add("apto", "Descripción del Postulante");
            dtgvFiltro.Columns.Add("Disciplina", "Disciplina");
            #endregion


            IList<Persona> listar = personaAux.GetAllPersonas();
            foreach (Persona item in listar)
            {
                Postulante postCons = new Postulante();
                postCons = postulanteAux.GetPostulanteById(item);
                if (postCons.activo == true)
                {
                    Perfil perf = new Perfil();
                    perf = perfilAux.GetPerfilById(postCons);
                    Disciplina disc = new Disciplina();
                    disc = perfilAux.GetDisciplinaById(perf);

                    PerfilAcademico academ = new PerfilAcademico();
                    academ = academAux.GetPerfilAcademicoById(perf);
                    NivelEducativo nivel = new NivelEducativo();
                    nivel = academAux.GetNivelAcademicoById(academ);

                    PerfilProfesional profesion = new PerfilProfesional();
                    profesion = profesionalAux.GetPerfilProfesionalById(perf);

                    PerfilPsicologico psico = new PerfilPsicologico();
                    psico = psicologicoAux.GetPerfilPsicologicoById(perf);


                    //Ingreso de datos de Persona en una línea en la grilla.
                    dtgvFiltro.Rows.Add(item.nombre + " " + item.apellido, item.dni, nivel.NombreNivel, profesion.Lugar_Empresa, psico.descripcion, disc.nombreDisciplina);

                }
                //Ajustar tamaño de columnas y celdas.
                dtgvFiltro.AutoResizeColumns();
                dtgvFiltro.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
           
            dtgvFiltro.DataSource = null;
            dtgvFiltro.Rows.Clear();

            Persona perCons = new Persona();

            perCons = personaAux.BuscarPersona(txtDNIFiltro);
           
                Postulante postCons = new Postulante();
                postCons = postulanteAux.GetPostulanteById(perCons);
                if (postCons.activo == true)
                {
                Perfil perf = new Perfil();
                perf = perfilAux.GetPerfilById(postCons);
                Disciplina disc = new Disciplina();
                disc = perfilAux.GetDisciplinaById(perf);
                PerfilAcademico academ = new PerfilAcademico();
                academ = academAux.GetPerfilAcademicoById(perf);
                NivelEducativo nivel = new NivelEducativo();
                nivel = academAux.GetNivelAcademicoById(academ);

                PerfilProfesional profesion = new PerfilProfesional();
                profesion = profesionalAux.GetPerfilProfesionalById(perf);

                PerfilPsicologico psico = new PerfilPsicologico();
                psico = psicologicoAux.GetPerfilPsicologicoById(perf);


                dtgvFiltro.Rows.Add(postCons.Persona.nombre + " " + postCons.Persona.apellido, postCons.Persona.dni, nivel.NombreNivel, profesion.Lugar_Empresa, psico.descripcion, disc.nombreDisciplina);
                }
                else
                {
                    MessageBox.Show("Postulante no registrado");
                    txtDNIFiltro.Text = "";
                }
            
           
                dtgvFiltro.AutoResizeColumns();
                dtgvFiltro.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            
        }
    }
}
