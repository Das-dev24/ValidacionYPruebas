using System;
using Practica.Model;

namespace Database {
    public interface ICapaDatos {
        /// Este Interfaz se entrga a modo de requisitos mínimos a implementar y probar.
        /// Debéis de incluir funcionalidades adicionales

        /// <summary>
        /// Almacena el usuario.
        /// </summary>
        /// <param name="u">Objeto de la clase User que se desea almacenar.</param>
        /// <returns>Verdadero o falso en función de si ha conseguido insertar/actualizar la información.</returns>
        bool GuardaUser(User u);

        /// <summary>
        /// Lee los datos del usuario que se corresponde con la clave que se recibe como parámetro.
        /// </summary>
        /// <param name="email">Cadena con el EMail del usuario que se quiere consultar.</param>
        /// <returns>Retorna el objeto con la infromación del usuario buscado o NULL si no se localiza.</returns>
        User LeeUser(String email);

        /// <summary>
        /// Comprueba si el usuario existe existe y el password se corresponde con la almacenada de forma cifrada.
        /// </summary>
        /// <param name="email">Cadena con el EMail del usuario que se quiere consultar.</param>
        /// <param name="password">Cadena con el EMail del usuario que se quiere consultar.</param>
        /// <returns>Retorna TRUE si los datos de autenticación son válidos.</returns>
        bool ValidaUser(string email, string password);

        /// <summary>
        /// Retorna el número de usuarios registrados.
        /// </summary>
        /// <returns>Número de Users.</returns>
        int NumUsers();

        /// <summary>
        /// OPCIONAL
        /// Retorna el número de usuarios registrados.
        /// </summary>
        /// <returns>Número de Users.</returns>
        int NumUsersActivos();

        /// <summary>
        /// Almacena una Activityes que puede ser:
        /// </summary>
        /// <param name="e">Objeto de la clase Activity que se quiere almacenar.</param>
        /// <returns>Verdadero o falso en función de si ha conseguido insertar/ actualizar la información.</returns>
        bool GuardaActivity(Activity e);

        /// <summary>
        /// Lee los datos del elemento referenciado por su ID.
        /// </summary>
        /// <param name="idElemento">Identificador del Activity que se quiere consultar.</param>
        /// <returns>Retorna el objeto con la infromación del conponente buscado o NULL si no se localiza.</returns>
        Activity LeeActivity(int idElemento);

        /// <summary>
        /// Retorna el número de Activityes registrados.
        /// </summary>
        /// <param name="idUser">Identificador del User cuyos datos se quieren consultar.</param>
        /// <returns>Número de Activityes.</returns>
        int NumActivityes(int idUser);

        void Register(string name, string lastName, string email, string password);
    }
}