using _0_Framework.Domain;

namespace LangoTop.Domain
{
    public class Article : EntityBase
    {
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public long AuthorId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureSmall { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public long CategoryId { get; set; }
        public bool IsRemoved { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string ShortLink { get; set; }
        public ArticleCategory ArticleCategory { get; set; }
        public Account Author { get; set; }

        public Article(string title, string pageTitle, long authorId, string shortDescription, string description,
            string picture, string pictureSmall, string pictureAlt,
            string pictureTitle, long categoryId, string keywords, string metaDescription, string slug,
            string shortLink)
        {
            Title = title;
            PageTitle = pageTitle;
            AuthorId = authorId;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureSmall = pictureSmall;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            CategoryId = categoryId;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            ShortLink = shortLink;
        }


        public void Edit(string title, string pageTitle, long authorId, string shortDescription, string description,
            string picture, string pictureSmall, string pictureAlt,
            string pictureTitle, long categoryId, string keywords, string metaDescription, string slug,
            string shortLink)
        {
            Title = title;
            PageTitle = pageTitle;
            AuthorId = authorId;
            ShortDescription = shortDescription;
            Description = description;

            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            if (!string.IsNullOrWhiteSpace(pictureSmall))
                PictureSmall = pictureSmall;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            CategoryId = categoryId;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            ShortLink = shortLink;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void ReStore()
        {
            IsRemoved = false;
        }
    }
}