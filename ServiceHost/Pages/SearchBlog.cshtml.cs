using _01_Query.Contracts.Article;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchBlogModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        public string SearchModelInput;
        public PagingArticleQueryModel Articles;

        public SearchBlogModel(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public void OnGet(string searchModel, int id = 1)
        {
            SearchModelInput = searchModel;
            Articles = _articleQuery.Search(searchModel, id);
        }
    }
}
