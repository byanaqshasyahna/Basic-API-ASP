using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace API.Base
{

    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase 
        where Entity : class
        where Repository : IRepository<Entity, Key> 
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            /*try
            {
                if (result.Count() > 0)
                {
                   
                    return Ok(new {status = HttpStatusCode.OK, result, message = "Data Ditemukan"});
                }
                else
                {
                    return StatusCode(404, new { status = HttpStatusCode.NotFound,result, message = "Data Tidak ditemukan" });
                }
            }
            catch
            {
                return StatusCode(500, new {status = HttpStatusCode.BadRequest, result, message = "Terjadi Kesalahan"});
            }*/

            return StatusCode(200,result);
            
        }


        [HttpGet("{Key}")]
        public ActionResult<Entity> GetByKey(Key key)
        {
            var result = repository.Get(key);
            return result;
        }


        [HttpPost]
        public ActionResult<Entity> Insert(Entity entity)
        {
            var result = repository.Insert(entity);
            return Ok(entity);
        }

        [HttpPut]
        public ActionResult<Entity> UpdateData(Entity entity)
        {
            var result = repository.Update(entity);
            return Ok(result);
        }

        [HttpDelete]
        public ActionResult<Entity> DeleteAll(Entity entity)
        {
            var result = repository.Delete(entity);
            return Ok(result);
        }

        [HttpDelete("{Key}")]
        public ActionResult<Entity> DeleteByKey(Key key)
        {
            var result = repository.DeleteByKey(key);
            return Ok(result);
        }


    }
}
