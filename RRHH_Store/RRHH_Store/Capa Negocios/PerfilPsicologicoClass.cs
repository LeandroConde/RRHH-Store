using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RRHH_Store.Capa_Datos;



namespace RRHH_Store.Capa_Negocios
{
    public class  PerfilPsicologicoClass
    {
        public IList<PerfilPsicologico> GetPerfilPsicologico()
        {
            IList<PerfilPsicologico> list = new List<PerfilPsicologico>();
            IList<PerfilPsicologico> lista = new List<PerfilPsicologico>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.PerfilPsicologico.ToList();
                foreach (PerfilPsicologico psi in list)
                {
                    if (psi.activo == true)
                    {
                        lista.Add(psi);
                    }
                }
            }

            return lista;
        }

        public PerfilPsicologico GetPerfilPsicologicoById(Perfil p1)
        {
            PerfilPsicologico psic = new PerfilPsicologico();
            IList<PerfilPsicologico> listadisp = new List<PerfilPsicologico>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listadisp = db.PerfilPsicologico.ToList();
                foreach (PerfilPsicologico item in listadisp)
                {
                    if (p1.PerfilPsicologico_id == item.idPsicologico)
                    {
                        psic.idPsicologico = item.idPsicologico;
                        psic = item;
                    }

                }

            }

            return psic;
        }
       
        public bool GuardarPerfilPsicologico(CheckBox apto, TextBox descripcion)
        {
            bool r = false;
            Perfil pef = new Perfil();

            try
            {
                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {
                    PerfilPsicologico psicologico = new PerfilPsicologico();
                    if (apto.Checked)
                    {
                        psicologico.apto = true;
                        psicologico.puntaje = 40;
                    }
                    else
                    {
                        psicologico.apto = false;
                        psicologico.puntaje = 0;
                    }
                    psicologico.descripcion = descripcion.Text;
                    if (descripcion.Text == "")
                    {
                        MessageBox.Show("Ingrese una breve introducción al perfil del postulante.");
                        r = false;
                    }
                    else
                    {
                        psicologico.activo = true;
                        db.PerfilPsicologico.Add(psicologico);
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

        public bool GuardarPerfilPsicologicoVacio(CheckBox apto, TextBox descripcion)
        {
            bool r = false;
            Perfil pef = new Perfil();

            try
            {
                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {
                    PerfilPsicologico psicologico = new PerfilPsicologico();
                    if (apto.Checked)
                    {
                        psicologico.apto = true;
                        psicologico.puntaje = 40;
                    }
                    else
                    {
                        psicologico.apto = false;
                        psicologico.puntaje = 0;
                    }
                    psicologico.descripcion = descripcion.Text;
                    psicologico.activo = true;
                    db.PerfilPsicologico.Add(psicologico);
                    if (db.SaveChanges() == 1)
                    {
                        r = true;
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
