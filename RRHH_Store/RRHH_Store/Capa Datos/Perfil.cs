//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RRHH_Store.Capa_Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Perfil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Perfil()
        {
            this.Postulante = new HashSet<Postulante>();
        }
    
        public long idPerfil { get; set; }
        public Nullable<long> disciplinaID { get; set; }
        public Nullable<long> tipoPostulanteID { get; set; }
        public Nullable<long> estadoPostulanteID { get; set; }
        public Nullable<int> PuntuaciónTotal { get; set; }
        public Nullable<bool> activo { get; set; }
        public Nullable<long> PerfilPsicologico_id { get; set; }
        public Nullable<long> PerfilAcademico_id { get; set; }
        public Nullable<long> PerfilProfesional_id { get; set; }
    
        public virtual Disciplina Disciplina { get; set; }
        public virtual EstadoPostulante EstadoPostulante { get; set; }
        public virtual PerfilAcademico PerfilAcademico { get; set; }
        public virtual PerfilProfesional PerfilProfesional { get; set; }
        public virtual PerfilPsicologico PerfilPsicologico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Postulante> Postulante { get; set; }
        public virtual TipoPostulante TipoPostulante { get; set; }
    }
}