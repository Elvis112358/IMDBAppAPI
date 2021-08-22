using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFModels
{
    public class Actor
    {
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public ICollection<ActorMovie> ActorMovies { get; set; } = new List<ActorMovie>();

    }
}
