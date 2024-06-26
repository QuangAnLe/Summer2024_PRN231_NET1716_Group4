using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.ViewModel;
using Newtonsoft.Json;

namespace ClientMilkTeamPage.Services
{

    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public void AddToCart(Tea tea, int quantity)
        {
            List<CartItem> cart = GetCart();
            var existingCartItem = cart.FirstOrDefault(c => c.TeaID == tea.TeaID);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    TeaID = tea.TeaID,
                    TeaName = tea.TeaName,
                    Price = tea.Price,
                    Quantity = quantity
                });
            }

            SaveCart(cart);
        }

        public void RemoveFromCart(int teaID)
        {
            List<CartItem> cart = GetCart();
            var cartItem = cart.FirstOrDefault(c => c.TeaID == teaID);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                SaveCart(cart);
            }
        }

        public List<CartItem> GetCart()
        {
            var cartJson = Session.GetString("Cart");
            return cartJson != null ? JsonConvert.DeserializeObject<List<CartItem>>(cartJson) : new List<CartItem>();
        }

        public void SaveCart(List<CartItem> cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            Session.SetString("Cart", cartJson);
        }
    }
}
