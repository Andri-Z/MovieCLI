using MovieCLI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

HttpClient client = new HttpClient();

var file = File.ReadAllText("C:\\Users\\Andri\\source\\repos\\MovieCLI\\Privado.txt");

var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.themoviedb.org/3/movie/now_playing?language=en-US"),
    Headers =
    {
        {"accept","application/json" },
        {"Authorization", $"Bearer {file}" }
    },
};

using (var response = await client.SendAsync(request))
{
    List<NowPlayingModel> playingModels = new List<NowPlayingModel>();
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    var json = JObject.Parse(body);
    var results = json["results"].ToObject<List<NowPlayingModel>>();
    foreach (var item in results)
    {
        Console.WriteLine($"\x1b[1mTitle:\x1b[0m {item.OriginalTittle}");
        Console.WriteLine($"\x1b[1mOverview:\x1b[0m {item.Overview}");
        Console.WriteLine($"\x1b[1mVotes Averages:\x1b[0m {item.VoteAverage}");
        Console.WriteLine($"\x1b[1mTotal Votes:\x1b[0m {item.VoteCount}");
        Console.WriteLine($"\x1b[1mOriginal Language:\x1b[0m {item.OriginalLanguage}\n");
        Console.ReadKey();
    }
}