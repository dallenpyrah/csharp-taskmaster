using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using taskmaster.Models;
using taskmaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace taskmaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ProfilesService _ps;
        private readonly BoardsService _bservice;

        public AccountController(ProfilesService ps, BoardsService bservice)
        {
            _ps = ps;
            _bservice = bservice;
        }

        [HttpGet]
        public async Task<ActionResult<Profile>> Get()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ps.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("boards")]
        public async Task<ActionResult<IEnumerable<BoardMemberViewModel>>> GetBoardsAsync()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_bservice.GetByAccountId(userInfo.Id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);                
            }
        }
    }
}