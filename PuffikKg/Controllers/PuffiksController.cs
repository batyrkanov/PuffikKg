using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PuffikKg.Data;
using PuffikKg.Models;

namespace PuffikKg.Controllers
{
    public class PuffiksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PuffiksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Puffiks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Puffiks.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Puffiks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puffik = await _context.Puffiks
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puffik == null)
            {
                return NotFound();
            }

            return View(puffik);
        }

        // GET: Puffiks/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Puffiks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ShortDescription,FullDescription,Count,Price,CategoryId")] Puffik puffik)
        {
            if (ModelState.IsValid)
            {
                puffik.Id = Guid.NewGuid();
                _context.Add(puffik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", puffik.CategoryId);
            return View(puffik);
        }

        // GET: Puffiks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puffik = await _context.Puffiks.FindAsync(id);
            if (puffik == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", puffik.CategoryId);
            return View(puffik);
        }

        // POST: Puffiks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ShortDescription,FullDescription,Count,Price,CategoryId")] Puffik puffik)
        {
            if (id != puffik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puffik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuffikExists(puffik.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", puffik.CategoryId);
            return View(puffik);
        }

        // GET: Puffiks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puffik = await _context.Puffiks
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puffik == null)
            {
                return NotFound();
            }

            return View(puffik);
        }

        // POST: Puffiks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var puffik = await _context.Puffiks.FindAsync(id);
            _context.Puffiks.Remove(puffik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuffikExists(Guid id)
        {
            return _context.Puffiks.Any(e => e.Id == id);
        }
    }
}
