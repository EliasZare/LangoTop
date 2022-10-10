using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using LangoTop.Application.Contract.Section;
using LangoTop.Domain;
using LangoTop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LangoTop.Infrastructure.Repository
{
    public class SectionRepository : RepositoryBase<long, Section>, ISectionRepository
    {
        private readonly LangoTopContext _context;

        public SectionRepository(LangoTopContext context) : base(context)
        {
            _context = context;
        }


        public EditSection GetDetails(long id)
        {
            return _context.Sections.Select(x => new EditSection
            {
                Title = x.Title,
                Id = x.Id,
                CourseId = x.CourseId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SectionViewModel> GetSections()
        {
            return _context.Sections.Include(x => x.Course).Select(x => new SectionViewModel
            {
                Id = x.Id,
                Title = x.Title,
                CourseId = x.CourseId,
                Course = x.Course.Title,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).ToList();
        }

        public List<SectionViewModel> GetSectionsBy(long courseId)
        {
            return _context.Sections.Where(x => x.CourseId == courseId).Include(x => x.Course).Select(x =>
                new SectionViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CourseId = x.CourseId,
                    Course = x.Course.Title,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
