# API Newshore

Descripcion
Una característica clave de la solución es la capacidad de persistencia de datos. Cuando se calcula una nueva ruta de viaje, esta información se almacena en un sistema de persistencia, lo que permite recuperar fácilmente la ruta en el futuro si se ingresan los mismos parámetros. Esto mejora la eficiencia al evitar recálculos innecesarios y proporciona una experiencia más rápida al usuario al acceder a rutas previamente calculadas.

La API sigue una arquitectura en capas, con una clara separación de responsabilidades. La capa de presentación gestiona las interacciones con los usuarios. La arquitectura facilita la escalabilidad y el mantenimiento, sigue principios SOLID, incluye medidas de seguridad. En resumen, se enfoca en la modularidad y organización para lograr un desarrollo eficiente y sostenible.

Tambien se realizan todos los puntos que solicitan, ademas realizo metodos para busqueda de rutas cortas (se usa algoritmo de Dijkstra) y largas (se usa algoritmo de Búsqueda en Profundidad), desde la solicitud de la API se valida si ya hubo una solicitud y se guarda en un diccionario, ademas se tiene encriptada la URL de la consulta a la API en AES, se encripta cadena de conexion pero esta se deja comentada ya que para realizar pruebas en otro equipo debe cambiarse la cadena de conexion.

Se usan Patrones de diseño como Repository, para ayuda a implementar todos los metodos de la unidad de trabajo para los modelos, se usa CQRS para la conexion entre controlador y servicio, se usa inyeccion de dependencias para no tener un alto acomplamiento en el codigo.

Estado del proyecto 100%

Acceso al Proyecto
https://github.com/JhonCV97/API_Newshore

Recomendaciones:
-Cambiar la cadena de conexion a base de datos en appsettings.json
-Se debe abrir la consola del administrador de paquetes, cambiar el proyecto determinado a Infra.Data y realizar el update-database para subir todas las Migrations

Tecnologías:
-C#
-.Net 7 
-SQL