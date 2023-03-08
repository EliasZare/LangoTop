using System.ComponentModel.DataAnnotations;

namespace LangoTop.Application.Contract.Page
{
    public class CreatePage
    {
        [MaxLength(300)] public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        [MaxLength(5)] public string ShortKey { get; set; }
        public int Type { get; set; }
    }
}
