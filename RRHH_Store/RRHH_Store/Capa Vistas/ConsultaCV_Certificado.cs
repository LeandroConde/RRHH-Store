using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using RRHH_Store.Capa_Negocios;
using RRHH_Store.Capa_Datos;

namespace RRHH_Store
{
    public partial class ConsultaCV_Certificado : Form
    {
        #region INSTANCIACIONES
        PersonaClass personaAux = new PersonaClass();
        PostulanteClass postulanteAux = new PostulanteClass();
        PerfilClass perfilAux = new PerfilClass();
        PerfilProfesionalClass perfilP = new PerfilProfesionalClass();
        PerfilAcademicoClass perfilAcadAux = new PerfilAcademicoClass();
        #endregion
        public ConsultaCV_Certificado()
        {
            InitializeComponent();
        }

        #region BOTONES DE LA INTERFAZ
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

        private void picBoxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form ConsultaCV = new Capa_Vistas.MenuPrincipal();
            ConsultaCV.Show();
        }


        #endregion


        private void CargaGrilla(IList<Persona> per)
        {
            #region DISEÑO DE GRILLA
            dgvPDF.DataSource = null;
            dgvPDF.Refresh();
            dgvPDF.Rows.Clear();
            dgvPDF.SelectAll();
            dgvPDF.ClearSelection();
            dgvPDF.Columns.Add("nombrePostulante", "Nombre");
            dgvPDF.Columns.Add("dni", "Dni");
            dgvPDF.Columns.Add("nombreArchivo", "Nombre Archivo");
            dgvPDF.Columns.Add("certificados", "Certificados");
            dgvPDF.Columns.Add("tipoPostulante", "Tipo Postulante");
            dgvPDF.Columns.Add("Disciplina", "Disciplina");
            #endregion

            foreach (Persona item in per)
            {
                Postulante postCons = new Postulante();
                postCons = postulanteAux.GetPostulanteById(item);
                if (postCons.activo == true)
                {
                    Perfil perf = new Perfil();
                    perf = perfilAux.GetPerfilById(postCons);
                    PerfilAcademico perfilAcad = new PerfilAcademico();
                    perfilAcad = perfilAcadAux.GetPerfilAcademicoById(perf);
                    PerfilProfesional perfProf = new PerfilProfesional();
                    perfProf = perfilP.GetPerfilProfesionalById(perf);

                    TipoPostulante tipoPost = new TipoPostulante();
                    tipoPost = perfilAux.GetTipoPostulanteById(perf);

                    Disciplina disc = new Disciplina();
                    disc = perfilAux.GetDisciplinaById(perf);
                    //Ingreso de datos de Persona en una línea en la grilla.

                    dgvPDF.Rows.Add(item.nombre + " " + item.apellido, item.dni, perfProf.nombreArchivo, perfilAcad.nombreArchivo, tipoPost.nombreTipoPostulante, disc.nombreDisciplina);
                }

            }
            //Ajustar tamaño de columnas y celdas.
            dgvPDF.AutoResizeColumns();
            dgvPDF.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


        }
        private void ConsultaCV_Certificado_Load(object sender, EventArgs e)
        {
            IList<Persona> listaTotal = new List<Persona>();
            listaTotal = personaAux.GetAllPersonas();
            CargaGrilla(listaTotal);
        }

    


