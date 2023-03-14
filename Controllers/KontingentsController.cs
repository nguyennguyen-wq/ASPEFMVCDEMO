using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMVCDemo.Data;
using WebMVCDemo.Models;

namespace WebMVCDemo.Controllers
{
    public class KontingentsController : Controller
    {
        private readonly WebMVCDemoContext _context;

        public KontingentsController(WebMVCDemoContext context)
        {
            _context = context;
        }

        // GET: Kontingents
        public async Task<IActionResult> Index()
        {
              return View(await _context.Kontingent.ToListAsync());
        }

        // GET: Kontingents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kontingent == null)
            {
                return NotFound();
            }

            var kontingent = await _context.Kontingent
                .FirstOrDefaultAsync(m => m.KontintId == id);
            if (kontingent == null)
            {
                return NotFound();
            }

            return View(kontingent);
        }

        // GET: Kontingents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kontingents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KontintId,Name")] Kontingent kontingent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kontingent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kontingent);
        }

        // GET: Kontingents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kontingent == null)
            {
                return NotFound();
            }

            var kontingent = await _context.Kontingent.FindAsync(id);
            if (kontingent == null)
            {
                return NotFound();
            }
            return View(kontingent);
        }

        // POST: Kontingents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KontintId,Name")] Kontingent kontingent)
        {
            if (id != kontingent.KontintId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kontingent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KontingentExists(kontingent.KontintId))
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
            return View(kontingent);
        }

        // GET: Kontingents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kontingent == null)
            {
                return NotFound();
            }

            var kontingent = await _context.Kontingent
                .FirstOrDefaultAsync(m => m.KontintId == id);
            if (kontingent == null)
            {
                return NotFound();
            }

            return View(kontingent);
        }

        // POST: Kontingents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kontingent == null)
            {
                return Problem("Entity set 'WebMVCDemoContext.Kontingent'  is null.");
            }
            var kontingent = await _context.Kontingent.FindAsync(id);
            if (kontingent != null)
            {
                _context.Kontingent.Remove(kontingent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KontingentExists(int id)
        {
          return _context.Kontingent.Any(e => e.KontintId == id);
        }
    }
}
