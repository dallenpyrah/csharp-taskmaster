using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskmaster.Models;
using taskmaster.Services;

namespace taskmaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly TodosService _cservice;

        public TodosController(TodosService cservice)
        {
            _cservice = cservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetAll()
        {
            try
            {
                return Ok(_cservice.GetAll());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Todo> GetOne(int id)
        {
            try
            {
                return Ok(_cservice.GetOne(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Todo>> CreateOne([FromBody] Todo newTodo)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newTodo.CreatorId = userInfo.Id;
                newTodo.Creator = userInfo;
                return Ok(_cservice.CreateOne(newTodo));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Todo>> EditOne(int id, [FromBody] Todo editTodo)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editTodo.CreatorId = userInfo.Id;
                editTodo.Id = id;
                editTodo.Creator = userInfo;
                return Ok(_cservice.EditOne(editTodo));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Todo>> DeleteOne(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_cservice.DeleteOne(id, userInfo.Id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);                
            }
        }
    }
}