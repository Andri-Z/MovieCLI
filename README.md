# 🎬 TMDB CLI App

Aplicación de consola en C# que consume la API pública de [The Movie Database (TMDB)](https://developer.themoviedb.org/reference/intro/getting-started)) 
para mostrar información de películas en diferentes categorías, la idea de esta aplicacion proviene de aqui: [Roadmap.sh (tmdb-cli)](https://roadmap.sh/projects/tmdb-cli)

---

## 🚀 ¿Qué hace esta aplicación?

Permite obtener listas de películas desde la línea de comandos usando los siguientes tipos:

- `playing`: Películas en cartelera actualmente
- `popular`: Películas populares
- `top`: Películas mejor valoradas
- `upcoming`: Próximos estrenos

También permite navegar entre páginas de resultados con los comandos:

- `next`: Página siguiente
- `prev`: Página anterior
- `salir`: Salir de la aplicación

---

## 🧩 Estructura del Proyecto

- `Program.cs` – Punto de entrada de la aplicación
- `Movies.cs` – Controla el flujo principal y paginación
- `Get.cs` – Servicio que se encarga de hacer peticiones HTTP
- `Messages.cs` – Encargado de mostrar mensajes en consola
- `MoviesGeneric.cs` – Clase genérica que muestra cualquier modelo que implemente `IMovies`

---

## 📦 Cómo ejecutar el proyecto

1. Clona el repositorio:

   ```bash
   git clone https://github.com/Andri-Z/MovieCLI.git
   cd MovieCLI
   dotnet run
