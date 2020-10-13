 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 using WebStore.Interfaces.Services;

 namespace WebStore.Controllers
{

    [Route("api/cart")] // http://localhost:5000/api/cart
    [ApiController]
    public class CartApiController : ControllerBase
    {
        private readonly ICartService _CartService;

        public CartApiController(ICartService CartService) => _CartService = CartService;

        [HttpGet("view")]
        public IActionResult CartView() => new ViewComponentResult {ViewComponentName = "Cart"};

        [HttpGet("add/{id}")]
        [HttpGet("increment/{id}")]
        public IActionResult AddToCart(int id)
        {
            _CartService.AddToCart(id);
            //return Json(new { id, message = $"Товар с id:{id} был добавлен в корзину" }); - если бы наследовались от просто  Controller
            return new JsonResult(new { id, message = $"Товар с id:{id} был добавлен в корзину" });
        }

        [HttpGet("decrement/{id}")]
        public IActionResult DecrementFromCart(int id)
        {
            _CartService.DecrementFromCart(id);
            return Ok();
        }

        [HttpGet("remove/{id}")]
        public IActionResult RemoveFromCart(int id)
        {
            _CartService.RemoveFromCart(id);
            return Ok();
        }


        [HttpGet("clear")]
        public IActionResult Clear()
        {
            _CartService.Clear();
            return Ok();
        }
    }
}
