using System.Collections.Generic;
using _0_Framework.Application;
using LangoTop.Application.Contract.Page;
using LangoTop.Domain;
using LangoTop.Interfaces;

namespace LangoTop.Application
{
    public class PageApplication : IPageApplication
    {
        public readonly IPageRepository _pageRepository;

        public PageApplication(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public OperationResult Create(CreatePage command)
        {
            var operation = new OperationResult();

            if (_pageRepository.Exists(x => x.Slug == command.Slug))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var page = new Page(command.Title, command.Slug, command.Description, command.Type, command.ShortKey);
            _pageRepository.Create(page);
            _pageRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditPage command)
        {
            var operation = new OperationResult();

            var page = _pageRepository.Get(command.Id);


            if (page == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_pageRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            page.Edit(command.Title, command.Slug, command.Description, command.Type, command.ShortKey);
            _pageRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var part = _pageRepository.Get(id);

            if (part == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            part.Remove();
            _pageRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var part = _pageRepository.Get(id);

            if (part == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            part.ReStore();
            _pageRepository.SaveChanges();
            return operation.Success();
        }

        public EditPage GetDetails(long id)
        {
            return _pageRepository.GetDetails(id);
        }

        public PageViewModel GetSlugBy(string key)
        {
            return _pageRepository.GetSlugBy(key);
        }

        public List<PageViewModel> GetPages()
        {
            return _pageRepository.GetPages();
        }
    }
}
