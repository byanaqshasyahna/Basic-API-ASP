using API.Base;
using API.Models;
using API.Repository.Data;
using API.VirtualModel;
using API.VirtualModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public AccountsController(AccountRepository repository) : base(repository)
        {
            accountRepository = repository;
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var result = accountRepository.Register(registerVM);

            if (result == 2)
            {
                return BadRequest("Email Sudah Digunakan");
            }
            else
            {
                return Ok("Registerasi berhasil");
            }
            /*if (result == 2)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }*/
            /*return Ok(result);*/

        }

        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPass(ForgotPasswordVM forgotPassword)
        {
            var result = accountRepository.ForgotPassword(forgotPassword);

            if (result == 2)
            {
                return BadRequest("Email Tidak ada");
            }else if(result == 0)
            {
                return Ok("Terjadi Kesalahan");
            }
            return Ok("Kode OTP Telah dikirim melalui Email");
        }

        [HttpPut("ChangePassword")]
        public ActionResult ChangePass(ChangePasswordVM changePassword)
        {
            var result = accountRepository.ChangePassword(changePassword);

            if (result == 0)
            {
                return Ok("Otp Sudah Digunakan");

            }
            else if (result == 1)
            {
                return Ok("Berhasil ganti password");
            }
            else if (result == 2)
            {
                return BadRequest("OTP Salah");
            }
            else if (result == 3)
            {
                return BadRequest("OTP Expired");
            }
            else if (result == 4)
            {
                return BadRequest("Confirm password salah");
            }
            else
                return Ok("Gagal ubah password");
        }

        [HttpDelete("deleteaccount/{NIK}")]
        public ActionResult DeleteByNIK(string NIK)
        {
            var result = accountRepository.DeleteByNIK(NIK);

            /*if(result !=0)
            return Ok(new { status = HttpStatusCode.OK, result, message = "Data Berhasil di Hapus" });
            else
                return  BadRequest(new { status = HttpStatusCode.BadRequest, result, message = "Data Tidak Berhasil di Hapus" });*/

            return Ok();
        }
    }
}
