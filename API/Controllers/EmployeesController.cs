using API.Base;
using API.Context;
using API.Models;
using API.Repository;
using API.Repository.Data;
using API.VirtualModel;
using API.VirtualModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        private readonly MyContext myContext;
        public IConfiguration configuration;

        public object HttpGetStatusCode { get; private set; }

        public EmployeesController(EmployeeRepository repository, IConfiguration configuration, MyContext context) : base(repository)
        {
            employeeRepository = repository;
            myContext = context;
            this.configuration = configuration;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginvm)
        {
            var result = employeeRepository.Login(loginvm);


            if (result == 2) 
            {
                var dataEmp = myContext.Employees.Where(emp => emp.Email == loginvm.Email).SingleOrDefault();
                var NIK = dataEmp.NIK;
                var cekRole = getRole(NIK);


                var claims = new List<Claim>();
                claims.Add(new Claim("Email", loginvm.Email));

                foreach (var item in cekRole)
                {
                    claims.Add(new Claim("roles", item));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwr:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                    );
                var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                claims.Add(new Claim("TokenSecurity", idToken.ToString()));

                return Ok(new { status = HttpStatusCode.OK, TokenJWT = idToken, message ="Login Success"});

            }
            else if (result == 0)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Akun Tidak Ada" });
            }
            else
            return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Password Salah" });
            
        }


        /*[Authorize(Roles = "Director,Manager, Employee")]*/
        [HttpGet("MasterData")]
        public ActionResult GetMasterData()
        {
            var result = employeeRepository.GetMasterData();
            return Ok(result);
        }
        
        [Authorize(Roles = "Director,Employee")]
        [HttpGet("JWT")]
        public ActionResult testJWT()
        {
            return Ok("Test Jwt Success");
        }

        [Authorize(Roles = "Director,Manager")]
        [HttpPost("SignManager")]
        public ActionResult SignManager(SignManagerVM signManager)
        {
            var result = employeeRepository.SignManager(signManager);
            if(result ==  0)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Email Tidak ditemukan" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, message = "berhasil SignManager" });
            }
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCors()
        {
            return Ok("Test Cors Berhasil");
        }




        public List<string> getRole(string NIK)
        {
            var result = (from a in myContext.Roles
                          join c in myContext.RoleAccounts on a.Id equals c.RoleId
                          where c.NIK == NIK
                          select a.RoleName).ToList();
            

            return result;
        }
    }
}
