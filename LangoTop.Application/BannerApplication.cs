using System.Collections.Generic;
using _0_Framework.Application;
using LangoTop.Application.Contract.Banner;
using LangoTop.Domain;
using LangoTop.Interfaces;

namespace LangoTop.Application
{
    public class BannerApplication : IBannerApplication
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IFileUploader _fileUploader;

        public BannerApplication(IBannerRepository bannerRepository, IFileUploader fileUploader)
        {
            _bannerRepository = bannerRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateBanner command)
        {
            var operation = new OperationResult();

            var path = $"Ads/{command.CompanyName}";
            var filename = _fileUploader.Upload(command.BannerPicture, path);
            var banner = new Banner(command.CompanyName, command.Link, filename, command.PictureAlt,
                command.PictureTitle, command.Width, command.Height, command.Title, command.Type,
                command.StartDate.ToGeorgianDateTime(),
                command.EndDate.ToGeorgianDateTime());

            _bannerRepository.Create(banner);
            _bannerRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditBanner command)
        {
            var operation = new OperationResult();
            var banner = _bannerRepository.Get(command.Id);
            if (banner is null) return operation.Failed(ApplicationMessages.RecordNotFound);
            var path = $"Ads/{command.CompanyName}";
            var filename = _fileUploader.Upload(command.BannerPicture, path);
            banner.Edit(command.CompanyName, command.Link, filename, command.PictureAlt,
                command.PictureTitle, command.Width, command.Height, command.Title, command.Type,
                command.StartDate.ToGeorgianDateTime(),
                command.EndDate.ToGeorgianDateTime());
            _bannerRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var banner = _bannerRepository.Get(id);
            if (banner is null) return operation.Failed(ApplicationMessages.RecordNotFound);
            banner.Remove();
            _bannerRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var banner = _bannerRepository.Get(id);
            if (banner is null) return operation.Failed(ApplicationMessages.RecordNotFound);
            banner.Restore();
            _bannerRepository.SaveChanges();
            return operation.Success();
        }

        public List<BannerViewModel> GetBanners()
        {
            return _bannerRepository.GetBanners();
        }

        public EditBanner GetDetails(long id)
        {
            return _bannerRepository.GetDetails(id);
        }
    }
}
