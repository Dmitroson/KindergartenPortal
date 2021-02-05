using Education.BLL.DTO.Register;
using Education.BLL.DTO.User;
using Education.BLL.Services.AdminService.Interfaces;
using Education.BLL.Services.ConfigService.Interfaces;
using Education.BLL.Services.RegisterServices;
using Education.BLL.Services.RegisterServices.Interfaces;
using Education.BLL.Services.UserServices.Interfaces;
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
    public class RegisterController : Controller
    {
        private IClaimService ClaimService;
        private IAdminService AdminService;
        private IRegisterService RegisterService;

        private UserDTO GetUser()
        {
            return ClaimService.GetUser(User.Claims);
        }

        public RegisterController(IClaimService claimService, IAdminService adminService, IRegisterService registerService)
        {
            ClaimService = claimService;
            AdminService = adminService;
            RegisterService = registerService;
        }

        public IActionResult Index(int groupId, int week = 0)
        {
            var targetDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday + week * 7);
            var register = CreateRegister(groupId, targetDate, targetDate.AddDays(5));

            register.Week = week;

            var user = GetUser();
            if (AdminService.IsAdmin(user))
                register.IsAdmin = true;
            return View("_Register", register);
        }

        public ActionResult CreateChild(ChildDTO childDTO, int week)
        {
            var user = GetUser();
            if (!AdminService.IsAdmin(user))
                return Unauthorized();

            return RedirectToAction("Index", new { groupId = childDTO.GroupId, week});
        }

        [HttpPost]
        public ActionResult DeleteChild(int id)
        {
            var user = GetUser();
            if (!AdminService.IsAdmin(user))
                return Unauthorized();

            return Ok();
        }

        [HttpPost]
        public ActionResult UpdateChild(ChildDTO childDTO)
        {
            var user = GetUser();
            if (!AdminService.IsAdmin(user))
                return Unauthorized();

            return Ok();
        }

        public RegisterPage CreateRegister(int groupId, DateTime startDate, DateTime endDate)
        {
            var registerPage = new RegisterPage();

            registerPage.GroupId = groupId;

            registerPage.Children = RegisterService.GetChildren(groupId);

            var marksByDays = new List<DayMarks>();
            var currentDate = startDate;
            while ((endDate - currentDate).TotalDays > 0)
            {
                var marks = RegisterService.GetMarks(groupId, currentDate);

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
            var user = GetUser();
            if (!AdminService.IsAdmin(user))
                return Unauthorized();

            var mark = new MarkDTO() { Id = id, ChildId = childId, Value = value, Date = date.Date };

            id = RegisterService.UpdateMark(mark);

            return Json(new { id });
        }
    }
}
