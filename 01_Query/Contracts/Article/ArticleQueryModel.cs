using System.Collections.Generic;
using _01_Query.Contracts.Comment;

namespace _01_Query.Contracts.Article
{
    public class ArticleQueryModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureSmall { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string PublishDate { get; set; }
        public string AuthorProfile { get; set; }
        public string AuthorName { get; set; }
        public string Slug { get; set; }
        public string MetaDescription { get; set; }
        public string Keywords { get; set; }
        public List<string> KeywordList { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
        public string AuthorUsername { get; set; }
        public List<CommentQueryModel> Comments { get; set; }
        public bool IsRemoved { get; set; }
        public string AuthorBio { get; set; }
        public string ShortLink { get; set; }
    }
}
