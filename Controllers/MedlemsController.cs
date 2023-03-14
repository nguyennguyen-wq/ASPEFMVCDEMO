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
    public class MedlemsController : Controller
    {
        private readonly WebMVCDemoContext _context;

        public MedlemsController(WebMVCDemoContext context)
        {
            _context = context;
        }

        // GET: Medlems
        public async Task<IActionResult> Index()
        {
              return View(await _context.Medlem.ToListAsync());
        }

        // GET: Medlems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Medlem == null)
            {
                return NotFound();
            }

            var medlem = await _context.Medlem
                .FirstOrDefaultAsync(m => m.Medlem_Id == id);
            if (medlem == null)
            {
                return NotFound();
            }

            return View(medlem);
        }

        // GET: Medlems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medlems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Medlem_Id,Fornavn,Etternavn,Bosted,MobilTlf,Email,Fodselsdato,CurrentKontintId")] Medlem medlem)
        {
            if (ModelState.IsValid)
            {
                medlem.Medlem_Id = Guid.NewGuid();
                _context.Add(medlem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medlem);
        }

        // GET: Medlems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Medlem == null)
            {
                return NotFound();
            }

            var medlem = await _context.Medlem.FindAsync(id);
            if (medlem == null)
            {
                return NotFound();
            }
            return View(medlem);
        }

        // POST: Medlems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Medlem_Id,Fornavn,Etternavn,Bosted,MobilTlf,Email,Fodselsdato,CurrentKontintId")] Medlem medlem)
        {
            if (id != medlem.Medlem_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            }
            return View(medlem);
        }

        // GET: Medlems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Medlem == null)
            {
                return NotFound();
            }

            var medlem = await _context.Medlem
                .FirstOrDefaultAsync(m => m.Medlem_Id == id);
            if (medlem == null)
            {
                return NotFound();
            }

            return View(medlem);
        }

        // POST: Medlems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Medlem == null)
            {
                return Problem("Entity set 'WebMVCDemoContext.Medlem'  is null.");
            }
            var medlem = await _context.Medlem.FindAsync(id);
            if (medlem != null)
            {
                _context.Medlem.Remove(medlem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedlemExists(Guid id)
        {
          return _context.Medlem.Any(e => e.Medlem_Id == id);
        }
    }
}
