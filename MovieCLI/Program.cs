using System.ComponentModel.DataAnnotations;
using MovieCLI;

MoviesService movies = new();

//Lista de comandos validos
List<string> commands = ["tmdb-app --type \"playing\"", "tmdb-app --type \"popular\"",
                         "tmdb-app --type \"top\"","tmdb-app --type \"upcoming\"",
                         "exit"];


while (true)
{
    Console.WriteLine("Ingresar comando:");
    string? value = Console.ReadLine();

    //Validacion de entrada de datos y validar si la lista de comandos validos contiene algun valor de la entrada.
    if (string.IsNullOrWhiteSpace(value) || !commands.Contains(value))
    {
        //Mostrando comandos validos al usuario.
        Console.WriteLine("Comando no valido, ingrese uno de estos:");
        foreach (var item in commands)
            Console.WriteLine(item);
    }
    else
    {
        if (value == "exit")
        {
            Console.WriteLine("Saliendo de la aplicacion.");
            Environment.Exit(0);        
        }

        int pageValid;
        while (true)
        {
            Console.WriteLine("Page:");
            var page = Console.ReadLine();

            _ = int.TryParse(page, out pageValid);
            break;
        }
        //Limpia la terminal y llama al metodo para obtener las peliculas solicitadas.
        Console.Clear();
        await movies.GetMoviesAsync(value,pageValid);
    }
}