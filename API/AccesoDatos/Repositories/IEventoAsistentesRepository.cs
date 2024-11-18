using AccesoDatos.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositories
{
    public interface IEventoAsistentesRepository : IGenericRepository<Evento_Asistentes>
    {
        Task<IEnumerable<Evento_Asistentes>> GetByFieldAsync(string fieldName, object value);
        Task<Evento_Asistentes> GetUsuarioEventoAsistencias(int idUser, int idEvent);
    }
}
