using _0_Framework.Domain;

namespace LangoTop.Domain
{
    public class OrderItem : EntityBase
    {
        public long ProductId { get; }
        public int Count { get; }
        public double UnitPrice { get; }
        public int DiscountRate { get; }
        public long OrderId { get; set; }
        public Order Order { get; set; }

        public OrderItem(long productId, int count, double unitPrice, int discountRate)
        {
            ProductId = productId;
            Count = count;
            UnitPrice = unitPrice;
            DiscountRate = discountRate;
        }
    }
}