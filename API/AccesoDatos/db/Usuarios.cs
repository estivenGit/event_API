namespace AccesoDatos.db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            Evento_Asistentes = new HashSet<Evento_Asistentes>();
            Eventos = new HashSet<Eventos>();
            Usuarios_TokenRefresh = new HashSet<Usuarios_TokenRefresh>();
        }

        [Key]
        public int Id_Usuario { get; set; }

        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Pass { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        public bool? Activo { get; set; }

        [StringLength(100)]
        public string Usuario { get; set; }

        public int? Id_Rol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evento_Asistentes> Evento_Asistentes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Eventos> Eventos { get; set; }

        public virtual Rol Rol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuarios_TokenRefresh> Usuarios_TokenRefresh { get; set; }
    }
}
