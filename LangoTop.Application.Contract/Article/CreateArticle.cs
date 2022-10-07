﻿using Microsoft.AspNetCore.Http;

namespace LangoTop.Application.Contract.Article
{
    public class CreateArticle
    {
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public long AuthorId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        public IFormFile PictureSmall { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public long CategoryId { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string ShortLink { get; set; }
    }
}