using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RRHH_Store.Capa_Datos;

namespace RRHH_Store.Capa_Negocios
{
    public class PersonaClass
    {
        public IList<Persona> GetAllPersonas()
        {
            IList<Persona> list = new List<Persona>();
            IList<Persona> lista = new List<Persona>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.Persona.ToList();
                foreach (Persona per in list)
                {
                    if (per.activa == true)
                    {
                        lista.Add(per);
                    }
                }
            }

            return lista;
        }

        #region Disponibilidad

        public IList<Disponibilidad> GetDisponibilidad()
        {
            IList<Disponibilidad> list = new List<Disponibilidad>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.Disponibilidad.ToList();
            }

            return list;
        }

        public Disponibilidad GetDisponibilidadById(Persona p1)
        {
            Disponibilidad disp = new Disponibilidad();
            IList<Disponibilidad> listadisp = new List<Disponibilidad>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listadisp = db.Disponibilidad.ToList();
                foreach (Disponibilidad item in listadisp)
                {
                    if (p1.disponibilidadID == item.idDisponibilidad)
                    {
                        disp.idDisponibilidad = item.idDisponibilidad;
                        disp = item;
                    }

                }

            }

            return disp;
        }

        #endregion

        #region Estado Civil
        public IList<EstadoCivil> GetEstadoCivil()
        {
            IList<EstadoCivil> list = new List<EstadoCivil>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.EstadoCivil.ToList();
            }

            return list;
        }

        public EstadoCivil GetEstadoCivilById(Persona p1)
        {
            EstadoCivil est = new EstadoCivil();
            IList<EstadoCivil> listaEst = new List<EstadoCivil>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listaEst = db.EstadoCivil.ToList();
                foreach (EstadoCivil item in listaEst)
                {
                    if (p1.estadoCivilID == item.idEstadoCivil)
                    {
                        est.idEstadoCivil = item.idEstadoCivil;
                        est = item;
                    }

                }

            }

            return est;
        }


        #endregion

        #region Sexo
        public IList<Sexo> GetSexo()
        {
            IList<Sexo> list = new List<Sexo>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.Sexo.ToList();
            }

            return list;
        }


        public Sexo GetSexoById(Persona p1)
        {
            Sexo sex = new Sexo();
            IList<Sexo> listaSexo = new List<Sexo>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listaSexo = db.Sexo.ToList();
                foreach (Sexo item in listaSexo)
                {
                    if (p1.sexoID == item.idSexo)
                    {
                        sex.idSexo = item.idSexo;
                        sex = item;
                    }

                }

            }

            return sex;
        }

        #endregion



        public bool GuardarPersona(TextBox nombre, TextBox apellido, TextBox dni, DateTimePicker fechaNac, TextBox nacionalidad, TextBox domicilio, ComboBox disponibilidad, ComboBox estadoCivil, CheckBox hijos, TextBox cantidadHijos, ComboBox sexo, TextBox telCelular, TextBox telFijo, TextBox telAlternativo, TextBox mail, byte[] fileFoto)
        {
            Persona per = new Persona();
            bool r = false;

            IList<EstadoCivil> listaE = new List<EstadoCivil>();
            IList<Sexo> listS = new List<Sexo>();

            try
            {
                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {
                    per.nombre = nombre.Text;
                    per.apellido = apellido.Text;
                    per.dni = dni.Text;
                    per.fechaNacimiento = DateTime.Parse(fechaNac.Text);
                    per.nacionalidad = nacionalidad.Text;
                    per.domicilio = domicilio.Text;
                    foreach (Disponibilidad disponible in db.Disponibilidad)
                    {
                        if (disponibilidad.Text == disponible.estadoDisponibilidad)
                        {
                            per.disponibilidadID = disponible.idDisponibilidad;
                            per.Disponibilidad = disponible;
                        }

                    }

                    listaE = db.EstadoCivil.ToList();
                    foreach (EstadoCivil estadoC in listaE)
                    {
                        if (estadoCivil.Text == estadoC.nombreEstadoCivil)
                        {
                            per.estadoCivilID = estadoC.idEstadoCivil;
                            per.EstadoCivil = estadoC;
                        }

                    }
                    if (hijos.Checked)
                    {
                        per.hijos = true;
                        per.cantHijos = int.Parse(cantidadHijos.Text);
                    }
                    else
                    {
                        per.hijos = false;
                    }
                    listS = db.Sexo.ToList();
                    foreach (Sexo s in listS)
                    {
                        if (sexo.Text == s.nombreSexo)
                        {
                            per.sexoID = s.idSexo;
                            per.Sexo = s;
                        }
                    }
                    per.telefono = telCelular.Text;
                    per.telFijo = telFijo.Text;
                    per.telAlternativo = telAlternativo.Text;
                    per.mail = mail.Text;
                    per.foto = fileFoto;
                    if (nombre.Text == "" || apellido.Text == "" || dni.Text == "" || nacionalidad.Text == "" || domicilio.Text == "" || disponibilidad.Text == "Seleccione una opción" || estadoCivil.Text == "Seleccione una opción" || sexo.Text == "Seleccione una opción" || telCelular.Text == "" || mail.Text == "")
                    {
                        MessageBox.Show("Complete todos los campos obligatorios.");
                        r = false;
                    }
                    else
                    {
                        per.activa = true;
                        db.Persona.Add(per);
                        if (db.SaveChanges() == 1)
                        {
                            r = true;
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                string m = ex.Message;
            }
            return r;
        }


        public Persona BuscarPersona(TextBox dni)
        {
            Persona postEncontrado = new Persona();

            IList<Persona> list = new List<Persona>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.Persona.ToList();
            }

            foreach (Persona item in list)
            {
                if (dni.Text == item.dni)
                {
                    postEncontrado.idPersona = item.idPersona;
                    postEncontrado = item;
                }

            }
            return postEncontrado;
        }

        public Persona GetPersonaById(Postulante p1)
        {
            Persona est = new Persona();
            IList<Persona> listaEst = new List<Persona>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listaEst = db.Persona.ToList();
                foreach (Persona item in listaEst)
                {
                    if (p1.Persona_Id == item.idPersona)
                    {
                        est.idPersona = item.idPersona;
                        est = item;
                    }

                }

            }

            return est;
        }
    }
}
