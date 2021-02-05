using Education.BLL.DTO.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.BLL.Services.RegisterServices
{
    public interface IChildService
    {
        void Add(ChildDTO child);
        void Update(ChildDTO child);
        void Delete(int childId);
    }
}
