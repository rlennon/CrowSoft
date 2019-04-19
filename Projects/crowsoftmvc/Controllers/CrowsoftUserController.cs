using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using crowsoftmvc.Areas.Identity.Data;
using crowsoftmvc.Data;
using Microsoft.AspNetCore.Authorization;

namespace crowsoftmvc.Controllers
{
    [Authorize(Policy = "RequireAdminOnly")]
    public class CrowsoftUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CrowsoftUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CrowsoftUser
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: CrowsoftUser/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crowsoftUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crowsoftUser == null)
            {
                return NotFound();
            }

            return View(crowsoftUser);
        }

        // GET: CrowsoftUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CrowsoftUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] CrowsoftUser crowsoftUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crowsoftUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crowsoftUser);
        }

        // GET: CrowsoftUser/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crowsoftUser = await _context.Users.FindAsync(id);
            if (crowsoftUser == null)
            {
                return NotFound();
            }
            return View(crowsoftUser);
        }

        // POST: CrowsoftUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] CrowsoftUser crowsoftUser)
        {
            if (id != crowsoftUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crowsoftUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrowsoftUserExists(crowsoftUser.Id))
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
            return View(crowsoftUser);
        }

        // GET: CrowsoftUser/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crowsoftUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crowsoftUser == null)
            {
                return NotFound();
            }

            return View(crowsoftUser);
        }

        // POST: CrowsoftUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var crowsoftUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(crowsoftUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrowsoftUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
