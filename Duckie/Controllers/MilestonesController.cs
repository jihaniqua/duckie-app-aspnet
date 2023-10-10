using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Duckie.Data;
using Duckie.Models;

namespace Duckie.Controllers
{
    public class MilestonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MilestonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Milestones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Milestone.Include(m => m.ChildProfile);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Milestones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Milestone == null)
            {
                return NotFound();
            }

            var milestone = await _context.Milestone
                .Include(m => m.ChildProfile)
                .FirstOrDefaultAsync(m => m.MilestoneID == id);
            if (milestone == null)
            {
                return NotFound();
            }

            return View(milestone);
        }

        // GET: Milestones/Create
        public IActionResult Create()
        {
            ViewData["ChildProfileId"] = new SelectList(_context.ChildProfile, "ChildProfileId", "Name");
            return View();
        }

        // POST: Milestones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MilestoneID,MilestoneName,MilestoneDate,Comments,Photo,ChildProfileId")] Milestone milestone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(milestone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChildProfileId"] = new SelectList(_context.ChildProfile, "ChildProfileId", "Name", milestone.ChildProfileId);
            return View(milestone);
        }

        // GET: Milestones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Milestone == null)
            {
                return NotFound();
            }

            var milestone = await _context.Milestone.FindAsync(id);
            if (milestone == null)
            {
                return NotFound();
            }
            ViewData["ChildProfileId"] = new SelectList(_context.ChildProfile, "ChildProfileId", "Name", milestone.ChildProfileId);
            return View(milestone);
        }

        // POST: Milestones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MilestoneID,MilestoneName,MilestoneDate,Comments,Photo,ChildProfileId")] Milestone milestone)
        {
            if (id != milestone.MilestoneID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(milestone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MilestoneExists(milestone.MilestoneID))
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
            ViewData["ChildProfileId"] = new SelectList(_context.ChildProfile, "ChildProfileId", "Name", milestone.ChildProfileId);
            return View(milestone);
        }

        // GET: Milestones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Milestone == null)
            {
                return NotFound();
            }

            var milestone = await _context.Milestone
                .Include(m => m.ChildProfile)
                .FirstOrDefaultAsync(m => m.MilestoneID == id);
            if (milestone == null)
            {
                return NotFound();
            }

            return View(milestone);
        }

        // POST: Milestones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Milestone == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Milestone'  is null.");
            }
            var milestone = await _context.Milestone.FindAsync(id);
            if (milestone != null)
            {
                _context.Milestone.Remove(milestone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MilestoneExists(int id)
        {
          return (_context.Milestone?.Any(e => e.MilestoneID == id)).GetValueOrDefault();
        }
    }
}
