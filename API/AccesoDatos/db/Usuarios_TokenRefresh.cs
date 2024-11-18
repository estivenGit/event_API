namespace AccesoDatos.db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuarios_TokenRefresh
    {
        [Key]
        public int Id_TokenRefresh { get; set; }

        public int Id_Usuario { get; set; }

        [Required]
        [StringLength(500)]
        public string Token { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaExpiracion { get; set; }

        public DateTime? FechaRevocacion { get; set; }

        [StringLength(500)]
        public string TokenReemplazo { get; set; }

        public bool? EstaActivo { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
