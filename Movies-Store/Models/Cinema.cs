using Movies_Store.Data.Base;

namespace Movies_Store.Models
{
    public class Cinema:IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
