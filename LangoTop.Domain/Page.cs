using _0_Framework.Domain;

namespace LangoTop.Domain
{
    public class Page : EntityBase
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ShortKey { get; set; }
        public int Type { get; set; }
        public bool IsRemoved { get; set; }

        public Page(string title, string slug, string description, int type, string shortKey)
        {
            Title = title;
            Slug = slug;
            Description = description;
            ShortKey = shortKey;
            IsRemoved = false;
            Type = type;
        }

        public void Edit(string title, string slug, string description, int type, string shortKey)
        {
            Title = title;
            Slug = slug;
            Description = description;
            ShortKey = shortKey;
            Type = type;
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
