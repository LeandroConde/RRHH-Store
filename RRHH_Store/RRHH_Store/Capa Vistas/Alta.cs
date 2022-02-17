using System;
using System.Drawing;
using System.Windows.Forms;
using RRHH_Store.Capa_Datos;
using RRHH_Store.Capa_Negocios;
using System.IO;
using System.Net.Mail;

namespace RRHH_Store.Capa_Vistas
{
    public partial class Alta : Form
    {
        #region INSTANCIACIONES
        private PersonaClass p = new PersonaClass();
        private PostulanteClass postulante = new PostulanteClass();
        private PerfilAcademicoClass academic = new PerfilAcademicoClass();
        private PerfilClass perf = new PerfilClass();
        private PerfilPsicologicoClass psico = new PerfilPsicologicoClass();
        private PerfilProfesionalClass prof = new PerfilProfesionalClass();
        byte[] fileCertificaciones = null;
        byte[] file= null;
        #endregion
        public Alta()
        {
            InitializeComponent();
        }

        #region BOTONES DE INTERFAZ
        #region ICONOS
        private void picBoxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void picBoxMaximizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            picBoxMaximizar.Visible = false;
            picBoxRestaurar.Visible = true;
        }

        #endregion
        private void btnDatosPersonales_Click(object sender, EventArgs e)
        {
            btnDatosPersonales.BackColor = Color.LightSteelBlue;
        }

        private void btnFormacionAcademica_Click(object sender, EventArgs e)
        {
            btnFormacionAcademica.BackColor = Color.LightSteelBlue;
            btnDatosPersonales.BackColor = Color.Black;
        }

        private void btnInformacionProfesional_Click(object sender, EventArgs e)
        {
            btnInformacionProfesional.BackColor = Color.LightSteelBlue;
            btnFormacionAcademica.BackColor = Color.Black;
        }

        private void btnGeneral_Click(object sender, EventArgs e)
        {
            btnGeneral.BackColor = Color.LightSteelBlue;
        }

       

