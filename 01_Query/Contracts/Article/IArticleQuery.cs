using System.Collections.Generic;

namespace _01_Query.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> LatestArticles();
        List<ArticleQueryModel> GetArticles();
        ArticleQueryModel GetArticleDetails(string slug);
    }
}
