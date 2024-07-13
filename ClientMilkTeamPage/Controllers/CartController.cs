using Microsoft.AspNetCore.Mvc;
using ClientMilkTeamPage.Services;
using System.Text.Json;
using ClientMilkTeamPage.ViewModel;

namespace ClientMilkTeamPage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("UpdateMaterial")]
        public IActionResult UpdateMaterial([FromBody] UpdateMaterialRequest request)
        {
            var cartItems = _cartService.GetCart();
            var cartItem = cartItems.FirstOrDefault(item => item.TeaID == request.TeaId);
            if (cartItem != null)
            {
                var selectedMaterial = cartItem.SelectedMaterials.FirstOrDefault(sm => sm.MaterialID == request.MaterialId);
                if (selectedMaterial != null)
                {
                    if (request.Quantity > 0)
                    {
                        selectedMaterial.Quantity = request.Quantity;
                    }
                    else
                    {
                        cartItem.SelectedMaterials.Remove(selectedMaterial);
                    }
                }
                else if (request.Quantity > 0)
                {
                    cartItem.SelectedMaterials.Add(new SelectedMaterial
                    {
                        MaterialID = request.MaterialId,
                        MaterialName = request.MaterialName,
                        Price = request.MaterialPrice,
                        Quantity = request.Quantity
                    });
                }

                cartItem.TotalPrice = CalculateTotalPrice(cartItem);
                _cartService.UpdateCart(cartItems);

                return Ok(new
                {
                    itemTotal = cartItem.TotalPrice,
                    cartTotal = cartItems.Sum(item => item.TotalPrice)
                });
            }

            return BadRequest();
        }

        private double CalculateTotalPrice(CartItem item)
        {
            return (item.Price + item.SelectedMaterials.Sum(m => m.Price * m.Quantity)) * item.Quantity;
        }
    }

    public class UpdateMaterialRequest
    {
        public int TeaId { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public double MaterialPrice { get; set; }
        public int Quantity { get; set; }
    }
}