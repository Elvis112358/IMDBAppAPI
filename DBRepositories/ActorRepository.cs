using EFModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBRepositories
{
    public class ActorRepository : Repository<Actor>, IActorRepository
    {
        public ActorRepository(MoviesDBContext context)
                : base(context)
        {

        }

        public override Actor GetById(object id)
        {
            throw new NotImplementedException();
        }
    }
}
