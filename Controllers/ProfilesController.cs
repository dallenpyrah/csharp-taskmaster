using System;
using taskmaster.Models;
using taskmaster.Services;
using Microsoft.AspNetCore.Mvc;

namespace taskmaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _service;

        private readonly BoardsService _bservice;

        public ProfilesController(ProfilesService service, BoardsService bservice)
        {
            _service = service;
            _bservice = bservice;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> Get(string id)
        {
            try
            {
                Profile profile = _service.GetProfileById(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/boards")]
        public ActionResult<BoardMemberViewModel> GetByProfileId(string id)
        {
            try
            {
                return Ok(_bservice.GetByProfileId(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);                
            }
        }


    }
}