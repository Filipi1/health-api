using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using health_api.Data;
using health_api.Model;
using health_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace health_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Auth(Auth model)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.CPF == model.CPF && x.Password == model.Password)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "CPF ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            user.Password = null;

            return new
            {
                user = user,
                auth = token
            };
        }
    }
}
