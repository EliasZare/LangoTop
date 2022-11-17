using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace LangoTop.Application.Contract.CourseCategory

{
    public class CreateCourseCategory
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(200, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(1000, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Description { get; set; }

        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = "حجم فایل مورد نظر بیشتر از 262kb است")]
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(125, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(60, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(120, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(150, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(200, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Slug { get; set; }
    }
}