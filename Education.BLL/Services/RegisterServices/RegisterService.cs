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
            using (var unitOfWork = unitOfWorkFactory.Get())
            {
                var childRepository = unitOfWork.ChildRepository;
                var children = childRepository.Get().Where(child => child.GroupId == groupId).ToList();

                var childrenDTO = new List<ChildDTO>();
                foreach (var child in children)
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
            using (var unitOfWork = unitOfWorkFactory.Get())
            {
                var childRepository = unitOfWork.ChildRepository;
                var childrenIds = childRepository.Get().Where(child => child.GroupId == groupId).Select(child => child.Id).ToList();

                var markRepository = unitOfWork.MarkRepository;
                var marks = markRepository.Get().Where(m => childrenIds.Contains(m.ChildId) && m.Date.Date == fromDate.Date).ToList();

                var marksDTO = new List<MarkDTO>();
                foreach (var mark in marks)
                {
                    marksDTO.Add(new MarkDTO
                    {
                        Id = mark.Id,
                        Value = mark.Value,
                        Date = mark.Date,
                        ChildId = mark.ChildId
                    });
                }
                return marksDTO;
            }
        }

        public int UpdateMark(MarkDTO editedMark)
        {
            using (var unitOfWork = unitOfWorkFactory.Get())
            {
                var markRepository = unitOfWork.MarkRepository;
                var existingMark = markRepository.Get().SingleOrDefault(m => m.Id == editedMark.Id);

                if (existingMark == null)
                {
                    var mark = new Mark
                    {
                        Value = editedMark.Value,
                        Date = editedMark.Date,
                        ChildId = editedMark.ChildId
                    };
                    markRepository.Add(mark);
                    unitOfWork.SaveChanges();
                    return mark.Id;
                }
                else
                {
                    existingMark.Value = editedMark.Value;
                    markRepository.Edited(existingMark);
                    unitOfWork.SaveChanges();
                    return existingMark.Id;
                }
            }
        }
    }
}
