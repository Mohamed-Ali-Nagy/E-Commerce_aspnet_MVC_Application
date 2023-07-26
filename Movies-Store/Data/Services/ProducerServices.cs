using Movies_Store.Data.Base;
using Movies_Store.Models;

namespace Movies_Store.Data.Services
{
    public class ProducerServices:EntityBaseRepository<Producer>,IProducerService
    {
        public ProducerServices(CinemaContext cinemaContext):base(cinemaContext) { }
    }
}
