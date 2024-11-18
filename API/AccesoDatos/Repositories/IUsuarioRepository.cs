using AccesoDatos.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Models;

namespace AccesoDatos.Repositories
{
    public interface IUsuarioRepository 
    {

        Task<Usuarios> LoginAsync(UserCredentials usuario);
        Task<Usuarios> GetByIdAsync(int id);

    }
}
