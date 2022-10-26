using System.Collections.Generic;
using _01_Query.Contracts.Article;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class BlogModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        public List<ArticleQueryModel> Articles;

        public BlogModel(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public void OnGet()
        {
            Articles = _articleQuery.GetArticles();
        }
    }
}
