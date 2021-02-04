using System;
using System.Collections.Generic;
using System.Text;

namespace Education.BLL.DTO.Register
{
    public class MarkDTO
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime Date { get; set; }
        public int ChildId { get; set; }
        public ChildDTO Child { get; set; }
    }
}