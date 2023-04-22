using System.Collections.Generic;
using _0_Framework.Application;
using LangoTop.Application.Contract.Image;
using LangoTop.Domain;
using LangoTop.Interfaces;

namespace LangoTop.Application
{
    public class ImageApplication : IImageApplication
    {
        private readonly IImageRepository _imageRepository;
        private readonly IFileUploader _fileUploader;

        public ImageApplication(IImageRepository imageRepository, IFileUploader fileUploader)
        {
            _imageRepository = imageRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateImage command)
        {
            var operation = new OperationResult();

            var fileName = _fileUploader.Upload(command.Image, $"/Upload//{command.Image.FileName}");
            var link = $"https://langotop.ir/Pictures/{fileName}";
            var createImage = new Image(command.Name, fileName, link);
            _imageRepository.Create(createImage);
            _imageRepository.SaveChanges();
            return operation.Success();
        }

        public List<ImageViewModel> GetImages()
        {
            return _imageRepository.GetImages();
        }
    }
}
