using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTO
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Genre { get; set; }
        public string Image { get; set; }
        public DateTime Duration { get; set; }

        //Navigation Properties

        // public int PublisherId { get; set; }
        // public Publisher Publisher { get; set; }
        public ICollection<ActorMovie> Actors { get; set; }
    }
}
