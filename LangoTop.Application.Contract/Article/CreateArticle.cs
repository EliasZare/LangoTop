using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace LangoTop.Application.Contract.Article
{
    public class CreateArticle
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(150, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Title { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(60, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string PageTitle { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long AuthorId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(150, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Description { get; set; }

        [MaxFileSize(4 * 1024 * 1024, ErrorMessage = "حجم فایل مورد نظر بیشتر از 524kb است")]
        public IFormFile Picture { get; set; }

        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = "حجم فایل مورد نظر بیشتر از 262kb است")]
        public IFormFile PictureSmall { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(125, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(60, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string PictureTitle { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(120, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(150, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(500, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string ShortLink { get; set; }
    }
}