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
    public class JournalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JournalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Journals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Journal.Include(j => j.ChildProfile);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Journals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Journal == null)
            {
                return NotFound();
            }

            var journal = await _context.Journal
                .Include(j => j.ChildProfile)
                .FirstOrDefaultAsync(m => m.JournalId == id);
            if (journal == null)
            {
                return NotFound();
            }

            return View(journal);
        }

        // GET: Journals/Create
        public IActionResult Create()
        {
            ViewData["ChildProfileId"] = new SelectList(_context.ChildProfile, "ChildProfileId", "Name");
            return View();
        }

        // POST: Journals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JournalId,JournalDate,JournalBody,ChildProfileId")] Journal journal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(journal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChildProfileId"] = new SelectList(_context.ChildProfile, "ChildProfileId", "Name", journal.ChildProfileId);
            return View(journal);
        }

        // GET: Journals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Journal == null)
            {
                return NotFound();
            }

            var journal = await _context.Journal.FindAsync(id);
            if (journal == null)
            {
                return NotFound();
            }
            ViewData["ChildProfileId"] = new SelectList(_context.ChildProfile, "ChildProfileId", "Name", journal.ChildProfileId);
            return View(journal);
        }

        // POST: Journals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JournalId,JournalDate,JournalBody,ChildProfileId")] Journal journal)
        {
            if (id != journal.JournalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(journal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JournalExists(journal.JournalId))
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
            ViewData["ChildProfileId"] = new SelectList(_context.ChildProfile, "ChildProfileId", "Name", journal.ChildProfileId);
            return View(journal);
        }

        // GET: Journals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Journal == null)
            {
                return NotFound();
            }

            var journal = await _context.Journal
                .Include(j => j.ChildProfile)
                .FirstOrDefaultAsync(m => m.JournalId == id);
            if (journal == null)
            {
                return NotFound();
            }

            return View(journal);
        }

        // POST: Journals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Journal == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Journal'  is null.");
            }
            var journal = await _context.Journal.FindAsync(id);
            if (journal != null)
            {
                _context.Journal.Remove(journal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JournalExists(int id)
        {
          return (_context.Journal?.Any(e => e.JournalId == id)).GetValueOrDefault();
        }
    }
}
