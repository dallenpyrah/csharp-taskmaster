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
    public class BoardMembersController : ControllerBase
    {
       private readonly BoardMembersService _service;

        public BoardMembersController(BoardMembersService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BoardMember>> CreateOne([FromBody] BoardMember newBoardMem)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newBoardMem.CreatorId = userInfo.Id;
                return Ok(_service.CreateOne(newBoardMem));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<BoardMember>> DeleteOne(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_service.DeleteOne(id, userInfo.Id));
            }
            catch (System.Exception err) 
            {
                return BadRequest(err.Message);                
            }
        }
    }
}