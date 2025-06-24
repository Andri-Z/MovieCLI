using MovieCLI;

Movies movies = new();

var commands = new Dictionary<string, string>
{
    {"tmdb-app --type \"playing\"","Valido"},
    {"tmdb-app --type \"popular\"", "Valido"},
    {"tmdb-app --type \"top\"", "Valido"},
    {"tmdb-app --type \"upcoming\"", "Valido"}
};
while (true)
{
    Console.WriteLine("Ingresar comando:");
    string value = Console.ReadLine().Trim().ToLower();

    if (commands.ContainsKey(value))
    {
        Console.Clear();
        await movies.GetMoviesAsync(value);
    }
    else
    {
        Console.WriteLine("Comando no valido, ingrese uno de estos:");
        foreach (var item in commands)
        {
            Console.WriteLine(item.Key);
        }
    }
}
