using System.Collections.Generic;
using _0_Framework.Application;
using _01_Query.Contracts.Course;
using _01_Query.Contracts.Order;
using LangoTop.Application.Contract.Comment;
using LangoTop.Application.Contract.Order;
using LangoTop.Application.Contract.Part;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class CourseModel : PageModel
    {
        private readonly ICommentApplication _commentApplication;
        private readonly ICourseQuery _courseQuery;
        private readonly IAuthHelper _authHelper;
        private readonly IOrderQuery _orderQuery;
        private readonly IPartApplication _partApplication;
        private readonly IOrderApplication _orderApplication;
        public bool IsPaid;

        //[TempData] public string DownloadLink { get; set; }
        public CourseQueryModel Course;
        public List<CourseQueryModel> LatestCourses;

        public CourseModel(ICourseQuery courseQuery, ICommentApplication commentApplication, IAuthHelper authHelper,
            IOrderQuery orderQuery, IPartApplication partApplication, IOrderApplication orderApplication)
        {
            _courseQuery = courseQuery;
            _commentApplication = commentApplication;
            _authHelper = authHelper;
            _orderQuery = orderQuery;
            _partApplication = partApplication;
            _orderApplication = orderApplication;
        }

        public void OnGet(string id)
        {
            Course = _courseQuery.GetDetails(id);
            LatestCourses = _courseQuery.LatestCourses(3);
        }

        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type = CommentType.Course;
            var result = _commentApplication.AddComment(command);
            return RedirectToPage("/Course", new {Id = articleSlug});
        }

        public IActionResult OnGetDownloadFile(long partId, long courseId)
        {
            var account = _authHelper.CurrentAccountInfo();
            if (account == null) return RedirectToPage("Login");
            var course = _courseQuery.GetDetails(courseId);
            if (course.DoublePrice < 1)
            {
                IsPaid = true;
                var part = _partApplication.GetDetails(partId);
                if (part == null) return RedirectToPage("/");
                return Redirect(part.DownloadLink);
            }

            var paidCourses = _orderQuery.GetCoursesBy(account.Id);

            foreach (var course1 in paidCourses)
                if (course1.Id == courseId)
                {
                    IsPaid = true;
                    break;
                }

            if (IsPaid)
            {
                var part = _partApplication.GetDetails(partId);
                if (part == null) return RedirectToPage("/");
                return Redirect(part.DownloadLink);
            }

            return RedirectToPage("/Aboutus");
        }

        public IActionResult OnGetFreeCourse(long courseId, long partId)
        {
            if (_authHelper.IsAuthenticated())
            {
                var course = _courseQuery.GetDetails(courseId);
                var account = _authHelper.CurrentAccountInfo();
                var paidCourses = _orderQuery.GetCoursesBy(account.Id);
                foreach (var course1 in paidCourses)
                    if (course1.Id == courseId)
                    {
                        IsPaid = true;
                        break;
                    }

                if (!IsPaid)
                {
                    if (course.DoublePrice <= 0)
                    {
                        var cartItems = new List<CartItem>();
                        cartItems.Add(new CartItem
                        {
                            Id = courseId,
                            Count = 1,
                            DiscountRate = course.DiscountRate,
                            Name = course.Title,
                            Picture = course.Picture,
                            UnitPrice = course.DoublePrice
                        });
                        var cart = new Cart
                        {
                            Items = cartItems
                        };
                        var oId = _orderApplication.PlaceOrder(cart);
                        _orderApplication.PaymentSucceeded(oId, 1);
                        var part = _partApplication.GetDetails(partId);
                        if (part == null) return RedirectToPage("/");
                        return Redirect(part.DownloadLink);
                    }

                    return RedirectToPage("/");
                }

                if (course.DoublePrice <= 0)
                {
                    var part = _partApplication.GetDetails(partId);
                    if (part == null) return RedirectToPage("/");
                    return Redirect(part.DownloadLink);
                }

                return RedirectToPage("/");
            }

            return RedirectToPage("/Login");
        }
    }
}
