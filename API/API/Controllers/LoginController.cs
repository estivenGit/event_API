using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LogicaNegocio.Services.Authentication;
using Transversal.Models;
using LogicaNegocio.Services.TokenGenerator;
using System.IdentityModel.Tokens.Jwt;
using System.Web;

namespace API.Controllers
{
    /// <summary>
    /// Controlador principal de logeo de servicio.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenGenerator _tokenGenerator;

        ///// <summary>
        ///// Inicializa la instancia <see cref="LoginController"/> class.
        ///// </summary>
        ///// <param name="authenticationService">The authentication service.</param>
        public LoginController(IAuthenticationService authenticationService, ITokenGenerator tokenGenerator)
        {
            _authenticationService = authenticationService;
            _tokenGenerator = tokenGenerator;
        }
        /// <summary>
        /// Método que realiza la autenticación de usuarios
        /// </summary>
        /// <param name="login">credenciales de logeo del usuario.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException">Excepción que solo sucede si las credenciales se encuentran vacias.</exception>
        [HttpPost]
        [Route("authenticate")]
        public async Task<IHttpActionResult> Login(UserCredentials login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.Unauthorized);


            if (string.IsNullOrEmpty(login.Username))
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            UserSession loginStatus = await _authenticationService.LoginAsync(login);
            if (loginStatus == null)
                throw new HttpResponseException(HttpStatusCode.Unauthorized);


            var signResult = _authenticationService.Sign(_tokenGenerator.GenerateTokenJwt((secretKey) => _authenticationService.SetClaimsIdentity(loginStatus, secretKey)));
            string tokenJson = JsonConvert.SerializeObject(signResult);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Set-Cookie", loginStatus.tokenRefresh);
            response.Content = new StringContent(tokenJson, Encoding.UTF8, "application/json");

            return ResponseMessage(response);


        }

        /// <summary>
        /// Método que permite realizar la renovar el access token con el refresh token
        /// </summary>
        [HttpPost]
        [Route("renovarRefreshToken")]
        public async Task<IHttpActionResult> renovarRefreshToken(HttpRequestMessage request)
        {
            string refreshToken = GetRefreshTokenFromCookie(request);
            if (!TryRetrieveToken(request, out string token) || refreshToken ==null)
            {
                return Unauthorized();
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            string codigoUsuario = jsonToken?.Claims.FirstOrDefault(c => c.Type == "codigo_usuario")?.Value;
            string Usuario = jsonToken?.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            string id_Rol = jsonToken?.Claims.FirstOrDefault(c => c.Type == "id_Rol")?.Value;
            string nombre_usuario = jsonToken?.Claims.FirstOrDefault(c => c.Type == "nombre_usuario")?.Value;

            UserSession UserCredentialLogin = new UserSession
            {
                Name = jsonToken?.Claims.FirstOrDefault(c => c.Type == "nombre_usuario")?.Value,
                UserId = Convert.ToInt32(jsonToken?.Claims.FirstOrDefault(c => c.Type == "codigo_usuario")?.Value),
                Rol = Convert.ToInt32(jsonToken?.Claims.FirstOrDefault(c => c.Type == "id_Rol")?.Value),
                tokenRefresh = refreshToken,
                User = jsonToken?.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value

            };

            UserSession loginStatus = await _authenticationService.renovarRefreshTokenAsync(UserCredentialLogin);

            var signResult = _authenticationService.Sign(_tokenGenerator.GenerateTokenJwt((secretKey) => _authenticationService.SetClaimsIdentity(loginStatus, secretKey)));
            string tokenJson = JsonConvert.SerializeObject(signResult); // Serializa a JSON
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Set-Cookie", loginStatus.tokenRefresh);
            response.Content = new StringContent(tokenJson, Encoding.UTF8, "application/json");

            return ResponseMessage(response);
        }

        /// <summary>
        /// Método para recuperar el token.
        /// </summary>
        /// <param name="request">Request de la petición.</param>
        /// <param name="token">El token.</param>
        /// <returns>Verdadero o falso</returns>
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;

            if (!request.Headers.TryGetValues("Authorization", out IEnumerable<string> authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        private string GetRefreshTokenFromCookie(HttpRequestMessage request)
        {
            var cookies = request.Headers.GetCookies("refreshToken").FirstOrDefault();
            if (cookies != null)
            {
                var refreshTokenCookie = cookies["refreshToken"];
                return refreshTokenCookie?.Value;
            }
            return null;
        }

    }
}
