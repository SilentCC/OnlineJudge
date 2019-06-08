using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineJudgeServer.Models;
using OnlineJudgeServer.Services;

namespace OnlineJudgeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllOrigin")]
    public class JudgeController : Controller
    {
        private readonly ExecuteCplusProgram _executeCplusProgram;
        private readonly OnlineJudgeContext _context;

        public JudgeController(ExecuteCplusProgram executeCplusProgram, OnlineJudgeContext context)
        {
            _executeCplusProgram = executeCplusProgram;
            _context = context;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Post([FromBody] Submit submit)
        {
            var res = _executeCplusProgram.Execute(submit);

            submit.JudgeStatus = (int) res;
            submit.JudgeResult = res.ToString();
            submit.SubmitTime = DateTime.Now;

            _context.Add(submit);
            await _context.SaveChangesAsync();

            var result =
                _context.Submits.LastOrDefaultAsync(m => m.UserId == submit.UserId && m.ProblemId == submit.ProblemId);

            return Ok(result.Result);
        }
    }
}