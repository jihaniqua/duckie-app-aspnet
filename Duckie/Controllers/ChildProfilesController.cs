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
    public class ChildProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChildProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChildProfiles
        public async Task<IActionResult> Index()
        {
              return _context.ChildProfile != null ? 
                          View(await _context.ChildProfile.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ChildProfile'  is null.");
        }

        // GET: ChildProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChildProfile == null)
            {
                return NotFound();
            }

            var childProfile = await _context.ChildProfile
                .FirstOrDefaultAsync(m => m.ChildProfileId == id);
            if (childProfile == null)
            {
                return NotFound();
            }

            return View(childProfile);
        }

        // GET: ChildProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChildProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChildProfileId,Name,Birthdate")] ChildProfile childProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(childProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(childProfile);
        }

        // GET: ChildProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChildProfile == null)
            {
                return NotFound();
            }

            var childProfile = await _context.ChildProfile.FindAsync(id);
            if (childProfile == null)
            {
                return NotFound();
            }
            return View(childProfile);
        }

        // POST: ChildProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChildProfileId,Name,Birthdate")] ChildProfile childProfile)
        {
            if (id != childProfile.ChildProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(childProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildProfileExists(childProfile.ChildProfileId))
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
            return View(childProfile);
        }

        // GET: ChildProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChildProfile == null)
            {
                return NotFound();
            }

            var childProfile = await _context.ChildProfile
                .FirstOrDefaultAsync(m => m.ChildProfileId == id);
            if (childProfile == null)
            {
                return NotFound();
            }

            return View(childProfile);
        }

        // POST: ChildProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChildProfile == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ChildProfile'  is null.");
            }
            var childProfile = await _context.ChildProfile.FindAsync(id);
            if (childProfile != null)
            {
                _context.ChildProfile.Remove(childProfile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChildProfileExists(int id)
        {
          return (_context.ChildProfile?.Any(e => e.ChildProfileId == id)).GetValueOrDefault();
        }
    }
}
