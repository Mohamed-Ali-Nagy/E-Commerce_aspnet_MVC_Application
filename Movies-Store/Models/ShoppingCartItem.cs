using System.ComponentModel.DataAnnotations.Schema;

namespace Movies_Store.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int Amount { get; set; }


        public string ShoppingCartId { get; set; }
    }
}
