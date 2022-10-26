namespace _01_Query.Contracts.Order
{
    public class OrderItemQueryModel
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Course { get; set; }
        public double UnitPrice { get; set; }
        public int DiscountRate { get; set; }
        public long OrderId { get; set; }
    }
}
