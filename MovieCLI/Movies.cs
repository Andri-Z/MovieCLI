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
            switch (request)
            {
                case "tmdb-app --type \"playing\"":
                    await NowPlaying();
                    break;

                default:
                    break;
            }
        }
        private async Task NowPlaying()
        {
            var client = new HttpClient();
            using (var response = await client.SendAsync(await GetHttpRequestAsync()))
            {
                response.EnsureSuccessStatusCode();
                var json = JObject.Parse(await response.Content.ReadAsStringAsync());
                var results = json["results"].ToObject<List<NowPlayingModel>>();
                var msg = new Messages<NowPlayingModel>(results);
                msg.ShowAll();
            }
        }
        private async Task<string> GetKeyAsync()
        {
            return await File.ReadAllTextAsync("C:\\Users\\Andri\\source\\repos\\MovieCLI\\Privado.txt");
        }
        private async Task<HttpRequestMessage> GetHttpRequestAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.themoviedb.org/3/movie/now_playing?language=en-US"),
                Headers =
                {
                    {"accept","application/json" },
                    {"Authorization", $"Bearer {await GetKeyAsync()}" }
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
            Console.WriteLine($"\x1b[1mTitle:\x1b[0m {model.Tittle}");
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
