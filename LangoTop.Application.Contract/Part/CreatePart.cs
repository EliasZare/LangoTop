using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using LangoTop.Application.Contract.Section;

namespace LangoTop.Application.Contract.Part
{
    public class CreatePart
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(150, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Title { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string DownloadLink { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long SectionId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(60, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Time { get; set; }

        public List<SectionViewModel> Sections { get; set; }
    }
}
