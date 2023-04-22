using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using LangoTop.Application.Contract.Image;
using LangoTop.Domain;
using LangoTop.Interfaces;

namespace LangoTop.Infrastructure.Repository
{
    public class ImageRepository : RepositoryBase<long, Image>, IImageRepository
    {
        private readonly LangoTopContext _context;

        public ImageRepository(LangoTopContext context) : base(context)
        {
            _context = context;
        }

        public List<ImageViewModel> GetImages()
        {
            return _context.Images.Select(x => new ImageViewModel
            {
                Id = x.Id,
                CreationDate = x.CreationDate.ToFarsi(),
                Link = x.Link,
                Image = x.File,
                Name = x.Name
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
