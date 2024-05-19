
using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;


namespace BDSKProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class UserController : ControllerBase
    {
        public readonly IUserService userService;
        public readonly IMapper mapper;
        public UserController(IUserService userService,IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDTO userDTO)
        {
            User user= mapper.Map<UserDTO, User>(userDTO);
            User userRes = await userService.Register(user);
            if (userRes != null)
            {
                return Ok(userRes);
            }
            else if (userRes == null)
            {
                return NoContent();
            }
            return BadRequest();
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            User userRes = await userService.Login(user);
            if (userRes != null)
            {
                return Ok(userRes);
            }
            return Unauthorized();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, [FromBody] User user)
        {
            User u = await userService.Update(id, user);
            if (u != null)
                return Ok(u);
            return NoContent();
        }
        [HttpPost("password")]
        public ActionResult Password([FromBody] string password)
        {
            int score=userService.checkPassword(password);
            if(score>=2)
            {
                return Ok(score);
            }
            return Accepted(score);
        }
    }
}
