using System.Collections.Generic;
using _0_Framework.Application;

namespace LangoTop.Application.Contract.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle command);
        OperationResult Edit(EditArticle command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditArticle GetDetails(long id);
        List<ArticleViewModel> GetArticles();
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}