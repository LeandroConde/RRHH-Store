using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RRHH_Store.Capa_Datos;

namespace RRHH_Store.Capa_Negocios
{
    public class PostulanteClass
    {
        public IList<Postulante> GetPostulante()
        {
            IList<Postulante> listP = new List<Postulante>();
            IList<Postulante> lista = new List<Postulante>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listP = db.Postulante.ToList();
                foreach (Postulante post in listP)
                {
                    if (post.activo == true)
                    {
                        lista.Add(post);
                    }
                }
            }

            return listP;
        }

        public bool GuardarPostulante()
        {
            bool r = false;
            try
            {
                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {
                    Postulante postulante = new Postulante();
                    Persona persona = new Persona();
                    Perfil perf1 = new Perfil();
                    foreach (var item in db.Persona)
                    {
                        if (item.idPersona == db.Persona.Count())
                        {
                            persona = item;
                        }
                    }
                    foreach (var item in db.Perfil)
                    {
                        if (item.idPerfil == db.Perfil.Count())
                        {
                            perf1 = item;
                        }
                    }
                    postulante.fechaRegistro = DateTime.Today;
                    postulante.Persona_Id = persona.idPersona;
                    postulante.Persona = persona;
                    postulante.Perfil_id = perf1.idPerfil;
                    postulante.Perfil = perf1;
                    postulante.activo = true;

                    db.Postulante.Add(postulante);
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


        public Postulante GetPostulanteById(Persona p1)
        {
            Postulante post = new Postulante();
            IList<Postulante> list = new List<Postulante>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.Postulante.ToList();
            }

            foreach (Postulante item in list)
            {
                if (p1.idPersona == item.Persona_Id)
                {
                    post.Persona_Id = item.Persona_Id;
                    post = item;
                }
            }
            return post;
        }


        public IList<Postulante> BuscarPostulante(Disciplina disciplina, TipoPostulante tipo, EstadoPostulante estadoPost, bool experiencia)
        {
            PerfilClass t = new PerfilClass();
            IList<Postulante> listP = new List<Postulante>();
            Postulante ps = new Postulante();
            
                IList<Perfil> listPef = new List<Perfil>();
                listPef = t.BuscarPerfil(disciplina, tipo, estadoPost, experiencia);

                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {
                    foreach (Postulante item in db.Postulante)
                    {
                        foreach (Perfil pef in listPef)
                        {
                            if (item.Perfil_id == pef.idPerfil)
                            {
                                ps.idPostulante = item.idPostulante;
                                ps = item;
                                listP.Add(ps);
                            }
                        }
                    }

                }
            
            return listP;
        }

    }
}
