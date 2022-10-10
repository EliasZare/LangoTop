using System.Collections.Generic;
using _0_Framework.Application;
using LangoTop.Application.Contract.Article;
using LangoTop.Domain;
using LangoTop.Interfaces;

namespace LangoTop.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();

            if (_articleRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var slug = command.Slug.Slugify();
            var filePath = $"Articles/{slug}";
            var pictureSmallFilePath = $"Articles/{slug}-small";
            var fileName = _fileUploader.Upload(command.Picture, filePath);

            var pictureSmallFileName = _fileUploader.Upload(command.PictureSmall, pictureSmallFilePath);

            var article = new Article(command.Title, command.PageTitle, command.AuthorId, command.ShortDescription,
                command.Description,
                fileName,
                pictureSmallFileName,
                command.PictureAlt, command.PictureTitle, command.CategoryId, command.Keywords, command.MetaDescription,
                slug, command.ShortLink);

            _articleRepository.Create(article);
            _articleRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            var article = _articleRepository.Get(command.Id);

            if (article == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_articleRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var slug = command.Slug.Slugify();
            var filePath = $"{slug}";
            var pictureSmallFilePath = $"Articles/{slug}-small";
            var fileName = _fileUploader.Upload(command.Picture, filePath);
            var pictureSmallFileName = _fileUploader.Upload(command.PictureSmall, pictureSmallFilePath);

            article.Edit(command.Title, command.PageTitle, command.AuthorId, command.ShortDescription,
                command.Description, fileName,
                pictureSmallFileName,
                command.PictureAlt,
                command.PictureTitle, command.CategoryId, command.Keywords, command.MetaDescription, slug,
                command.ShortLink);

            _articleRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var courseCategory = _articleRepository.Get(id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            courseCategory.Remove();

            _articleRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var courseCategory = _articleRepository.Get(id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            courseCategory.ReStore();

            _articleRepository.SaveChanges();
            return operation.Success();
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> GetArticles()
        {
            return _articleRepository.GetArticles();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}