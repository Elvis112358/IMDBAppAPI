using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTO
{
    public class Actor
    {
        // public int ActorId { get; set; }
        public string ActorName { get; set; }
        public ICollection<Movie> Movies { get; set; }

    }
}
