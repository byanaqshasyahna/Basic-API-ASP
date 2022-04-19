using API.Context;
using API.Models;
using API.VirtualModel;
using API.VirtualModels;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext eContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
           eContext = myContext;
        }

        public int Login(LoginVM loginVM) {
            
            
            var cekEmail = eContext.Employees.Where(e => e.Email == loginVM.Email).SingleOrDefault();

            
            
            //var cekLogin = eContext.Employees.Include("Account").Where(e => e.Email == loginVM.Email && e.Account.Password == loginVM.Password).SingleOrDefault();

            //var cekPassword = eContext.Accounts.Where(e => e.Password == loginVM.Password);
            /* cekEmail.Account.Password;*/

            if (cekEmail == null) {
                //email tidak ada (Akun tidak ada)
                return 0;
            }
            else 
            {
                var getPassword = eContext.Accounts.Where(a => a.NIK == cekEmail.NIK).SingleOrDefault();
                var validate = Hasing.ValidatePassword(loginVM.Password, getPassword.Password);

                if ((cekEmail.Email == loginVM.Email) && (!validate))
                {
                    return 1;
                }
                return 2;
            }
                
        }

        public IEnumerable GetMasterData()
        {
            
            var result = (from e in eContext.Employees
                          join a in eContext.Accounts on e.NIK equals a.NIK
                          join p in eContext.Profilings on a.NIK equals p.NIK
                          join c in eContext.Educations on p.EducationId equals c.Id
                          join d in eContext.Universities on c.UniversityId equals d.Id
                          select new
                          {
                              NIK = e.NIK,
                              Fullname = e.Firstname + " " + e.Lastname,
                              Phone = e.Phone,
                              Gender = ((Gender)e.Gender).ToString(),
                              Email = e.Email,
                              Birthdate = e.BirthDate,
                              Salary = e.Salary,
                              EducationId = p.EducationId,
                              GPA = c.GPA,
                              UniversityName = d.Name,
                              Role = eContext.Roles.Where(r => r.RoleAccounts.Any(ra => ra.NIK == e.NIK )).ToList()
                          }).ToList();
            return result;
        }

        public int SignManager(SignManagerVM signManager)
        {
            var getData = eContext.Employees.Where(e => e.Email == signManager.Email).SingleOrDefault();

            if(getData != null)
            {
                var roleAccount = new RoleAccount
                {
                    NIK = getData.NIK,
                    RoleId = 2
                };

                eContext.RoleAccounts.Add(roleAccount);
                eContext.SaveChanges();
                return 1;
            }
            

            return 0;
        }
        
       
    }
}
