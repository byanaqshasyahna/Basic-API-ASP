using API.Base;
using API.Models;
using API.Repository.Data;

namespace API.Controllers
{
    public class ProfilingsController : BaseController<Profiling, ProfilingRepository, string>
    {
        public ProfilingsController(ProfilingRepository repository) : base(repository)
        {
        }
    }
}
