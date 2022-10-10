using System.Collections.Generic;
using _0_Framework.Application;
using LangoTop.Application.Contract.Section;
using LangoTop.Domain;
using LangoTop.Interfaces;

namespace LangoTop.Application
{
    public class SectionApplication : ISectionApplication
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionApplication(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public OperationResult Create(CreateSection command)
        {
            var operation = new OperationResult();

            if (_sectionRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var section = new Section(command.Title, command.CourseId);
            _sectionRepository.Create(section);
            _sectionRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditSection command)
        {
            var operation = new OperationResult();

            var section = _sectionRepository.Get(command.Id);

            if (section == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_sectionRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            section.Edit(command.Title, command.CourseId);
            _sectionRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var section = _sectionRepository.Get(id);

            if (section == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            section.Remove();
            _sectionRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var section = _sectionRepository.Get(id);

            if (section == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            section.Restore();
            _sectionRepository.SaveChanges();
            return operation.Success();
        }

        public EditSection GetDetails(long id)
        {
            return _sectionRepository.GetDetails(id);
        }

        public List<SectionViewModel> GetSections()
        {
            return _sectionRepository.GetSections();
        }

        public List<SectionViewModel> GetSectionsBy(long courseId)
        {
            return _sectionRepository.GetSectionsBy(courseId);
        }
    }
}
