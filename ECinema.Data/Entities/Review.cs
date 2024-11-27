using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string ReviewText { get; set; }
        public int ReviewRating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
