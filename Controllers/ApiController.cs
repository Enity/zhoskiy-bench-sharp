using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZhoskiyBenchSharp.Models;

namespace ZhoskiyBenchSharp.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly AppContext _context;

        public ApiController(AppContext context)
        {
            _context = context;
        }

        [HttpGet("plaintext")]
        public string PlainText()
        {
            return "ok lets go";
        }
        
        [HttpGet("read-data")]
        public async Task<List<Bear>> ReadData()
        {
            return await _context.Bears.ToListAsync();
        }
    }
}