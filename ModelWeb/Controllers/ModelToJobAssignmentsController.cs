using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModelWeb.Areas.Identity.Data;
using ModelWeb.Models;

namespace ModelWeb.Controllers
{
    public class ModelToJobAssignmentsController : Controller
    {
        private readonly ModelWebContext _context;

        public ModelToJobAssignmentsController(ModelWebContext context)
        {
            _context = context;
        }

        // GET: ModelToJobAssignments
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModelToJobAssignment.ToListAsync());
        }

        // GET: ModelToJobAssignments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelToJobAssignment = await _context.ModelToJobAssignment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelToJobAssignment == null)
            {
                return NotFound();
            }

            return View(modelToJobAssignment);
        }

        // GET: ModelToJobAssignments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModelToJobAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModelId,JobId")] ModelToJobAssignment modelToJobAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelToJobAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modelToJobAssignment);
        }

        // GET: ModelToJobAssignments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelToJobAssignment = await _context.ModelToJobAssignment.FindAsync(id);
            if (modelToJobAssignment == null)
            {
                return NotFound();
            }
            return View(modelToJobAssignment);
        }

        // POST: ModelToJobAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ModelId,JobId")] ModelToJobAssignment modelToJobAssignment)
        {
            if (id != modelToJobAssignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelToJobAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelToJobAssignmentExists(modelToJobAssignment.Id))
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
            return View(modelToJobAssignment);
        }

        // GET: ModelToJobAssignments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelToJobAssignment = await _context.ModelToJobAssignment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelToJobAssignment == null)
            {
                return NotFound();
            }

            return View(modelToJobAssignment);
        }

        // POST: ModelToJobAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var modelToJobAssignment = await _context.ModelToJobAssignment.FindAsync(id);
            _context.ModelToJobAssignment.Remove(modelToJobAssignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelToJobAssignmentExists(long id)
        {
            return _context.ModelToJobAssignment.Any(e => e.Id == id);
        }
    }
}
