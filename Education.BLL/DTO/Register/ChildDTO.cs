using Education.BLL.DTO.Forum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.BLL.DTO.Register
{
    public class ChildDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public GroupDTO Group { get; set; }
        public IEnumerable<MarkDTO> Marks { get; set; }
    }
}
