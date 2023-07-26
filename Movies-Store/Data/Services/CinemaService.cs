using Movies_Store.Data.Base;
using Movies_Store.Models;

namespace Movies_Store.Data.Services
{
    public class CinemaService:EntityBaseRepository<Cinema>,ICinemaService
    {
        public CinemaService(CinemaContext cinemaContext):base(cinemaContext) { }
    }
}
