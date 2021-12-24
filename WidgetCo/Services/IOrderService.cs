using Models;

namespace WidgetAndCo.Services
{
    public interface IOrderService : IService<Order>
    {
        public void ShipOrder(Order order);
        public void ShipOrder(string id);

    }

}
