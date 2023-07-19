using Microsoft.EntityFrameworkCore;
using Movies_Store.Data.Base;
using Movies_Store.Models;

namespace Movies_Store.Data.Services
{
    public class ActorService :EntityBaseRepository<Actor>, IActorService
    {
        public ActorService(CinemaContext _context) : base(_context) { }
   
    }
}
