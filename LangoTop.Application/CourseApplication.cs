using System.Collections.Generic;
using _0_Framework.Application;
using LangoTop.Application.Contract.Course;
using LangoTop.Application.Contract.Page;
using LangoTop.Domain;
using LangoTop.Interfaces;

namespace LangoTop.Application
{
    public class CourseApplication : ICourseApplication
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IPageApplication _pageApplication;
        private readonly IGenerateShortKey _generateShortKey;

        public CourseApplication(IFileUploader fileUploader, ICourseRepository courseRepository,
            IPageApplication pageApplication, IGenerateShortKey generateShortKey)
        {
            _fileUploader = fileUploader;
            _courseRepository = courseRepository;
            _pageApplication = pageApplication;
            _generateShortKey = generateShortKey;
        }

        public OperationResult Create(CreateCourse command)
        {
            var operation = new OperationResult();

            if (_courseRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var slug = command.Slug.Slugify();

            var key = _generateShortKey.Generate();
            var page = new CreatePage
            {
                Title = command.Title,
                Slug = command.Slug,
                ShortKey = key,
                Description = command.Title,
                Type = PageTypes.Course
            };
            _pageApplication.Create(page);

            var filePath = $"Courses//{slug}";
            var pictureSmallFilePath = $"Courses//{slug}";
            var fileName = _fileUploader.Upload(command.Picture, filePath);
            var pictureSmallFileName = _fileUploader.Upload(command.PictureSmall, pictureSmallFilePath);

            var course = new Course(command.Title, command.PageTitle, command.TeacherId, command.ShortDescription,
                command.Description, fileName, pictureSmallFileName,
                command.PictureAlt, command.PictureTitle, command.Level, command.Time, command.Price,
                command.CategoryId, command.Keywords, command.MetaDescription, slug, $"https://Langotop.ir/p/{key}");
            _courseRepository.Create(course);
            _courseRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditCourse command)
        {
            var operation = new OperationResult();
            var courseCategory = _courseRepository.Get(command.Id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_courseRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var slug = command.Slug.Slugify();

            var filePath = $"Courses//{slug}";
            var pictureSmallFilePath = $"Courses//{slug}-small";
            var fileName = _fileUploader.Upload(command.Picture, filePath);
            var pictureSmallFileName = _fileUploader.Upload(command.PictureSmall, pictureSmallFilePath);

            courseCategory.Edit(command.Title, command.PageTitle, command.TeacherId, command.ShortDescription,
                command.Description, fileName, pictureSmallFileName,
                command.PictureAlt, command.PictureTitle, command.Level, command.Time, command.Price,
                command.CategoryId, command.Keywords, command.MetaDescription, slug, command.ShortLink);

            _courseRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var courseCategory = _courseRepository.Get(id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            courseCategory.Remove();

            _courseRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var courseCategory = _courseRepository.Get(id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            courseCategory.Restore();

            _courseRepository.SaveChanges();
            return operation.Success();
        }

        public EditCourse GetDetails(long id)
        {
            return _courseRepository.GetDetails(id);
        }

        public List<CourseViewModel> GetCourses()
        {
            return _courseRepository.GetCourses();
        }

        public List<CourseViewModel> Search(CourseSearchModel searchModel)
        {
            return _courseRepository.Search(searchModel);
        }
    }
}