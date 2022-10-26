using System.Collections.Generic;

namespace LangoTop.Application.Contract.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);
        double GetAmountBy(long id);
        double GetTotalSale();
        long GetOrderCounts();
        string PaymentSucceeded(long orderId, long refId);
        void Cancel(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        List<OrderItemViewModel> GetItems(long orderId);
        OrderViewModel GetInfoBy(long id);
    }
}
