using System.Collections.Generic;

namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public AuthViewModel()
        {
        }

        public AuthViewModel(long id, long roleId, string fullname, string username, string mobile, string email,
            string biography,
            List<int> permissions, string profilePicture)
        {
            Id = id;
            RoleId = roleId;
            Fullname = fullname;
            Username = username;
            Mobile = mobile;
            Email = email;
            Biography = biography;
            Permissions = permissions;
            ProfilePicture = profilePicture;
        }

        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        public List<int> Permissions { get; set; }
        public string ProfilePicture { get; set; }
    }
}