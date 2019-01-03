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
    public class JobsController : Controller
    {
        private readonly ModelWebContext _context;

        public JobsController(ModelWebContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jobs.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> UpcomingJobs()
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
            var currentUser = await _context.Users.Where(id => id.Id == currentUserId).FirstAsync();
        
            var currentModel = await _context.Model.Where(pn => pn.PhoneNumber == currentUser.PhoneNumber).FirstAsync();
        
            var currentUserAssignments = await _context.ModelToJobAssignment.Where(id => id.ModelId == currentModel.Id).ToListAsync();
        
            var currentUserJobs = new List<Jobs>();
        
            foreach (var assignment in currentUserAssignments)
            {
                var job = await _context.Jobs.Where(id => id.Id == assignment.JobId).FirstAsync();
                if (job.StartDate > DateTime.Now)
                {
                    currentUserJobs.Add(job);
                }
            }
        
            return View(currentUserJobs);
        }

        [Authorize]
        public async Task<IActionResult> CompletedJobs()
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUser = await _context.Users.Where(id => id.Id == currentUserId).FirstAsync();

            var currentModel = await _context.Model.Where(pn => pn.PhoneNumber == currentUser.PhoneNumber).FirstAsync();

            var currentUserAssignments = await _context.ModelToJobAssignment.Where(id => id.ModelId == currentModel.Id).ToListAsync();

            var currentUserJobs = new List<Jobs>();

            foreach (var assignment in currentUserAssignments)
            {
                var job = await _context.Jobs.Where(id => id.Id == assignment.JobId).FirstAsync();
                if (job.StartDate < DateTime.Now)
                {
                    currentUserJobs.Add(job);
                }
            }

            return View(currentUserJobs);
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobs = await _context.Jobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobs == null)
            {
                return NotFound();
            }

            return View(jobs);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerName,StartDate,NumberOfDays,Location,NumberOfModels,Comments,IsPlanned")] Jobs jobs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobs);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobs = await _context.Jobs.FindAsync(id);
            if (jobs == null)
            {
                return NotFound();
            }
            return View(jobs);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CustomerName,StartDate,NumberOfDays,Location,NumberOfModels,Comments,IsPlanned")] Jobs jobs)
        {
            if (id != jobs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobsExists(jobs.Id))
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
            return View(jobs);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobs = await _context.Jobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobs == null)
            {
                return NotFound();
            }

            return View(jobs);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var jobs = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(jobs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobsExists(long id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
