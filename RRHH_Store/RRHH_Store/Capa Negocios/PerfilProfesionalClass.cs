using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RRHH_Store.Capa_Datos;

namespace RRHH_Store.Capa_Negocios
{
    public class PerfilProfesionalClass
    {
        public IList<PerfilProfesional> GetPerfilProfesional()
        {
            IList<PerfilProfesional> listP = new List<PerfilProfesional>();
            IList<PerfilProfesional> lista = new List<PerfilProfesional>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listP = db.PerfilProfesional.ToList();
                foreach (PerfilProfesional prof in listP)
                {
                    if (prof.activo == true)
                    {
                        lista.Add(prof);
                    }
                }
            }

            return lista;
        }

        public PerfilProfesional GetPerfilProfesionalById(Perfil p1)
        {
            PerfilProfesional prof = new PerfilProfesional();
            IList<PerfilProfesional> listadisp = new List<PerfilProfesional>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listadisp = db.PerfilProfesional.ToList();
                foreach (PerfilProfesional item in listadisp)
                {
                    if (p1.PerfilProfesional_id == item.idProfesional)
                    {
                        prof.idProfesional = item.idProfesional;
                        prof = item;
                    }
                }
            }
            return prof;
        }




        public bool GuardarPerfilProfesional(TextBox linkedIn, CheckBox experiencia, TextBox añosExp, TextBox empresa, TextBox puesto, TextBox desde, TextBox hasta, TextBox nombre, byte[] file)
        {
            bool r = false;
            PerfilProfesional profesional = new PerfilProfesional();
            try
            {
                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {
                    profesional.LinkedIn = linkedIn.Text;

                    if (experiencia.Checked == true)
                    {
                        profesional.Experiencia = true;
                        profesional.AñosExperiencia = int.Parse(añosExp.Text);
                        profesional.Lugar_Empresa = empresa.Text;
                        profesional.DescripcionPuesto = puesto.Text;
                        profesional.PeriodoInicio = int.Parse(desde.Text);
                        profesional.PeriodoFin = int.Parse(hasta.Text);
                        profesional.puntaje = 30;
                        if(empresa.Text == ""|| puesto.Text == "" ||desde.Text == "" ||hasta.Text == "")
                        {
                            MessageBox.Show("Complete todos los campos.");
                        }
                    }
                    else
                    {
                        profesional.Experiencia = false;
                        profesional.puntaje = 0;
                    }
                    profesional.nombreArchivo = nombre.Text;
                    profesional.cv = file;
                    if (nombre.Text == "" || file == null)
                    {
                        MessageBox.Show("Complete todos los campos.");
                        r = false;
                    }
                    else
                    {
                        profesional.activo = true;
                        db.PerfilProfesional.Add(profesional);
                        if (db.SaveChanges() == 1)
                        {
                            r = true; ;
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


        public bool GuardarPerfilProfesionalVacio(TextBox linkedIn, CheckBox experiencia, TextBox añosExp, TextBox empresa, TextBox puesto, TextBox desde, TextBox hasta, TextBox nombre, byte[] file)
        {
            bool r = false;
            PerfilProfesional profesional = new PerfilProfesional();
            try
            {
                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {
                    profesional.LinkedIn = linkedIn.Text;

                    if (experiencia.Checked == true)
                    {
                        profesional.Experiencia = true;
                        profesional.AñosExperiencia = int.Parse(añosExp.Text);
                        profesional.Lugar_Empresa = empresa.Text;
                        profesional.DescripcionPuesto = puesto.Text;
                        profesional.PeriodoInicio = int.Parse(desde.Text);
                        profesional.PeriodoFin = int.Parse(hasta.Text);
                        profesional.puntaje = 30;
                      
                    }
                    else
                    {
                        profesional.Experiencia = false;
                        profesional.puntaje = 0;
                    }
                    profesional.nombreArchivo = nombre.Text;
                    profesional.cv = file;
                    profesional.activo = true;
                    db.PerfilProfesional.Add(profesional);
                    if (db.SaveChanges() == 1)
                    {
                        r = true; ;
                    }
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
            return r;
        }

    }
}
