using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RRHH_Store.Capa_Negocios;
using RRHH_Store.Capa_Datos;
using System.IO;

namespace RRHH_Store.Capa_Vistas
{
    public partial class Consulta : Form
    {
        #region Intanciaciones
        PersonaClass perAux = new PersonaClass();
        PostulanteClass postAux = new PostulanteClass();
        PerfilClass perfilAux = new PerfilClass();
        PerfilAcademicoClass perfilAcadAux = new PerfilAcademicoClass();
        PerfilProfesionalClass perfilProfAux = new PerfilProfesionalClass();
        PerfilPsicologicoClass perfilPsicoAux = new PerfilPsicologicoClass();
        IList<Postulante> ListPostEncontrados = new List<Postulante>();

        #endregion
        public Consulta()
        {
            InitializeComponent();
        }

        #region BOTONES DE LA VISTA
        #region ICONOS
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

        private void picBoxMaximizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            picBoxMaximizar.Visible = false;
            picBoxRestaurar.Visible = true;
        }

        private void picBoxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion
        private void btnVolverVistaMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form Consulta = new MenuPrincipal();
            Consulta.Show();
        }
        #endregion
       
        private void Consulta_Load(object sender, EventArgs e)
        {
            IList<Persona> listar = new List<Persona>();
            DeshabilitarTXT();

            cmbBusqDisciplina.DataSource = perfilAux.GetDisciplina();
            cmbBusqDisciplina.DisplayMember = "nombreDisciplina";
            cmbBusqDisciplina.Text = "Seleccione una opción";

            cmbBusqTipoPost.DataSource = perfilAux.GetTipoPostulante();
            cmbBusqTipoPost.DisplayMember = "nombreTipoPostulante";
            cmbBusqTipoPost.Text = "Seleccione una opción";

            cmbBusqEstadoPost.DataSource = perfilAux.GetEstadoPostulante();
            cmbBusqEstadoPost.DisplayMember = "nombreEstadoPostulante";
            cmbBusqEstadoPost.Text = "Seleccione una opción";

            #region Diseño de Grilla
            GrillaConsulta.SelectAll();
            GrillaConsulta.ClearSelection();
            GrillaConsulta.Columns.Add("id", "Id");
            GrillaConsulta.Columns.Add("nombrePostulante", "Nombre");
            GrillaConsulta.Columns.Add("apellidoPostulante", "Apellido");
            GrillaConsulta.Columns.Add("Dni", "Dni");
            GrillaConsulta.Columns.Add("Disponibilidad", "Disponibilidad");
            GrillaConsulta.Columns.Add("estadoCivil", "Estado Civil");

            GrillaConsulta.Columns.Add("tipoPostulante", "Tipo Postulante");
            GrillaConsulta.Columns.Add("estadoPostulante", "Estado Postulante");
            GrillaConsulta.Columns.Add("Disciplina", "Disciplina");
            #endregion

            listar = perAux.GetAllPersonas();
            foreach (Persona item in listar)
            {
                EstadoCivil est = new EstadoCivil();
                est = perAux.GetEstadoCivilById(item);
                Disponibilidad disp = new Disponibilidad();
                disp = perAux.GetDisponibilidadById(item);
                Postulante postCons = new Postulante();
                postCons = postAux.GetPostulanteById(item);
                if (postCons.activo == true)
                {
                    Perfil perf = new Perfil();
                    perf = perfilAux.GetPerfilById(postCons);
                    TipoPostulante tipoPost = new TipoPostulante();
                    tipoPost = perfilAux.GetTipoPostulanteById(perf);
                    EstadoPostulante estadoPost = new EstadoPostulante();
                    estadoPost = perfilAux.GetEstadoPostulanteById(perf);
                    Disciplina disc = new Disciplina();
                    disc = perfilAux.GetDisciplinaById(perf);
                    //Ingreso de datos de Persona en una línea en la grilla.

                    GrillaConsulta.Rows.Add(item.idPersona, item.nombre, item.apellido, item.dni, disp.estadoDisponibilidad, est.nombreEstadoCivil, tipoPost.nombreTipoPostulante, estadoPost.nombreEstadoPostulante, disc.nombreDisciplina);
                }
            }
            //Ajustar tamaño de columnas y celdas.
            GrillaConsulta.AutoResizeColumns();
            GrillaConsulta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

      
        private void btnOk_Click(object sender, EventArgs e)
        {
            #region INSTANCIACIÓN DE VARIABLES
            Persona perCons = new Persona();
            Postulante postCons = new Postulante();
            Perfil perfilCons = new Perfil();
            PerfilAcademico perfilAcadCons = new PerfilAcademico();
            PerfilProfesional perfilProfeCons = new PerfilProfesional();
            PerfilPsicologico perfilPsicoCons = new PerfilPsicologico();
            #endregion


            #region Datos Personales
            perCons = perAux.BuscarPersona(txtConsultaDni);
            postCons = postAux.GetPostulanteById(perCons);
            if (postCons.activo==true)
            {
                EstadoCivil est = new EstadoCivil();
                est = perAux.GetEstadoCivilById(perCons);
                Disponibilidad disp = new Disponibilidad();
                disp = perAux.GetDisponibilidadById(perCons);
                Sexo sex = new Sexo();
                sex = perAux.GetSexoById(perCons);

                txtNombre.Text = perCons.nombre;
                txtApellido.Text = perCons.apellido;
                txtAlternativo.Text = perCons.apellido;
                txtDNI.Text = perCons.dni;
                txtFecNac.Text = perCons.fechaNacimiento.ToShortDateString().ToString();
                txtNacionalidad.Text = perCons.nacionalidad;
                txtDomicilio.Text = perCons.domicilio;
                cmbActEstadoCivil.Text = est.nombreEstadoCivil;
                if (perCons.hijos == true)
                {
                    checkHijos.CheckState = CheckState.Checked;
                    txtCantHijos.Text = (perCons.cantHijos).ToString();
                }
                else
                {
                    checkHijos.CheckState = CheckState.Unchecked;
                }
                cmbActSexo.Text = sex.nombreSexo;
                cmbActDisponibilidad.Text = disp.estadoDisponibilidad;
                txtCelular.Text = perCons.telefono;
                txtFijo.Text = perCons.telFijo;
                txtAlternativo.Text = perCons.telAlternativo;
                txtmail.Text = perCons.mail;


                #region MOSTRAR FOTO

                try
                {
                    if (GrillaConsulta.Rows.Count > 0)
                    {
                        int id = int.Parse(GrillaConsulta.Rows[GrillaConsulta.CurrentRow.Index].Cells[0].Value.ToString());
                        using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                        {
                            PersonaClass objFoto = new PersonaClass();
                            var mostrarImagen = objFoto.GetPersonaById(postCons);
                            MemoryStream ms = new MemoryStream(mostrarImagen.foto);
                            Bitmap bmp = new Bitmap(ms);
                            PictureFotoPerfil.Image = Image.FromStream(ms);
                            PictureFotoPerfil.SizeMode = PictureBoxSizeMode.StretchImage;

                        }
                    }

                }
                catch (Exception ex)
                {
                    string error = ex.Message;

                    PictureFotoPerfil.Image = null;
                }

                #endregion


                #endregion


                #region Perfil General
                //Con la persona, busco el id de postulante, para luego encontrar el id de Perfil

                perfilCons = perfilAux.GetPerfilById(postCons);

                EstadoPostulante estPost = new EstadoPostulante();
                TipoPostulante tipoPost = new TipoPostulante();
                Disciplina discipPost = new Disciplina();

                discipPost = perfilAux.GetDisciplinaById(perfilCons);
                cmbActDisciplina.Text = discipPost.nombreDisciplina;
                tipoPost = perfilAux.GetTipoPostulanteById(perfilCons);
                cmbActTipoPost.Text = tipoPost.nombreTipoPostulante;
                estPost = perfilAux.GetEstadoPostulanteById(perfilCons);
                cmbActEstadoPost.Text = estPost.nombreEstadoPostulante;
                txtPuntaje.Text = (perfilCons.PuntuaciónTotal).ToString();

                #endregion


                #region Perfil Academico

                NivelEducativo nivelCons = new NivelEducativo();
                EstadoNivelEduc estNivelCons = new EstadoNivelEduc();

                perfilAcadCons = perfilAcadAux.GetPerfilAcademicoById(perfilCons);
                nivelCons = perfilAcadAux.GetNivelAcademicoById(perfilAcadCons);
                cmbActNivel.Text = nivelCons.NombreNivel;
                estNivelCons = perfilAcadAux.GetEstadoNivelEducById(perfilAcadCons);
                cmbActEstadoNivel.Text = estNivelCons.nombreEstadoNivelEduc;
                txtTitulo.Text = perfilAcadCons.Titulo;
                txtEstablecimiento.Text = perfilAcadCons.Establecimiento;

                #endregion

                #region Perfil Psicologico y Profesional

                perfilPsicoCons = perfilPsicoAux.GetPerfilPsicologicoById(perfilCons);
                if (perfilPsicoCons.apto == true)
                {
                    checkApto.CheckState = CheckState.Checked;
                }
                else
                {
                    checkApto.CheckState = CheckState.Unchecked;
                }
                txtDescripcion.Text = perfilPsicoCons.descripcion;


                perfilProfeCons = perfilProfAux.GetPerfilProfesionalById(perfilCons);
                if (perfilProfeCons.Experiencia == true)
                {
                    checkExperienciaLaboral.CheckState = CheckState.Checked;
                }
                else
                {
                    checkExperienciaLaboral.CheckState = CheckState.Unchecked;
                }

                LinkedInCons.Text = perfilProfeCons.LinkedIn;
                txtAñosExp.Text = (perfilProfeCons.AñosExperiencia).ToString();
                txtEmpresa.Text = perfilProfeCons.Lugar_Empresa;
                txtPuesto.Text = perfilProfeCons.DescripcionPuesto;
                txtDesde.Text = (perfilProfeCons.PeriodoInicio).ToString();
                txtHasta.Text = (perfilProfeCons.PeriodoFin).ToString();


                #endregion

            }
            else
            {
                MessageBox.Show("Postulante no registrado");
                txtConsultaDni.Text = "";
            }
        }

        private void GrillaConsulta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            GrillaConsulta.CurrentRow.Selected = true;
            if (GrillaConsulta.Rows.Count != 1)
            {
                if (GrillaConsulta.Rows[e.RowIndex].Cells["Dni"].Value.ToString() != null)
                {
                    txtConsultaDni.Text = GrillaConsulta.Rows[e.RowIndex].Cells["Dni"].Value.ToString();
                    btnOk.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("No se encontraron postulantes registrados.Debe registrar uno previamente para poder visualizarlo en esta sección.");
            }
        }

    
        public void LimpiarCampos()
        {
            txtConsultaDni.Text = "";
            #region PERSONA
            txtNombre.Text = " ";
            txtApellido.Text = " ";
            txtDNI.Text = " ";
            txtFecNac.Text = " ";
            txtNacionalidad.Text = " ";
            txtDomicilio.Text = " ";
            cmbActDisponibilidad.Text = "Seleccione una opción";
            cmbActEstadoCivil.Text = "Seleccione una opción";
            checkHijos.Enabled = false;
            txtCantHijos.Text = " ";
            cmbActSexo.Text = "Seleccione una opción";
            txtCelular.Text = " ";
            txtFijo.Text = " ";
            txtAlternativo.Text = " ";
            txtmail.Text = " ";

            #endregion

            #region PERFIL
            cmbActDisciplina.Text = "Seleccione una opción";
            cmbActTipoPost.Text = "Seleccione una opción";
            cmbActNivel.Text = "Seleccione una opción";
            txtPuntaje.Text = "";
            #endregion

            #region Perfil Académico

            cmbActNivel.Text = "Seleccione una opción";
            cmbActEstadoNivel.Text = "Seleccione una opción";
            txtTitulo.Text = "";
            txtEstablecimiento.Text = "";

            #endregion

            #region Perfil Psicologico y Profesional

            checkApto.Enabled = false;

            txtDescripcion.Text = "";

            checkExperienciaLaboral.Enabled = false;

            LinkedInCons.Text = "";
            txtAñosExp.Text = "";
            txtEmpresa.Text = "";
            txtPuesto.Text = "";
            txtDesde.Text = "";
            txtHasta.Text = "";
            #endregion
        }


        private void btnGuardarCmbios_Click(object sender, EventArgs e)
        {
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
           
            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                Persona perA = new Persona();
                Postulante postA = new Postulante();


                IList<Persona> lista = new List<Persona>();
                lista = db.Persona.ToList();
                foreach (var item in db.Postulante)
                {
                    if (txtConsultaDni.Text == item.Persona.dni)
                    {
                        postA.Persona_Id = item.Persona_Id;
                        postA = item;
                    }
                }


                #region Datos Personales
                postA.Persona.nombre = txtNombre.Text;
                postA.Persona.apellido = txtApellido.Text;
                postA.Persona.dni = txtDNI.Text;
                txtFecNac.Text = perA.fechaNacimiento.ToShortDateString().ToString();//FECHA NACIMIENTO
                postA.Persona.nacionalidad = txtNacionalidad.Text;
                postA.Persona.domicilio = txtDomicilio.Text;

                foreach (Disponibilidad disponible in db.Disponibilidad)
                {
                    if (cmbActDisciplina.Text == disponible.estadoDisponibilidad)
                    {
                        postA.Persona.disponibilidadID = disponible.idDisponibilidad;
                        postA.Persona.Disponibilidad = disponible;
                    }

                }
                foreach (EstadoCivil estadoC in db.EstadoCivil)
                {
                    if (cmbActEstadoCivil.Text == estadoC.nombreEstadoCivil)
                    {
                        postA.Persona.estadoCivilID = estadoC.idEstadoCivil;
                        postA.Persona.EstadoCivil = estadoC;
                    }

                }

                foreach (Sexo s in db.Sexo)
                {
                    if (cmbActSexo.Text == s.nombreSexo)
                    {
                        postA.Persona.sexoID = s.idSexo;
                        postA.Persona.Sexo = s;
                    }
                }

                if (checkHijos.Checked == true)
                {

                    postA.Persona.hijos = true;
                    postA.Persona.cantHijos = int.Parse(txtCantHijos.Text);
                }
                else
                {
                    postA.Persona.hijos = false;
                }

                postA.Persona.telefono = txtCelular.Text;
                postA.Persona.telFijo = txtFijo.Text;
                postA.Persona.telAlternativo = txtAlternativo.Text;
                postA.Persona.mail = txtmail.Text;

                #endregion

                #region Perfil General

                foreach (TipoPostulante tipo in db.TipoPostulante)
                {
                    if (cmbActTipoPost.Text == tipo.nombreTipoPostulante)
                    {
                        postA.Perfil.tipoPostulanteID = tipo.idTipoPostulante;
                        postA.Perfil.TipoPostulante = tipo;
                    }
                }

                foreach (EstadoPostulante estado in db.EstadoPostulante)
                {
                    if (cmbActEstadoPost.Text == estado.nombreEstadoPostulante)
                    {
                        postA.Perfil.estadoPostulanteID = estado.idEstadoPostulante;
                        postA.Perfil.EstadoPostulante = estado;
                    }
                }
                
                foreach (Disciplina disc in db.Disciplina)
                {
                    if (cmbActDisciplina.Text == disc.nombreDisciplina)
                    {
                        postA.Perfil.disciplinaID = disc.idDisciplina;
                        postA.Perfil.Disciplina = disc;
                    }

                }

                #endregion


                #region Perfil Academico


                foreach (NivelEducativo nivel in db.NivelEducativo)
                {
                    if (cmbActNivel.Text == nivel.NombreNivel)
                    {
                        postA.Perfil.PerfilAcademico.Academico_NivelID = nivel.id_Nivel;
                        postA.Perfil.PerfilAcademico.NivelEducativo = nivel;
                    }

                }

                foreach (EstadoNivelEduc estadoN in db.EstadoNivelEduc)
                {
                    if (cmbActEstadoNivel.Text == estadoN.nombreEstadoNivelEduc)
                    {
                        postA.Perfil.PerfilAcademico.estadoNivelID = estadoN.idEstadoNivelEduc;
                        postA.Perfil.PerfilAcademico.EstadoNivelEduc = estadoN;
                    }

                }

                postA.Perfil.PerfilAcademico.Titulo = txtTitulo.Text;
                postA.Perfil.PerfilAcademico.Establecimiento = txtEstablecimiento.Text;


                #endregion

                #region Perfil Psicologico y Profesional


                if (checkApto.Checked == true)
                {
                    postA.Perfil.PerfilPsicologico.apto = true;
                }
                else
                {
                    postA.Perfil.PerfilPsicologico.apto = false;
                }
                postA.Perfil.PerfilPsicologico.descripcion = txtDescripcion.Text;



                if (checkExperienciaLaboral.Checked == true)
                {
                    postA.Perfil.PerfilProfesional.Experiencia = true;
                }
                else
                {
                    postA.Perfil.PerfilProfesional.Experiencia = false;
                }

                if (postA.Perfil.PerfilProfesional.Experiencia == true)
                {

                    postA.Perfil.PerfilProfesional.AñosExperiencia = int.Parse(txtAñosExp.Text);
                    postA.Perfil.PerfilProfesional.Lugar_Empresa = txtEmpresa.Text;
                    postA.Perfil.PerfilProfesional.DescripcionPuesto = txtPuesto.Text;
                    postA.Perfil.PerfilProfesional.PeriodoInicio = int.Parse(txtDesde.Text);
                    postA.Perfil.PerfilProfesional.PeriodoFin = int.Parse(txtDesde.Text);

                }
                postA.Perfil.PerfilProfesional.LinkedIn = LinkedInCons.Text;


                #endregion

                if (db.SaveChanges() >= 1)
                {
                    MessageBox.Show("Los cambios fueron guardados correctamente");
                }

            }

            #region REFRESH
            GrillaConsulta.DataSource = null;
            GrillaConsulta.Refresh();
            GrillaConsulta.Rows.Clear();
            LimpiarCampos();
            DeshabilitarTXT();

            IList<Persona> listar = perAux.GetAllPersonas();
            foreach (Persona item in listar)
            {
                EstadoCivil est = new EstadoCivil();
                est = perAux.GetEstadoCivilById(item);
                Disponibilidad disp = new Disponibilidad();
                disp = perAux.GetDisponibilidadById(item);
                Postulante postCons = new Postulante();
                postCons = postAux.GetPostulanteById(item);
                Perfil perf = new Perfil();
                perf = perfilAux.GetPerfilById(postCons);
                TipoPostulante tipoPost = new TipoPostulante();
                tipoPost = perfilAux.GetTipoPostulanteById(perf);
                EstadoPostulante estadoPost = new EstadoPostulante();
                estadoPost = perfilAux.GetEstadoPostulanteById(perf);
                Disciplina disc = new Disciplina();
                disc = perfilAux.GetDisciplinaById(perf);

                //Ingreso de datos de Persona en una línea en la grilla.

                GrillaConsulta.Rows.Add(item.idPersona, item.nombre, item.apellido, item.dni, disp.estadoDisponibilidad, est.nombreEstadoCivil, tipoPost.nombreTipoPostulante, estadoPost.nombreEstadoPostulante, disc.nombreDisciplina);
                #endregion

            btnEliminar.Enabled = true;
            btnActualizar.Enabled = true;
            }
        }

        private void VisitLink()
        {
            // Cambia el color del link cuando se lo visita con LinkVisited
            // a true.
            LinkedInCons.LinkVisited = true;
            //Llama al Metodo Process.Start para abrir con el navegador por defecto
            //la URL:

            System.Diagnostics.Process.Start(LinkedInCons.Text);
        }


        private void LinkedInCons_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        { //CLICK PARA INGRESAR AL LINK DE LINKEDIN
            try
            {
                VisitLink();
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                MessageBox.Show("No funciona el link que ha clickeado.");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtConsultaDni.Text = "";
            checkBusqExperieciaLaboral.Enabled = false;

            cmbBusqDisciplina.DataSource = perfilAux.GetDisciplina();
            cmbBusqDisciplina.DisplayMember = "nombreDisciplina";
            cmbBusqDisciplina.Text = "Seleccione una opción";

            cmbBusqTipoPost.DataSource = perfilAux.GetTipoPostulante();
            cmbBusqTipoPost.DisplayMember = "nombreTipoPostulante";
            cmbBusqTipoPost.Text = "Seleccione una opción";

            cmbBusqEstadoPost.DataSource = perfilAux.GetEstadoPostulante();
            cmbBusqEstadoPost.DisplayMember = "nombreEstadoPostulante";
            cmbBusqEstadoPost.Text = "Seleccione una opción";

            checkBusqExperieciaLaboral.Checked = false;

            GrillaConsulta.DataSource = null;
            GrillaConsulta.Refresh();
            GrillaConsulta.Rows.Clear();
            DeshabilitarTXT();

            IList<Persona> listar = perAux.GetAllPersonas();
            foreach (Persona item in listar)
            {
                EstadoCivil est = new EstadoCivil();
                est = perAux.GetEstadoCivilById(item);
                Disponibilidad disp = new Disponibilidad();
                disp = perAux.GetDisponibilidadById(item);
                Postulante postCons = new Postulante();
                postCons = postAux.GetPostulanteById(item);
                Perfil perf = new Perfil();
                perf = perfilAux.GetPerfilById(postCons);
                TipoPostulante tipoPost = new TipoPostulante();
                tipoPost = perfilAux.GetTipoPostulanteById(perf);
                EstadoPostulante estadoPost = new EstadoPostulante();
                estadoPost = perfilAux.GetEstadoPostulanteById(perf);
                Disciplina disc = new Disciplina();
                disc = perfilAux.GetDisciplinaById(perf);

                //Ingreso de datos de Persona en una línea en la grilla.

                GrillaConsulta.Rows.Add(item.idPersona, item.nombre, item.apellido, item.dni, disp.estadoDisponibilidad, est.nombreEstadoCivil, tipoPost.nombreTipoPostulante, estadoPost.nombreEstadoPostulante, disc.nombreDisciplina);
            }
            //Ajustar tamaño de columnas y celdas.
            GrillaConsulta.AutoResizeColumns();
            GrillaConsulta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            #region Inicialización/Instanciación

            Disciplina discFiltro = new Disciplina();
            TipoPostulante tipoFiltro = new TipoPostulante();
            EstadoPostulante estadoFiltro = new EstadoPostulante();
            bool exp = false;
            IList<Postulante> ListPostEncontrados = new List<Postulante>();
            IList<Disciplina> discList = perfilAux.GetDisciplina();
            #endregion

            foreach (var item in discList)
            {

                if (item.nombreDisciplina == cmbBusqDisciplina.Text)
                {
                    discFiltro = item;

                }

            }

            IList<TipoPostulante> tipoList = perfilAux.GetTipoPostulante();
            foreach (var item in tipoList)
            {
                if (item.nombreTipoPostulante == cmbBusqTipoPost.Text)
                {
                    tipoFiltro = item;

                }

            }

            IList<EstadoPostulante> estadoList = perfilAux.GetEstadoPostulante();
            foreach (var item in estadoList)
            {
                if (item.nombreEstadoPostulante == cmbBusqEstadoPost.Text)
                {
                    estadoFiltro = item;

                }
               
            }
            if (checkBusqExperieciaLaboral.Checked == true)
            {
                exp = true;
            }


            IList<Persona> lista2 = new List<Persona>();
            ListPostEncontrados = postAux.BuscarPostulante(discFiltro, tipoFiltro, estadoFiltro, exp);


            GrillaConsulta.Refresh();
            GrillaConsulta.Rows.Clear();
            Persona perCon = new Persona();
            GrillaConsulta.SelectAll();
            GrillaConsulta.ClearSelection();
            
           

            foreach (Postulante item in ListPostEncontrados)
            {
                perCon = perAux.GetPersonaById(item);
                EstadoCivil est = new EstadoCivil();
                est = perAux.GetEstadoCivilById(perCon);
                Disponibilidad disp = new Disponibilidad();
                disp = perAux.GetDisponibilidadById(perCon);
                
                Postulante postCons = new Postulante();
                postCons = postAux.GetPostulanteById(perCon);

                if (postCons.activo == true)
                {
                    Perfil perf = new Perfil();
                    perf = perfilAux.GetPerfilById(postCons);
                    TipoPostulante tipoPost = new TipoPostulante();
                    tipoPost = perfilAux.GetTipoPostulanteById(perf);
                    EstadoPostulante estadoPost = new EstadoPostulante();
                    estadoPost = perfilAux.GetEstadoPostulanteById(perf);
                    Disciplina disc = new Disciplina();
                    disc = perfilAux.GetDisciplinaById(perf);

                    GrillaConsulta.Rows.Add(perCon.idPersona, perCon.nombre, perCon.apellido, perCon.dni, disp.estadoDisponibilidad, est.nombreEstadoCivil, tipoPost.nombreTipoPostulante, estadoPost.nombreEstadoPostulante, disc.nombreDisciplina);
                }
                else
                {
                    MessageBox.Show("No se encontró un postulante que reuna las características solicitadas.");
                }
            }
            
            GrillaConsulta.AutoResizeColumns();
            GrillaConsulta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
   
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            btnActualizar.Enabled = false;

            DialogResult opcion;

            opcion = MessageBox.Show("¿Desea eliminar al postulante seleccionado?", "Acciones", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (opcion == DialogResult.Yes)
            {
                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {
                    Persona perA = new Persona();
                    Postulante postA = new Postulante();


                    IList<Persona> lista = new List<Persona>();
                    lista = db.Persona.ToList();
                    foreach (var item in db.Postulante)
                    {
                        if (txtConsultaDni.Text == item.Persona.dni)
                        {
                            postA.Persona_Id = item.Persona_Id;
                            postA = item;
                        }
                    }

                    postA.Persona.activa = false;
                    postA.Perfil.activo = false;
                    postA.Perfil.PerfilAcademico.activo = false;
                    postA.Perfil.PerfilProfesional.activo = false;
                    postA.Perfil.PerfilPsicologico.activo = false;
                    if (db.SaveChanges() >= 1)
                    {
                        MessageBox.Show("Postulante correctamente eliminado.");
                    }

                }

                #region REFRESH
                GrillaConsulta.DataSource = null;
                GrillaConsulta.Refresh();
                GrillaConsulta.Rows.Clear();
                LimpiarCampos();
                DeshabilitarTXT();

                IList<Persona> listar = perAux.GetAllPersonas();
                foreach (Persona item in listar)
                {
                    EstadoCivil est = new EstadoCivil();
                    est = perAux.GetEstadoCivilById(item);
                    Disponibilidad disp = new Disponibilidad();
                    disp = perAux.GetDisponibilidadById(item);
                    Postulante postCons = new Postulante();
                    postCons = postAux.GetPostulanteById(item);
                    Perfil perf = new Perfil();
                    perf = perfilAux.GetPerfilById(postCons);
                    TipoPostulante tipoPost = new TipoPostulante();
                    tipoPost = perfilAux.GetTipoPostulanteById(perf);
                    EstadoPostulante estadoPost = new EstadoPostulante();
                    estadoPost = perfilAux.GetEstadoPostulanteById(perf);
                    Disciplina disc = new Disciplina();
                    disc = perfilAux.GetDisciplinaById(perf);

                    //Ingreso de datos de Persona en una línea en la grilla.

                    GrillaConsulta.Rows.Add(item.idPersona, item.nombre, item.apellido, item.dni, disp.estadoDisponibilidad, est.nombreEstadoCivil, tipoPost.nombreTipoPostulante, estadoPost.nombreEstadoPostulante, disc.nombreDisciplina);
                    #endregion
                }
            }
            btnActualizar.Enabled = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            btnEliminar.Enabled = false;
            HabilitarTXT();
            #region INICIALIZACIONES
            cmbActEstadoCivil.DataSource = perAux.GetEstadoCivil();
            cmbActEstadoCivil.DisplayMember = "nombreEstadoCivil";
            cmbActEstadoCivil.Text = "Seleccione una opción";

            cmbActSexo.DataSource = perAux.GetSexo();
            cmbActSexo.DisplayMember = "nombreSexo";
            cmbActSexo.Text = "Seleccione una opción";

            cmbActDisponibilidad.DataSource = perAux.GetDisponibilidad();
            cmbActDisponibilidad.DisplayMember = "estadoDisponibilidad";
            cmbActDisponibilidad.Text = "Seleccione una opción";

            cmbActNivel.DataSource = perfilAcadAux.GetAllNivel();
            cmbActNivel.DisplayMember = "NombreNivel";
            cmbActNivel.Text = "Seleccione una opción";

            cmbActEstadoNivel.DataSource = perfilAcadAux.GetAllEstadoNivelEduc();
            cmbActEstadoNivel.DisplayMember = "nombreEstadoNivelEduc";
            cmbActEstadoNivel.Text = "Seleccione una opción";

            cmbActDisciplina.DataSource = perfilAux.GetDisciplina();
            cmbActDisciplina.DisplayMember = "nombreDisciplina";
            cmbActDisciplina.Text = "Seleccione una opción";

            cmbActTipoPost.DataSource = perfilAux.GetTipoPostulante();
            cmbActTipoPost.DisplayMember = "nombreTipoPostulante";
            cmbActTipoPost.Text = "Seleccione una opción";

            cmbActEstadoPost.DataSource = perfilAux.GetEstadoPostulante();
            cmbActEstadoPost.DisplayMember = "nombreEstadoPostulante";
            cmbActEstadoPost.Text = "Seleccione una opción";
            #endregion
        }

        public void HabilitarTXT()
        {
            #region PERSONA
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDNI.Enabled = true;
            txtFecNac.Enabled = true;
            txtNacionalidad.Enabled = true;
            txtDomicilio.Enabled = true;
            cmbActDisponibilidad.Enabled = true;
            cmbActEstadoCivil.Enabled = true;
            checkHijos.Enabled = true;
            txtCantHijos.Enabled = true;
            cmbActSexo.Enabled = true;
            txtCelular.Enabled = true;
            txtFijo.Enabled = true;
            txtAlternativo.Enabled = true;
            txtmail.Enabled = true;
            PictureFotoPerfil.Enabled = true;
            #endregion

            #region PERFIL
            cmbActDisciplina.Enabled = true;
            cmbActTipoPost.Enabled = true;
            cmbActEstadoPost.Enabled = true;
            cmbActNivel.Enabled = true;
            txtPuntaje.Enabled = true;
            #endregion

            #region Perfil Academico

            cmbActNivel.Enabled = true;
            cmbActEstadoNivel.Enabled = true;
            txtTitulo.Enabled = true;
            txtEstablecimiento.Enabled = true;

            #endregion

            #region Perfil Psicologico y Profesional

            checkApto.Enabled = true;

            txtDescripcion.Enabled = true;

            checkExperienciaLaboral.Enabled = true;

            txtAñosExp.Enabled = true;
            txtEmpresa.Enabled = true;
            txtPuesto.Enabled = true;
            txtDesde.Enabled = true;
            txtHasta.Enabled = true;
            #endregion
        }

        public void DeshabilitarTXT()
        {
            #region PERSONA
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDNI.Enabled = false;
            txtFecNac.Enabled = false;
            txtNacionalidad.Enabled = false;
            txtDomicilio.Enabled = false;
            cmbActDisponibilidad.Enabled = false;
            cmbActEstadoCivil.Enabled = false;
            checkHijos.Enabled = false;
            txtCantHijos.Enabled = false;
            cmbActSexo.Enabled = false;
            txtCelular.Enabled = false;
            txtFijo.Enabled = false;
            txtAlternativo.Enabled = false;
            txtmail.Enabled = false;
            PictureFotoPerfil.Enabled = false;



            #endregion

            #region PERFIL
            cmbActDisciplina.Enabled = false;
            cmbActTipoPost.Enabled = false;
            cmbActEstadoPost.Enabled = false;
            cmbActNivel.Enabled = false;
            txtPuntaje.Enabled = false;
            #endregion

            #region Perfil Academico

            cmbActNivel.Enabled = false;
            cmbActEstadoNivel.Enabled = false;
            txtTitulo.Enabled = false;
            txtEstablecimiento.Enabled = false;

            #endregion

            #region Perfil Psicologico y Profesional

            checkApto.Enabled = false;

            txtDescripcion.Enabled = false;

            checkExperienciaLaboral.Enabled = false;


            txtAñosExp.Enabled = false;
            txtEmpresa.Enabled = false;
            txtPuesto.Enabled = false;
            txtDesde.Enabled = false;
            txtHasta.Enabled = false;
            #endregion

        }
    }
}
