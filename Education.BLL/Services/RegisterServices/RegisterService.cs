using Education.BLL.DTO.Register;
using Education.DAL.Entities.Register;
using Education.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Education.BLL.Services.RegisterServices
{
    public class RegisterService : IRegisterService
    {
        private IUOWFactory unitOfWorkFactory;

        public RegisterService(IUOWFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public IEnumerable<ChildDTO> GetChildren(int groupId)
        {
            using(var unitOfWork = unitOfWorkFactory.Get())
            {
                var childRepository = unitOfWork.ChildRepository;
                var children = childRepository.Get().Where(child => child.GroupId == groupId).ToList();

                var childrenDTO = new List<ChildDTO>();
                foreach(var child in children)
                {
                    childrenDTO.Add(new ChildDTO
                    {
                        Id = child.Id,
                        FullName = child.FullName
                    });
                }
                return childrenDTO;
            }
        }

        public IEnumerable<MarkDTO> GetMarks(int groupId, DateTime fromDate)
        {
            throw new NotImplementedException();
        }

        public void Update(MarkDTO editedMark)
        {
            using(var unitOfWork = unitOfWorkFactory.Get())
            {
                var markRepository = unitOfWork.MarkRepository;
                var existingMark = markRepository.Get().SingleOrDefault(m => m.Id == editedMark.Id);

                if(existingMark == null)
                {
                    var mark = new Mark
                    {
                        Value = editedMark.Value,
                        Date = existingMark.Date,
                        ChildId = editedMark.ChildId
                    };
                    markRepository.Add(mark);
                    unitOfWork.SaveChanges();
                }
                else
                {
                    existingMark.Value = editedMark.Value;
                    markRepository.Edited(existingMark);
                    unitOfWork.SaveChanges();
                }
            }
        }
    }
}
