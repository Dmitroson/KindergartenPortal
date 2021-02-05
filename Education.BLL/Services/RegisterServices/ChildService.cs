using Education.BLL.DTO.Register;
using Education.DAL.Entities.Register;
using Education.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Education.BLL.Services.RegisterServices
{
    public class ChildService : IChildService
    {
        private IUOWFactory unitOfWorkFactory;

        public ChildService(IUOWFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Add(ChildDTO child)
        {
            using (var unitOfWork = unitOfWorkFactory.Get())
            {
                var childRepository = unitOfWork.ChildRepository;
                childRepository.Add(new Child
                {
                    FullName = child.FullName,
                    GroupId = child.GroupId
                });
                unitOfWork.SaveChanges();
            }
        }

        public void Update(ChildDTO editedChild)
        {
            using (var unitOfWork = unitOfWorkFactory.Get())
            {
                var childRepository = unitOfWork.ChildRepository;
                var existingChild = childRepository.Get().SingleOrDefault(m => m.Id == editedChild.Id);

                existingChild.FullName = editedChild.FullName;

                childRepository.Edited(existingChild);
                unitOfWork.SaveChanges();
            }
        }

        public void Delete(int childId)
        {
            using (var unitOfWork = unitOfWorkFactory.Get())
            {
                var childRepository = unitOfWork.ChildRepository;
                var child = childRepository.Get().SingleOrDefault(c => c.Id == childId);
                childRepository.Delete(child);
                unitOfWork.SaveChanges();
            }
        }
    }
}
