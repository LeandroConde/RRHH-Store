//RRHH STORE
using System;
using System.Collections.Generic;
using System.Linq;
using RRHH_Store.Capa_Datos;
    

namespace RRHH_Store.Capa_Negocios
{
    public class AdministradorClass : Persona
    {
        public IList<Administrador> GetAllAdministrador()
        {
            IList<Administrador> list = new List<Administrador>();
            IList<Administrador> lista = new List<Administrador>();

            using (RRHH_STOREFINALEntities db = new RRHH_STOREFINALEntities())
            {
                list = db.Administrador.ToList();
                foreach (Administrador admin in list)
                {
                    if (admin.activo == true)
                    {
                        lista.Add(admin);
                    }
                }
            }

            return lista;
        }
    }

}
