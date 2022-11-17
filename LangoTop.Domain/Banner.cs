using System;
using _0_Framework.Domain;

namespace LangoTop.Domain
{
    public class Banner : EntityBase

    {
        public string CompanyName { get; set; }
        public string Link { get; set; }
        public string BannerPicture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Title { get; set; }
        public long Type { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Banner(string companyName, string link, string bannerPicture, string pictureAlt, string pictureTitle,
            string width, string height, string title, long type, DateTime startDate, DateTime endDate)
        {
            CompanyName = companyName;
            Link = link;
            BannerPicture = bannerPicture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Width = width;
            Height = height;
            Title = title;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            IsRemoved = false;
        }

        public void Edit(string companyName, string link, string bannerPicture, string pictureAlt, string pictureTitle,
            string width, string height, string title, long type, DateTime startDate, DateTime endDate)
        {
            CompanyName = companyName;
            Link = link;

            if (!string.IsNullOrWhiteSpace(bannerPicture))
                BannerPicture = bannerPicture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Width = width;
            Height = height;
            Title = title;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}

