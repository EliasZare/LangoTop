using _01_Query.Contracts.Article;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        public PagingArticleQueryModel Articles;

        public ArticleCategoryModel(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public void OnGet(string slug, int id = 1)
        {
            Articles = _articleQuery.GetArticlesBy(slug);
        }
    }
}
