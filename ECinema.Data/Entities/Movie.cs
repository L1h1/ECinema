using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TrailerUrl { get; set; }

        public int StudioId { get; set; }
    }
}
