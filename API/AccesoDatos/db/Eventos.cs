namespace AccesoDatos.db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Eventos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Eventos()
        {
            Evento_Asistentes = new HashSet<Evento_Asistentes>();
        }

        [Key]
        public int Id_Evento { get; set; }

        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(2000)]
        public string Descripcion { get; set; }

        public DateTime? FechaHora { get; set; }

        [StringLength(500)]
        public string Ubicacion { get; set; }

        public int? Capacidad { get; set; }

        public int? Id_UsuarioCreador { get; set; }

        public bool? Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evento_Asistentes> Evento_Asistentes { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