        private void btnVolverGeneral_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form vistaPrevia = new FiltroPreviaAlta();
            vistaPrevia.Show();
        }

        private void btnVolverInformacionProfesional_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form vistaPrevia = new FiltroPreviaAlta();
            vistaPrevia.Show();
        }

        private void BtnVolverDatosPersonales_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form vistaPrevia = new FiltroPreviaAlta();
            vistaPrevia.Show();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form vistaPrevia = new FiltroPreviaAlta();
            vistaPrevia.Show();
        }

        #endregion

        private void Alta_Load(object sender, EventArgs e)
        {
            tabFormacionAcadem.Enabled = false;
            btnFormacionAcademica.Enabled = false;
            tabInfoProfesional.Enabled = false;
            btnInformacionProfesional.Enabled = false;
            tabGeneral.Enabled = false;
            btnGeneral.Enabled = false;
            btnDatosPersonales.BackColor = Color.LightSteelBlue;

            #region PERSONA

            cmbDisponibilidad.DataSource = p.GetDisponibilidad();
            cmbDisponibilidad.DisplayMember = "estadoDisponibilidad";
            cmbDisponibilidad.Text = "Seleccione una opción";

            cmbEstadoCivil.DataSource = p.GetEstadoCivil();
            cmbEstadoCivil.DisplayMember = "nombreEstadoCivil";
            cmbEstadoCivil.Text = "Seleccione una opción";

            cmbSexo.DataSource = p.GetSexo();
            cmbSexo.DisplayMember = "nombreSexo";
            cmbSexo.Text = "Seleccione una opción";

            groupBoxHijos.Enabled = false;

            #endregion

            #region PERFIL ACADÉMICO
            gbCertificaciones.Enabled = false;
            cmbNivelEducativo.DataSource = academic.GetAllNivel();
            cmbNivelEducativo.DisplayMember = "NombreNivel";
            cmbNivelEducativo.SelectedItem = null;
            cmbNivelEducativo.Text = "Seleccione una opción";

            cmbEstadoEducativo.DataSource = academic.GetAllEstadoNivelEduc();
            cmbEstadoEducativo.DisplayMember = "nombreEstadoNivelEduc";
            cmbEstadoEducativo.SelectedItem = null;
            cmbEstadoEducativo.Text = "Seleccione una opción";
            #endregion

            #region PERFIL
            cmbDisciplina.DataSource = perf.GetDisciplina();
            cmbDisciplina.DisplayMember = "nombreDisciplina";
            cmbDisciplina.SelectedItem = null;
            cmbDisciplina.Text = "Seleccione una opción";

            cmbTipoPostulante.DataSource = perf.GetTipoPostulante();
            cmbTipoPostulante.DisplayMember = "nombreTipoPostulante";
            cmbTipoPostulante.SelectedItem = null;
            cmbTipoPostulante.Text = "Seleccione una opción";

            cmbEstadoPostulante.DataSource = perf.GetEstadoPostulante();
            cmbEstadoPostulante.DisplayMember = "nombreEstadoPostulante";
            cmbEstadoPostulante.SelectedItem = null;
            cmbEstadoPostulante.Text = "Seleccione una opción";
            #endregion

            #region PERFIL PROFESIONAL
            groupBoxDatosExpLaboral.Enabled = false;
            #endregion
        }



        #region TAB CONTROL DATOS PERSONALES
       
        private void btnSubirFoto_Click(object sender, EventArgs e)
        {
            DialogResult r = cargaDatos.ShowDialog();
            cargaDatos.InitialDirectory = "c:\\";
            cargaDatos.Filter = "Archivos (JPG,JPEG,PNG)|*.JPG;*.JPEG;*.PNG;";
            cargaDatos.FilterIndex = 1;
            cargaDatos.RestoreDirectory = true;
            if (r == DialogResult.OK)
            {
                pictureBoxFoto.Text = cargaDatos.FileName;
                pictureBoxFoto.Image = Image.FromFile(cargaDatos.FileName);
                pictureBoxFoto.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            
        }


        private void btnGuardarDatosPersonales_Click(object sender, EventArgs e)
        {
            DialogResult opcion;
            validarComboDisponibilidad();
            validarComboEstadoCivil();
            validarComboSexo();


            #region FOTO
            byte[] fileFoto = null;
            try
            {
                Stream myStream = cargaDatos.OpenFile();
                using (MemoryStream ms = new MemoryStream())
                {
                    myStream.CopyTo(ms);
                    fileFoto = ms.ToArray();
                }

            }
            catch (Exception ex)
            {
                string m = ex.Message;
                pictureBoxFoto.Image = null;
            }
            #endregion

            bool r = p.GuardarPersona(txtNombre, txtApellido, txtDNI, dateTimePickerFechNac, txtNacionalidad, txtDomicilio, cmbDisponibilidad, cmbEstadoCivil, checkHijos, txtCantHijos, cmbSexo, txtCelular, txtFijo, txtAlternativo, txtmail, fileFoto);
            if (r==true)
            {

                MessageBox.Show("Datos cargados correctamente.");

                opcion = MessageBox.Show("¿Desea seguir registrando información del postulante?", "Acciones", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (opcion == DialogResult.Yes)
                {
                    btnFormacionAcademica_Click(sender, e);
                    tabFormacionAcadem.Enabled = true;
                    tabAlta.SelectTab("tabFormacionAcadem");
                    TabDatosPersonales.Enabled = false;
                    btnDatosPersonales.Enabled = false;

                }
                else
                {
                    academic.GuardarPerfilAcademicoVacio(cmbNivelEducativo, cmbEstadoEducativo, txtTitulo, txtEstablecimiento, checkCursos, txtNombreCertificacion, fileCertificaciones);
                    prof.GuardarPerfilProfesionalVacio(textLinkedIn, checkBoxExpLaboral, txtAñosExperiencia, txtEmpresa, txtAñosExperiencia, txtDesde, txtHasta, txtNombreCV, file);
                    psico.GuardarPerfilPsicologicoVacio(checkApto, txtDescPsico);
                    perf.GuardarPerfilGeneralVacio(cmbTipoPostulante, cmbEstadoPostulante, lblPuntuacionTotal, cmbDisciplina);
                    postulante.GuardarPostulante();
                    MessageBox.Show(" ¡Carga realizada!");
                    this.Hide();
                    Form vistaMenu = new MenuPrincipal();
                    vistaMenu.Show();
                }

            }
            else
            {
                MessageBox.Show("Se produjo un error. Corrobore los datos ingresados.");
            }
        }

        private void checkHijos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHijos.Checked == true)
            {
                groupBoxHijos.Enabled = true;
            }
            else
            {
                groupBoxHijos.Enabled = false;
            }
        }


        #region VALIDACIONES DATOS PERSONALES
        static bool validarEmail(string email)
        {
            try
            {
                new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }

        }
       
        private void txtmail_Leave(object sender, EventArgs e)
        {
            if (validarEmail(txtmail.Text)==false)
            {
                MessageBox.Show("Dirección de correo electrónico no válida, el correo debe tener el siguiente formato: nombre@dominio.com," + "por favor ingrese un correo válido ", "validacián de correo electrónico ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmail.SelectAll();
                txtmail.Focus();
            }
        }

        #region  TEXTBOX KeyPress
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO LETRAS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
           
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO LETRAS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO NUMEROS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
        }

        private void txtNacionalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO LETRAS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
        }

        private void txtDomicilio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 46) || (e.KeyChar >= 59 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 166) || (e.KeyChar >= 168 && e.KeyChar <= 255))
            {
                MessageBox.Show("Caracter no valido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCantHijos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO NUMEROS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO NUMEROS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
        }

        private void txtFijo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO NUMEROS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
        }

        private void txtAlternativo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO NUMEROS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
        }
        #endregion


        #region ComboBox SelectedIndex
        public void validarComboDisponibilidad()
        {
            if (cmbDisponibilidad.Text == "Seleccione una opción")
            {
                errorProvider.SetError(cmbDisponibilidad, "Campo obligatorio.");
            }
        }

        public void validarComboEstadoCivil()
        {
            if (cmbEstadoCivil.Text == "Seleccione una opción")
            {
                errorProvider.SetError(cmbEstadoCivil, "Campo obligatorio.");
            }
        }

        public void validarComboSexo()
        {
            if (cmbSexo.Text == "Seleccione una opción")
            {
                errorProvider.SetError(cmbSexo, "Campo obligatorio.");
            }
        }
        #endregion


        #region TextChanged 
        //Carga la primera letra con mayúscula
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            txtNombre.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNombre.Text);
            txtNombre.SelectionStart = txtNombre.Text.Length;
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            txtApellido.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtApellido.Text);
            txtApellido.SelectionStart = txtApellido.Text.Length;
        }


        private void txtNacionalidad_TextChanged(object sender, EventArgs e)
        {
            txtNacionalidad.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNacionalidad.Text);
            txtNacionalidad.SelectionStart = txtNacionalidad.Text.Length;
        }

        private void txtDomicilio_TextChanged(object sender, EventArgs e)
        {
            txtDomicilio.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtDomicilio.Text);
            txtDomicilio.SelectionStart = txtDomicilio.Text.Length;
        }

        private void txtmail_TextChanged(object sender, EventArgs e)
        {
            txtmail.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToLower(txtmail.Text);
            txtmail.SelectionStart = txtmail.Text.Length;
        }
        #endregion

        #endregion

        #endregion


        #region TAB CONTROL PERFIL ACADEMICO
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult opcion;
            validarComboNivelEducativo();
            validarComboEstadoEducativo();
            // Guardado de certificado
           
            bool r = academic.GuardarPerfilAcademico(cmbNivelEducativo, cmbEstadoEducativo, txtTitulo, txtEstablecimiento, checkCursos, txtNombreCertificacion, fileCertificaciones);
            if (r==true)
            {

                MessageBox.Show("Datos guardados correctamente.");

                opcion = MessageBox.Show("¿Desea seguir registrando información del postulante?", "Acciones", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (opcion == DialogResult.Yes)
                {
                    btnInformacionProfesional_Click(sender, e);
                    tabInfoProfesional.Enabled = true;
                    tabAlta.SelectTab("tabInfoProfesional");
                    tabFormacionAcadem.Enabled = false;
                    btnFormacionAcademica.Enabled = false;
                }
                else
                {
                    prof.GuardarPerfilProfesionalVacio(textLinkedIn, checkBoxExpLaboral, txtAñosExperiencia, txtEmpresa, txtAñosExperiencia, txtDesde, txtHasta, txtNombreCV, file);
                    psico.GuardarPerfilPsicologicoVacio(checkApto, txtDescPsico);
                    perf.GuardarPerfilGeneralVacio(cmbTipoPostulante, cmbEstadoPostulante, lblPuntuacionTotal, cmbDisciplina);
                    postulante.GuardarPostulante();
                    MessageBox.Show(" ¡Carga realizada!");
                    this.Hide();
                    Form vistaMenu = new MenuPrincipal();
                    vistaMenu.Show();
                }
            }
            else
            {
                MessageBox.Show("Se ha producido un error en la carga.Corrobore los datos ingresados.");
            }
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            // Codigo para subir el Certificado
            cargaDatos.InitialDirectory = "C:\\";
            cargaDatos.Filter = "PDF (PDF)|*.PDF";//AQUI HACEMOS UN FILTRO PARA LOS ARCHIVOS QUE QUEREMOS
            cargaDatos.FilterIndex = 1;// INDICAMOS LA PRIORIDAD DEL TIPO DE ARCHIVO SEGUN EL ORDEN DE ARRIBA
            cargaDatos.RestoreDirectory = true;//AQUI DEJA GUARDADO EN CACHE, EL ULTIMO DIRECTORIO EN DONDE ESTUVIMOS AL SUBIR UN ARCHIVO
            if (cargaDatos.ShowDialog() == DialogResult.OK)// 'DialogResult' ES UNA CONSTANTESI ESTA SALE BIEN ENTONCES EJECUTA LO DE ABAJO
            {
                txtArchivo.Text = cargaDatos.FileName;
            }
        }
        
        private void checkCursos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCursos.Checked == true)
            {
                gbCertificaciones.Enabled = true;

                #region CERTIFICACIONES
                // Guardado de certificado
                
                Stream myStream = cargaDatos.OpenFile();
                using (MemoryStream ms = new MemoryStream())
                {
                    myStream.CopyTo(ms);
                    fileCertificaciones = ms.ToArray();
                }

                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {
                    PerfilAcademico objCurriculum = new PerfilAcademico();
                    objCurriculum.certificacion = fileCertificaciones;
                    objCurriculum.nombreArchivo = cargaDatos.SafeFileName;

                    db.PerfilAcademico.Add(objCurriculum);
                    db.SaveChanges();
                }
                #endregion
            }
            else
            {
                gbCertificaciones.Enabled = false;
            }
        }


        #region VALIDACIONES PERFIL ACADÉMICO

        #region COMBOBOX
        public void validarComboNivelEducativo()
        {
            if (cmbNivelEducativo.Text == "Seleccione una opción")
            {
                errorProvider.SetError(cmbNivelEducativo, "Campo obligatorio.");
            }
        }
        public void validarComboEstadoEducativo()
        {
            if (cmbEstadoEducativo.Text == "Seleccione una opción")
            {
                errorProvider.SetError(cmbEstadoEducativo, "Campo obligatorio.");
            }
        }
        #endregion

        #region KEY PRESS

        private void txtTitulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO LETRAS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
        }
      
        private void txtEstablecimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO LETRAS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
        }
        #endregion

        # region TEXTCHANGED
        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {
            txtTitulo.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtTitulo.Text);
            txtTitulo.SelectionStart = txtTitulo.Text.Length;
        }

        private void txtEstablecimiento_TextChanged(object sender, EventArgs e)
        {
            txtEstablecimiento.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtEstablecimiento.Text);
            txtEstablecimiento.SelectionStart = txtEstablecimiento.Text.Length;
        }
        #endregion

        #endregion

        #endregion




        #region TAB CONTROL INFORMACION PROFESIONAL


        private void btnGuardarDatosLaborales_Click(object sender, EventArgs e)
        {
            DialogResult opcion;

            #region CV
            // Guardado del curriculum 
            if (txtNombreCV.Text.Trim().Equals("") || txtArchivoCV.Text.Trim().Equals(""))
            {
                MessageBox.Show("Nombre Obligatorio");
                return;
            }
            Stream myStream = cargaDatos.OpenFile();
            using (MemoryStream ms = new MemoryStream())
            {

                myStream.CopyTo(ms);
                file = ms.ToArray();
            }

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                PerfilProfesional objCurriculum = new PerfilProfesional();
                objCurriculum.cv = file;
                objCurriculum.nombreArchivo = cargaDatos.SafeFileName;

                db.PerfilProfesional.Add(objCurriculum);
                db.SaveChanges();
            }
            #endregion
            bool r = prof.GuardarPerfilProfesional(textLinkedIn, checkBoxExpLaboral, txtAñosExperiencia, txtEmpresa, txtAñosExperiencia, txtDesde, txtHasta, txtNombreCV, file);
            if (r)
            {
                
                MessageBox.Show("Datos guardados correctamente.");


                opcion = MessageBox.Show("¿Desea seguir registrando información del postulante?", "Acciones", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (opcion == DialogResult.Yes)
                {
                    btnGeneral_Click(sender,e);
                    tabGeneral.Enabled = true;
                    tabAlta.SelectTab("tabGeneral");
                    tabInfoProfesional.Enabled = false;
                    btnGeneral.Enabled = false;

                }
                else
                {
                    psico.GuardarPerfilPsicologicoVacio(checkApto, txtDescPsico);
                    perf.GuardarPerfilGeneralVacio(cmbTipoPostulante, cmbEstadoPostulante, lblPuntuacionTotal, cmbDisciplina);
                    postulante.GuardarPostulante();
                    MessageBox.Show(" ¡Carga realizada!");
                    this.Hide();
                    Form vistaMenu = new MenuPrincipal();
                    vistaMenu.Show();
                }
            }
            else
            {
                MessageBox.Show("Se ha producido un error en la carga.Corrobore los datos ingresados.");
            }
        }


        private void btnSubirCV_Click_1(object sender, EventArgs e)
        {
            // Codigo para subir el Curriculum 
            cargaDatos.InitialDirectory = "C:\\";
            cargaDatos.Filter = "PDF (PDF)|*.PDF";//AQUI HACEMOS UN FILTRO PARA LOS ARCHIVOS QUE QUEREMOS
            cargaDatos.FilterIndex = 1;// INDICAMOS LA PRIORIDAD DEL TIPO DE ARCHIVO SEGUN EL ORDEN DE ARRIBA
            cargaDatos.RestoreDirectory = true;//AQUI DEJA GUARDADO EN CACHE, EL ULTIMO DIRECTORIO EN DONDE ESTUVIMOS AL SUBIR UN ARCHIVO
            if (cargaDatos.ShowDialog() == DialogResult.OK)// 'DialogResult' ES UNA CONSTANTESI ESTA SALE BIEN ENTONCES EJECUTA LO DE ABAJO
            {
                txtArchivoCV.Text = cargaDatos.FileName;
            }
            if(cargaDatos ==null)
            {
                MessageBox.Show("Debe registrar el Currículum Vitae del postulante");
                btnSubirCV_Click_1(sender,e);
            }
        }
     
        private void checkBoxExpLaboral_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxExpLaboral.Checked == true)
            {
                groupBoxDatosExpLaboral.Enabled = true;
            }
            else
            {
                groupBoxDatosExpLaboral.Enabled = false;
            }
        }


        #region VALIDACIONES PERFIL PROFESIONAL

        #region KEYPRESS
        private void txtLinkedIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO LETRAS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
        }
        private void txtDesde_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO NUMEROS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
        }

        private void txtHasta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOLO NUMEROS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;

                return;
            }
        }
        #endregion

        #region TEXTCHANGED
        private void textLinkedIn_TextChanged(object sender, EventArgs e)
        {
            textLinkedIn.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToLower(textLinkedIn.Text);
            textLinkedIn.SelectionStart = textLinkedIn.Text.Length;
        }

        private void txtPuesto_TextChanged_1(object sender, EventArgs e)
        {
            txtPuesto.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtPuesto.Text);
            txtPuesto.SelectionStart = txtPuesto.Text.Length;
        }

        private void txtEmpresa_TextChanged_1(object sender, EventArgs e)
        {
            txtEmpresa.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtEmpresa.Text);
            txtEmpresa.SelectionStart = txtEmpresa.Text.Length;
        }

        private void txtNombreCertificacion_TextChanged(object sender, EventArgs e)
        {
            txtNombreCertificacion.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNombreCertificacion.Text);
            txtNombreCertificacion.SelectionStart = txtNombreCertificacion.Text.Length;
        }

 
        private void txtEmpresa_TextChanged(object sender, EventArgs e)
        {
            txtEmpresa.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtEmpresa.Text);
            txtEmpresa.SelectionStart = txtEmpresa.Text.Length;
        }

        private void txtPuesto_TextChanged(object sender, EventArgs e)
        {
            txtPuesto.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtAñosExperiencia.Text);
            txtAñosExperiencia.SelectionStart = txtAñosExperiencia.Text.Length;
        }

        private void txtNombreCV_TextChanged(object sender, EventArgs e)
        {
            txtNombreCV.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNombreCV.Text);
            txtNombreCV.SelectionStart = txtNombreCV.Text.Length;

        }

       
        #endregion
      
        #endregion
        #endregion

     
        
        
        
        #region TAB CONTROL GENERAL
        private void btnGuardarPsico_Click(object sender, EventArgs e)
        {
            DialogResult opc;
            bool result = psico.GuardarPerfilPsicologico(checkApto, txtDescPsico);

            if (result == true)
            {
                MessageBox.Show("Datos cargados correctamente.");
                opc = MessageBox.Show("¿Desea seguir registrando información del postulante?", "Acciones", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (opc == DialogResult.No)
                {
                    perf.GuardarPerfilGeneral(cmbTipoPostulante, cmbEstadoPostulante, lblPuntuacionTotal, cmbDisciplina);
                    postulante.GuardarPostulante();
                }
            }
            else
            {
                MessageBox.Show("Se produjo un error.Corrobore los datos ingresados.");
            }
        }


        private void btnGuardarObservaciones_Click(object sender, EventArgs e)
        {
            validarComboTipoPostulante();
            validarComboEstadoPostulante();
            validarComboDisciplina();
            bool result = perf.GuardarPerfilGeneral(cmbTipoPostulante, cmbEstadoPostulante, lblPuntuacionTotal, cmbDisciplina);
            if (result)
            {
                MessageBox.Show("Datos cargados correctamente.");
                postulante.GuardarPostulante();
                this.Hide();
                Form vistaMenu = new MenuPrincipal();
                vistaMenu.Show();
            }
            else
            {
                MessageBox.Show("Se produjo un error.Corrobore los datos ingresados.");
            }
        }


        #region VALIDACIONES
        public void validarComboTipoPostulante()
        {
            if (cmbTipoPostulante.Text == "Seleccione una opción")
            {
                errorProvider.SetError(cmbTipoPostulante, "Campo obligatorio.");
            }
        }

        public void validarComboEstadoPostulante()
        {
            if (cmbEstadoPostulante.Text == "Seleccione una opción")
            {
                errorProvider.SetError(cmbEstadoPostulante, "Campo obligatorio.");
            }
        }

        public void validarComboDisciplina()
        {
            if (cmbDisciplina.Text == "Seleccione una opción")
            {
                errorProvider.SetError(cmbDisciplina, "Campo obligatorio.");

            }
        }
        #endregion

        #endregion


    }
}
