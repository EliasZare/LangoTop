using System.Collections.Generic;

namespace _01_Query.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        List<ArticleCategoryQueryModel> GetArticleCategories();
    }
}
