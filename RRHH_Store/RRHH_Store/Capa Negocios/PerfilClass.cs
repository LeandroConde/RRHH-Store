using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RRHH_Store.Capa_Datos;
    

namespace RRHH_Store.Capa_Negocios
{
    public class PerfilClass
    {
        public IList<Perfil> GetPerfil()
        {
            IList<Perfil> list = new List<Perfil>();
            IList<Perfil> lista = new List<Perfil>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.Perfil.ToList();

                foreach (Perfil perf in list)
                {
                    if (perf.activo == true)
                    {
                        lista.Add(perf);
                    }
                }
            }

            return lista;
        }


        #region Tipo Postulante
        public IList<TipoPostulante> GetTipoPostulante()
        {
            IList<TipoPostulante> list = new List<TipoPostulante>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.TipoPostulante.ToList();
            }

            return list;
        }
        public TipoPostulante GetTipoPostulanteById(Perfil p1)
        {
            TipoPostulante disp = new TipoPostulante();
            IList<TipoPostulante> listadisp = new List<TipoPostulante>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listadisp = db.TipoPostulante.ToList();
                foreach (TipoPostulante item in listadisp)
                {
                    if (p1.tipoPostulanteID == item.idTipoPostulante)
                    {
                        disp.idTipoPostulante = item.idTipoPostulante;
                        disp = item;
                    }

                }

            }

            return disp;
        }
        #endregion



        #region Estado Postulante
        public IList<EstadoPostulante> GetEstadoPostulante()
        {
            IList<EstadoPostulante> list = new List<EstadoPostulante>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.EstadoPostulante.ToList();
            }

            return list;
        }
        public EstadoPostulante GetEstadoPostulanteById(Perfil p1)
        {
            EstadoPostulante disp = new EstadoPostulante();
            IList<EstadoPostulante> listadisp = new List<EstadoPostulante>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listadisp = db.EstadoPostulante.ToList();
                foreach (EstadoPostulante item in listadisp)
                {
                    if (p1.estadoPostulanteID == item.idEstadoPostulante)
                    {
                        disp.idEstadoPostulante = item.idEstadoPostulante;
                        disp = item;
                    }

                }

            }

            return disp;
        }
        #endregion




        #region Disciplina
        public IList<Disciplina> GetDisciplina()
        {
            IList<Disciplina> list = new List<Disciplina>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.Disciplina.ToList();
            }
            return list;
        }
        public Disciplina GetDisciplinaById(Perfil perfil1)
        {
            Disciplina disc = new Disciplina();
            IList<Disciplina> listadisc = new List<Disciplina>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                listadisc = db.Disciplina.ToList();
                foreach (Disciplina item in listadisc)
                {
                    if (perfil1.disciplinaID == item.idDisciplina)
                    {
                        disc.idDisciplina = item.idDisciplina;
                        disc = item;
                    }

                }

            }

            return disc;
        }
        #endregion

        public bool GuardarPerfilGeneral(ComboBox tipoPostulante, ComboBox estadoPostulante, Label puntuacion, ComboBox disciplina)
        {
            bool r = false;

            IList<TipoPostulante> listaTipoP = new List<TipoPostulante>();
            IList<EstadoPostulante> listaEstadoP = new List<EstadoPostulante>();

            try
            {
                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {

                    PerfilAcademico pAcad = new PerfilAcademico();
                    PerfilPsicologico pPsic = new PerfilPsicologico();
                    PerfilProfesional pProf = new PerfilProfesional();
                    Perfil perfil = new Perfil();
                    listaTipoP = db.TipoPostulante.ToList();
                    foreach (TipoPostulante tipo in listaTipoP)
                    {
                        if (tipoPostulante.Text == tipo.nombreTipoPostulante)
                        {
                            perfil.tipoPostulanteID = tipo.idTipoPostulante;
                            perfil.TipoPostulante = tipo;
                        }
                    }
                    listaEstadoP = db.EstadoPostulante.ToList();
                    foreach (EstadoPostulante estado in listaEstadoP)
                    {
                        if (estadoPostulante.Text == estado.nombreEstadoPostulante)
                        {
                            perfil.estadoPostulanteID = estado.idEstadoPostulante;
                            perfil.EstadoPostulante = estado;
                        }
                    }
                    foreach (Disciplina disc in db.Disciplina)
                    {
                        if (disciplina.Text == disc.nombreDisciplina)
                        {
                            perfil.disciplinaID = disc.idDisciplina;
                            perfil.Disciplina = disc;
                        }

                    }

                    #region agregacion de los perfiles

                    foreach (var item in db.PerfilAcademico)
                    {
                        if (item.idPerfilAcademico == db.PerfilAcademico.Count())
                        {
                            pAcad = item;
                        }
                    }
                    perfil.PerfilAcademico_id = pAcad.idPerfilAcademico;
                    perfil.PerfilAcademico = pAcad;

                    foreach (var item in db.PerfilPsicologico)
                    {
                        if (item.idPsicologico == db.PerfilPsicologico.Count())
                        {
                            pPsic = item;
                        }
                    }
                    perfil.PerfilPsicologico_id = pPsic.idPsicologico;
                    perfil.PerfilPsicologico = pPsic;

                    foreach (var item in db.PerfilProfesional)
                    {
                        if (item.idProfesional == db.PerfilProfesional.Count())
                        {
                            pProf = item;
                        }
                    }
                    perfil.PerfilProfesional_id = pProf.idProfesional;
                    perfil.PerfilProfesional = pProf;
                    #endregion
                    perfil.PuntuaciónTotal = pPsic.puntaje + pProf.puntaje + pAcad.puntaje;
                    puntuacion.Text = (perfil.PuntuaciónTotal).ToString();
                    if(tipoPostulante.Text == "Seleccione una opción" || estadoPostulante.Text == "Seleccione una opción" || disciplina.Text == "Seleccione una opción")
                    {
                        MessageBox.Show("Complete todos los campos obligatorios.");
                        r = false;
                    }
                    else
                    {
                        perfil.activo = true;
                        db.Perfil.Add(perfil);

                        if (db.SaveChanges() == 1)
                        {
                            r = true;
                        }
                    }   
                }
            }

            catch (Exception e)
            {
                string mensaje = e.Message;
            }
            return r;
        }

        public Perfil GetPerfilById(Postulante p2)
        {
            Perfil perfil2 = new Perfil();
            IList<Perfil> list = new List<Perfil>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.Perfil.ToList();
                foreach (Perfil item in list)
                {
                    if (p2.Perfil_id == item.idPerfil)
                    {
                        perfil2.idPerfil = item.idPerfil;
                        perfil2 = item;
                    }

                }

            }

            return perfil2;
        }



        public IList<Perfil> BuscarPerfil(Disciplina disciplina, TipoPostulante tipo, EstadoPostulante estado, bool experiencia)
        {
            IList<Perfil> list = new List<Perfil>();
          
            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                Perfil perf = new Perfil();
                foreach (var item in db.Perfil)
                {
                    #region Selección de DOS comboBox

                    //en caso de que se seleccionó disciplina y tipo postulante
                    if (disciplina.nombreDisciplina != null && tipo.nombreTipoPostulante != null && estado.nombreEstadoPostulante == null)
                    {
                        if (experiencia == false)
                        {
                            if (item.Disciplina != null && item.TipoPostulante != null)
                            {
                                if (item.Disciplina.nombreDisciplina == disciplina.nombreDisciplina && item.TipoPostulante.nombreTipoPostulante == tipo.nombreTipoPostulante)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (item.Disciplina != null && item.TipoPostulante != null)
                            {
                                if (item.Disciplina.nombreDisciplina == disciplina.nombreDisciplina && item.TipoPostulante.nombreTipoPostulante == tipo.nombreTipoPostulante && item.PerfilProfesional.Experiencia == true)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                    }
                    //en caso de que se seleccionó disciplina y estado postualante
                    if (disciplina.nombreDisciplina != null && estado.nombreEstadoPostulante != null && tipo.nombreTipoPostulante == null)
                    {
                        if (experiencia == false)
                        {
                            if (item.Disciplina != null && item.EstadoPostulante != null)
                            {
                                if (item.Disciplina.nombreDisciplina == disciplina.nombreDisciplina && item.EstadoPostulante.nombreEstadoPostulante == estado.nombreEstadoPostulante)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (item.Disciplina != null && item.EstadoPostulante != null)
                             {
                                 if (item.Disciplina.nombreDisciplina == disciplina.nombreDisciplina && item.EstadoPostulante.nombreEstadoPostulante == estado.nombreEstadoPostulante && item.PerfilProfesional.Experiencia == true)
                                    {
                                        list.Add(item);
                                    }
                             }
                        }

                    }
                    //en caso de que se seleccionó tipo y estado
                    if (tipo.nombreTipoPostulante != null && estado.nombreEstadoPostulante != null && disciplina.nombreDisciplina == null)
                    {
                        if (experiencia == false)
                        {
                            if (item.TipoPostulante != null && item.EstadoPostulante != null)
                            {
                                if (item.TipoPostulante.nombreTipoPostulante == tipo.nombreTipoPostulante && item.EstadoPostulante.nombreEstadoPostulante == estado.nombreEstadoPostulante)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (item.TipoPostulante != null && item.EstadoPostulante != null)
                            {
                                if (item.TipoPostulante.nombreTipoPostulante == tipo.nombreTipoPostulante && item.EstadoPostulante.nombreEstadoPostulante == estado.nombreEstadoPostulante && item.PerfilProfesional.Experiencia == true)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                    }

                    #endregion

                    #region Selección de 1 combobox

                    //en caso de que se seleccionó solamente tipo
                    if (tipo.nombreTipoPostulante != null && estado.nombreEstadoPostulante == null && disciplina.nombreDisciplina == null)
                    {
                        if (experiencia == false)
                        {
                            if (item.TipoPostulante != null)
                            {
                                if (item.TipoPostulante.nombreTipoPostulante == tipo.nombreTipoPostulante)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (item.TipoPostulante != null)
                            {
                                if (item.TipoPostulante.nombreTipoPostulante == tipo.nombreTipoPostulante && item.PerfilProfesional.Experiencia == true)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                    }
                   
                    //en caso de que se seleccionó solamente estado
                    if (tipo.nombreTipoPostulante == null && estado.nombreEstadoPostulante != null && disciplina.nombreDisciplina == null)
                    {
                        if (experiencia == false)
                        {
                            if (item.EstadoPostulante != null)
                            {
                                if (item.EstadoPostulante.nombreEstadoPostulante == estado.nombreEstadoPostulante)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (item.EstadoPostulante != null)
                            {
                                if (item.EstadoPostulante.nombreEstadoPostulante == estado.nombreEstadoPostulante && item.PerfilProfesional.Experiencia == true)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                    }

                    //en caso de que se selecciono solamente disciplina
                  
                        if (tipo.nombreTipoPostulante == null && estado.nombreEstadoPostulante == null && disciplina.nombreDisciplina != null)
                        {
                            if (experiencia == false)
                            {
                               if (item.Disciplina != null)
                               {
                                  if (item.Disciplina.nombreDisciplina == disciplina.nombreDisciplina)
                                  {
                                    list.Add(item);
                                  }
                               }
                            }
                            else
                            {
                              if (item.Disciplina != null)
                               {
                                 if (item.Disciplina.nombreDisciplina == disciplina.nombreDisciplina && item.PerfilProfesional.Experiencia == true)
                                   {
                                    list.Add(item);
                                   }
                              }
                            }
                        
                    }
                   
                    #endregion


                    //en caso de que se seleccione todo
                    if (tipo.nombreTipoPostulante != null && estado.nombreEstadoPostulante != null && disciplina.nombreDisciplina != null)
                    {
                        if (experiencia == false)
                        {
                            if (item.Disciplina != null && item.TipoPostulante != null && item.EstadoPostulante != null)
                            {
                                if (item.Disciplina.nombreDisciplina == disciplina.nombreDisciplina && item.TipoPostulante.nombreTipoPostulante == tipo.nombreTipoPostulante && item.EstadoPostulante.nombreEstadoPostulante == estado.nombreEstadoPostulante)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (item.Disciplina != null && item.TipoPostulante != null && item.EstadoPostulante != null)
                            {
                                if (item.Disciplina.nombreDisciplina == disciplina.nombreDisciplina && item.TipoPostulante.nombreTipoPostulante == tipo.nombreTipoPostulante && item.EstadoPostulante.nombreEstadoPostulante == estado.nombreEstadoPostulante && item.PerfilProfesional.Experiencia == true)
                                {
                                    list.Add(item);
                                }
                            }
                        }

                    }
                    //en caso q se selecciono solo el checkBox Experiencia
                    if (tipo.nombreTipoPostulante == null && estado.nombreEstadoPostulante == null && disciplina.nombreDisciplina == null && experiencia == true)
                    {
                        if (item.PerfilProfesional.Experiencia != null)
                        { 
                            if (item.PerfilProfesional.Experiencia == experiencia)
                            {
                                list.Add(item);
                            }
                        }
                    }



                }

            }

            return list;
        }


        public bool GuardarPerfilGeneralVacio(ComboBox tipoPostulante, ComboBox estadoPostulante, Label puntuacion, ComboBox disciplina)
        {
            bool r = false;

            IList<TipoPostulante> listaTipoP = new List<TipoPostulante>();
            IList<EstadoPostulante> listaEstadoP = new List<EstadoPostulante>();

            try
            {
                using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
                {

                    PerfilAcademico pAcad = new PerfilAcademico();
                    PerfilPsicologico pPsic = new PerfilPsicologico();
                    PerfilProfesional pProf = new PerfilProfesional();
                    Perfil perfil = new Perfil();
                    listaTipoP = db.TipoPostulante.ToList();
                    foreach (TipoPostulante tipo in listaTipoP)
                    {
                        if (tipoPostulante.Text == tipo.nombreTipoPostulante)
                        {
                            perfil.tipoPostulanteID = tipo.idTipoPostulante;
                            perfil.TipoPostulante = tipo;
                        }
                    }
                    listaEstadoP = db.EstadoPostulante.ToList();
                    foreach (EstadoPostulante estado in listaEstadoP)
                    {
                        if (estadoPostulante.Text == estado.nombreEstadoPostulante)
                        {
                            perfil.estadoPostulanteID = estado.idEstadoPostulante;
                            perfil.EstadoPostulante = estado;
                        }
                    }
                    foreach (Disciplina disc in db.Disciplina)
                    {
                        if (disciplina.Text == disc.nombreDisciplina)
                        {
                            perfil.disciplinaID = disc.idDisciplina;
                            perfil.Disciplina = disc;
                        }

                    }

                    #region agregacion de los perfiles

                    foreach (var item in db.PerfilAcademico)
                    {
                        if (item.idPerfilAcademico == db.PerfilAcademico.Count())
                        {
                            pAcad = item;
                        }
                    }
                    perfil.PerfilAcademico_id = pAcad.idPerfilAcademico;
                    perfil.PerfilAcademico = pAcad;

                    foreach (var item in db.PerfilPsicologico)
                    {
                        if (item.idPsicologico == db.PerfilPsicologico.Count())
                        {
                            pPsic = item;
                        }
                    }
                    perfil.PerfilPsicologico_id = pPsic.idPsicologico;
                    perfil.PerfilPsicologico = pPsic;

                    foreach (var item in db.PerfilProfesional)
                    {
                        if (item.idProfesional == db.PerfilProfesional.Count())
                        {
                            pProf = item;
                        }
                    }
                    perfil.PerfilProfesional_id = pProf.idProfesional;
                    perfil.PerfilProfesional = pProf;
                    #endregion
                    perfil.PuntuaciónTotal = pPsic.puntaje + pProf.puntaje + pAcad.puntaje;
                    puntuacion.Text = (perfil.PuntuaciónTotal).ToString();

                    perfil.activo = true;


                    db.Perfil.Add(perfil);

                    if (db.SaveChanges() == 1)
                    {
                        r = true;
                    }
                }
            }

            catch (Exception e)
            {
                string mensaje = e.Message;
            }
            return r;
        }

    }
}
