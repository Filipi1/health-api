using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using health_api.Data;
using health_api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        private async Task<ActionResult> createExam(Exam exam)
        {
            return Ok(exam);
        }
    }
}
