using API.Base;
using API.Models;
using API.Repository.Data;

namespace API.Controllers
{
    public class RoleAccountsController : BaseController<RoleAccount, RoleAccountRepository, string>
    {
        public RoleAccountsController(RoleAccountRepository repository) : base(repository)
        {
        }
    }
}
