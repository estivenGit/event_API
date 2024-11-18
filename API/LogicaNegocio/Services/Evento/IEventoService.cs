using AccesoDatos.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Models;

namespace LogicaNegocio.Services.Evento
{
    public interface IEventoService
    {
        Task CreateEventAsync(EventoModel evento);
        Task UpdateEventAsync(EventoModel evento);
        Task DeleteEventAsync(EventoModel evento);
        Task<EventoModel> GetEventByIdAsync(int id);
        Task<IEnumerable<EventoModel>> GetAllEventsAsync(int id);
    }
}
