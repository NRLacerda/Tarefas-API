using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<String>> GetAllUsers() 
        {
            string Teste = "Funcionou a rota interna!";
            return Ok(Teste);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<User>> InsertUser([FromBody] User usermodel)
        {
            User usuario = await _userRepository.AddUser(usermodel);
            return Ok(usuario);
        }
    }
}
