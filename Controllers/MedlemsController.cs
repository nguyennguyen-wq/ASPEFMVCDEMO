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
    public class MedlemsController : Controller
    {
        private readonly WebEFMVCDemoContext _context;
        public MedlemsController(WebEFMVCDemoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var webEFMVCDemoContext = _context.Medlems.Include(m => m.Kontingent);
            return View(await webEFMVCDemoContext.ToListAsync());
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Medlems == null)
            {
                return NotFound();
            }

            var medlem = await _context.Medlems
                .Include(m => m.Kontingent)
                .FirstOrDefaultAsync(m => m.Medlem_Id == id);
            if (medlem == null)
            {
                return NotFound();
            }

            return View(medlem);
        }
        public IActionResult Create()
        {
            ViewData["KontintId"] = new SelectList(_context.Kontingents, "KontintId", "KontintId");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Medlem_Id,Fornavn,Etternavn,Bosted,MobilTlf,Email,Fodselsdato,KontintId")] Medlem medlem)
        {
                medlem.Medlem_Id = Guid.NewGuid();
                _context.Add(medlem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            ViewData["KontintId"] = new SelectList(_context.Kontingents, "KontintId", "KontintId", medlem.KontintId);
            return View(medlem);
        }
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Medlems == null)
            {
                return NotFound();
            }

            var medlem = await _context.Medlems.FindAsync(id);
            if (medlem == null)
            {
                return NotFound();
            }
            ViewData["KontintId"] = new SelectList(_context.Kontingents, "KontintId", "KontintId", medlem.KontintId);
            return View(medlem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Medlem_Id,Fornavn,Etternavn,Bosted,MobilTlf,Email,Fodselsdato,KontintId")] Medlem medlem)
        {
                if (id != medlem.Medlem_Id)
                {
                   return NotFound();
                }

                try
                { 
                    _context.Update(medlem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedlemExists(medlem.Medlem_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
              ViewData["KontintId"] = new SelectList(_context.Kontingents, "KontintId", "KontintId", medlem.KontintId);
              return View(medlem);
        }
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Medlems == null)
            {
                return NotFound();
            }

            var medlem = await _context.Medlems
                .Include(m => m.Kontingent)
                .FirstOrDefaultAsync(m => m.Medlem_Id == id);
            if (medlem == null)
            {
                return NotFound();
            }

            return View(medlem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Medlems == null)
            {
                return Problem("Entity set 'WebEFMVCDemoContext.Medlems'  is null.");
            }
            var medlem = await _context.Medlems.FindAsync(id);
            if (medlem != null)
            {
                _context.Medlems.Remove(medlem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool MedlemExists(Guid id)
        {
          return _context.Medlems.Any(e => e.Medlem_Id == id);
        }
    }
}
