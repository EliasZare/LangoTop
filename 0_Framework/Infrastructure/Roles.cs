namespace _0_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Administrator = "1";
        public const string SystemUser = "2";
        public const string Teacher = "3";
        public const string Admin = "4";

        public static string GetRoleBy(long id)
        {
            switch (id)
            {
                case 1:
                    return "مدیر سیستم";
                case 2:
                    return "کاربر";
                case 3:
                    return "مدرس";
                case 4:
                    return "ادمین";
                default:
                    return "Default";
            }
        }
    }
}
