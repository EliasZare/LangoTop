using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using LangoTop.Application.Contract.Course;

namespace LangoTop.Application.Contract.Section
{
    public class CreateSection
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(150, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Title { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long CourseId { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}
