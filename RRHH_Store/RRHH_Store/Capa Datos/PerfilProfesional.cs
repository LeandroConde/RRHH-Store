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
    
    public partial class PerfilProfesional
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PerfilProfesional()
        {
            this.Perfil = new HashSet<Perfil>();
        }
    
        public long idProfesional { get; set; }
        public string LinkedIn { get; set; }
        public Nullable<bool> Experiencia { get; set; }
        public Nullable<int> AñosExperiencia { get; set; }
        public string Lugar_Empresa { get; set; }
        public string DescripcionPuesto { get; set; }
        public Nullable<int> PeriodoInicio { get; set; }
        public Nullable<int> PeriodoFin { get; set; }
        public string nombreArchivo { get; set; }
        public byte[] cv { get; set; }
        public Nullable<int> puntaje { get; set; }
        public Nullable<bool> activo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Perfil> Perfil { get; set; }
    }
}
