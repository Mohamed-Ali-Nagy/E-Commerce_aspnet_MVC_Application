namespace Movies_Store.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ImageUrl { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
