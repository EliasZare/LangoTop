using System.Collections.Generic;
using LangoTop.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategory
{
    public class IndexModel : PageModel
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public List<ArticleCategoryViewModel> ArticleCategories;
        public ArticleCategorySearchModel SearchModel;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        public string Message { get; set; }

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _articleCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticleCategory());
        }

        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _articleCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var articleCategory = _articleCategoryApplication.GetDetails(id);
            return Partial("./Edit", articleCategory);
        }

        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            var result = _articleCategoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _articleCategoryApplication.Remove(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _articleCategoryApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}