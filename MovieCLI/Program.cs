using MovieCLI;

var _movies = Console.ReadLine();
Movies movies = new();
await movies.GetMoviesAsync(_movies.Trim());