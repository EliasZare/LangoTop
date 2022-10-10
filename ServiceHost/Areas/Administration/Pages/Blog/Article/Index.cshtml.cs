using System.Collections.Generic;
using LangoTop.Application.Contract.Account;
using LangoTop.Application.Contract.Article;
using LangoTop.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class IndexModel : PageModel
    {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IAccountApplication _accountApplication;
        public List<ArticleViewModel> Articles;
        public ArticleSearchModel SearchModel;
        public string Message { get; set; }
        public SelectList ArticleCategories { get; set; }
        public SelectList Authors;

        public IndexModel(IArticleApplication articleApplication,
            IArticleCategoryApplication articleCategoryApplication, IAccountApplication accountApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
            _accountApplication = accountApplication;
        }


        public void OnGet(ArticleSearchModel searchModel)
        {
            Articles = _articleApplication.Search(searchModel);
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
            Authors = new SelectList(_accountApplication.GetAccounts(), "Id", "Fullname");
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _articleApplication.Remove(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }


        public IActionResult OnGetRestore(long id)
        {
            var result = _articleApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}