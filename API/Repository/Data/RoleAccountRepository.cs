using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class RoleAccountRepository : GeneralRepository<MyContext, RoleAccount, string>
    {
        public RoleAccountRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
