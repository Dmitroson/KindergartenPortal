using Education.BLL.DTO.Register;
using Education.DAL.Entities.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Models
{
    public class DayMarks
    {
        public DateTime Date { get; set; }
        public IEnumerable<MarkDTO> Marks { get; set; }
    }
}
