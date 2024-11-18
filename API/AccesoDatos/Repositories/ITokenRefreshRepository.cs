using AccesoDatos.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Models;

namespace AccesoDatos.Repositories
{
    public interface ITokenRefreshRepository
    {
        Task CreateAsync(Usuarios_TokenRefresh entity);
        Task updateTokenRefreshByUserAsync(Usuarios user, string newToken);
        Task<bool> validarRefreshTokenAsync(int userID, string tokenRefresh);
    }
}
