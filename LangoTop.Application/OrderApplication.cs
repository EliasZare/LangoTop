using System.Collections.Generic;
using _0_Framework.Application;
using LangoTop.Application.Contract.Order;
using LangoTop.Domain;
using LangoTop.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LangoTop.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;

        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _configuration = configuration;
        }

        public long PlaceOrder(Cart cart)
        {
            var currentAccountId = _authHelper.CurrentAccountId();

            var order = new Order(currentAccountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount);

            foreach (var cartItem in cart.Items)
            {
                var orderItem =
                    new OrderItem(cartItem.Id, cartItem.Count, cartItem.UnitPrice, cartItem.DiscountRate);
                order.Add(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChanges();
            return order.Id;
        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }

        public double GetTotalSale()
        {
            return _orderRepository.GetTotalSale();
        }

        public long GetOrderCounts()
        {
            return _orderRepository.GetOrderCounts();
        }

        public string PaymentSucceeded(long orderId, long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(1);
            var symbol = "S";
            var issueTrackingNo = CodeGenerator.Generate(symbol);
            order.SetIssueTrackingNo(issueTrackingNo);

            _orderRepository.SaveChanges();
            return issueTrackingNo;
        }

        public void Cancel(long id)
        {
            var order = _orderRepository.Get(id);
            order.Cancel();
            _orderRepository.SaveChanges();
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            return _orderRepository.Search(searchModel);
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            return _orderRepository.GetItems(orderId);
        }

        public OrderViewModel GetInfoBy(long id)
        {
            return _orderRepository.GetInfoBy(id);
        }
    }
}