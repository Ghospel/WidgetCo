using Microsoft.AspNetCore.Mvc;
using Models;
using WidgetAndCo.Services;

namespace WidgetAndCo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            IEnumerable<Order> result = await _orderService.GetEntities();
            return result.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(string id)
        {
            var order = await _orderService.GetEntity(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(string id, Order order)
        {
            if (id != order.id)
            {
                return BadRequest();
            }

            await _orderService.UpdateEntity(order);

            return NoContent();
        }

        [HttpPost("{id}/ship")]
        public async Task<ActionResult<Order>> ShipOrder(string id)
        {
            _orderService.ShipOrder(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            return await _orderService.CreateEntity(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            await _orderService.DeleteEntity(id);
            return NoContent();
        }
    }
}
