using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using crowsoftmvc.Data;
using crowsoftmvc.Models;
using crowsoftmvc.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace crowsoftmvc.Controllers
{
    [Authorize]
    public class BuildingQuotesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IEnumerable<SelectListItem> MeasurementsList = Measurements.GetMeasurements();
        private readonly UserManager<CrowsoftUser> _userManager;

        public BuildingQuotesController(ApplicationDbContext context, UserManager<CrowsoftUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BuildingQuotes
        public async Task<IActionResult> Index()
        {
            var userID = _userManager.GetUserId(User);
            var userAccount = _context.UserAccount.Where(u => u.AspNetUserID == userID).FirstOrDefault();
            ViewData["UserName"] = userAccount.FirstName + " " + userAccount.LastName;
            return View(await _context.BuildingQuote.Where(b => b.UserAccountIdUserAccount == userAccount.idUserAccount).ToListAsync());
        }

        // GET: BuildingQuotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingQuote = await _context.BuildingQuote
                .FirstOrDefaultAsync(m => m.IdBuildingQuote == id);
            if (buildingQuote == null)
            {
                return NotFound();
            }

            ViewData["StatusMessage"] = "Quote has been generated!";
            // Message on screen if Quote is still in Created status
            if (buildingQuote.Status == "Created")
            {
                ViewData["StatusMessage"] = "Quote has not been calculated by the system!";
            }

            return View(buildingQuote);
        }
        // CreateFeature
        public async Task<IActionResult> CreateFeature(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingQuote = await _context.BuildingQuote
                .FirstOrDefaultAsync(m => m.IdBuildingQuote == id);
            if (buildingQuote == null)
            {
                return NotFound();
            }
            return RedirectToAction(actionName: "Index", controllerName: "BuildingFeatures", routeValues: new { idBuildingQoute = buildingQuote.IdBuildingQuote });
        }

        public async Task<IActionResult> AddImages(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingQuote = await _context.BuildingQuote
                .FirstOrDefaultAsync(m => m.IdBuildingQuote == id);
            if (buildingQuote == null)
            {
                return NotFound();
            }
            return RedirectToAction(actionName: "Index", controllerName: "BuildingImages", routeValues: new { idBuildingQoute = buildingQuote.IdBuildingQuote });
        }

        [Authorize(Roles = "Client")]
        public IActionResult Create()
        {
            BuildingQuote buildingQuote = new BuildingQuote();
            var userID = _userManager.GetUserId(User);
            var userAccount = _context.UserAccount.Where(u => u.AspNetUserID == userID).FirstOrDefault();
            buildingQuote.UpdatedById = userAccount.idUserAccount;
            buildingQuote.UserAccountIdUserAccount = userAccount.idUserAccount;
            buildingQuote.DateCreated = DateTime.Now;
            buildingQuote.MeasurementType = "Meters";
            buildingQuote.TotalCost = decimal.Parse("00.00");
            buildingQuote.Status = "Created";
            return View(buildingQuote);
        }

        // POST: BuildingQuotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBuildingQuote,UserAccountIdUserAccount,Description,MeasurementType,Height,Width,Depth,PurposeOfBuilding,BuildingSize,TotalCost,DateCreated,Status,TimeFrame,DateUpdated,UpdatedById")] BuildingQuote buildingQuote)
        {
            
            if (ModelState.IsValid)
            {
                // Added by Charles = setup data for Building Qoutes
                UserAccountsController userAccountsController = new UserAccountsController(_context);
                var userID = _userManager.GetUserId(User);
                var userAccount = _context.UserAccount.Where(u => u.AspNetUserID == userID).FirstOrDefault();
                buildingQuote.UpdatedById = userAccount.idUserAccount;
                buildingQuote.UserAccountIdUserAccount = userAccount.idUserAccount;
                buildingQuote.DateCreated = DateTime.Now;
                buildingQuote.Status = "Created";
                
                // Save to Database
                _context.Add(buildingQuote);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: "Index", controllerName: "BuildingFeatures", routeValues: new { idBuildingQoute = buildingQuote.IdBuildingQuote});
            }
            return View(buildingQuote);
        }

        // GET: BuildingQuotes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingQuote = await _context.BuildingQuote.FindAsync(id);
            if (buildingQuote.Status != "Created")
            {
                   return Content("You cannot edit the quote if its submitted! (Try again or contact the System Administrator) ");
            }

            if (buildingQuote == null)
            {
                return NotFound();
            }
            return View(buildingQuote);
        }

        // POST: BuildingQuotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBuildingQuote,UserAccountIdUserAccount,Description,MeasurementType,Height,Width,Depth,PurposeOfBuilding,BuildingSize,TotalCost,DateCreated,Status,TimeFrame,DateUpdated,UpdatedById")] BuildingQuote buildingQuote)
        {
            if (id != buildingQuote.IdBuildingQuote)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Added by Charles = setup data for Building Qoutes
                    UserAccountsController userAccountsController = new UserAccountsController(_context);
                    var userID = _userManager.GetUserId(User);
                    var userAccount = _context.UserAccount.Where(u => u.AspNetUserID == userID).FirstOrDefault();
                    buildingQuote.UpdatedById = userAccount.idUserAccount;
                    buildingQuote.DateUpdated = DateTime.Now;

                    _context.Update(buildingQuote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingQuoteExists(buildingQuote.IdBuildingQuote))
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
            return View(buildingQuote);
        }

        // GET: BuildingQuotes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingQuote = await _context.BuildingQuote
                .FirstOrDefaultAsync(m => m.IdBuildingQuote == id);
            if (buildingQuote == null)
            {
                return NotFound();
            }

            return View(buildingQuote);
        }

        // POST: BuildingQuotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildingQuote = await _context.BuildingQuote.FindAsync(id);
            _context.BuildingQuote.Remove(buildingQuote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildingQuoteExists(int id)
        {
            return _context.BuildingQuote.Any(e => e.IdBuildingQuote == id);
        }
    }
}
