using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using LangoTop.Application.Contract.Banner;
using LangoTop.Domain;
using LangoTop.Interfaces;

namespace LangoTop.Infrastructure.Repository
{
    public class BannerRepository : RepositoryBase<long, Banner>, IBannerRepository
    {
        private readonly LangoTopContext _context;

        public BannerRepository(LangoTopContext context) : base(context)
        {
            _context = context;
        }

        public List<BannerViewModel> GetBanners()
        {
            return _context.Banners.Select(x => new BannerViewModel
            {
                Id = x.Id,
                BannerPicture = x.BannerPicture,
                Title = x.Title,
                CompanyName = x.CompanyName,
                EndDate = x.EndDate.ToFarsi(),
                Height = x.Height,
                Link = x.Link,
                StartDate = x.StartDate.ToFarsi(),
                Type = x.Type,
                Width = x.Width,
                IsRemoved = x.IsRemoved
            }).ToList();
        }

        public EditBanner GetDetails(long id)
        {
            return _context.Banners.Select(x => new EditBanner
            {
                Id = x.Id,
                Title = x.Title,
                CompanyName = x.CompanyName,
                EndDate = x.EndDate.ToString(),
                Height = x.Height,
                Link = x.Link,
                StartDate = x.StartDate.ToString(),
                Type = x.Type,
                Width = x.Width,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}
