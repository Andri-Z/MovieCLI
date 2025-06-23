using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCLI.Models
{
    public class Movies
    {
        public Movies(string request)
        {
            switch (request)
            {
                case "tmdb-app --type \"playing\"\r\n":
                    NowPlaying();
                    break;

                default:
                    break;
            }
        }
        void NowPlaying()
        {

        }
    }
}
