using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCLI
{
    public interface IMovies
    {
        //string Title { get; set; }
        string Overview { get; set; }
        string OriginalLanguage { get; set; }
        double VoteAverage { get; set; }
        int VoteCount { get; set; }
        string OriginalTitle { get; set; }
    }
}
