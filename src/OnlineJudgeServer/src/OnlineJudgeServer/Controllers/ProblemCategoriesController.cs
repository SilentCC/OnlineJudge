using System;
using System.Collections.Generic;
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
    public class ProblemCategoriesController : Controller
    {
        private readonly OnlineJudgeContext _context;

        public ProblemCategoriesController(OnlineJudgeContext context)
        {
            _context = context;
        }
        [HttpGet("api/Categories")]
        public async Task<IActionResult> IndexApi()
        {
            return Ok(await _context.ProgramCategories.ToListAsync());
        }

        // GET: ProblemCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProgramCategories.ToListAsync());
        }

        // GET: ProblemCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemCategory = await _context.ProgramCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (problemCategory == null)
            {
                return NotFound();
            }

            return View(problemCategory);
        }

        [HttpGet("api/Categories/Problems/{id}")]
        public async Task<IActionResult> CategoryProblems(int id,int userId)
        {
            if (id == null)
                return NotFound();
            var result = await _context.Problems.Where(s => s.CategoryId == id).ToListAsync();
            
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
            
            return Ok(ans);
        }

        // GET: ProblemCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProblemCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,Description,TotalProblemNum")] ProblemCategory problemCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(problemCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(problemCategory);
        }

        // GET: ProblemCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemCategory = await _context.ProgramCategories.FindAsync(id);
            if (problemCategory == null)
            {
                return NotFound();
            }
            return View(problemCategory);
        }

        // POST: ProblemCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,Description,TotalProblemNum")] ProblemCategory problemCategory)
        {
            if (id != problemCategory.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problemCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemCategoryExists(problemCategory.CategoryId))
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
            return View(problemCategory);
        }

        // GET: ProblemCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemCategory = await _context.ProgramCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (problemCategory == null)
            {
                return NotFound();
            }

            return View(problemCategory);
        }

        // POST: ProblemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problemCategory = await _context.ProgramCategories.FindAsync(id);
            _context.ProgramCategories.Remove(problemCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemCategoryExists(int id)
        {
            return _context.ProgramCategories.Any(e => e.CategoryId == id);
        }
    }
}
