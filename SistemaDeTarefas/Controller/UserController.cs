using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllUsers : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<UserModel>> FetchUsers() { return Ok(); }
    }

}
