using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Models
{
    public class EventoModel
    {
        public int IdEvento { get; set; } 
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public int CantidadAsistentes { get; set; }
        public bool userAsistente { get; set; }
        public int IdUsuarioCreador { get; set; } 
        public bool Activo { get; set; }
        public bool esCreador { get; set; }


        public virtual ICollection<EventoAsistenteModel> Asistentes { get; set; } = new List<EventoAsistenteModel>();
    }
}
