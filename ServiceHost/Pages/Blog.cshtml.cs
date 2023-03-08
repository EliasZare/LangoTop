using _01_Query.Contracts.Article;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class BlogModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        public PagingArticleQueryModel Articles;

        public BlogModel(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public void OnGet(int id = 1)
        {
            Articles = _articleQuery.GetArticles(id);
        }
    }
}
