﻿using Microsoft.AspNetCore.Http;
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
                return Ok("TOKEN THERE");
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

    }
}