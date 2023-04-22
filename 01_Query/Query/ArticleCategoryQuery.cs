using System.Collections.Generic;
using System.Linq;
using _01_Query.Contracts.ArticleCategory;
using LangoTop.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace _01_Query.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly LangoTopContext _context;

        public ArticleCategoryQuery(LangoTopContext context)
        {
            _context = context;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _context.ArticleCategories
                .Where(x => !x.IsRemoved)
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    ArticlesCounts = x.Articles.Count,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    Slug = x.Slug,
                    IsRemoved = x.IsRemoved
                }).Take(12).ToList();
        }

        public ArticleCategoryQueryModel GetArticleCategory(string slug)
        {
            return _context.ArticleCategories.Select(x => new ArticleCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                MetaDescription = x.MetaDescription,
                Keywords = x.Keywords,
                Description = x.Description,
                IsRemoved = x.IsRemoved,
                Picture = x.Picture
            }).Where(x => !x.IsRemoved).FirstOrDefault(x => x.Slug == slug);
        }
    }
}
