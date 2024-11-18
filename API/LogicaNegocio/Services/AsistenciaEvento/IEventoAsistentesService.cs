using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Models;

namespace LogicaNegocio.Services.AsistenciaEvento
{
    public interface IEventoAsistentesService
    {

        Task CreateAsync(EventoAsistenteModel asistenciaEvento);
        Task DeleteEventAsync(EventoAsistenteModel evento);
        Task<int> CantidadAsistentesByEvent(int IdEvent);
        Task<int> CantidadAsistentesByUser(int IdUser);
        Task<bool> usuarioEventoAsistencia(int idUser, int idEvent);

    }
}
