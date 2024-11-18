using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AccesoDatos.db;
using AccesoDatos.Repositories;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using LogicaNegocio.Services.TokenGenerator;

namespace LogicaNegocio.Services.TokenGenerator
{
    public class TokenGenerator : ITokenGenerator
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenRefreshRepository _tokenRefreshRepository;

        public TokenGenerator(IUsuarioRepository usuarioRepository, ITokenRefreshRepository tokenRefreshRepository)
        {
            _usuarioRepository = usuarioRepository;
            _tokenRefreshRepository = tokenRefreshRepository;
        }


        /// <summary>
        /// Método que permite generar el token JWT para la cuenta de usuario ingresado.
        /// </summary>
        /// <param name="claimsIdentityForUserSession">Función de retorno.</param>
        /// <returns>Token</returns>
        public string GenerateTokenJwt(Func<string, ClaimsIdentity> claimsIdentityForUserSession)
        {
            // appsetting for Token JWT
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = claimsIdentityForUserSession(secretKey);

            // create token to the user
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }

        public async Task<string> ConfigureCookieRefresh(Usuarios User)
        {
            string refreshToken = await GenerateRefreshToken(User);
            var cookieOptions = new CookieHeaderValue("refreshToken", refreshToken)
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddHours(4),
                Path = "/"
            };


            string cookieString = cookieOptions.ToString() + "; SameSite=Strict";
            return cookieString;
        }


        // Método para generar el refresh token
        public async Task<string> GenerateRefreshToken(Usuarios User)
        {
            using (var rng = new RNGCryptoServiceProvider()) // Generador de números aleatorios seguros
            {
                byte[] tokenData = new byte[32]; // Tamaño de 32 bytes (256 bits)
                rng.GetBytes(tokenData); // Llenamos el arreglo con valores aleatorios

                string tokenGenerado = Convert.ToBase64String(tokenData);

                Usuarios_TokenRefresh tokenRefresh = new Usuarios_TokenRefresh
                {
                    Id_Usuario = User.Id_Usuario,
                    Token = tokenGenerado,
                    FechaCreacion = DateTime.Now,
                    FechaExpiracion = DateTime.Now.AddHours(4),
                    EstaActivo = true
                };

                //actualiza tokens antiguo
                await _tokenRefreshRepository.updateTokenRefreshByUserAsync(User, tokenGenerado);
                //guardar nuevo token refresh
                await _tokenRefreshRepository.CreateAsync(tokenRefresh);

                return tokenGenerado;
                // Convertir el arreglo de bytes a una cadena en base64

            }
        }
    }
}
