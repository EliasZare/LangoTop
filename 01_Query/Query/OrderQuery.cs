using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_Query.Contracts.Account;
using _01_Query.Contracts.Course;
using _01_Query.Contracts.Order;
using LangoTop.Domain;
using LangoTop.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace _01_Query.Query
{
    public class OrderQuery : IOrderQuery
    {
        private readonly LangoTopContext _context;

        public OrderQuery(LangoTopContext context)
        {
            _context = context;
        }

        public List<CourseQueryModel> GetCoursesBy(long accountId)
        {
            var coursesId = new List<long>();
            var courses = new List<CourseQueryModel>();
            var orderItems = new List<OrderItemQueryModel>();
            var products = _context.Courses.Select(x => new {x.Id, x.Title}).ToList();
            var orders = _context.Orders.Where(x => x.AccountId == accountId && x.IsPaid);
            if (!orders.Any())
                return new List<CourseQueryModel>();

            foreach (var order in orders)
            {
                var items = order.Items.Select(x => new OrderItemQueryModel
                {
                    Id = x.Id,
                    DiscountRate = x.DiscountRate,
                    CourseId = x.ProductId,
                    UnitPrice = x.UnitPrice
                }).ToList();
                coursesId.AddRange(items.Select(item => item.CourseId));
            }

            foreach (var item in coursesId)
            {
                var course = _context.Courses.Include(x => x.Teacher).Include(x => x.CourseCategory).Select(x =>
                    new CourseQueryModel
                    {
                        Title = x.Title,
                        Id = x.Id,
                        IsRemoved = x.IsRemoved,
                        Price = x.Price.ToMoney(),
                        Teacher = x.Teacher.Fullname,
                        Category = x.CourseCategory.Name,
                        IsPaid = true,
                        Picture = x.Picture,
                        Slug = x.Slug,
                        CategorySlug = x.CourseCategory.Slug
                    }).ToList().FirstOrDefault(x => x.Id == item);
                courses.Add(course);
            }

            return courses;
        }

        public List<OrderQueryModel> GetOrders()
        {
            throw new NotImplementedException();
        }

        public List<OrderQueryModel> GetOrdersBy(long accountId)
        {
            var account = _context.Accounts
                .Select(x => new {x.Id, x.Fullname})
                .FirstOrDefault(x => x.Id == accountId);

            var query = _context.Orders.Where(x => x.AccountId == accountId).Select(x => new OrderQueryModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCanceled,
                IsPaid = x.IsPaid,
                PayAmount = x.PayAmount,
                TotalAmount = x.TotalAmount,
                CreationDate = x.CreationDate.ToFarsi(),
                AccountFullName = account.Fullname
            }).ToList();
            return query;
        }

        public List<OrderItemQueryModel> GetItems(long orderId)
        {
            var products = _context.Courses.Select(x => new {x.Id, x.Title}).ToList();
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
                return new List<OrderItemQueryModel>();

            var items = order.Items.Select(x => new OrderItemQueryModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                CourseId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).ToList();

            foreach (var item in items) item.Course = products.FirstOrDefault(x => x.Id == item.CourseId)?.Title;
            return items;
        }

        public long GetStudentCourseCount(long courseId)
        {
            long count = 0;
            var products = _context.Courses.Select(x => new {x.Id, x.Title}).ToList();
            var orders = _context.Orders.Select(x => new OrderQueryModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCanceled,
                IsPaid = x.IsPaid,
                PayAmount = x.PayAmount,
                TotalAmount = x.TotalAmount,
                CreationDate = x.CreationDate.ToFarsi(),
                OrderItems = MapItems(x.Items)
            }).AsNoTracking().ToList();


            foreach (var item in orders.SelectMany(order => order.OrderItems))
            {
                item.Course = products.FirstOrDefault(x => x.Id == item.CourseId && x.Id == courseId)?.Title;
                if (!string.IsNullOrWhiteSpace(item.Course)) count++;
            }

            return count;
        }

        public List<AccountQueryModel> GetCourseStudents(long courseId)
        {
            var students = new List<AccountQueryModel>();

            var orderQuery = new List<OrderQueryModel>();

            var student = new AccountQueryModel();

            var products = _context.Courses.Select(x => new {x.Id, x.Title}).ToList();

            var orders = _context.Orders.Select(x => new OrderQueryModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCanceled,
                IsPaid = x.IsPaid,
                PayAmount = x.PayAmount,
                TotalAmount = x.TotalAmount,
                CreationDate = x.CreationDate.ToFarsi(),
                OrderItems = MapItems(x.Items)
            }).AsNoTracking().ToList();


            foreach (var order in orders)
            foreach (var item in order.OrderItems)
                if (products.FirstOrDefault(x => x.Id == item.CourseId && x.Id == courseId) != null)
                    orderQuery.Add(orders.FirstOrDefault(x => x.Id == item.OrderId));


            foreach (var item in orderQuery)
            {
                student = _context.Accounts.Include(x => x.Role).Select(x => new AccountQueryModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    Fullname = x.Fullname,
                    PaidDate = item.CreationDate,
                    Role = x.Role.Name,
                    RoleId = x.RoleId,
                    ProfilePhoto = x.ProfilePhoto
                }).FirstOrDefault(x => x.Id == item.AccountId);
                students.Add(student);
            }

            return students.OrderByDescending(x => x.PaidDate).ToList();
        }

        public static List<OrderItemQueryModel> MapItems(List<OrderItem> section)
        {
            if (section == null) return new List<OrderItemQueryModel>();
            return section.Select(x => new OrderItemQueryModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                CourseId = x.ProductId,
                UnitPrice = x.UnitPrice,
                OrderId = x.OrderId
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
