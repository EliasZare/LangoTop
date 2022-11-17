using System.Collections.Generic;
using _01_Query.Contracts.Article;
using _01_Query.Contracts.ArticleCategory;
using _01_Query.Contracts.Banner;
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
        public BannerQueryModel AdsBanner;

        private readonly IBannerQuery _bannerQuery;
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICommentApplication _commentApplication;

        public ArticleModel(IArticleQuery articleQuery, ICommentApplication commentApplication,
            IArticleCategoryQuery articleCategoryQuery, IBannerQuery bannerQuery)
        {
            _articleQuery = articleQuery;
            _commentApplication = commentApplication;
            _articleCategoryQuery = articleCategoryQuery;
            _bannerQuery = bannerQuery;
        }


        public void OnGet(string id)
        {
            Article = _articleQuery.GetArticleDetails(id);
            LatestArticleCategories = _articleCategoryQuery.GetArticleCategories();
            LatestArticles = _articleQuery.LatestArticles();
            AdsBanner = _bannerQuery.GetBannerBy(3);
            AdsBanner.Width = AdsBanner.Width + "px";
            AdsBanner.Height = AdsBanner.Height + "px";
        }
        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type = CommentType.Article;
            var result = _commentApplication.AddComment(command);
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}
