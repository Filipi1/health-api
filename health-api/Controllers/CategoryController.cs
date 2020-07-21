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
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;
        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> createCategory(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> getCategories()
        {
            var categories = _context.Categories.ToListAsync();
            return Ok(categories);
        }
    }
}
