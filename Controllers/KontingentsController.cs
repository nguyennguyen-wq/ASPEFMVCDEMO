using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebEFMVCDemo.Data;
using WebEFMVCDemo.Models;
namespace WebEFMVCDemo.Controllers
{
    public class KontingentsController : Controller
    {
        private readonly WebEFMVCDemoContext _context;
        public KontingentsController(WebEFMVCDemoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
              return View(await _context.Kontingents.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kontingents == null)
            {
                return NotFound();
            }

            var kontingent = await _context.Kontingents
                .FirstOrDefaultAsync(m => m.KontintId == id);
            if (kontingent == null)
            {
                return NotFound();
            }
            return View(kontingent);
        }
        public IActionResult Create()
        {
            return View();
        }
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kontingents == null)
            {
                return NotFound();
            }

            var kontingent = await _context.Kontingents.FindAsync(id);
            if (kontingent == null)
            {
                return NotFound();
            }
            return View(kontingent);
        }
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kontingents == null)
            {
                return NotFound();
            }

            var kontingent = await _context.Kontingents
                .FirstOrDefaultAsync(m => m.KontintId == id);
            if (kontingent == null)
            {
                return NotFound();
            }
            return View(kontingent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kontingents == null)
            {
                return Problem("Entity set 'WebEFMVCDemoContext.Kontingents'  is null.");
            }
            var kontingent = await _context.Kontingents.FindAsync(id);
            if (kontingent != null)
            {
                _context.Kontingents.Remove(kontingent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool KontingentExists(int id)
        {
          return _context.Kontingents.Any(e => e.KontintId == id);
        }
    }
}
