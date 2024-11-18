using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Models
{
    public class TokenRefreshModel
    {
        public int Id_TokenRefresh { get; set; }
        public int Id_Usuario { get; set; }
        public string Token { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public System.DateTime FechaExpiracion { get; set; }
        public Nullable<System.DateTime> FechaRevocacion { get; set; }
        public string TokenReemplazo { get; set; }
        public int EstaActivo { get; set; }
    }
}
