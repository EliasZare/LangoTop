using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Article;
using LangoTop.Domain.ArticleAgg;

namespace LangoTop.Interfaces.ArticleAgg
{
    public interface IArticleRepository : IRepository<long, Article>
    {
        EditArticle GetDetails(long id);
        List<ArticleViewModel> GetArticles();
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}