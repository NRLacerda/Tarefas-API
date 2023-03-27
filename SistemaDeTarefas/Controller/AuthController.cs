using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using System.Security.Cryptography;

namespace SistemaDeTarefas.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // intancia objeto de User
        public static User user = new User(); 
        // 12:45

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register (UserDto request)
        {
            // chama o metodo que hasheia a password 
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash= passwordHash;
            user.PasswordSalt= passwordSalt;
            user.Username = request.Username;
            return Ok(user); //retorna usuario com nome senha hash e salt hash
        }
        private void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] PasswordSalt)
        {
            // Essa bomba aqui cria o salt e o hash da senha passada pra ele no método acima
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
