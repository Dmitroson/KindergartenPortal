using Education.DAL.Entities.Register;
using Education.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult Index()
        {
            var register = CreateRegister(0, DateTime.Now.Date, DateTime.Now.Date.AddDays(5));
            return View("_RegisterAdmin", register);
        }
        public RegisterPage CreateRegister(int groupId, DateTime startDate, DateTime endDate)
        {
            var registerPage = new RegisterPage();

            registerPage.Children = new List<Child> { new Child() { FullName = "Толік Анатолій"} };//childrenRepostory.Children.Where(u => u.Groupid == groupId);

            var marksByDays = new List<DayMarks>();
            var currentDate = startDate;
            while ((endDate - currentDate).TotalDays > 0)
            {
                var marks = new List<Mark>();//markRepository.Mark.Where(u => u.Date.Date == currentDate.Date);

                var marksByDay = new DayMarks() { Date = currentDate, Marks = marks };
                marksByDays.Add(marksByDay);

                currentDate = currentDate.AddDays(1);
            }

            registerPage.MarksByDays = marksByDays;

            return registerPage;
        }

        [Authorize]
        public IActionResult UpdateMark(int id, string value, int childId, DateTime date)
        {
            dynamic values = new { failed = false, id };

            if (id != -1)
            {
                var mark = new Mark() { ChildId = childId, Value = value, Date = date.Date };
                //markRepository.Add(mark);
                //db.SaveChanges();

                values.id = mark.Id;
            }
            else
            {
                var mark = default(Mark);//markRepository.Marks.SingleOrDefault(u => u.Id = id);
                if (mark == null)
                {
                    values.failed = true;
                }
                else
                {
                    mark.Value = value;

                    //db.SaveChanges();
                }

            }
            return Json(values);
        }

    }
}
