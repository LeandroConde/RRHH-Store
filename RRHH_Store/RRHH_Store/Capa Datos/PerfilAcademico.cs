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
    
    public partial class PerfilAcademico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PerfilAcademico()
        {
            this.Perfil = new HashSet<Perfil>();
        }
    
        public long idPerfilAcademico { get; set; }
        public Nullable<long> Academico_NivelID { get; set; }
        public Nullable<long> estadoNivelID { get; set; }
        public string Titulo { get; set; }
        public string Establecimiento { get; set; }
        public Nullable<bool> cursosRelacionados { get; set; }
        public string nombreArchivo { get; set; }
        public byte[] certificacion { get; set; }
        public Nullable<int> puntaje { get; set; }
        public Nullable<bool> activo { get; set; }
    
        public virtual EstadoNivelEduc EstadoNivelEduc { get; set; }
        public virtual NivelEducativo NivelEducativo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Perfil> Perfil { get; set; }
    }
}