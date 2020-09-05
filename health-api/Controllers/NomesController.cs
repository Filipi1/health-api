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
    public class NomesController : ControllerBase
    {
        private readonly DataContext _context;
        public NomesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar([FromBody] Nomes nome)
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

            _context.Nomes.Add(nome);
            await _context.SaveChangesAsync();

            return Ok(nome);
        }

        [HttpGet]
        public async Task<ActionResult> Obter()
        {
            var nomes = await _context.Nomes.OrderBy(a => a.id).ToListAsync();
            return Ok(nomes);
        }  
    }
}
