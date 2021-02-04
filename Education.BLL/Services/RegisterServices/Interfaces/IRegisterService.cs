using Education.BLL.DTO.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.BLL.Services.RegisterServices
{
    public interface IRegisterService
    {
        IEnumerable<MarkDTO> GetMarks(DateTime fromDate);
        IEnumerable<ChildDTO> GetChildren(int groupId);
        void Update(MarkDTO mark);
    }
}
