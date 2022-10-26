using System.Collections.Generic;
using _01_Query.Contracts.Course;

namespace _01_Query.Contracts.Order
{
    public interface IOrderQuery
    {
        List<CourseQueryModel> GetCoursesBy(long accountId);
        List<OrderQueryModel> GetOrders();
        List<OrderQueryModel> GetOrdersBy(long accountId);
        List<OrderItemQueryModel> GetItems(long orderId);
        long GetStudentCourseCount(long courseId);
    }
}
