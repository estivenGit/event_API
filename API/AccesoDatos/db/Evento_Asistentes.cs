namespace AccesoDatos.db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Evento_Asistentes
    {
        [Key]
        public int Id_EventoAsistente { get; set; }

        public int? Id_Evento { get; set; }

        public int? Id_Usuario { get; set; }

        public virtual Eventos Eventos { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
