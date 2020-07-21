using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using health_api.Data;
using health_api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace health_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> getUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> createUser(User userModel)
        {
            var users = await _context.Users.Where(u => u.Email == userModel.Email).ToListAsync();
            if (users.Count > 0)
                return BadRequest(new { message = "Endereço de Email já cadastrado" });

            userModel.Role = "User";
            userModel.Sexo = userModel.Sexo != null ? userModel.Sexo : "M"; 

            _context.Add<User>(userModel);
            await _context.SaveChangesAsync();

            return Ok(userModel);
        }
    }
}
