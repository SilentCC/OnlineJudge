using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineJudgeServer.Models;
using OnlineJudgeServer.Services;

namespace OnlineJudgeServer.Controllers
{
    [EnableCors("AllowAllOrigin")]
    public class ProblemsController : Controller
    {
        private readonly OnlineJudgeContext _context;

        public ProblemsController(OnlineJudgeContext context)
        {
            _context = context;
        }

        [HttpGet("Problems")]
        // GET: Problems
        public async Task<IActionResult> Index()
        {
            return View(await _context.Problems.ToListAsync());
        }

        [HttpGet("api/Problems")]
        public async Task<IActionResult> IndexApi(int userId)
        {
            var stopWatch = Stopwatch.StartNew();

            var result = await _context.Problems.ToListAsync();

            var ans = new List<HomeProblem>();

            foreach (var problem in result)
            {
                int isAc = -1;
                if (userId != 0)
                {
                    var submits =
                        await _context.Submits.Where(m =>
                            m.UserId == userId && m.ProblemId == problem.ProblemId).ToListAsync();

                    foreach (var submit in submits)
                    {
                        isAc = 0;
                        if (submit.JudgeStatus == (int) JudgeStatus.Accept)
                        {
                            isAc = 1;
                            break;
                        }
                    }
                }

                ans.Add(new HomeProblem
                {
                    Problem = problem,
                    Status = isAc
                });
            }
            
            Console.WriteLine(stopWatch.ElapsedMilliseconds + "ms");

            return Ok(ans);
        }

        public class HomeProblem
        {
            public Problem Problem;
            public int Status;
        }


        [HttpGet("api/Problems/Detail/{id}")]
        public async Task<IActionResult> DetailsApi(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems
                .FirstOrDefaultAsync(m => m.ProblemId == id);
            if (problem == null)
            {
                return NotFound();
            }

            return Ok(problem);
        }

        // GET: Problems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // GET: Problems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Problems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ProblemId,Id,Title,Content,Note,ExampleInput,ExampleOutPut,PushlishId,PublishTime")]
            Problem problem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(problem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(problem);
        }

        // GET: Problems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems.FindAsync(id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // POST: Problems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("ProblemId,Id,Title,Content,Note,ExampleInput,ExampleOutPut,PushlishId,PublishTime")]
            Problem problem)
        {
            if (id != problem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemExists(problem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(problem);
        }

        // GET: Problems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // POST: Problems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problem = await _context.Problems.FindAsync(id);
            _context.Problems.Remove(problem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemExists(int id)
        {
            return _context.Problems.Any(e => e.Id == id);
        }
    }
}