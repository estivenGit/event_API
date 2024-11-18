using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Text;
using LogicaNegocio.Services.TokenGenerator;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Web.Configuration;
using System.Web.Http;
using System.Runtime.Remoting.Messaging;
using static System.Net.WebRequestMethods;
using System.Security.Claims;

namespace API.JWT
{
    public class TokenValidationHandler : DelegatingHandler
    {
        /// <summary>
        /// Envía una solicitud HTTP al controlador interno para enviarla al servidor como una operación asincrónica
        /// </summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string mensaje = string.Empty;
            string token = string.Empty;
           
            if (request.Method.Method == "OPTIONS")
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                return Task.FromResult(response);
            }
            else if (request.RequestUri.AbsolutePath.EndsWith("/authenticate"))
            {
                return base.SendAsync(request, cancellationToken);
            }
            else if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                var response = new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent("Token no proporcionado o inválido.")
                };
                return Task.FromResult(response);
            }

            try
            {
                var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));

                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audienceToken,
                    ValidIssuer = issuerToken,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey
                };

                // Extract and assign Current Principal and user
                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
                HttpContext.Current.User = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                HttpContext.Current.Items["codigo_usuario"] = ObtenerCodigoUsuario();
                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenInvalidLifetimeException)
            {
                if (!request.RequestUri.AbsolutePath.EndsWith("login/renovarRefreshToken"))
                {
                    statusCode = HttpStatusCode.Unauthorized;
                    mensaje = "Token expired";
                }
                else
                {
                    return base.SendAsync(request, cancellationToken);                   
                }
            }
            catch (SecurityTokenValidationException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }
            return Task<HttpResponseMessage>.Factory.StartNew(() =>
                new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent(mensaje, Encoding.UTF8, "application/json")
                });
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

        /// <summary>
        /// Método que valida el ciclo de vida del token.
        /// </summary>
        /// <param name="notBefore">The not before.</param>
        /// <param name="expires">The expires.</param>
        /// <param name="securityToken">The security token.</param>
        /// <param name="validationParameters">The validation parameters.</param>
        /// <returns></returns>
        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }

        // <summary>
        /// Obtiene el código de usuario del contexto de autenticación actual.
        /// Este método extrae el valor del claim "codigo_usuario" del usuario autenticado en el contexto HTTP actual.
        /// Si el usuario no está autenticado o el claim no está presente, devuelve 0.
        /// </summary>
        /// <returns>El código de usuario como entero si está disponible; de lo contrario, 0.</returns>
        public int ObtenerCodigoUsuario()
        {
            var userClaims = HttpContext.Current.User as ClaimsPrincipal;

            if (userClaims != null && userClaims.Identity.IsAuthenticated)
            {
                var codigoUsuario = int.Parse(userClaims.FindFirst("codigo_usuario")?.Value);
                return codigoUsuario;
            }

            return 0;
        }
    }
}