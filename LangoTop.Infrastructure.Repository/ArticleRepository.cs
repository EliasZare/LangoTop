using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using LangoTop.Application.Contract.Article;
using LangoTop.Domain;
using LangoTop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LangoTop.Infrastructure.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly LangoTopContext _context;

        public ArticleRepository(LangoTopContext context) : base(context)
        {
            _context = context;
        }


        public EditArticle GetDetails(long id)
        {
            return _context.Articles.Select(x => new EditArticle
            {
                Id = x.Id,
                Title = x.Title,
                PageTitle = x.PageTitle,
                AuthorId = x.AuthorId,
                Description = x.Description,
                ShortDescription = x.ShortDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                CategoryId = x.CategoryId,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                ShortLink = x.ShortLink
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> GetArticles()
        {
            return _context.Articles.Include(x => x.ArticleCategory).Include(x => x.Author).Select(x =>
                new ArticleViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    PageTitle = x.PageTitle,
                    AuthorId = x.AuthorId,
                    Author = x.Author.Fullname,
                    Picture = x.Picture,
                    PictureSmall = x.PictureSmall,
                    ShortDescription = x.ShortDescription,
                    CategoryId = x.CategoryId,
                    CreationDate = x.CreationDate.ToFarsi(),
                    IsRemoved = x.IsRemoved,
                    Category = x.ArticleCategory.Name,
                    ShortLink = x.ShortLink
                }).OrderByDescending(x => x.Id).ToList();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles.Include(x => x.ArticleCategory).Include(x => x.Author).Select(x =>
                new ArticleViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    PageTitle = x.PageTitle,
                    AuthorId = x.AuthorId,
                    Author = x.Author.Fullname,
                    Picture = x.Picture,
                    PictureSmall = x.PictureSmall,
                    ShortDescription = x.ShortDescription,
                    CategoryId = x.CategoryId,
                    CreationDate = x.CreationDate.ToFarsi(),
                    IsRemoved = x.IsRemoved,
                    Category = x.ArticleCategory.Name,
                    ShortLink = x.ShortLink
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            if (searchModel.AuthorId > 0)
                query = query.Where(x => x.AuthorId == searchModel.AuthorId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}