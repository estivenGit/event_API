using System;
using AccesoDatos.Repositories;
using LogicaNegocio.Services.AsistenciaEvento;

namespace LogicaNegocio.Container
{
    public class Dependence
    {

        /// <summary>
        /// Envia las dependecias de la logica de negocio.
        /// </summary>
      
        public static void RegisterDependencies(Action<Type, Type> registerAction)
        {
            registerAction(typeof(IUsuarioRepository), typeof(UsuarioRepository));
            registerAction(typeof(ITokenRefreshRepository), typeof(TokenRefreshRepository));
            registerAction(typeof(IEventoRepository), typeof(EventoRepository));
            registerAction(typeof(IEventoAsistentesRepository), typeof(EventoAsistentesRepository));
            registerAction(typeof(IEventoAsistentesService), typeof(EventoAsistentesService));
        }


    }
}
