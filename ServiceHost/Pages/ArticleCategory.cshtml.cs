using _01_Query.Contracts.Article;
using _01_Query.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategory;
        public PagingArticleQueryModel Articles;
        public ArticleCategoryQueryModel ArticleCategory;
        [TempData] public string PageId { get; set; }

        public ArticleCategoryModel(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public void OnGet(string slug, int id = 1)
        {
            PageId = id.ToString();
            Articles = _articleQuery.GetArticlesBy(slug);
            ArticleCategory = _articleCategory.GetArticleCategory(slug);
        }
    }
}
