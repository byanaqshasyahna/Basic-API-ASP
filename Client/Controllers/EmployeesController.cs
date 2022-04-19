using API.Models;
using API.VirtualModels;
using Client.Base;
using Client.Models;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository empRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.empRepository = repository;
        }

        /*[Authorize(Roles = "Employee")]*/
        [HttpGet]
        public async Task<JsonResult> GetMasterData()
        {
            var result = await empRepository.GetMasterData();
            return Json(result);
        }

        [HttpPost]
        public  JsonResult Login( LoginVM loginVM)
        {
            var result = empRepository.Login(loginVM);

            if (result.TokenJWT != null)
            {
                HttpContext.Session.SetString("JWToken", result.TokenJWT);
                //HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
                //HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");
            }

            return Json(result);

        }


        [Authorize(Roles = "Director")]
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }
    }
}
