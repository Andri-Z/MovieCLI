using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Text.Json.Nodes;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MovieCLI
{
    public class MoviesService
    {
        public async Task GetMoviesAsync(string request,int page)
        {
            //Obtener todas las url para las consultas a la API Externa
            var uris = GetUris();

            //Llama a al metodo GetRequestAsync para realizar la consulta en la API Externa y mostrar los resultados de la operacion.
            switch (request)
            {
                case "tmdb-app --type \"playing\"":
                    await GetRequestAsync(uris[0]+$"&page={page}");
                    break;
                case "tmdb-app --type \"popular\"":
                    await GetRequestAsync(uris[1]+$"&page={page}");
                    break;
                case "tmdb-app --type \"top\"":
                    await GetRequestAsync(uris[2]+$"&page={page}");
                    break;
                case "tmdb-app --type \"upcoming\"":
                    await GetRequestAsync(uris[3]+$"&page={page}");
                    break;
                default:
                    break;
            }
        }
        private static async Task GetRequestAsync(string uri)
        {
            HttpClient client = new();

            //Realiza una peticion a la API externa
            using var request = await client.SendAsync(await GetHttpRequest(uri));

            //Verifica si la peticion fue exitosa
            if (request.IsSuccessStatusCode)
            {
                var json = await request.Content.ReadAsStringAsync();
                if (json is not null)
                {
                    //Parsea el resultado a JsonNode, para poder mostrar los datos en un formato Json.
                    var response = JsonNode.Parse(json)!.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine(response);
                }
                else
                    Console.WriteLine("Error: No se han podido recuperar las peliculas.");
            }
            else
                Console.WriteLine($"Error: Ha ocurrido un error durante la consulta a la API Externa:{request.StatusCode}");


        }
        private static List<string> GetUris()
        {
            List<string> uris =
            [
                "https://api.themoviedb.org/3/movie/now_playing?language=en-US", //Url de peliculas Now Playing
                "https://api.themoviedb.org/3/movie/popular?language=en-US", //Url de peliculas Populares
                "https://api.themoviedb.org/3/movie/top_rated?language=en-US", //Url de peliculas Top Rated
                "https://api.themoviedb.org/3/movie/upcoming?language=en-US", //Url de peliculas Up Comming
            ];
            return uris;
        }
        private static async Task<string> GetTokenApi()
        {
            string? path = Path.Combine(AppContext.BaseDirectory, "Privado.txt");
            if (!File.Exists(path))
            {
                Console.WriteLine("Ha ocurrido un error con el archivo que contiene el token de la API.");
                Environment.Exit(0);
            }
            return await File.ReadAllTextAsync(path); //retorna el token de la API que se almacena en el archivo Privado.txt
        }
        private static async Task<HttpRequestMessage> GetHttpRequest(string uri)
        {
            //Se obtiene el token de la API de un archivo local.
            var tokenApi = await GetTokenApi();
            if (tokenApi is null)
            {
                Console.WriteLine("Error: El token de la API no existe.");
            }
            //Modelo de peticion para la API Externa
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers =
                {
                    {"accept","application/json" },
                    {"Authorization", $"Bearer {tokenApi}" } 
                },
            };
            return request;
        }
    }
}
