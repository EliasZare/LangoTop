using LangoTop.Application.Contract.Account;
using LangoTop.Application.Contract.Article;
using LangoTop.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class CreateModel : PageModel
    {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IAccountApplication _accountApplication;
        public SelectList ArticleCategories;
        public SelectList Authors;
        public CreateArticle Command;

        public CreateModel(IArticleApplication articleApplication,
            IArticleCategoryApplication articleCategoryApplication, IAccountApplication accountApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
            _accountApplication = accountApplication;
        }


        public void OnGet()
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
            Authors = new SelectList(_accountApplication.GetAccounts(), "Id", "Fullname");
        }

        public IActionResult OnPost(CreateArticle command)
        {
            ;
            var result = _articleApplication.Create(command);
            return RedirectToPage("./Index");
        }
    }
}