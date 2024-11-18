using System;
using Transversal.Utilidades;

namespace AccesoDatos.Container
{
    public class Dependence
    {
        /// <summary>
        /// Envia las dependecias de la logica de negocio.
        /// </summary>

        public static void RegisterDependencies(Action<Type, Type> registerAction)
        {
            registerAction(typeof(IPasswordManage), typeof(PasswordManage));
        }
    }
}
