using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Models;

namespace Transversal.Utilidades
{
    public interface IPasswordManage
    {
        bool VerificarContraseña(string contrasenaIngresada, string contrasenaAlmacenada);

        string encriptarContraseña(string Password);
    }
}
