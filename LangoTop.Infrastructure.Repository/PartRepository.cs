using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using LangoTop.Application.Contract.Part;
using LangoTop.Domain;
using LangoTop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LangoTop.Infrastructure.Repository
{
    public class PartRepository : RepositoryBase<long, Part>, IPartRepository
    {
        private readonly LangoTopContext _context;

        public PartRepository(LangoTopContext context) : base(context)
        {
            _context = context;
        }

        public EditPart GetDetails(long id)
        {
            return _context.Parts.Select(x => new EditPart
            {
                Id = x.Id,
                Title = x.Title,
                SectionId = x.SectionId,
                DownloadLink = x.DownloadLink
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<PartViewModel> GetPartsBy(long sectionId)
        {
            return _context.Parts.Where(x => x.SectionId == sectionId).Include(x => x.Section)
                .ThenInclude(x => x.Course).Select(x =>
                    new PartViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        SectionId = x.SectionId,
                        DownloadLink = x.DownloadLink,
                        IsRemoved = x.IsRemoved,
                        Section = x.Section.Title,
                        Course = x.Section.Course.Title,
                        CourseId = x.Section.Course.Id
                    }).OrderByDescending(x => x.Id).ToList();
        }

        public List<PartViewModel> GetParts()
        {
            return _context.Parts.Include(x => x.Section).ThenInclude(x => x.Course).Select(x =>
                new PartViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    SectionId = x.SectionId,
                    DownloadLink = x.DownloadLink,
                    IsRemoved = x.IsRemoved,
                    Section = x.Section.Title,
                    Course = x.Section.Course.Title,
                    CourseId = x.Section.Course.Id
                }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
