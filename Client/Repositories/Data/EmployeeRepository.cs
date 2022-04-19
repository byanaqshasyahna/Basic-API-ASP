using API.Models;
using API.VirtualModel;
using API.VirtualModels;
using Client.Base;
using Client.VirtualModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        /*private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;*/

        public EmployeeRepository(Address address) : base(address, "Employees/")
        {
            /*this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };*/
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }

        public async Task<List<MasterDataVM>> GetMasterData()
        {
            /// isi codingan kalian disini
            /// 
            List<MasterDataVM> entities = new List<MasterDataVM>();

            using (var response = await httpClient.GetAsync(request + "MasterData/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<MasterDataVM>>(apiResponse);
            }
            return entities;
            
        }

        public LoginResponseVM Login(LoginVM loginVM)
        {
            LoginResponseVM result;
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");

            using (var response = httpClient.PostAsync(request + "Login", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<LoginResponseVM>(apiResponse);
            }

            return result;
        }

    }
}
