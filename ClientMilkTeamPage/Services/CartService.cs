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

        public void AddToCart(CartItem item)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(i => i.TeaID == item.TeaID);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                existingItem.SelectedMaterials = item.SelectedMaterials;
            }
            else
            {
                cart.Add(item);
            }
            SaveCart(cart);
        }

        public void UpdateCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            string cartJson = JsonConvert.SerializeObject(cart);
            session.SetString("Cart", cartJson);
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
