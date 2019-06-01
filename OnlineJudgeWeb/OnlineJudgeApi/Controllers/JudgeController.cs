using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineJudgeApi.Entity;
using OnlineJudgeApi.Services;

namespace OnlineJudgeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JudgeController : ControllerBase
    {
        private readonly ExecuteCplusProgram _executeCplusProgram;

        public JudgeController(ExecuteCplusProgram executeCplusProgram)
        {
            _executeCplusProgram = executeCplusProgram;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("submit")]
        public ActionResult Post([FromBody] Submit submit)
        {
            var inputData = new List<string> {"1 2", "3 4"};
            var outputData = new List<string> {"3\n", "7\n"};
            _executeCplusProgram.Execute(submit,inputData,outputData);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}