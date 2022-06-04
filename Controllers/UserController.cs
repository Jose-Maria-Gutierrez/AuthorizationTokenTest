using Microsoft.AspNetCore.Mvc;
using TestApis.Authorization;
using TestApis.Models;
using TestApis.Service;

namespace TestApis.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<User> users = _userService.GetAll();
            return Ok(users);
        }

        [Authorize(Role.Admin)]
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            User user = this._userService.GetById(id);
            return Ok(user);
        }

    }
}
