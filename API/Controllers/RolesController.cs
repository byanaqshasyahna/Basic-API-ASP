using API.Base;
using API.Models;
using API.Repository.Data;

namespace API.Controllers
{
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
        public RolesController(RoleRepository repository) : base(repository)
        {
        }
    }
}
