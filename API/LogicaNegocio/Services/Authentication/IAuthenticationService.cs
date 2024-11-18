using AccesoDatos.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Transversal.Models;

namespace LogicaNegocio.Services.Authentication
{
    /// <summary>
    /// Interfaz que define los métodos de autenticación.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary> 
        /// Método que permite realizar la autenticación de un usuario de forma asíncrona.
        /// </summary>
        /// <param name="credentials">Credenciales del usuario a autenticar.</param>
        /// <returns>Verdadero o falso</returns>
        Task<UserSession> LoginAsync(UserCredentials credentials);

        /// <summary>
        /// Método que permite realizar el cierre de autenticación del usuario.
        /// </summary>
        /// <param name="credentials">Credenciales del usuario ya autenticado.</param>
        /// <returns></returns>
        Task<bool> EndLogin(UserCredentials credentials);

        ClaimsIdentity SetClaimsIdentity(UserSession userSession, params object[] parameters);
        dynamic Sign(string token);

        Task<UserSession> renovarRefreshTokenAsync(UserSession userSession);
    }
}
