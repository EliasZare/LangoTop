using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace LangoTop.Application.Contract.Role
{
    public class CreateRole
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Name { get; set; }
        public List<int> Permissions { get; set; }
    }
}
