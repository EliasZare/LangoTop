using System.Collections.Generic;
using _01_Query.Contracts.Account;
using _01_Query.Contracts.Order;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Dashboard.Pages
{
    public class StudentsModel : PageModel
    {
        private readonly IOrderQuery _orderQuery;
        public List<AccountQueryModel> Students;

        public StudentsModel(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }

        public void OnGet(long id)
        {
            Students = _orderQuery.GetCourseStudents(id);
        }
    }
}
