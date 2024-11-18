/**
 * Definición de interfaz para el estado de la autenticación.
 */
export interface IAuthenticateStatus {
    /**
     * Identificador de usuario.
     */
    nameid: string;
    /**
     * Método de autenticación utilizado.
     */
    authenticationmethod: string;
    /**
     * Tipo de autenticación.
     */
    authentication: string;
    /**
     * Fecha de ingreso a TOTAL Home.
     */
    fecha_ingreso: number;
  
    /**
     * Código de usuario proporcionado por TOTALReportDB.
     */
    codigo_usuario: number;
    /**
     * Nombre real del usuario
     */
    nombre_usuario: string;
    /**
     * Código de la entidad actual de trabajo.
     */
    codigo_entidad_actual: number;
    /**
     * Nombre de la entidad actual de trabajo.
     */
    nombre_entidad_actual: string;

    /**
     * Rol que desempeña el usuario en la aplicación.
     */
    role: string;

    /**
     * Arreglo de permisos sobre acciones en función al rol del usuario.
     */
    permission_list: Array<string>;
  }