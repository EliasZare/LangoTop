using System.Collections.Generic;
using _0_Framework.Application;
using _01_Query.Contracts.Order;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Dashboard.Pages
{
    public class OrderModel : PageModel
    {
        public List<OrderQueryModel> Orders { get; set; }
        private readonly IOrderQuery _orderQuery;
        private readonly IAuthHelper _authHelper;

        public OrderModel(IOrderQuery orderQuery, IAuthHelper authHelper)
        {
            _orderQuery = orderQuery;
            _authHelper = authHelper;
        }

        public void OnGet()
        {
            Orders = _orderQuery.GetOrdersBy(_authHelper.CurrentAccountInfo().Id);
        }
    }
}
