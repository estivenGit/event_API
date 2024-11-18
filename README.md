# Eventos API

## Descripción

"Eventos API" es una aplicación web diseñada para gestionar eventos. Permite crear, editar y eliminar eventos, así como suscribirse como asistente a ellos, aplicando las reglas de negocio solicitadas. La aplicación está dividida en un backend construido con .NET Framework 4.8 y un frontend en Angular, con autenticación mediante JWT.

## Tecnologías

- **Backend**: .NET Framework 4.8
- **Frontend**: Angular 18.2.11
- **Autenticación**: JWT (JSON Web Tokens)
- **Dependencias adicionales**: 
  - Unity (contenedor de dependencias)
  - Entity Framework (ORM)
  - CORS
  - Middleware: TokenValidationHandler
  - Angular Material (para el frontend)

## Instalación

1. **Base de Datos**: 
   - Crea la base de datos utilizando el script `Base de datos.sql` ubicado en el proyecto.
   - Después de crear la base de datos, ejecuta las sentencias adicionales para la creación de las tablas y la inserción de datos iniciales.
   
2. **Usuarios**: 
   - Se crearán 4 usuarios para acceder a la aplicación con la siguiente contraseña: `1234`:
     - `EstivenR`
     - `prueba1`
     - `prueba2`
     - `prueba3`
     - `prueba4`
   - Estos usuarios son necesarios para iniciar sesión y acceder a la administración de eventos.

## Uso

- Para más detalles sobre cómo interactuar con la API, se incluye un documento explicativo que describe el funcionamiento de los endpoints disponibles.

## Dependencias

- **Backend**:
  - .NET Framework 4.8
  - JWT para la autenticación
  - Unity como contenedor de dependencias
  - Entity Framework como ORM
  - CORS para permitir solicitudes entre dominios
  - `TokenValidationHandler` como middleware de validación de tokens
  
- **Frontend**:
  - Angular CLI 18.2.11
  - Node.js 22.11.0
  - Angular Material (para componentes de UI)

## Estructura del Proyecto

- **API**:
  - La API está organizada en las siguientes capas: 
    - **Presentación** (Controladores)
    - **Lógica de negocio**
    - **Acceso a datos** (Entity Framework)
    - **Transversal** (utilidades y configuraciones comunes)
  - Utiliza JWT para autenticación, gestionando un **token de acceso** y un **token de refresh**.

- **Frontend**:
  - El frontend está constituido por **componentes**, **servicios** e **interceptores** para manejar la autenticación.
  - El **token de acceso** se almacena en el local storage y el **refresh token** en las cookies.

## Autor

**Estiven Rodríguez**

## Notas Adicionales

Esta aplicación proporciona funcionalidades básicas para la gestión de eventos. Sin embargo, aún quedan aspectos pendientes por completar y mejorar, tales como:
- Mensajes personalizados de error y éxito
- Funcionalidad de logout
- Mejoras en la experiencia de usuario (UX)
