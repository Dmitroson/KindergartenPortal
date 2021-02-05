using Education.BLL.DTO.Register;
using Education.DAL.Entities.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education.Models
{
    public class RegisterPage
    {
        public IEnumerable<ChildDTO> Children { get; set; }

        public IEnumerable<DayMarks> MarksByDays { get; set; }

        public bool IsAdmin { get; set; }

        public int GroupId { get; set; }

        public int Week { get; set; }
    }
}