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
        //Router 

        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            // Injeção de dependência, igual no Angular
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers() 
        {
            List<UserModel> users = await _userRepository.GetAllUsers();
            return Ok(users);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            UserModel user = await _userRepository.GetUser(id);
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<UserModel>> InsertUser([FromBody] UserModel usermodel)
        {
            UserModel usuario = await _userRepository.AddUser(usermodel);
            return Ok(usuario);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> UpdateUser([FromBody] UserModel usermodel, int id)
        {
            usermodel.id= id;
            UserModel usuario = await _userRepository.UpdateUser(usermodel, id);
            return Ok(usuario);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> DeleteUser([FromBody] int id)
        {
            bool deleted = await _userRepository.DeleteUser(id);
            return Ok(deleted);
        }
    }

}
