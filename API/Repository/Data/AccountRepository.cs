using API.Context;
using API.Models;
using API.VirtualModel;
using API.VirtualModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext1;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            myContext1 = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            DateTime today = DateTime.Now;
            string increamentNIK;
            //Cek isi table kosong atau tidak
            if (myContext1.Employees.ToList().Count != 0)
            {
                int increament = Convert.ToInt32(lastNIK()) + 1;
                increamentNIK = increament.ToString();
            }
            else
            {
                increamentNIK = today.Year.ToString() + "00" + 1;
            }

            //Cek Email yang sudah digunakan
            if (cekEmail(registerVM).Count > 0)
            {
                return 2;
            }
            else
            {
                var emp = new Employee
                {
                    NIK = increamentNIK,
                    Firstname = registerVM.FirstName,
                    Lastname = registerVM.LastName,
                    Phone = registerVM.Phone,
                    Gender = (Models.Gender)registerVM.Gender,
                    Salary = registerVM.Salary,
                    BirthDate = registerVM.BirthDate,
                    Email = registerVM.Email,

                };
                myContext1.Employees.Add(emp);

                var passHasing = Hasing.HashPassword(registerVM.Password);

                var acc = new Account
                {
                    NIK = emp.NIK,
                    Password = passHasing,
                };
                myContext1.Accounts.Add(acc);

                var edu = new Education
                {
                    Degree = registerVM.Degree,
                    GPA = registerVM.GPA,
                    UniversityId = registerVM.UniversityId

                };
                myContext1.Educations.Add(edu);
                myContext1.SaveChanges();

                var profil = new Profiling
                {
                    NIK = emp.NIK,
                    EducationId = edu.Id

                };
                myContext1.Profilings.Add(profil);

                var roleAccount = new RoleAccount
                {
                    NIK = emp.NIK,
                    RoleId = 3
                };
                myContext1.RoleAccounts.Add(roleAccount);

                myContext1.SaveChanges();
                return 1;
            }
        }

        public string lastNIK()
        {
            //mendapatkan NIK terakhir 
            return myContext1.Employees.ToList().LastOrDefault().NIK;
        }

        public List<Employee> cekEmail(RegisterVM registerVM)
        {
            var result = myContext1.Employees.Where(e => e.Email == registerVM.Email).ToList();
            return result;
        }


        //Forgot PASSWORD
        public int ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            Random rand = new Random();
            DateTime timeExpired = DateTime.Now.AddMinutes(5);
            string otp = rand.Next(100000,999999).ToString();
            string to = forgotPassword.Email; //To address

            var getDataEmp = myContext1.Employees.Where(e => e.Email == forgotPassword.Email).SingleOrDefault();
            

            if(getDataEmp != null)
            {
                //update entity Account
                var getDataAcc = myContext1.Accounts.Where(a => a.NIK == getDataEmp.NIK).SingleOrDefault();
                getDataAcc.OTP = Convert.ToInt32(otp);
                getDataAcc.ExpiredOTP = timeExpired;
                getDataAcc.IsUsed = false;
                myContext1.Entry(getDataAcc).State = EntityState.Modified;
                myContext1.SaveChanges();

                //Sending Email
                string from = "sonbydude@gmail.com"; //From address    
                MailMessage message = new MailMessage(from, to);

                string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
                message.Subject = "Sending Email Using Asp.Net & C#";
                message.Body = "Your OTP : " + otp;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("sonbydude", "087885612712");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                try
                {
                    client.Send(message);
                    return 1;
                }

                catch (Exception ex)
                {
                    throw ex;
                    return 0;
                }
            }
            else
            {
                return 2;
            }
            
        }
    
        // Change PASSWORD
        public int ChangePassword(ChangePasswordVM changePassword)
        {

            DateTime cekTime = DateTime.Now;
                                    
            var getDataEmp = myContext1.Employees.Where(e => e.Email == changePassword.Email).SingleOrDefault();
            if (getDataEmp != null)
            {
                var getDataAcc = myContext1.Accounts.Where(a => a.NIK == getDataEmp.NIK).SingleOrDefault();

                if (getDataAcc.IsUsed == true)
                {
                    return 0;
                }
                else
                {
                    if(getDataAcc.OTP == changePassword.OTP && cekTime <= getDataAcc.ExpiredOTP && changePassword.NewPassword == changePassword.ConfirmPassword)
                    {
                        getDataAcc.OTP = changePassword.OTP;
                        getDataAcc.IsUsed = true;
                        var passHasing = Hasing.HashPassword(changePassword.NewPassword);
                        getDataAcc.Password = passHasing;
                        myContext1.Entry(getDataAcc).State = EntityState.Modified;
                        myContext1.SaveChanges();
                        return 1;
                    }else if(getDataAcc.OTP != changePassword.OTP)
                    {
                        return 2;
                    }else if(cekTime >= getDataAcc.ExpiredOTP)
                    {
                        return 3;
                    }else if(changePassword.NewPassword != changePassword.ConfirmPassword)
                    {
                        return 4;
                    }
                    
                }
            }
            return 5;
        }

        public int DeleteByNIK(string NIK)
        {
            
            var entiProf = myContext1.Profilings.Where(p => p.NIK == NIK).FirstOrDefault();
            
            var deleteEducation = myContext1.Educations.Where(a => a.Id == entiProf.EducationId).SingleOrDefault();
            myContext1.Remove(deleteEducation);
            myContext1.SaveChanges();

            var deleteEmployees = myContext1.Employees.Find(NIK);
            myContext1.Remove(deleteEmployees);

            
            var result = myContext1.SaveChanges();
            return result;
        }
    }
}
