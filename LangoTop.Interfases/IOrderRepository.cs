using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Order;
using LangoTop.Domain;

namespace LangoTop.Interfaces
{
    public interface IOrderRepository : IRepository<long, Order>
    {
        List<OrderItemViewModel> GetItems(long orderId);
        double GetTotalSale();
        long GetOrderCounts();
        OrderViewModel GetInfoBy(long id);
        double GetAmountBy(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
    }
}
