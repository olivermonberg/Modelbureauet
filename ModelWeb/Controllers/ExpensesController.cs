using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModelWeb.Areas.Identity.Data;
using ModelWeb.Models;

namespace ModelWeb.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ModelWebContext _context;

        public ExpensesController(ModelWebContext context)
        {
            _context = context;
        }

        // GET: Expenses
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUser = await _context.Users.Where(id => id.Id == currentUserId).FirstAsync();

            var currentModel = await _context.Model.Where(pn => pn.PhoneNumber == currentUser.PhoneNumber).FirstAsync();

            var currentUserExpenses = await _context.Expenses.Where(id => id.ModelId == currentModel.Id).ToListAsync();

            return View(currentUserExpenses);
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.Expenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Expenses/Create/{jobIdParameter}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long jobIdParameter, [Bind("Id,JobId,ModelId,StartDate,Comments,Amount")] Expenses expenses)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var currentUser = await _context.Users.Where(id => id.Id == currentUserId).FirstAsync();

                var currentModel = await _context.Model.Where(pn => pn.PhoneNumber == currentUser.PhoneNumber).FirstAsync();

                expenses.ModelId = currentModel.Id;
                expenses.JobId = jobIdParameter;

                _context.Add(expenses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenses);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.Expenses.FindAsync(id);
            if (expenses == null)
            {
                return NotFound();
            }
            return View(expenses);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,JobId,ModelId,StartDate,Comments,Amount")] Expenses expenses)
        {
            if (id != expenses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpensesExists(expenses.Id))
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
            return View(expenses);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.Expenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var expenses = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expenses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpensesExists(long id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
