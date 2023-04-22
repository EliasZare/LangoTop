using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Image;
using LangoTop.Domain;

namespace LangoTop.Interfaces
{
    public interface IImageRepository : IRepository<long, Image>
    {
        List<ImageViewModel> GetImages();
    }
}
