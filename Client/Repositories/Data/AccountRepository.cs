using API.Models;
using Client.Base;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System;
using API.VirtualModel;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Net;

namespace Client.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        public AccountRepository(Address address, string request = "Accounts/") : base(address, request) 
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public HttpStatusCode register(RegisterVM registerVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync(request + "Register/", content).Result;

            return response.StatusCode;
        }
        public HttpStatusCode DeleteAcc(string NIK)
        {
            var response = httpClient.DeleteAsync(request + "deleteaccount/" + NIK).Result;
            return response.StatusCode;
        }

    }
}
