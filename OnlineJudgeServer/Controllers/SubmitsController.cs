using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineJudgeServer.Models;

namespace OnlineJudgeServer.Controllers
{
    [EnableCors("AllowAllOrigin")]
    public class SubmitsController : Controller
    {
        private readonly OnlineJudgeContext _context;

        public SubmitsController(OnlineJudgeContext context)
        {
            _context = context;
        }

        // GET: Submits
        public async Task<IActionResult> Index()
        {
            return View(await _context.Submits.ToListAsync());
        }

        [HttpGet("api/Submits")]
        public async Task<IActionResult> IndexApi()
        {
            var submit = await _context.Submits.OrderByDescending(m => m.SubmitTime).AsNoTracking().ToListAsync();
            var result = new List<object>();
            foreach (var sub in submit)
            {
                var problem = await _context.Problems.FirstOrDefaultAsync(m => m.ProblemId == sub.ProblemId);
                
                result.Add(new
                {
                    id = sub.SubmitId,
                    title = problem.Title,
                    status = sub.JudgeResult,
                    userName = "dacc",
                    submitTime = sub.SubmitTime
                });
            }
           
            return Ok(result);
        }

        // GET: Submits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submit = await _context.Submits
                .FirstOrDefaultAsync(m => m.SubmitId == id);
            if (submit == null)
            {
                return NotFound();
            }

            return View(submit);
        }

        // GET: Submits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Submits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("SubmitId,ProblemId,UserId,JudgeStatus,JudgeResult,SubmitTime,CodeType,CodeSuffix,SourceCode")]
            Submit submit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(submit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(submit);
        }

        // GET: Submits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submit = await _context.Submits.FindAsync(id);
            if (submit == null)
            {
                return NotFound();
            }

            return View(submit);
        }

        // POST: Submits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("SubmitId,ProblemId,UserId,JudgeStatus,JudgeResult,SubmitTime,CodeType,CodeSuffix,SourceCode")]
            Submit submit)
        {
            if (id != submit.SubmitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(submit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubmitExists(submit.SubmitId))
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

            return View(submit);
        }

        // GET: Submits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submit = await _context.Submits
                .FirstOrDefaultAsync(m => m.SubmitId == id);
            if (submit == null)
            {
                return NotFound();
            }

            return View(submit);
        }

        // POST: Submits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var submit = await _context.Submits.FindAsync(id);
            _context.Submits.Remove(submit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubmitExists(int id)
        {
            return _context.Submits.Any(e => e.SubmitId == id);
        }
    }
}