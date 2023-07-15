namespace Movies_Store.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public string Bio { get; set; }

        public List<Actor_Movie> Actor_Movies { get; set; } = new List<Actor_Movie>();

    }
}
