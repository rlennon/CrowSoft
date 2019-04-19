using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using crowsoftmvc.Data;
using crowsoftmvc.Models;
using Microsoft.AspNetCore.Authorization;

namespace crowsoftmvc.Controllers
{
    [Authorize(Policy = "RequireAdminOnly")]
    public class DefaultFeatureController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public IEnumerable<SelectListItem> MeasurementsList = Measurements.GetMeasurements();

        public DefaultFeatureController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Controllers/DefaultFeature
        public async Task<IActionResult> Index()
        {
            return View(await _context.DefaultFeature.ToListAsync());
        }

        // GET: Controllers/DefaultFeature/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultFeature = await _context.DefaultFeature
                .FirstOrDefaultAsync(m => m.IdDefaultFeature == id);
            if (defaultFeature == null)
            {
                return NotFound();
            }

            return View(defaultFeature);
        }

        // GET: Controllers/DefaultFeature/Create
        public IActionResult Create()
        {
            DefaultFeature defaultFeature = new DefaultFeature();
            return View(defaultFeature);
        }

        // POST: Controllers/DefaultFeature/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDefaultFeature,Description,Measurement,UnitPrice,IsDefaultFeature")] DefaultFeature defaultFeature)
        {
            if (ModelState.IsValid)
            {
                if (defaultFeature.IsDefaultFeature == null)
                {
                    defaultFeature.IsDefaultFeature = 0;
                }
                _context.Add(defaultFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(defaultFeature);
        }

        // GET: Controllers/DefaultFeature/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultFeature = await _context.DefaultFeature.FindAsync(id);
            if (defaultFeature == null)
            {
                return NotFound();
            }
            return View(defaultFeature);
        }

        // POST: Controllers/DefaultFeature/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDefaultFeature,Description,Measurement,UnitPrice,IsDefaultFeature")] DefaultFeature defaultFeature)
        {
            if (id != defaultFeature.IdDefaultFeature)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(defaultFeature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DefaultFeatureExists(defaultFeature.IdDefaultFeature))
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
            return View(defaultFeature);
        }

        // GET: Controllers/DefaultFeature/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultFeature = await _context.DefaultFeature
                .FirstOrDefaultAsync(m => m.IdDefaultFeature == id);
            if (defaultFeature == null)
            {
                return NotFound();
            }

            return View(defaultFeature);
        }

        // POST: Controllers/DefaultFeature/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var defaultFeature = await _context.DefaultFeature.FindAsync(id);
            _context.DefaultFeature.Remove(defaultFeature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DefaultFeatureExists(int id)
        {
            return _context.DefaultFeature.Any(e => e.IdDefaultFeature == id);
        }
    }
}
