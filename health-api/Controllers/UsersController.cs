using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using health_api.Data;
using health_api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
        [Authorize(Roles = "Admnistrator")]
        public async Task<ActionResult<List<User>>> getUsers()
        {
            var users = await _context.Users.Include(x => x.Role).ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{cpf:int}")]
        [Authorize(Roles = "Admnistrator")]
        public async Task<ActionResult<List<User>>> getUsers(string cpf)
        {
            var users = await _context.Users.Where(u => u.CPF == cpf).Include(x => x.Role).FirstOrDefaultAsync();
            return Ok(users);
        }

        [HttpPost]
        [Authorize(Roles = "User, Admnistrator")]
        public async Task<ActionResult<List<User>>> createUser(User userModel)
        {
            var users = await _context.Users.Where(u => u.Email == userModel.Email || u.CPF == userModel.CPF).ToListAsync();
            if (users.Count > 0)
                return BadRequest(new { message = "Usuário já cadastrado" });

            userModel.RoleId = 1;
            userModel.Active = true;
            userModel.Sexo = userModel.Sexo != null ? userModel.Sexo : "M";

            _context.Add<User>(userModel);
            await _context.SaveChangesAsync();

            return Ok(userModel);
        }

        [HttpPut]
        [Authorize(Roles = "User, Admnistrator")]
        [Route("{id}")]
        public async Task<ActionResult<User>> updateUser(Guid id, UserViewModel userModel)
        {
            if (id != userModel.Id)
                return NotFound(new { message = "Usuário não encontrado" });

            User user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            PropertyInfo[] userProperties = userModel.GetType().GetProperties();
            foreach (PropertyInfo info in userProperties)
            {
                var uValue = user.GetType().GetProperty(info.Name).GetValue(user);
                var pValue = userModel.GetType().GetProperty(info.Name).GetValue(userModel);

                if (!(pValue == null || pValue.ToString() == ""))
                    user.GetType().GetProperty(info.Name).SetValue(user, pValue);
            }
            
            _context.Entry<User>(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        [Authorize(Roles = "Admnistrator")]
        [Route("role/{cpf?}")]
        public async Task<ActionResult<User>> updateRole(string cpf, [FromQuery(Name = "role")] int roleId)
        {
            Role role = await _context.Roles.Where(r => r.Id == roleId).FirstOrDefaultAsync();
            if (role == null)
                return BadRequest(new { message = "Cargo não encontrado" });

            User user = await _context.Users.Where(u => u.CPF == cpf).FirstOrDefaultAsync();
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });

            user.RoleId = role.Id;

            _context.Entry<User>(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admnistrator")]
        [Route("roles")]
        public async Task<ActionResult<List<Role>>> getRoles()
        {
            List<Role> role = await _context.Roles.ToListAsync();
            if (role.Count == 0)
                return BadRequest(new { message = "Nenhum cargo encontrado" });
         
            return Ok(role);
        }

        [HttpDelete]
        [Authorize(Roles = "Admnistrator")]
        [Route("{cpf}")]
        public async Task<ActionResult<User>> updateUser(string cpf, [FromQuery(Name = "active")] bool active)
        {
            User user = await _context.Users.Where(u => u.CPF == cpf).FirstOrDefaultAsync();
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });

            user.Active = active;

            _context.Entry<User>(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(user);
        }
    }
}
