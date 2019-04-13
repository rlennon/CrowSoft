using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using crowsoftmvc.Data;
using crowsoftmvc.Models;

namespace crowsoftmvc.Controllers
{
    public class DummyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DummyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dummy
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dummy.ToListAsync());
        }

        // GET: Dummy/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dummy = await _context.Dummy
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (dummy == null)
            {
                return NotFound();
            }

            return View(dummy);
        }

        // GET: Dummy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dummy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,LastName,FirstName,Address,City")] Dummy dummy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dummy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dummy);
        }

        // GET: Dummy/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dummy = await _context.Dummy.FindAsync(id);
            if (dummy == null)
            {
                return NotFound();
            }
            return View(dummy);
        }

        // POST: Dummy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("PersonId,LastName,FirstName,Address,City")] Dummy dummy)
        {
            if (id != dummy.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dummy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DummyExists(dummy.PersonId))
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
            return View(dummy);
        }

        // GET: Dummy/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dummy = await _context.Dummy
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (dummy == null)
            {
                return NotFound();
            }

            return View(dummy);
        }

        // POST: Dummy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var dummy = await _context.Dummy.FindAsync(id);
            _context.Dummy.Remove(dummy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DummyExists(byte id)
        {
            return _context.Dummy.Any(e => e.PersonId == id);
        }
    }
}
