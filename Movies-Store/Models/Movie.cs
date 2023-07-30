using Movies_Store.Data.Base;
using Movies_Store.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies_Store.Models
{
    public class Movie:IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public MoviesCategory   MovieCategory{ get; set; }

        //Relationships
        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        [ForeignKey("Producer")]
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }

        public List<Actor_Movie> Actor_Movies { get; set; }= new List<Actor_Movie>();
    }
}
