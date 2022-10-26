using System.Collections.Generic;
using LangoTop.Application.Contract.Role;
using Microsoft.AspNetCore.Http;

namespace LangoTop.Application.Contract.Account
{
    public class RegisterAccount
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
        public string Biography { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        public string ActiveCode { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}