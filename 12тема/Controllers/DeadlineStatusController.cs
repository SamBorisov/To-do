using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using thema12.Data;
using thema12.Models;

namespace thema12.Controllers
{
    public class DeadlineStatusController : Controller
    {
        private readonly Sami1Context _context;

        public DeadlineStatusController(Sami1Context context)
        {
            _context = context;
        }

        // GET: DeadlineStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeadlineStatuses.ToListAsync());
        }

        // GET: DeadlineStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deadlineStatus = await _context.DeadlineStatuses
                .FirstOrDefaultAsync(m => m.DeadlineStatusID == id);
            if (deadlineStatus == null)
            {
                return NotFound();
            }

            return View(deadlineStatus);
        }

        // GET: DeadlineStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeadlineStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeadlineStatusID,DeadlineStatusName")] DeadlineStatus deadlineStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deadlineStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deadlineStatus);
        }

        // GET: DeadlineStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deadlineStatus = await _context.DeadlineStatuses.FindAsync(id);
            if (deadlineStatus == null)
            {
                return NotFound();
            }
            return View(deadlineStatus);
        }

        // POST: DeadlineStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeadlineStatusID,DeadlineStatusName")] DeadlineStatus deadlineStatus)
        {
            if (id != deadlineStatus.DeadlineStatusID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deadlineStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeadlineStatusExists(deadlineStatus.DeadlineStatusID))
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
            return View(deadlineStatus);
        }

        // GET: DeadlineStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deadlineStatus = await _context.DeadlineStatuses
                .FirstOrDefaultAsync(m => m.DeadlineStatusID == id);
            if (deadlineStatus == null)
            {
                return NotFound();
            }

            return View(deadlineStatus);
        }

        // POST: DeadlineStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deadlineStatus = await _context.DeadlineStatuses.FindAsync(id);
            _context.DeadlineStatuses.Remove(deadlineStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeadlineStatusExists(int id)
        {
            return _context.DeadlineStatuses.Any(e => e.DeadlineStatusID == id);
        }
    }
}
