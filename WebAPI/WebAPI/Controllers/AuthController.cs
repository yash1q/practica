using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "API is Active" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{login}/{password}")]
        public IActionResult Get(string login, string password)
        {
            if (Program.Context.Admin.FirstOrDefault(a => a.Login == login && a.Password == password) is Admin admin)
            {
                return Ok("Пользователь найден");
            }
            else
            {
                return NotFound("Не найдено");
            }
        }

        // POST api/<AuthController>
        [HttpPost]
        public IActionResult Post([FromBody] Admin adminToRegister)
        {
            var admin = Program.Context.Admin.FirstOrDefault(a => a.Login == adminToRegister.Login);
            if (admin != null)
            {
                return NotFound("Пользователь с таким логином уже существует");
            }
            else
            {
                Program.Context.Admin.Add(adminToRegister);
                Program.Context.SaveChanges();
                return Ok("Пользователь создан");
            }
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
