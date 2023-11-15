using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Duckie.Data;
using Duckie.Models;
using Microsoft.AspNetCore.Authorization;

namespace Duckie.Controllers
{
    [Authorize]
    public class MilestonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MilestonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Milestones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Milestone.Include(m => m.ChildProfile);
            return View(await applicationDbContext.ToListAsync());
        }

        [AllowAnonymous]
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
            ViewData["ChildProfileId"] = new SelectList(_context.ChildProfile.OrderBy(c => c.Name), "ChildProfileId", "Name");
            return View();
        }

        // POST: Milestones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MilestoneID,MilestoneName,MilestoneDate,Comments,Photo,ChildProfileId")] Milestone milestone, IFormFile? Photo)
        {
            if (ModelState.IsValid)
            {
                // Check for a photo upload and process it if there is one
                if (Photo != null)
                {
                    milestone.Photo = UploadPhoto(Photo);
                }
                _context.Add(milestone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChildProfileId"] = new SelectList(_context.ChildProfile.OrderBy(c => c.Name), "ChildProfileId", "Name", milestone.ChildProfileId);
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
        public async Task<IActionResult> Edit(int id, [Bind("MilestoneID,MilestoneName,MilestoneDate,Comments,Photo,ChildProfileId")] Milestone milestone, IFormFile? Photo, string? ExistingPhoto)
        {
            if (id != milestone.MilestoneID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Photo != null)
                    {
                        milestone.Photo = UploadPhoto(Photo);
                    }
                    else
                    {
                        milestone.Photo = ExistingPhoto;
                    }
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

        // Upload Milestone Photo method
        private string UploadPhoto(IFormFile Photo)
        {
            var tempPath = Path.GetTempFileName();
            var fileName = Guid.NewGuid().ToString() + "-" + Photo.FileName;
            var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\milestone-images\\" + fileName;
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                Photo.CopyTo(stream);
            }
            return fileName;
        }
    }
}
