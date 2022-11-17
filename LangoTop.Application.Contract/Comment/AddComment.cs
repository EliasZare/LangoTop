using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace LangoTop.Application.Contract.Comment
{
    public class AddComment
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(50, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(500, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(1000, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Message { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCanceled { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long OwnerRecordId { get; set; }
        public int Type { get; set; }
        public long ParentId { get; set; }
    }
}
