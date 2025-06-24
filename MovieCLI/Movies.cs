using MovieCLI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MovieCLI
{
    public class Movies
    {
        public async Task GetMoviesAsync(string request)
        {
            Get get = new();
            var uris = get.GetUris();
            switch (request)
            {
                case "tmdb-app --type \"playing\"":
                    await get.GetRequestAsync(uris[0]);
                    break;
                case "tmdb-app --type \"popular\"":
                    await get.GetRequestAsync(uris[1]);
                    break;
                case "tmdb-app --type \"top\"":
                    await get.GetRequestAsync(uris[2]);
                    break;
                case "tmdb-app --type \"upcoming\"":
                    await get.GetRequestAsync(uris[3]);
                    break;
                default:
                    break;
            }
        }
    }
    public class Get
    {
        public async Task GetRequestAsync(string uri)
        {
            var client = new HttpClient();
            using (var request = await client.SendAsync(await GetHttpRequestAsync(uri)))
            {
                request.EnsureSuccessStatusCode();
                var json = JObject.Parse(await request.Content.ReadAsStringAsync());
                var results = json["results"].ToObject<List<MoviesModel>>();
                var msg = new Messages<MoviesModel>(results);
                msg.ShowAll();
            }
        }
        public List<string> GetUris()
        {
            List<string> uris = new();
            uris.Add("https://api.themoviedb.org/3/movie/now_playing?language=en-US"); //Url de peliculas Now Playing
            uris.Add("https://api.themoviedb.org/3/movie/popular?language=en-US"); //Url de peliculas Populares
            uris.Add("https://api.themoviedb.org/3/movie/top_rated?language=en-US"); //Url de peliculas Top Rated
            uris.Add("https://api.themoviedb.org/3/movie/upcoming?language=en-US"); //Url de peliculas Up Comming
            return uris;
        }
        private async Task<string> GetKeyApiAsync()
        {
            return await File.ReadAllTextAsync("../../../../Privado.txt");
        }
        private async Task<HttpRequestMessage> GetHttpRequestAsync(string uri)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers =
                {
                    {"accept","application/json" },
                    {"Authorization", $"Bearer {await GetKeyApiAsync()}" }
                },
            };
            return request;
        }
    }
    public class Messages<T> where T: IMovies
    {
        private List<T> _listGeneric;
        public Messages(List<T> listGeneric)
        {
            _listGeneric = listGeneric;
        }
        private void ShowMoviesAysnc(T model)
        {
            Console.WriteLine($"\x1b[1mTitle:\x1b[0m {model.OriginalTitle}");
            Console.WriteLine($"\x1b[1mOverview:\x1b[0m {model.Overview}");
            Console.WriteLine($"\x1b[1mVotes Averages:\x1b[0m {model.VoteAverage}");
            Console.WriteLine($"\x1b[1mTotal Votes:\x1b[0m {model.VoteCount}");
            Console.WriteLine($"\x1b[1mOriginal Language:\x1b[0m {model.OriginalLanguage}\n");
        }
        public void ShowAll()
        {
            foreach (var item in _listGeneric)
            {
                ShowMoviesAysnc(item);
            }
        }
    }
}
