using Microsoft.EntityFrameworkCore;
using Movies_Store.Models;
using System;

namespace Movies_Store.Data.Cart
{
    public class ShoppingCart
    {
        private readonly CinemaContext context;
        public string CartId { get; set; }
        public List<ShoppingCartItem> CartItems { get; set; }=new List<ShoppingCartItem>();
        public ShoppingCart(CinemaContext _context)
        {

            context = _context;

        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<CinemaContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { CartId = cartId };
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
           var CartItems= (context.ShoppingCartItems.Where(s=>s.ShoppingCartId == CartId).Include(sh=>sh.Movie).ToList());
            return CartItems;
        }

        public double GetShoppingCartTotal()
        {
            return context.ShoppingCartItems.Where(sh=>sh.ShoppingCartId==CartId).Select(s=>s.Movie.Price*s.Amount).Sum();
        }

        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem=context.ShoppingCartItems.Where(sh=>sh.MovieId==movie.Id&&sh.ShoppingCartId==CartId).FirstOrDefault();
            if (shoppingCartItem==null)
            {
                shoppingCartItem=new ShoppingCartItem()
                {
                    ShoppingCartId=CartId,
                    MovieId=movie.Id,
                    Amount=1,
                };
                context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            context.SaveChanges();

        }
        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = context.ShoppingCartItems.Where(sh => sh.MovieId == movie.Id && sh.ShoppingCartId == CartId).FirstOrDefault();
            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 0) { shoppingCartItem.Amount--; }

                else { context.ShoppingCartItems.Remove(shoppingCartItem); }
            }
            context.SaveChanges();
        }

        public async Task ClearShoppingCartAsync()
        {
            List<ShoppingCartItem> items=await context.ShoppingCartItems.Where(sh=>sh.ShoppingCartId==CartId).ToListAsync();
            context.ShoppingCartItems.RemoveRange(items);
            await  context.SaveChangesAsync();
        }
    }
}
