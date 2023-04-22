using System.Collections.Generic;

namespace _01_Query.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> LatestArticles(int count);
        PagingArticleQueryModel GetArticles(int pageId = 1);
        PagingArticleQueryModel GetArticlesBy(string categorySlug, int pageId = 1);
        ArticleQueryModel GetArticleDetails(string slug);
        PagingArticleQueryModel Search(string searchModel, int pageId);
    }
}
