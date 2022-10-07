﻿using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.ArticleCategory;
using LangoTop.Domain.ArticleCategoryAgg;

namespace LangoTop.Interfaces.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        EditArticleCategory GetDetails(long id);
        List<ArticleCategoryViewModel> GetArticleCategories();
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}