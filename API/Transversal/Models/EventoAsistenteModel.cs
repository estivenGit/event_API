using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Models
{
    public class EventoAsistenteModel
    {
        public int IdEventoAsistente { get; set; } 
        public int IdEvento { get; set; } 
        public int IdUsuario { get; set; }

        public virtual EventoModel Evento { get; set; }
    }
}
