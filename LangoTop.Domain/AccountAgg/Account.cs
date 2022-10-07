using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Domain.ArticleAgg;
using LangoTop.Domain.CourseAgg;

namespace LangoTop.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public Account(string fullname, string username, string email, string mobile, string password,
            string profilePhoto, string activeCode)
        {
            Fullname = fullname;
            Username = username;
            Email = email;
            Mobile = mobile;
            Password = password;
            ProfilePhoto = profilePhoto;
            ActiveCode = activeCode;
            IsActive = false;
        }

        public string Fullname { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }
        public string ProfilePhoto { get; private set; }
        public string ActiveCode { get; private set; }
        public bool IsActive { get; private set; }
        public List<Article> Articles { get; set; }
        public List<Course> Courses { get; set; }

        public void Edit(string fullname, string username, string email, string mobile,
            string profilePhoto, string activeCode)
        {
            Fullname = fullname;
            Username = username;
            Email = email;
            Mobile = mobile;

            if (!string.IsNullOrWhiteSpace(profilePhoto))
                ProfilePhoto = profilePhoto;

            ActiveCode = activeCode;
            IsActive = false;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void Active()
        {
            IsActive = true;
        }
    }
}