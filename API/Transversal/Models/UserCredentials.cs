using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Models
{
    /// <summary>
    /// Credenciales de usuario.
    /// </summary>
    public partial class UserCredentials
    {
        /// <summary>
        /// Propiedad que define o establece el nombre de usuario..
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Propiedad que define o establece la contraseña del usuario.
        /// </summary>
        public string Password { get; set; }

    }
}
