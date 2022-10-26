using System.Collections.Generic;
using _01_Query.Contracts.Article;
using _01_Query.Contracts.ArticleCategory;
using LangoTop.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public ArticleQueryModel Article;
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> LatestArticleCategories;

        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICommentApplication _commentApplication;

        public ArticleModel(IArticleQuery articleQuery, ICommentApplication commentApplication,
            IArticleCategoryQuery articleCategoryQuery)
        {
            _articleQuery = articleQuery;
            _commentApplication = commentApplication;
            _articleCategoryQuery = articleCategoryQuery;
        }


        public void OnGet(string id)
        {
            Article = _articleQuery.GetArticleDetails(id);
            LatestArticleCategories = _articleCategoryQuery.GetArticleCategories();
            LatestArticles = _articleQuery.LatestArticles();
        }
        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type = CommentType.Article;
            var result = _commentApplication.AddComment(command);
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}
