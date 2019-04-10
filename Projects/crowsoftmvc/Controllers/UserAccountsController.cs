using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using crowsoftmvc.Data;
using crowsoftmvc.Models;
using Microsoft.AspNetCore.Routing;

namespace crowsoftmvc.Controllers
{
    public class UserAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Counties myCounties = new Counties();

        public UserAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserAccount.ToListAsync());
        }

        // GET: UserAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount
                .FirstOrDefaultAsync(m => m.idUserAccount == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // GET: UserAccounts/Create
        public IActionResult Create()
        {
            UserAccount user_account = new UserAccount();
            return View(user_account);
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idUserAccount,EmailAddress,Password,FirstName,LastName,TelephoneNo,AddressLine,County,Country,EirCode,CompanyName,TypeUser,DateCreated")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                // Calling the Security Helper Class to encode the string to bytearray before saving to database
                userAccount.Password = Helpers.SecurityHelper.ToByteArrayToString(userAccount.Password).ToString();
                _context.Add(userAccount);
                await _context.SaveChangesAsync();
                return Redirect("/Home");
            }
            return View(userAccount);
        }

        public IActionResult Login()
        {
            UserAccount user_account = new UserAccount();
            return View(user_account);
        }

        // GET: UserAccounts/Edit/5
        public async Task<IActionResult> Login(string EmailAddress, string Password)
        {
            if (EmailAddress == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount.FindAsync(EmailAddress);
            if (userAccount == null)
            {
                return NotFound();
            }
            return View(userAccount);
        }

        // GET: UserAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }
            return View(userAccount);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idUserAccount,EmailAddress,Password,FirstName,LastName,TelephoneNo,AddressLine,County,Country,EirCode,CompanyName,TypeUser,DateCreated")] UserAccount userAccount)
        {
            if (id != userAccount.idUserAccount)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccountExists(userAccount.idUserAccount))
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
            return View(userAccount);
        }

        // GET: UserAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount
                .FirstOrDefaultAsync(m => m.idUserAccount == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccount = await _context.UserAccount.FindAsync(id);
            _context.UserAccount.Remove(userAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountExists(int id)
        {
            return _context.UserAccount.Any(e => e.idUserAccount == id);
        }
    }
}
