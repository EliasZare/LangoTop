using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace LangoTop.Application.Contract.Account
{
    public class ChangePassword
    {
        public long Id { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string RePassword { get; set; }
        public string Code { get; set; }
    }
}