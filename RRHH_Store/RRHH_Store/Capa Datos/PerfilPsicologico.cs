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
    
    public partial class PerfilPsicologico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PerfilPsicologico()
        {
            this.Perfil = new HashSet<Perfil>();
        }
    
        public long idPsicologico { get; set; }
        public Nullable<bool> apto { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> puntaje { get; set; }
        public Nullable<bool> activo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Perfil> Perfil { get; set; }
    }
}
