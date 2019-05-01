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
    [Authorize]
    public class BuildingFeaturesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public int BuildQuotID = 0;

        [BindProperty]
        public InputModelBuidlingFeatures Input { get; set; }

        public BuildingFeaturesController(ApplicationDbContext context)
        {
            _context = context;
            Input = new InputModelBuidlingFeatures();
        }
        

        // GET: BuildingFeatures
        //[Route("InputModelBuidlingFeatures/{buildingQouteId:int}")]
        public async Task<IActionResult> Index(int idBuildingQoute)
        {
            BuildQuotID = idBuildingQoute;
            Input.buildingQouteId = idBuildingQoute.ToString();
            var applicationDbContext = _context.BuildingFeatures.Include(b => b.BuildingQuoteIdBuildingQuoteNavigation).Include(b => b.DefaultFeatureIdDefaultFeatureNavigation);
            Input.buildingFeaters = await applicationDbContext.Where(b => b.BuildingQuoteIdBuildingQuote == idBuildingQoute).ToListAsync();
            return View(Input);
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
            ViewData["BuildingQuoteId"] = buildingFeatures.BuildingQuoteIdBuildingQuote;
            return View(buildingFeatures);
        }

        // GET: BuildingFeatures/Create
        public IActionResult Create(int buildingQouteId)
        {
            Input.buildingQouteId = buildingQouteId.ToString();
            ViewData["BuildingQuoteIdBuildingQuote"] = new SelectList(_context.Set<BuildingQuote>(), "IdBuildingQuote", "Description");
            ViewData["DefaultFeatureIdDefaultFeature"] = new SelectList(_context.DefaultFeature.Where(f => f.IsDefaultFeature == 0).ToList(), "IdDefaultFeature", "Description");
            BuildingFeatures buildingFeatures = new BuildingFeatures();
            buildingFeatures.TotalCost = decimal.Parse("0.00");
            buildingFeatures.BuildingQuoteIdBuildingQuote = buildingQouteId;
            ViewData["BuildingQuoteId"] = buildingFeatures.BuildingQuoteIdBuildingQuote;
            return View(buildingFeatures);
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
                var feature = _context.DefaultFeature.Where(f => f.IdDefaultFeature == int.Parse(buildingFeatures.FeatureDescription)).FirstOrDefault();
                buildingFeatures.FeatureDescription = feature.Description;
                buildingFeatures.DefaultFeatureIdDefaultFeature = feature.IdDefaultFeature;
                buildingFeatures.UnitPrice = feature.UnitPrice;
                buildingFeatures.TotalCost = buildingFeatures.UnitPrice * buildingFeatures.Quantity;
               // buildingFeatures.BuildingQuoteIdBuildingQuote = int.Parse(Input.buildingQouteId);
                _context.Add(buildingFeatures);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: "Index", controllerName: "BuildingFeatures", routeValues: new { idBuildingQoute = buildingFeatures.BuildingQuoteIdBuildingQuote});
            }
            ViewData["BuildingQuoteIdBuildingQuote"] = new SelectList(_context.Set<BuildingQuote>(), "IdBuildingQuote", "Description", buildingFeatures.BuildingQuoteIdBuildingQuote);
            ViewData["DefaultFeatureIdDefaultFeature"] = new SelectList(_context.DefaultFeature, "IdDefaultFeature", "Description", buildingFeatures.DefaultFeatureIdDefaultFeature);
            ViewData["BuildingQuoteId"] = buildingFeatures.BuildingQuoteIdBuildingQuote;
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
            ViewData["BuildingQuoteId"] = buildingFeatures.BuildingQuoteIdBuildingQuote;
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
                    var feature = _context.DefaultFeature.Where(f => f.IdDefaultFeature == int.Parse(buildingFeatures.FeatureDescription)).FirstOrDefault();
                    buildingFeatures.FeatureDescription = feature.Description;
                    buildingFeatures.DefaultFeatureIdDefaultFeature = feature.IdDefaultFeature;
                    buildingFeatures.UnitPrice = feature.UnitPrice;
                    buildingFeatures.TotalCost = buildingFeatures.UnitPrice * buildingFeatures.Quantity;
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
                return RedirectToAction(actionName: "Index", controllerName: "BuildingFeatures", routeValues: new { idBuildingQoute = buildingFeatures.BuildingQuoteIdBuildingQuote });
            }
            ViewData["BuildingQuoteIdBuildingQuote"] = new SelectList(_context.Set<BuildingQuote>(), "IdBuildingQuote", "Description", buildingFeatures.BuildingQuoteIdBuildingQuote);
            ViewData["DefaultFeatureIdDefaultFeature"] = new SelectList(_context.DefaultFeature, "IdDefaultFeature", "Description", buildingFeatures.DefaultFeatureIdDefaultFeature);
            ViewData["BuildingQuoteId"] = buildingFeatures.BuildingQuoteIdBuildingQuote;
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
            ViewData["BuildingQuoteId"] = buildingFeatures.BuildingQuoteIdBuildingQuote;
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
            return RedirectToAction(actionName: "Index", controllerName: "BuildingFeatures", routeValues: new { idBuildingQoute = buildingFeatures.BuildingQuoteIdBuildingQuote });
        }

        private bool BuildingFeaturesExists(int id)
        {
            return _context.BuildingFeatures.Any(e => e.IdBuildingFeatures == id);
        }
    }
}
