using System.Collections.Generic;
using LangoTop.Application.Contract.Course;
using LangoTop.Application.Contract.DiscountCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Discount.DiscountCode
{
    public class IndexModel : PageModel
    {
        private readonly ICourseApplication _courseApplication;

        private readonly IDiscountCodeApplication _discountCodeApplication;
        public List<DiscountCodeViewModel> DiscountCodes;
        public DiscountCodeSearchModel SearchModel;

        public IndexModel(IDiscountCodeApplication discountCodeApplication, ICourseApplication courseApplication)
        {
            _discountCodeApplication = discountCodeApplication;
            _courseApplication = courseApplication;
        }

        public SelectList Courses { get; set; }


        public void OnGet(DiscountCodeSearchModel searchModel)
        {
            DiscountCodes = _discountCodeApplication.Search(searchModel);
            Courses = new SelectList(_courseApplication.GetCourses(), "Id", "Title");
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineDiscountCode
            {
                Courses = _courseApplication.GetCourses()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineDiscountCode command)
        {
            var result = _discountCodeApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var discountCode = _discountCodeApplication.GetDetails(id);
            discountCode.Courses = _courseApplication.GetCourses();
            return Partial("./Edit", discountCode);
        }

        public JsonResult OnPostEdit(EditDiscountCode command)
        {
            var result = _discountCodeApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}