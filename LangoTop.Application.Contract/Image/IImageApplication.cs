using System.Collections.Generic;
using _0_Framework.Application;

namespace LangoTop.Application.Contract.Image
{
    public interface IImageApplication
    {
        OperationResult Create(CreateImage command);
        List<ImageViewModel> GetImages();
    }
}
