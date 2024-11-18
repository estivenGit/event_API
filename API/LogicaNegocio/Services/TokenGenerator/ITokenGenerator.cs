using AccesoDatos.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Services.TokenGenerator
{
    public interface ITokenGenerator
    {
        /// <summary>
        /// Método que permite generar el token JWT para la cuenta de usuario ingresado.
        /// </summary>
        /// <param name="claimsIdentityForUserSession">Función de retorno.</param>
        /// <returns>Token JWT</returns>
        string GenerateTokenJwt(Func<string, ClaimsIdentity> claimsIdentityForUserSession);

        /// <summary>
        /// Método para generar un refresh token.
        /// </summary>
        /// <param name="user">Usuario para generar el refresh token.</param>
        /// <returns>Refresh token</returns>
        Task<string> GenerateRefreshToken(Usuarios user);

        /// <summary>
        /// Método que configura el refresh token como cookie.
        /// </summary>
        /// <param name="user">Usuario para generar el refresh token.</param>
        /// <returns>Cadena de cookie</returns>
        Task<string> ConfigureCookieRefresh(Usuarios user);
    }
}
