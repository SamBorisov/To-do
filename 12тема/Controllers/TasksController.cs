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
    public class TasksController : Controller
    {
        private readonly Sami1Context _context;

        public TasksController(Sami1Context context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var sami1Context = _context.Tasks.Include(t => t.Account).Include(t => t.Blocker_Task).Include(t => t.TaskStatus);
            return View(await sami1Context.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Account)
                .Include(t => t.Blocker_Task)
                .Include(t => t.TaskStatus)
                .FirstOrDefaultAsync(m => m.Task_ID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["Account_ID"] = new SelectList(_context.Accounts, "Account_ID", "E_mail");
            ViewData["Blocker_Task_ID"] = new SelectList(_context.Tasks, "Task_ID", "Task_ID");
            ViewData["TaskStatusID"] = new SelectList(_context.TaskStatuses, "TaskStatusID", "TaskStatusName");
           // ViewData["Task_Name"] = new SelectList(_context.Tasks, "Task_Name", "Task_Name");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Task_ID,TaskStatusID,Account_ID,Blocker_Task_ID,Task_Name")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Account_ID"] = new SelectList(_context.Accounts, "Account_ID", "E_mail", task.Account_ID);
            ViewData["Blocker_Task_ID"] = new SelectList(_context.Tasks, "Task_ID", "Task_ID", task.Blocker_Task_ID);
            ViewData["TaskStatusID"] = new SelectList(_context.TaskStatuses, "TaskStatusID", "TaskStatusName", task.TaskStatusID);
          //  ViewData["Task_Name"] = new SelectList(_context.Tasks, "Task_Name", "Task_Name", task.Task_Name);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["Account_ID"] = new SelectList(_context.Accounts, "Account_ID", "E_mail", task.Account_ID);
            ViewData["Blocker_Task_ID"] = new SelectList(_context.Tasks, "Task_ID", "Task_ID", task.Blocker_Task_ID);
            ViewData["TaskStatusID"] = new SelectList(_context.TaskStatuses, "TaskStatusID", "TaskStatusName", task.TaskStatusID);
           // ViewData["Task_Name"] = new SelectList(_context.Tasks, "Task_Name", "Task_Name", task.Task_Name);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Task_ID,TaskStatusID,Account_ID,Blocker_Task_ID,Task_Name")] Models.Task task)
        {
            if (id != task.Task_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Task_ID))
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
            ViewData["Account_ID"] = new SelectList(_context.Accounts, "Account_ID", "E_mail", task.Account_ID);
            ViewData["Blocker_Task_ID"] = new SelectList(_context.Tasks, "Task_ID", "Task_ID", task.Blocker_Task_ID);
            ViewData["TaskStatusID"] = new SelectList(_context.TaskStatuses, "TaskStatusID", "TaskStatusName", task.TaskStatusID);
           // ViewData["Task_Name"] = new SelectList(_context.Tasks, "Task_Name", "Task_Name", task.Task_Name);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Account)
                .Include(t => t.Blocker_Task)
                .Include(t => t.TaskStatus)
                .Include(t => t.Task_Name)
                .FirstOrDefaultAsync(m => m.Task_ID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Task_ID == id);
        }
    }
}
