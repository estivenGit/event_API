using AccesoDatos.db;
using AccesoDatos.Repositories;
using LogicaNegocio.Services.TokenGenerator;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Transversal.Models;

namespace LogicaNegocio.Services.Authentication
{
    /// <summary>
    /// Servicio para la autenticación y manejo de sesiones de usuarios.
    /// Proporciona métodos para iniciar sesión, renovar tokens y gestionar identidades de usuarios.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ITokenRefreshRepository _tokenRefreshRepository;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AuthenticationService"/>.
        /// </summary>
        /// <param name="usuarioRepository">Repositorio para la gestión de usuarios.</param>
        /// <param name="tokenGenerator">Servicio para la generación y manejo de tokens.</param>
        /// <param name="tokenRefreshRepository">Repositorio para la validación y manejo de tokens de renovación.</param>
        public AuthenticationService(IUsuarioRepository usuarioRepository, ITokenGenerator tokenGenerator, ITokenRefreshRepository tokenRefreshRepository)
        {
            _usuarioRepository = usuarioRepository;
            _tokenGenerator = tokenGenerator;
            _tokenRefreshRepository = tokenRefreshRepository;
        }

        /// <summary>
        /// Finaliza la sesión de un usuario con las credenciales proporcionadas.
        /// </summary>
        /// <param name="credentials">Credenciales del usuario.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public Task<bool> EndLogin(UserCredentials credentials)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inicia sesión con las credenciales proporcionadas.
        /// </summary>
        /// <param name="credentials">Credenciales del usuario.</param>
        /// <returns>Una tarea que representa la operación asincrónica, con una sesión de usuario si la autenticación es exitosa.</returns>
        public async Task<UserSession> LoginAsync(UserCredentials credentials)
        {
            Usuarios user = await _usuarioRepository.LoginAsync(credentials);
            if (user != null)
            {
                string cookieString = await _tokenGenerator.ConfigureCookieRefresh(user);

                return new UserSession
                {
                    Name = user.Nombre,
                    UserId = user.Id_Usuario,
                    Rol = (int)user.Id_Rol,
                    tokenRefresh = cookieString,
                    User = user.Usuario
                };
            }

            return null;
        }

        /// <summary>
        /// Establece una identidad de usuario basada en la sesión.
        /// </summary>
        /// <param name="userSession">Sesión del usuario.</param>
        /// <param name="parameters">Parámetros adicionales, como la clave secreta.</param>
        /// <returns>Una instancia de <see cref="ClaimsIdentity"/> con los reclamos del usuario.</returns>
        public ClaimsIdentity SetClaimsIdentity(UserSession userSession, params object[] parameters)
        {
            string secretKey = parameters[0].ToString();

            return new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, userSession.User),
                new Claim("codigo_usuario", userSession.UserId.ToString(), ClaimValueTypes.Integer),
                new Claim("nombre_usuario", userSession.Name),
                new Claim(ClaimTypes.Authentication, "bearer token"),
                new Claim("fecha_ingreso", DateTime.Now.Ticks.ToString(), ClaimValueTypes.Integer64),
                new Claim("id_Rol", userSession.Rol.ToString(), ClaimValueTypes.Integer),
            });
        }

        /// <summary>
        /// Renueva el token de actualización para un usuario.
        /// </summary>
        /// <param name="userSession">Sesión del usuario actual.</param>
        /// <returns>Una tarea que representa la operación asincrónica, con una nueva sesión si el token es válido.</returns>
        public async Task<UserSession> renovarRefreshTokenAsync(UserSession userSession)
        {
            if (!await _tokenRefreshRepository.validarRefreshTokenAsync(userSession.UserId, userSession.tokenRefresh))
            {
                return null;
            }

            Usuarios user = new Usuarios
            {
                Id_Usuario = userSession.UserId
            };
            string cookieString = await _tokenGenerator.ConfigureCookieRefresh(user);

            return new UserSession
            {
                Name = userSession.Name,
                UserId = userSession.UserId,
                Rol = userSession.Rol,
                tokenRefresh = cookieString,
                User = userSession.User
            };
        }

        /// <summary>
        /// Firma el token de acceso.
        /// </summary>
        /// <param name="token">Token de acceso a firmar.</param>
        /// <returns>Un objeto dinámico que contiene el token firmado.</returns>
        public dynamic Sign(string token)
        {
            return new
            {
                access_Token = token,
            };
        }
    }
}
