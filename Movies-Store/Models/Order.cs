namespace Movies_Store.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
       public List<OrderItem> Items { get; set;}=new List<OrderItem>();
    }
}
