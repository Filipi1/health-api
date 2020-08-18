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
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;
        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Admnistrator")]
        public async Task<ActionResult<Category>> createCategory(Category category)
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

            _context.Add(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Category>>> getCategories()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admnistrator")]
        public async Task<ActionResult<List<Category>>> updateCategory(int id, [FromQuery] bool active)
        {
            Category category = await _context.Categories.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (category == null)
                return NotFound(new { message = "Nenhuma categoria com este id" });

            category.Active = active;

            _context.Entry<Category>(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(category);
        }
    }
}
