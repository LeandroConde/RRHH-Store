using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRHH_Store.Capa_Datos;
using System.Windows.Forms;

namespace RRHH_Store.Capa_Negocios
{
    public class PerfilAcademicoClass
    {
        public IList<PerfilAcademico> GetPerfilAcademico()
        {
            IList<PerfilAcademico> listA = new List<PerfilAcademico>();
            IList<PerfilAcademico> lista = new List<PerfilAcademico>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listA = db.PerfilAcademico.ToList();
                foreach (PerfilAcademico acad in listA)
                {
                    if (acad.activo == true)
                    {
                        lista.Add(acad);
                    }
                }
            }

            return lista;
        }


        public PerfilAcademico GetPerfilAcademicoById(Perfil p1)
        {
            PerfilAcademico academ = new PerfilAcademico();
            IList<PerfilAcademico> listadisp = new List<PerfilAcademico>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listadisp = db.PerfilAcademico.ToList();
                foreach (PerfilAcademico item in listadisp)
                {
                    if (p1.PerfilAcademico_id == item.idPerfilAcademico)
                    {
                        academ.idPerfilAcademico = item.idPerfilAcademico;
                        academ = item;
                    }

                }

            }

            return academ;
        }

        public IList<NivelEducativo> GetAllNivel()
        {
            IList<NivelEducativo> listNivel = new List<NivelEducativo>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listNivel = db.NivelEducativo.ToList();
            }

            return listNivel;
        }


        public NivelEducativo GetNivelAcademicoById(PerfilAcademico p1)
        {
            NivelEducativo disp = new NivelEducativo();
            IList<NivelEducativo> listadisp = new List<NivelEducativo>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listadisp = db.NivelEducativo.ToList();
                foreach (NivelEducativo item in listadisp)
                {
                    if (p1.Academico_NivelID == item.id_Nivel)
                    {
                        disp.id_Nivel = item.id_Nivel;
                        disp = item;
                    }

                }

            }

            return disp;
        }

        public IList<EstadoNivelEduc> GetAllEstadoNivelEduc()
        {
            IList<EstadoNivelEduc> listEstadoNivelEduc = new List<EstadoNivelEduc>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listEstadoNivelEduc = db.EstadoNivelEduc.ToList();
            }

            return listEstadoNivelEduc;
        }


        public EstadoNivelEduc GetEstadoNivelEducById(PerfilAcademico p1)
        {
            EstadoNivelEduc nivelEdu = new EstadoNivelEduc();
            IList<EstadoNivelEduc> listadisp = new List<EstadoNivelEduc>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listadisp = db.EstadoNivelEduc.ToList();
                foreach (EstadoNivelEduc item in listadisp)
                {
                    if (p1.estadoNivelID == item.idEstadoNivelEduc)
                    {
                        nivelEdu.idEstadoNivelEduc = item.idEstadoNivelEduc;
                        nivelEdu = item;
                    }

                }

            }

            return nivelEdu;
        }


        public bool GuardarPerfilAcademico(ComboBox nivelEdu, ComboBox estadoEdu, TextBox titulo, TextBox establecimiento, CheckBox cursos, TextBox nombre, byte[] file)
        {
            bool r = false;

            IList<NivelEducativo> listaNivel = new List<NivelEducativo>();
            IList<EstadoNivelEduc> listaEstN = new List<EstadoNivelEduc>();

            try
            {
                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {
                    PerfilAcademico academ = new PerfilAcademico();
                    listaNivel = db.NivelEducativo.ToList();
                    foreach (NivelEducativo nivel in listaNivel)
                    {
                        if (nivelEdu.Text == nivel.NombreNivel)
                        {
                            academ.Academico_NivelID = nivel.id_Nivel;
                            academ.NivelEducativo = nivel;
                        }

                    }

                    listaEstN = db.EstadoNivelEduc.ToList();
                    foreach (EstadoNivelEduc estadoN in listaEstN)
                    {
                        if (estadoEdu.Text == estadoN.nombreEstadoNivelEduc)
                        {
                            academ.estadoNivelID = estadoN.idEstadoNivelEduc;
                            academ.EstadoNivelEduc = estadoN;
                        }

                    }
                    academ.Titulo = titulo.Text;
                    academ.Establecimiento = establecimiento.Text;
                    if (cursos.Checked)
                    {
                        academ.cursosRelacionados = true;
                        academ.puntaje = 30;
                        academ.nombreArchivo = nombre.Text;
                        academ.certificacion = file;
                        if(nombre.Text=="")
                        {
                            MessageBox.Show("Complete el nombre del archivo");
                        }
                    }
                    else
                    {
                        academ.cursosRelacionados = false;
                        academ.puntaje = 0;
                    }
                    
                    if (nivelEdu.Text == "Seleccione una opción"||estadoEdu.Text == "Seleccione una opción" || titulo.Text == " " || establecimiento.Text == " ")
                    {
                        MessageBox.Show("Complete todos los campos obligatorios.");
                        r = false;
                    }
                    else
                    {
                        academ.activo = true;
                        db.PerfilAcademico.Add(academ);
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


        public bool GuardarPerfilAcademicoVacio(ComboBox nivelEdu, ComboBox estadoEdu, TextBox titulo, TextBox establecimiento, CheckBox cursos, TextBox nombre, byte[] file)
        {
            bool r = false;

            IList<NivelEducativo> listaNivel = new List<NivelEducativo>();
            IList<EstadoNivelEduc> listaEstN = new List<EstadoNivelEduc>();

            try
            {
                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {
                    PerfilAcademico academ = new PerfilAcademico();
                    listaNivel = db.NivelEducativo.ToList();
                    foreach (NivelEducativo nivel in listaNivel)
                    {
                        if (nivelEdu.Text == nivel.NombreNivel)
                        {
                            academ.Academico_NivelID = nivel.id_Nivel;
                            academ.NivelEducativo = nivel;
                        }

                    }

                    listaEstN = db.EstadoNivelEduc.ToList();
                    foreach (EstadoNivelEduc estadoN in listaEstN)
                    {
                        if (estadoEdu.Text == estadoN.nombreEstadoNivelEduc)
                        {
                            academ.estadoNivelID = estadoN.idEstadoNivelEduc;
                            academ.EstadoNivelEduc = estadoN;
                        }

                    }
                    academ.Titulo = titulo.Text;
                    academ.Establecimiento = establecimiento.Text;
                    if (cursos.Checked)
                    {
                        academ.cursosRelacionados = true;
                        academ.puntaje = 30;
                        academ.nombreArchivo = nombre.Text;
                        academ.certificacion = file;
                    }
                    else
                    {
                        academ.cursosRelacionados = false;
                        academ.puntaje = 0;
                    }
                    academ.activo = true;

                    db.PerfilAcademico.Add(academ);
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
