using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using LangoTop.Application.Contract.Role;
using Microsoft.AspNetCore.Http;

namespace LangoTop.Application.Contract.Account
{
    public class RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(70, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Username { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(200, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(30, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Password { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long RoleId { get; set; }

        [MaxLength(150, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Biography { get; set; }

        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = "حجم فایل مورد نظر بیشتر از حد مجاز است")]
        public IFormFile ProfilePhoto { get; set; }

        public string ActiveCode { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}