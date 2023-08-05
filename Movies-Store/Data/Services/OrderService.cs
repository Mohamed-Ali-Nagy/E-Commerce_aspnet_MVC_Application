using Microsoft.EntityFrameworkCore;
using Movies_Store.Models;

namespace Movies_Store.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly CinemaContext context;
        public OrderService(CinemaContext _context)
        {
            context = _context;
        }
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string role)
        {
          //  var orders= await context.Orders.Include(o => o.Items).ThenInclude(o => o.Movie).ToListAsync();
            var orders = await context.Orders.Include(n => n.Items).ThenInclude(n => n.Movie).Include(n => n.User).ToListAsync();

            if (role != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;

        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
             Order order=new Order()
             {
                 UserEmail = userEmailAddress,
                 UserId = userId,
             };
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            foreach (var item in items)
            {
                OrderItem itemItem = new OrderItem()
                {
                    OrderId=order.Id,
                    Amount=item.Amount,
                    MovieId=item.MovieId,
                    Price=item.Movie.Price,
                };
                await context.OrderItems.AddAsync(itemItem);
            }
           await context.SaveChangesAsync();
            
        }
    }
}
