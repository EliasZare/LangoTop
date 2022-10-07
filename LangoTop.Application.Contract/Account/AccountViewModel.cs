using System.Collections.Generic;

namespace LangoTop.Application.Contract.Account
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ProfilePhoto { get; set; }
        public bool IsActive { get; set; }
        public string CreationDate { get; set; }
        public List<string> Courses { get; set; }
        public List<string> Articles { get; set; }
    }
}