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
    public class ExamsController : ControllerBase
    {
        private readonly DataContext _context;
        public ExamsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("{cpf}")]
        private async Task<ActionResult> createExam(string cpf, [FromBody] Exam exam)
        {
            var user = _context.Users.Where(u => u.CPF == cpf).FirstOrDefault();
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });

            _context.Add<Exam>(exam);
            await _context.SaveChangesAsync();

            return Ok(exam);
        }

        [HttpGet]
        [Route("{uid}")]
        private async Task<ActionResult<List<Exam>>> getExams(Guid uid)
        {
            var exams = await _context.Exams.Include(e => e.Colaborator).Where(u => u.UserId == uid).ToListAsync();
            if (exams.Count == 0)
                return NotFound(new { message = "Não possui nenhum exame agendado" });

            return Ok(exams);
        }
    }
}
