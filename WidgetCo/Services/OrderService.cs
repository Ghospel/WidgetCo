using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using DAL;
using Models;

namespace WidgetAndCo.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly ITablesService _tablesService;
        private readonly IQueueService _queueService;

        public OrderService(OrderRepository orderRepository, ITablesService tablesService, IQueueService queueService)
        {
            _orderRepository = orderRepository;
            _tablesService = tablesService;
            _queueService = queueService;
        }

        public Task<Order> GetEntity(string id)
        {
            return Task.FromResult(_orderRepository.Find(id));
        }

        public Task<IEnumerable<Order>> GetEntities()
        {
            return Task.FromResult(_orderRepository.FetchAll().AsEnumerable());
        }

        public Task<Order> CreateEntity(Order entity)
        {
            // enqueue the order
            _queueService.SendMessage("Orders", JsonSerializer.Serialize(entity));
            return Task.FromResult(entity);
        }

        public Task<Order> UpdateEntity(Order entity)
        {
            return Task.FromResult(_orderRepository.Update(entity));
        }

        public Task<Order> DeleteEntity(string id)
        {
            Order order = _orderRepository.Find(id);
            if (order == null) throw new Exception("Entity not found");
            return Task.FromResult(_orderRepository.Delete(order));
        }

        public void ShipOrder(Order order)
        {
            _tablesService.UpsertTableEntity(order);
        }

        public void ShipOrder(string id)
        {
            Order order = _orderRepository.Find(id);
            _tablesService.UpsertTableEntity(order);
        }

    }
}

