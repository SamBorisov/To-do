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
    public class DeadlinesController : Controller
    {
        private readonly Sami1Context _context;

        public DeadlinesController(Sami1Context context)
        {
            _context = context;
        }

        // GET: Deadlines
        public async Task<IActionResult> Index()
        {
            var sami1Context = _context.Deadlines.Include(d => d.DeadlineStatus).Include(d => d.Task);
            return View(await sami1Context.ToListAsync());
        }

        // GET: Deadlines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deadline = await _context.Deadlines
                .Include(d => d.DeadlineStatus)
                .Include(d => d.Task)
                .FirstOrDefaultAsync(m => m.Deadline_ID == id);
            if (deadline == null)
            {
                return NotFound();
            }

            return View(deadline);
        }

        // GET: Deadlines/Create
        public IActionResult Create()
        {
            ViewData["DeadlineStatusID"] = new SelectList(_context.DeadlineStatuses, "DeadlineStatusID", "DeadlineStatusName");
            ViewData["Task_ID"] = new SelectList(_context.Tasks, "Task_ID", "Task_ID");
            return View();
        }

        // POST: Deadlines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Deadline_ID,Reminders,DeadlineStatusID,Task_ID,Deadline1")] Deadline deadline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deadline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeadlineStatusID"] = new SelectList(_context.DeadlineStatuses, "DeadlineStatusID", "DeadlineStatusName", deadline.DeadlineStatusID);
            ViewData["Task_ID"] = new SelectList(_context.Tasks, "Task_ID", "Task_ID", deadline.Task_ID);
            return View(deadline);
        }

        // GET: Deadlines/Task/5
        public async Task<IActionResult> Task(int id)
        {
            var deadline =  _context.Deadlines.FirstOrDefault(t => t.Task_ID == id);
            if (deadline == null)
            {
                deadline = new Deadline();
                deadline.Task_ID = id;
            }
            ViewData["DeadlineStatusID"] = new SelectList(_context.DeadlineStatuses, "DeadlineStatusID", "DeadlineStatusName", deadline.DeadlineStatusID);
            return View(deadline);
        }
        // POST: Deadlines/Task/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Task(int? id, [Bind("Deadline_ID,Reminders,DeadlineStatusID,Task_ID,Deadline1")] Deadline deadline)
        {
 
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deadline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeadlineExists(deadline.Deadline_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Tasks");
            }
            ViewData["DeadlineStatusID"] = new SelectList(_context.DeadlineStatuses, "DeadlineStatusID", "DeadlineStatusName", deadline.DeadlineStatusID);
            return View(deadline);
        }


        // GET: Deadlines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deadline = await _context.Deadlines.FindAsync(id);
            if (deadline == null)
            {
                return NotFound();
            }
            ViewData["DeadlineStatusID"] = new SelectList(_context.DeadlineStatuses, "DeadlineStatusID", "DeadlineStatusName", deadline.DeadlineStatusID);
            ViewData["Task_ID"] = new SelectList(_context.Tasks, "Task_ID", "Task_ID", deadline.Task_ID);
            return View(deadline);
        }

        // POST: Deadlines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Deadline_ID,Reminders,DeadlineStatusID,Task_ID,Deadline1")] Deadline deadline)
        {
            if (id != deadline.Deadline_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deadline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeadlineExists(deadline.Deadline_ID))
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
            ViewData["DeadlineStatusID"] = new SelectList(_context.DeadlineStatuses, "DeadlineStatusID", "DeadlineStatusName", deadline.DeadlineStatusID);
            ViewData["Task_ID"] = new SelectList(_context.Tasks, "Task_ID", "Task_ID", deadline.Task_ID);
            return View(deadline);
        }

        // GET: Deadlines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deadline = await _context.Deadlines
                .Include(d => d.DeadlineStatus)
                .Include(d => d.Task)
                .FirstOrDefaultAsync(m => m.Deadline_ID == id);
            if (deadline == null)
            {
                return NotFound();
            }

            return View(deadline);
        }

        // POST: Deadlines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deadline = await _context.Deadlines.FindAsync(id);
            _context.Deadlines.Remove(deadline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeadlineExists(int id)
        {
            return _context.Deadlines.Any(e => e.Deadline_ID == id);
        }
    }
}
