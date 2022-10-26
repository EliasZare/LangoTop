using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using LangoTop.Application.Contract.Order;
using LangoTop.Domain;
using LangoTop.Interfaces;

namespace LangoTop.Infrastructure.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly LangoTopContext _context;

        public OrderRepository(LangoTopContext context) : base(context)
        {
            _context = context;
        }


        public List<OrderItemViewModel> GetItems(long orderId)
        {
            var products = _context.Courses.Select(x => new {x.Id, x.Title}).ToList();
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
                return new List<OrderItemViewModel>();

            var items = order.Items.Select(x => new OrderItemViewModel
            {
                Id = x.Id,
                Count = x.Count,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).ToList();

            foreach (var item in items) item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Title;
            return items;
        }

        public double GetTotalSale()
        {
            var orders = _context.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                IsPaid = x.IsPaid,
                PayAmount = x.PayAmount
            }).AsEnumerable().Where(x => x.IsPaid).OrderByDescending(x => x.Id).ToList();

            var Sale = orders.Sum(order => order.PayAmount);

            return Sale;
        }

        public long GetOrderCounts()
        {
            return _context.Orders.Where(x => x.IsPaid).ToList().Count();
        }

        public OrderViewModel GetInfoBy(long id)
        {
            var accountId = _context.Orders
                .Select(x => new {x.AccountId, x.Id})
                .ToList()
                .FirstOrDefault(x => x.Id == id)?.AccountId;

            var account = _context.Accounts
                .Select(x => new {x.Id, x.Fullname})
                .ToList()
                .FirstOrDefault(x => x.Id == accountId);

            var query = _context.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCanceled,
                IsPaid = x.IsPaid,
                IssueTrackingNo = x.IssueTrackingNo,
                PayAmount = x.PayAmount,
                RefId = x.RefId,
                TotalAmount = x.TotalAmount,
                CreationDate = x.CreationDate.ToFarsi(),
                AccountFullName = account.Fullname
            }).FirstOrDefault(x => x.Id == id);
            return query;
        }


        public double GetAmountBy(long id)
        {
            var amount = _context.Orders.Select(x => new {x.PayAmount, x.Id}).FirstOrDefault(x => x.Id == id);
            if (amount != null)
                return amount.PayAmount;

            return 0;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts = _context.Accounts.Select(x => new {x.Id, x.Fullname}).ToList();
            var query = _context.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCanceled,
                IsPaid = x.IsPaid,
                IssueTrackingNo = x.IssueTrackingNo,
                PayAmount = x.PayAmount,
                RefId = x.RefId,
                TotalAmount = x.TotalAmount,
                CreationDate = x.CreationDate.ToFarsi()
            });

            query = query.Where(x => x.IsCanceled == searchModel.IsCanceled);

            if (searchModel.AccountId > 0) query = query.Where(x => x.AccountId == searchModel.AccountId);

            if (!string.IsNullOrWhiteSpace(searchModel.IssueTrackingNo))
                query = query.Where(x => x.IssueTrackingNo == searchModel.IssueTrackingNo);

            var orders = query.OrderByDescending(x => x.Id).ToList();
            foreach (var order in orders)
                order.AccountFullName = accounts.FirstOrDefault(x => x.Id == order.AccountId)?.Fullname;

            return orders;
        }
    }
}
