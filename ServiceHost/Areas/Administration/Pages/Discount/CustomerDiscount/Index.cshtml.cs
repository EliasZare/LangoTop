
using System.Collections.Generic;
using LangoTop.Application.Contract.Course;
using LangoTop.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        private readonly ICourseApplication _courseApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        public SelectList Courses;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public CustomerDiscountSearchModel SearchModel;

        public IndexModel(ICustomerDiscountApplication customerDiscountApplication,
            ICourseApplication courseApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _courseApplication = courseApplication;
        }

        [TempData] public string Message { get; set; }


        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Courses = new SelectList(_courseApplication.GetCourses(), "Id", "Title");
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Courses = _courseApplication.GetCourses()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _customerDiscountApplication.GetDetails(id);
            customerDiscount.Courses = _courseApplication.GetCourses();
            return Partial("Edit", customerDiscount);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}