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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RRHH_STOREFINALEntities : DbContext
    {
        public RRHH_STOREFINALEntities()
            : base("name=RRHH_STOREFINALEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Disciplina> Disciplina { get; set; }
        public virtual DbSet<Disponibilidad> Disponibilidad { get; set; }
        public virtual DbSet<EstadoCivil> EstadoCivil { get; set; }
        public virtual DbSet<EstadoNivelEduc> EstadoNivelEduc { get; set; }
        public virtual DbSet<EstadoPostulante> EstadoPostulante { get; set; }
        public virtual DbSet<NivelEducativo> NivelEducativo { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<PerfilAcademico> PerfilAcademico { get; set; }
        public virtual DbSet<PerfilProfesional> PerfilProfesional { get; set; }
        public virtual DbSet<PerfilPsicologico> PerfilPsicologico { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Postulante> Postulante { get; set; }
        public virtual DbSet<Sexo> Sexo { get; set; }
        public virtual DbSet<TipoPostulante> TipoPostulante { get; set; }
    }
}
