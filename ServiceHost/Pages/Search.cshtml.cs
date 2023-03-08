using _01_Query.Contracts.Course;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string SearchModelInput;

        public PagingCourseQueryModel Courses;
        private readonly ICourseQuery _courseQuery;

        public SearchModel(ICourseQuery courseQuery)
        {
            _courseQuery = courseQuery;
        }

        public void OnGet(string s, int id = 1)
        {
            SearchModelInput = s;
            Courses = _courseQuery.Search(s, id);
        }
    }
}
