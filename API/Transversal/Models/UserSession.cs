using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Models
{
    public class UserSession
    {
        /// <summary>
        /// Propiedad que define o establece el codigo del usuario.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Propiedad que define o establece el el user.
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Propiedad que define o establece el nombre de la persona asociada al usuario.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Propiedad que define o establece el id rol asociado al usuario.
        /// </summary>
        public int Rol { get; set; }

        /// <summary>
        /// Propiedad que define el tokenRefresh.
        /// </summary>
        public string tokenRefresh { get; set; }


    }
}
