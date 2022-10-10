using System.Collections.Generic;
using _0_Framework.Application;
using LangoTop.Application.Contract.Part;
using LangoTop.Domain;
using LangoTop.Interfaces;

namespace LangoTop.Application
{
    public class PartApplication : IPartApplication
    {
        private readonly IPartRepository _partRepository;

        public PartApplication(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public OperationResult Create(CreatePart command)
        {
            var operation = new OperationResult();

            if (_partRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var section = new Part(command.Title, command.DownloadLink, command.SectionId, command.Time);
            _partRepository.Create(section);
            _partRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditPart command)
        {
            var operation = new OperationResult();

            var part = _partRepository.Get(command.Id);

            if (part == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_partRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            part.Edit(command.Title, command.DownloadLink, command.SectionId, command.Time);
            _partRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var part = _partRepository.Get(id);

            if (part == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            part.Remove();
            _partRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var part = _partRepository.Get(id);

            if (part == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            part.Restore();
            _partRepository.SaveChanges();
            return operation.Success();
        }

        public EditPart GetDetails(long id)
        {
            return _partRepository.GetDetails(id);
        }

        public List<PartViewModel> GetPartsBy(long sectionId)
        {
            return _partRepository.GetPartsBy(sectionId);
        }

        public List<PartViewModel> GetParts()
        {
            return _partRepository.GetParts();
        }
    }
}
