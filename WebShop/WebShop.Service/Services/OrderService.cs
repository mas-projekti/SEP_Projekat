using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.DomainModels;
using WebShop.Models.Enums;
using WebShop.Repository.Contract.Interfaces;
using WebShop.Service.Contract.Dto;
using WebShop.Service.Contract.Services;

namespace WebShop.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrderById(int orderId) => _mapper.Map<OrderDto>(await _orderRepository.GetOrderWithOrderItems(orderId));
        public async Task<IEnumerable<OrderDto>> GetUserOrders(int userId) => _mapper.Map<IEnumerable<OrderDto>>(await _orderRepository.GetUserOrders(userId));

        public async Task<OrderDto> InsertOrder(InputOrderDto inputOrder)
        {

            Order newOrder = CreateNewOrder(inputOrder);
            Order order = await _orderRepository.Add(newOrder);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> UpdateOrderStatus(Guid transactionId)
        {
            Order order = await _orderRepository.UpdateOrderStatus(transactionId, OrderStatus.Ordered);

            await UpdateProductAmount(order);

            return _mapper.Map<OrderDto>(order);
  

        }


        private async Task UpdateProductAmount(Order order)
        {
            foreach(OrderItem orderItem in order.OrderItems)
            {
                await _productRepository.UpdateProductAmount(orderItem.ProductId, orderItem.Amount);
            }
        }

        private Order CreateNewOrder(InputOrderDto orderData)
        {

            List<OrderItem> orderitems = new List<OrderItem>();

            foreach(InputOrderProductDto p in orderData.Products)
            {
                orderitems.Add(new OrderItem
                {
                    Id = 0,
                    Amount = p.Amount,
                    Price = p.Price,
                    ProductId = p.ProductId

                });
            }

            return new Order()
            {
                CustomerId = orderData.CustomerId,
                Id = 0,
                OrderItems = orderitems,
                OrderStatus = OrderStatus.Pending,
                Timestamp = DateTime.Now
            };
        }

    }
}
