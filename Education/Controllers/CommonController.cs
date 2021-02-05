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
    public class CommonController : Controller
    {
        private IClaimService ClaimService;
        private IAdminService AdminService;
        private IRegisterService RegisterService;

        private UserDTO GetUser()
        {
            return ClaimService.GetUser(User.Claims);
        }

        public CommonController(IClaimService claimService, IAdminService adminService, IRegisterService registerService)
        {
            ClaimService = claimService;
            AdminService = adminService;
            RegisterService = registerService;
        }

        public IActionResult Register(int groupId, int week = 0)
        {
            var targetDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday + week * 7);
            var register = CreateRegister(groupId, targetDate, targetDate.AddDays(5));
            var user = GetUser();
            if (AdminService.IsAdmin(user))
                register.IsAdmin = true;
            return View("_Register", register);
        }

        public RegisterPage CreateRegister(int groupId, DateTime startDate, DateTime endDate)
        {
            var registerPage = new RegisterPage();

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
