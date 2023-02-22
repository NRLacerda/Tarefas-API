using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> FetchAllUsers() 
        {
            List<UserModel> users = await _userRepository.FetchAllUsers();
            return Ok(users);
        }
    }

}
