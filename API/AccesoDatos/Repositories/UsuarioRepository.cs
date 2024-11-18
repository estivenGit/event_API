using AccesoDatos.db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Models;
using Transversal.Utilidades;

namespace AccesoDatos.Repositories
{
    /// <summary>
    /// Repositorio para gestionar las operaciones relacionadas con la entidad Usuarios en la base de datos.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DC_APIContainer _context;
        private readonly DbSet<Usuarios> _dbSet;
        private readonly IPasswordManage _passwordManage;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Constructor de la clase <see cref="UsuarioRepository"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos utilizado para las operaciones.</param>
        /// <param name="passwordManage">Servicio para la gestión de contraseñas.</param>
        /// <param name="loggerService">Servicio de registro de logs.</param>
        public UsuarioRepository(DC_APIContainer context, IPasswordManage passwordManage, ILoggerService loggerService)
        {
            _context = context;
            _dbSet = context.Set<Usuarios>();
            _passwordManage = passwordManage;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Realiza el inicio de sesión para un usuario.
        /// </summary>
        /// <param name="usuario">Credenciales del usuario.</param>
        /// <returns>
        /// El objeto <see cref="Usuarios"/> si las credenciales son correctas; de lo contrario, <c>null</c>.
        /// </returns>
        public async Task<Usuarios> LoginAsync(UserCredentials usuario)
        {
            try
            {
                var usuarioDb = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Usuario == usuario.Username);

                if (usuarioDb != null && _passwordManage.VerificarContraseña(usuario.Password, usuarioDb.Pass))
                {
                    return usuarioDb;
                }

                return null;
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, "Error en el login para el usuario: " + usuario);
                throw;
            }
        }

        /// <summary>
        /// Obtiene un usuario por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único del usuario.</param>
        /// <returns>
        /// El objeto <see cref="Usuarios"/> si existe; de lo contrario, <c>null</c>.
        /// </returns>
        public async Task<Usuarios> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Usuarios.FindAsync(id);
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex, $"Error consultando usuario por Id: {id}");
                throw;
            }
        }
    }
}
