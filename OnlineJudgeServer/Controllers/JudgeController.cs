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
        private readonly ExecutePythonProgram _executePythonProgram;
        private readonly OnlineJudgeContext _context;

        public JudgeController(ExecuteCplusProgram executeCplusProgram, ExecutePythonProgram executePythonProgram,
            OnlineJudgeContext context)
        {
            _executeCplusProgram = executeCplusProgram;
            _executePythonProgram = executePythonProgram;
            _context = context;
        }

        [HttpPost("submit/{memory}/{time}")]
        public async Task<IActionResult> Post([FromBody] Submit submit, int memory, int time)
        {
            JudgeMachineService judgeMachineService;
            
            if(submit.CodeType == (int)CodeType.gcc || submit.CodeType==(int)CodeType.gplus)
                judgeMachineService = new JudgeMachineService(_executeCplusProgram);
            else
            {
                judgeMachineService = new JudgeMachineService(_executePythonProgram);
                time = time * 10;
            }

            var res = await judgeMachineService.Judge(submit, memory, time);

            submit.JudgeStatus = (int) res;
            submit.JudgeResult = res.ToString();
            submit.SubmitTime = DateTime.Now;

            var problem = await _context.Problems.FirstOrDefaultAsync(m => m.ProblemId == submit.ProblemId);
            if (res == JudgeStatus.Accept)
            {
                problem.AcceptSubmit++;
            }

            problem.TotalSubmit++;

            _context.Update(problem);

            _context.Add(submit);
            await _context.SaveChangesAsync();

            var result = await
                _context.Submits.LastOrDefaultAsync(m => m.UserId == submit.UserId && m.ProblemId == submit.ProblemId);

            return Ok(result);
        }
    }
}