using Education.DAL.Entities.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education.Models
{
    public class RegisterPage
    {
        public IEnumerable<Child> Children { get; set; }
        public IEnumerable<DayMarks> MarksByDays { get; set; }
    }
}