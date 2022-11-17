namespace _0_Framework.Application
{
    public class AdsType
    {
        public const long IndexBanner = 1;
        public const long CourseBanner = 2;
        public const long ArticleBanner = 3;
        public const long FooterLink = 4;

        public string GetName(string id)
        {
            switch (id)
            {
                case "1":
                    return "صفحه اصلی";
                case "2":
                    return "دوره";
                case "3":
                    return "مقاله";
                case "4":
                    return "لینک فوتر";
            }

            return "";
        }
    }
}
