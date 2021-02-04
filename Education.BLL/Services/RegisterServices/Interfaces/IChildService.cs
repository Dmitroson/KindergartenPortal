using Education.BLL.DTO.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.BLL.Services.RegisterServices.Interfaces
{
    public interface IChildService
    {
        void Add(ChildDTO child);
        void Update(ChildDTO child);
        void Detele(int childId);
    }
}
