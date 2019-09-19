using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineJudgeServer.Models;

namespace OnlineJudgeServer.Controllers
{
    public class ProblemCategoriesController : Controller
    {
        private readonly OnlineJudgeContext _context;

        public ProblemCategoriesController(OnlineJudgeContext context)
        {
            _context = context;
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
