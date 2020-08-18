using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using health_api.Data;
using health_api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace health_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly DataContext _context;
        public ExamsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Admnistrator")]
        [Route("{cpf}")]
        public async Task<ActionResult> createExam([FromBody] Exam exam, string cpf)
        {
            // VERIFICAÇÃO DE ERROS NA MODEL STATE
            if (!ModelState.IsValid)
            {
                List<string> errorsReturn = new List<string>();
                var erros = ModelState.Select(x => x.Value.Errors).Where(y => y.Count() > 0);

                foreach (var error in erros)
                {
                    foreach (var e in error)
                    {
                        errorsReturn.Add(e.ErrorMessage);
                    }
                }

                return BadRequest(new { errors = errorsReturn });
            }

            var user = _context.Users.Where(u => u.CPF == cpf).FirstOrDefault();
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });

            exam.Active = true;

            try
            {
                _context.Add<Exam>(exam);
                await _context.SaveChangesAsync();

            } catch (Exception e)
            {

                return ValidationProblem();
            }


            return Ok(exam);
        }

        [HttpGet]
        [Authorize]
        [Route("{uid}")]
        public async Task<ActionResult<List<Exam>>> getExams([FromRoute] Guid uid)
        {
            var exams = await _context.Exams.Include(e => e.Colaborator).Where(u => u.UserId == uid).ToListAsync();
            if (exams.Count == 0)
                return NotFound(new { message = "Não possui nenhum exame agendado" });

            return Ok(exams);
        }
    }
}
