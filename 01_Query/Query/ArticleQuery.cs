using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_Query.Contracts.Article;
using _01_Query.Contracts.Comment;
using LangoTop.Application.Contract.Comment;
using LangoTop.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace _01_Query.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly LangoTopContext _context;

        public ArticleQuery(LangoTopContext context)
        {
            _context = context;
        }

        public List<ArticleQueryModel> LatestArticles()
        {
            return _context.Articles.Where(x => !x.IsRemoved).Include(x => x.ArticleCategory).Include(x => x.Author)
                .Where(x => x.CreationDate <= DateTime.Now).Select(x =>
                    new ArticleQueryModel
                    {
                        Title = x.Title,
                        Picture = x.Picture,
                        PictureAlt = x.PictureAlt,
                        PictureTitle = x.PictureTitle,
                        PublishDate = x.CreationDate.ToFarsi(),
                        ShortDescription = x.ShortDescription,
                        Slug = x.Slug,
                        IsRemoved = x.IsRemoved,
                        CategoryName = x.ArticleCategory.Name,
                        PictureSmall = x.PictureSmall,
                        AuthorProfile = x.Author.ProfilePhoto,
                        AuthorName = x.Author.Fullname
                    }).AsEnumerable().OrderByDescending(x => x.Id).Take(6).ToList();
        }

        public List<ArticleQueryModel> GetArticles()
        {
            return _context.Articles.Where(x => !x.IsRemoved).Include(x => x.ArticleCategory).Include(x => x.Author)
                .Where(x => x.CreationDate <= DateTime.Now).Select(x =>
                    new ArticleQueryModel
                    {
                        Title = x.Title,
                        Picture = x.Picture,
                        PictureAlt = x.PictureAlt,
                        PictureTitle = x.PictureTitle,
                        PublishDate = x.CreationDate.ToFarsi(),
                        ShortDescription = x.ShortDescription,
                        Slug = x.Slug,
                        IsRemoved = x.IsRemoved,
                        CategoryName = x.ArticleCategory.Name,
                        PictureSmall = x.PictureSmall,
                        AuthorProfile = x.Author.ProfilePhoto,
                        AuthorName = x.Author.Fullname
                    }).AsEnumerable().OrderByDescending(x => x.Id).ToList();
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _context.Articles
                .Where(x => !x.IsRemoved)
                .Include(x => x.ArticleCategory)
                .Include(x => x.Author)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CategoryName = x.ArticleCategory.Name,
                    CategorySlug = x.ArticleCategory.Slug,
                    Slug = x.Slug,
                    Description = x.Description,
                    IsRemoved = x.IsRemoved,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.CreationDate.ToFarsi(),
                    ShortDescription = x.ShortDescription,
                    PictureSmall = x.PictureSmall,
                    AuthorProfile = x.Author.ProfilePhoto,
                    AuthorBio = x.Author.Biography,
                    AuthorName = x.Author.Fullname,
                    CategoryId = x.CategoryId,
                    PageTitle = x.PageTitle,
                    AuthorUsername = x.Author.Username
                }).FirstOrDefault(x => x.Slug == slug);

            if (article == null) return new ArticleQueryModel();
            if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordList = article.Keywords.Split(",").ToList();


            var comments = _context.Comments
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.Type == CommentType.Article)
                .Where(x => x.OwnerRecordId == article.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    Message = x.Message,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();

            foreach (var comment in comments)
                if (comment.ParentId > 0)
                    comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;

            article.Comments = comments;
            return article;
        }
    }
}
