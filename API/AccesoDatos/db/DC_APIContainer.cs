using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AccesoDatos.db
{
    public partial class DC_APIContainer : DbContext
    {
        public DC_APIContainer()
            : base("name=DC_APIContainer")
        {
        }

        public virtual DbSet<Evento_Asistentes> Evento_Asistentes { get; set; }
        public virtual DbSet<Eventos> Eventos { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Usuarios_TokenRefresh> Usuarios_TokenRefresh { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Eventos>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Eventos>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Eventos>()
                .Property(e => e.Ubicacion)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Eventos)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.Id_UsuarioCreador);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Usuarios_TokenRefresh)
                .WithRequired(e => e.Usuarios)
                .WillCascadeOnDelete(false);
        }
    }
}
