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
    public class BoardsController : ControllerBase
    {
        private readonly BoardsService _bservice;

        public BoardsController(BoardsService bservice)
        {
            _bservice = bservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Board>> GetAll()
        {
            try
            {
                return Ok(_bservice.GetAll());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Board> GetOne(int id)
        {
            try
            {
                return Ok(_bservice.GetOne(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Board>> CreateOne([FromBody] Board newBoard)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newBoard.CreatorId = userInfo.Id;
                newBoard.Creator = userInfo;
                return Ok(_bservice.CreateOne(newBoard));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Board>> EditOne(int id, [FromBody] Board editBoard)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editBoard.CreatorId = userInfo.Id;
                editBoard.Id = id;
                editBoard.Creator = userInfo;
                return Ok(_bservice.EditOne(editBoard));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Board>> DeleteOne(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_bservice.DeleteOne(id, userInfo.Id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);                
            }
        }
    }
}