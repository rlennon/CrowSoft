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
    public class BuildingFeaturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BuildingFeaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BuildingFeatures
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BuildingFeatures.Include(b => b.BuildingQuoteIdBuildingQuoteNavigation).Include(b => b.DefaultFeatureIdDefaultFeatureNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BuildingFeatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingFeatures = await _context.BuildingFeatures
                .Include(b => b.BuildingQuoteIdBuildingQuoteNavigation)
                .Include(b => b.DefaultFeatureIdDefaultFeatureNavigation)
                .FirstOrDefaultAsync(m => m.IdBuildingFeatures == id);
            if (buildingFeatures == null)
            {
                return NotFound();
            }

            return View(buildingFeatures);
        }

        // GET: BuildingFeatures/Create
        public IActionResult Create()
        {
            ViewData["BuildingQuoteIdBuildingQuote"] = new SelectList(_context.Set<BuildingQuote>(), "IdBuildingQuote", "Description");
            ViewData["DefaultFeatureIdDefaultFeature"] = new SelectList(_context.DefaultFeature, "IdDefaultFeature", "Description");
            return View();
        }

        // POST: BuildingFeatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBuildingFeatures,FeatureDescription,Comments,Quantity,UnitPrice,TotalCost,BuildingQuoteIdBuildingQuote,DefaultFeatureIdDefaultFeature")] BuildingFeatures buildingFeatures)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buildingFeatures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingQuoteIdBuildingQuote"] = new SelectList(_context.Set<BuildingQuote>(), "IdBuildingQuote", "Description", buildingFeatures.BuildingQuoteIdBuildingQuote);
            ViewData["DefaultFeatureIdDefaultFeature"] = new SelectList(_context.DefaultFeature, "IdDefaultFeature", "Description", buildingFeatures.DefaultFeatureIdDefaultFeature);
            return View(buildingFeatures);
        }

        // GET: BuildingFeatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingFeatures = await _context.BuildingFeatures.FindAsync(id);
            if (buildingFeatures == null)
            {
                return NotFound();
            }
            ViewData["BuildingQuoteIdBuildingQuote"] = new SelectList(_context.Set<BuildingQuote>(), "IdBuildingQuote", "Description", buildingFeatures.BuildingQuoteIdBuildingQuote);
            ViewData["DefaultFeatureIdDefaultFeature"] = new SelectList(_context.DefaultFeature, "IdDefaultFeature", "Description", buildingFeatures.DefaultFeatureIdDefaultFeature);
            return View(buildingFeatures);
        }

        // POST: BuildingFeatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBuildingFeatures,FeatureDescription,Comments,Quantity,UnitPrice,TotalCost,BuildingQuoteIdBuildingQuote,DefaultFeatureIdDefaultFeature")] BuildingFeatures buildingFeatures)
        {
            if (id != buildingFeatures.IdBuildingFeatures)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buildingFeatures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingFeaturesExists(buildingFeatures.IdBuildingFeatures))
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
            ViewData["BuildingQuoteIdBuildingQuote"] = new SelectList(_context.Set<BuildingQuote>(), "IdBuildingQuote", "Description", buildingFeatures.BuildingQuoteIdBuildingQuote);
            ViewData["DefaultFeatureIdDefaultFeature"] = new SelectList(_context.DefaultFeature, "IdDefaultFeature", "Description", buildingFeatures.DefaultFeatureIdDefaultFeature);
            return View(buildingFeatures);
        }

        // GET: BuildingFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingFeatures = await _context.BuildingFeatures
                .Include(b => b.BuildingQuoteIdBuildingQuoteNavigation)
                .Include(b => b.DefaultFeatureIdDefaultFeatureNavigation)
                .FirstOrDefaultAsync(m => m.IdBuildingFeatures == id);
            if (buildingFeatures == null)
            {
                return NotFound();
            }

            return View(buildingFeatures);
        }

        // POST: BuildingFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildingFeatures = await _context.BuildingFeatures.FindAsync(id);
            _context.BuildingFeatures.Remove(buildingFeatures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildingFeaturesExists(int id)
        {
            return _context.BuildingFeatures.Any(e => e.IdBuildingFeatures == id);
        }
    }
}
