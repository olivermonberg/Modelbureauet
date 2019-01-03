using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelbureauetDB.Models;

namespace ModelbureauetDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelToJobAssignmentsController : ControllerBase
    {
        private readonly ModelbureauetDBContext _context;

        public ModelToJobAssignmentsController(ModelbureauetDBContext context)
        {
            _context = context;
        }

        // GET: api/ModelToJobAssignments
        [HttpGet]
        public IEnumerable<ModelToJobAssignment> GetModelToJobAssignment()
        {
            return _context.ModelToJobAssignment;
        }

        // GET: api/ModelToJobAssignments/GetAllJobsAssignedToModels
        [HttpGet("GetAllJobsAssignedToModels")]
        public IEnumerable<Jobs> GetAllJobsAssignedToModels()
        {
            IEnumerable<ModelToJobAssignment> listOfAssignments = _context.ModelToJobAssignment;

            IList<Jobs> jobsWithModels = new List<Jobs>();

            foreach (var assignment in listOfAssignments)
            {
                var temp = _context.Jobs.Where(job => job.Id == assignment.JobId).FirstOrDefault();
                jobsWithModels.Add(temp);
            }

            return jobsWithModels;
        }

        // GET: api/ModelToJobAssignments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModelToJobAssignment([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelToJobAssignment = await _context.ModelToJobAssignment.FindAsync(id);

            if (modelToJobAssignment == null)
            {
                return NotFound();
            }

            return Ok(modelToJobAssignment);
        }

        // PUT: api/ModelToJobAssignments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelToJobAssignment([FromRoute] long id, [FromBody] ModelToJobAssignment modelToJobAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modelToJobAssignment.Id)
            {
                return BadRequest();
            }

            _context.Entry(modelToJobAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelToJobAssignmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ModelToJobAssignments
        [HttpPost]
        public async Task<IActionResult> PostModelToJobAssignment([FromBody] ModelToJobAssignment modelToJobAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ModelToJobAssignment.Add(modelToJobAssignment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModelToJobAssignment", new { id = modelToJobAssignment.Id }, modelToJobAssignment);
        }

        // DELETE: api/ModelToJobAssignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelToJobAssignment([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelToJobAssignment = await _context.ModelToJobAssignment.FindAsync(id);
            if (modelToJobAssignment == null)
            {
                return NotFound();
            }

            _context.ModelToJobAssignment.Remove(modelToJobAssignment);
            await _context.SaveChangesAsync();

            return Ok(modelToJobAssignment);
        }

        private bool ModelToJobAssignmentExists(long id)
        {
            return _context.ModelToJobAssignment.Any(e => e.Id == id);
        }
    }
}