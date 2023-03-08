using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using LangoTop.Application.Contract.Page;
using LangoTop.Domain;
using LangoTop.Interfaces;

namespace LangoTop.Infrastructure.Repository
{
    public class PageRepository : RepositoryBase<long, Page>, IPageRepository
    {
        private readonly LangoTopContext _context;

        public PageRepository(LangoTopContext context) : base(context)
        {
            _context = context;
        }

        public EditPage GetDetails(long id)
        {
            var page = _context.Pages.Select(x => new EditPage
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                ShortKey = x.ShortKey,
                Description = x.Description,
                Type = x.Type
            }).FirstOrDefault(x => x.Id == id);
            return page;
        }

        public List<PageViewModel> GetPages()
        {
            var pages = _context.Pages.Select(x => new PageViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                ShortKey = x.ShortKey,
                Description = x.Description,
                CreationDate = x.CreationDate.ToFarsi(),
                IsRemoved = x.IsRemoved,
                Type = x.Type
            }).OrderByDescending(x => x.Id).ToList();
            return pages;
        }

        public PageViewModel GetSlugBy(string key)
        {
            var page = _context.Pages.Select(x => new PageViewModel
            {
                Id = x.Id,
                Type = x.Type,
                Title = x.Title,
                ShortKey = x.ShortKey,
                IsRemoved = x.IsRemoved,
                Slug = x.Slug,
                CreationDate = x.CreationDate.ToFarsi(),
                Description = x.Description
            }).Where(x => !x.IsRemoved).FirstOrDefault(x => x.ShortKey == key);
            if (page != null)
                return page;

            return new PageViewModel();
        }
    }
}
