using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaDeTarefas.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace SistemaDeTarefas.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // intancia objeto de User
        public static User user = new User();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [HttpGet("Teste")]
        [Authorize]
        public async Task<ActionResult<string>> Teste()
        {
            string Teste = "Funcionou!";
            return Teste;
        }
        [HttpPost("Register")]
        // recebe UserDto um modelo que contem Username e Password para registralos
        public async Task<ActionResult<User>> Register (UserDto request)
        {
            // esse request.Password é a senha passada no header da requisição
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash= passwordHash;
            user.PasswordSalt= passwordSalt;
            user.Username = request.Username;
            return Ok(user); //retorna usuario com nome senha hash e salt hash
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login (UserDto request)
        {
            // compara usuario e depois senhas
            if (user.Username != request.Username)
            {
                return BadRequest("Usuário não encontrado");
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Senha incorreta");
            }
            else
            {
                string token = CreateToken(user);
                return Ok(token);
            }

        }
        private void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] PasswordSalt)
        {
            // Essa bomba aqui cria o salt e o hash da senha passada pra ele no método acima
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                // cria o password hashado
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                /*
                   a senha vem como string e é convertida em array de bytes para ser comparada com outro array de bytes
                   que é o passwordHash = computedHash
                 */
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                // retorna true se o passwordhash for igual ao computed hash (senha hashada que o usuario enviou)
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // tempo de expiração do token: 1 hora
            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            UserToken usuario = new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
            return usuario.Token.ToString();
        }
    }
}
