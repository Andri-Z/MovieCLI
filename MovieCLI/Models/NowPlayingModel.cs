using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCLI.Models
{
    public class NowPlayingModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("genre_ids")]
        public int[] Genres { get; set; } = [];
        [JsonProperty("tittle")]
        public string Tittle { get; set; } = string.Empty;
        [JsonProperty("adult")]
        public bool Adult { get; set; }
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; } = string.Empty;
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; } = string.Empty;
        [JsonProperty("original_title")]
        public string OriginalTittle { get; set; } = string.Empty;
        [JsonProperty("overview")]
        public string Overview { get; set; } = string.Empty;
        [JsonProperty("popularity")]
        public double Popularity { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; } = string.Empty;
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; } = string.Empty;
        [JsonProperty("Video")]
        public bool video { get; set; }
        [JsonProperty("vote_average")] 
        public double VoteAverage { get; set; }
        [JsonProperty("vote_count")] 
        public int VoteCount { get; set; }
    }
}