        private void MostrarFoto(TextBox dni)
        {
            try
            {
                if (dgvPDF.Rows.Count > 0)
                {
                    Persona per = new Persona();
                    per = personaAux.BuscarPersona(txtDniPdf);

                    using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                    {
                        var mostrarImagen = per.foto;
                        MemoryStream ms = new MemoryStream(mostrarImagen);
                        Bitmap bmp = new Bitmap(ms);
                        PictureFotoPerfil.Image = Image.FromStream(ms);
                        PictureFotoPerfil.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                }


            }
            catch (Exception ex)
            {
                string m = ex.Message;
                PictureFotoPerfil.Image = null;
            }

        }
    
        private void dgvPDF_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPDF.CurrentRow.Selected = true;
            if (dgvPDF.Rows.Count != 1)
            {
                if (dgvPDF.Rows[e.RowIndex].Cells["Dni"].Value.ToString() != null)
                {
                txtDniPdf.Text = dgvPDF.Rows[e.RowIndex].Cells["Dni"].Value.ToString();
                btnBuscar.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("No se encontraron postulantes registrados.Debe registrar uno previamente para poder visualizarlo en esta sección.");
            }

        }

        private void btnMostrarCV_Click(object sender, EventArgs e)
        {
            
                if (dgvPDF.Rows.Count > 0)
                {
                    Persona per = new Persona();
                    per = personaAux.BuscarPersona(txtDniPdf);
                    Postulante postCons = new Postulante();
                    postCons = postulanteAux.GetPostulanteById(per);
                    Perfil perf = new Perfil();
                    perf = perfilAux.GetPerfilById(postCons);
                    PerfilAcademico perfilAcad = new PerfilAcademico();
                    perfilAcad = perfilAcadAux.GetPerfilAcademicoById(perf);

                    long id = perfilAcad.idPerfilAcademico;
               
                    using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                    {
                        var objCurriculum = db.PerfilAcademico.Find(id);

                        string path = AppDomain.CurrentDomain.BaseDirectory;
                        string folder = path + "/temp/";
                        
                            string fullfilepath = folder + objCurriculum.nombreArchivo;

                            if (!Directory.Exists(folder))
                            {
                                Directory.CreateDirectory(folder);
                            }


                            File.WriteAllBytes(fullfilepath, objCurriculum.certificacion);

                            Process.Start(fullfilepath);
                        
                    }
                }
                
        }
         
        

        private void btnMostrarCertificado_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPDF.Rows.Count > 0)
                {
                    Persona per = new Persona();
                    per = personaAux.BuscarPersona(txtDniPdf);
                    Postulante postCons = new Postulante();
                    postCons = postulanteAux.GetPostulanteById(per);
                    Perfil perf = new Perfil();
                    perf = perfilAux.GetPerfilById(postCons);
                    PerfilAcademico perfilAcad = new PerfilAcademico();
                    perfilAcad = perfilAcadAux.GetPerfilAcademicoById(perf);

                    long id = perfilAcad.idPerfilAcademico;

                    using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                    {
                        var objCurriculum = db.PerfilAcademico.Find(id);

                        string path = AppDomain.CurrentDomain.BaseDirectory;
                        string folder = path + "/temp/";

                        if (perfilAcad.cursosRelacionados == true)
                        {

                            string fullfilepath = folder + objCurriculum.nombreArchivo;

                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }


                        File.WriteAllBytes(fullfilepath, objCurriculum.certificacion);

                        Process.Start(fullfilepath);
                        }
                        else
                        {
                            perfilAcad.certificacion = null;
                            MessageBox.Show("No se registró un archivo de certificación para este postulante.");
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                string m = ex.Message;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            #region INSTANCIACIÓN DE VARIABLES
            Persona per = new Persona();
            per = personaAux.BuscarPersona(txtDniPdf);
            IList<Persona> lista = new List<Persona>();
            
            Postulante postCons = new Postulante();
            Perfil perfilCons = new Perfil();
            PerfilAcademico perfilAcadCons = new PerfilAcademico();
            PerfilProfesional perfilProfeCons = new PerfilProfesional();
            #endregion
        
            per = personaAux.BuscarPersona(txtDniPdf);
            postCons = postulanteAux.GetPostulanteById(per);
            if (postCons.activo == true)
            {
                lblNombre.Text = per.nombre;
                lblApellido.Text = per.apellido;
                MostrarFoto(txtDniPdf);
                perfilCons = perfilAux.GetPerfilById(postCons);

                TipoPostulante tipoPost = new TipoPostulante();
                Disciplina discipPost = new Disciplina();

                discipPost = perfilAux.GetDisciplinaById(perfilCons);
                lblDisciplina.Text = discipPost.nombreDisciplina;
                tipoPost = perfilAux.GetTipoPostulanteById(perfilCons);
                lblTipoPostulante.Text = tipoPost.nombreTipoPostulante;
            }
            else
            {
                MessageBox.Show("Postulante no registrado");
                txtDniPdf.Text = "";
            }
        }

        
    }
}