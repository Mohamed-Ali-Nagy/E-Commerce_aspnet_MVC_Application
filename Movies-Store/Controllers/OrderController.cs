using Microsoft.AspNetCore.Mvc;
using Movies_Store.Data;
using Movies_Store.Data.Cart;
using Movies_Store.Data.Services;
using Movies_Store.Data.ViewModels;
using Movies_Store.Models;
using System.Security.Claims;

namespace Movies_Store.Controllers
{
    public class OrderController : Controller
    {
     
        private readonly IMovieService movieService;
        private readonly ShoppingCart shoppingCart;
        private readonly IOrderService orderService;
        public OrderController(ShoppingCart _shoppingCart,IMovieService _movieService, IOrderService _orderService)
        {
            shoppingCart = _shoppingCart;
            movieService = _movieService;
            orderService = _orderService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);
            List<Order> orders =await orderService.GetOrdersByUserIdAndRoleAsync(userId,role);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items =shoppingCart.GetShoppingCartItems();
             shoppingCart.CartItems = items;

            var shoppingCartVM=new ShoppingCartVM()
            {
                ShoppingCartItems=items,
                Subtotal=shoppingCart.GetShoppingCartTotal(),
            };
           
            return View(shoppingCartVM);
        }
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
           Movie movie=await movieService.GetMovieByIdAsync(id);
            if(movie!=null)
            {
                shoppingCart.AddItemToCart(movie);
            }
            return RedirectToAction(nameof(shoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            Movie movie = await movieService.GetMovieByIdAsync(id);
            if(movie!=null)
            {
                shoppingCart.RemoveItemFromCart(movie);
            }
            return RedirectToAction(nameof(shoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmail = User.FindFirstValue(ClaimTypes.Role);
            List<ShoppingCartItem> shoppingCartItems=shoppingCart.GetShoppingCartItems();
            await orderService.StoreOrderAsync(shoppingCartItems, userId, userEmail);
            await shoppingCart.ClearShoppingCartAsync();
            return View();
        }
    }
}
