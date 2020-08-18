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
            // VERIFICAÇÃO DE ERROS NA MODEL STATE
            if (!ModelState.IsValid)
            {
                List<string> errorsReturn = new List<string>();
                var erros = ModelState.Select(x => x.Value.Errors).Where(y => y.Count() > 0);

                foreach (var error in erros) {
                    foreach (var e in error) {
                        errorsReturn.Add(e.ErrorMessage);
                    }
                }

                return BadRequest(new { errors = errorsReturn });
            }

            // OBTENÇÃO DO USUÁRIO
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.CPF == model.CPF && x.Password == model.Password)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "CPF ou senha inválidos" });

            // TRATATIVA DE DADOS DO USUÁRIO
            user.Password = null;

            // OBTENÇÃO DE TOKEN E RETORNO DE OBJETO
            var token = TokenService.GenerateToken(user);
            String expireDate = token.expires.ToLocalTime().ToString("dd/MM/yyyy HH:mm");
            expireDate = expireDate.Split(" ")[0] + " às " + expireDate.Split(" ")[1];

            return new
            {
                user,
                token.token,
                expires = expireDate
            };
        }
    }
}
