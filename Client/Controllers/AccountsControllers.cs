using API.Models;
using API.VirtualModel;
using Client.Base;
using Client.Models;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accRepository;
        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.accRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register(RegisterVM registerVM)
        {
            var result = accRepository.register(registerVM);
            return Json(result);
        }

        [HttpDelete]
        public JsonResult DeleteAcc(string NIK)
        {
            var result = accRepository.DeleteAcc(NIK);
            return Json(result);
        }

    }
}
