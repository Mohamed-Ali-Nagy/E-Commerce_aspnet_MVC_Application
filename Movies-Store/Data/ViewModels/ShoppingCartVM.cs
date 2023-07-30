using Movies_Store.Models;

namespace Movies_Store.Data.ViewModels
{
    public class ShoppingCartVM
    {
      public List<ShoppingCartItem> ShoppingCartItems { get; set; }= new List<ShoppingCartItem>();
      public double Subtotal { get; set; }

    }
}
