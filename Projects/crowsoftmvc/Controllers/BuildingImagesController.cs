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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Hosting;

namespace crowsoftmvc.Controllers
{
    [Authorize]
    public class BuildingImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider fileProvider;
        private readonly IHostingEnvironment hostingEnvironment;

        public BuildingImagesController(ApplicationDbContext context, IFileProvider fileProvider, IHostingEnvironment env)
        {
            _context = context;
            this.fileProvider = fileProvider;
            hostingEnvironment = env;
        }

        // GET: BuildingImages
        public async Task<IActionResult> Index(int idBuildingQoute)
        {
            ViewData["BuildingQuoteId"] = idBuildingQoute;
            ViewData["MainPath"] = Directory.GetCurrentDirectory();
            return View(await _context.BuildingImage.Where(b => b.BuildingQuoteIdBuildingQuote == idBuildingQoute).ToListAsync());
        }

        // GET: BuildingImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingImage = await _context.BuildingImage
                .FirstOrDefaultAsync(m => m.IdBuildingImage == id);
            if (buildingImage == null)
            {
                return NotFound();
            }
            ViewData["BuildingQuoteId"] = buildingImage.BuildingQuoteIdBuildingQuote;
            return View(buildingImage);
        }

        // GET: BuildingImages/Create
        public IActionResult Create(int idBuildingQoute)
        {
            ViewData["BuildingQuoteIdBuildingQuote"] = new SelectList(_context.Set<BuildingQuote>(), "IdBuildingQuote", "Description");
            BuildingImage buildingImage = new BuildingImage();
            buildingImage.BuildingQuoteIdBuildingQuote = idBuildingQoute;
            ViewData["BuildingQuoteId"] = idBuildingQoute;
            return View(buildingImage);
        }

        // POST: BuildingImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBuildingImage,Description,ImagePath,BuildingQuoteIdBuildingQuote")] BuildingImage buildingImage, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");
                
                var path = await UploadFile(file, buildingImage.BuildingQuoteIdBuildingQuote);
                if (path != "")
                {
                    buildingImage.ImagePath = path;
                    _context.Add(buildingImage);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(actionName: "Index", controllerName: "BuildingImages", routeValues: new { idBuildingQoute = buildingImage.BuildingQuoteIdBuildingQuote });
                }
                else
                {
                    return NotFound();
                }
                
            }
            ViewData["BuildingQuoteId"] = buildingImage.BuildingQuoteIdBuildingQuote;
            return View(buildingImage);
        }

        // GET: BuildingImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingImage = await _context.BuildingImage.FindAsync(id);
            if (buildingImage == null)
            {
                return NotFound();
            }
            ViewData["BuildingQuoteIdBuildingQuote"] = new SelectList(_context.Set<BuildingQuote>(), "IdBuildingQuote", "Description");
            ViewData["BuildingQuoteId"] = buildingImage.BuildingQuoteIdBuildingQuote;
            return View(buildingImage);
        }

        // POST: BuildingImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBuildingImage,Description,ImagePath,BuildingQuoteIdBuildingQuote")] BuildingImage buildingImage, IFormFile file)
        {
            if (id != buildingImage.IdBuildingImage)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buildingImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingImageExists(buildingImage.IdBuildingImage))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(actionName: "Index", controllerName: "BuildingImages", routeValues: new { idBuildingQoute = buildingImage.BuildingQuoteIdBuildingQuote });
            }
            return View(buildingImage);
        }

        public async Task<string> UploadFile(IFormFile file,int QuoteId)
        {
            if (file == null || file.Length == 0)
                return "";

            FileInfo fi = new FileInfo(file.FileName);

            var newFilename = QuoteId + "_" + String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000) + fi.Extension;
            string webPath = hostingEnvironment.WebRootPath;
            var path = Path.Combine("" , webPath + @"\ImageFiles\" + newFilename);
            var pathToSave = @"/ImageFiles/" + newFilename;
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return pathToSave;
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            string webPath = hostingEnvironment.WebRootPath;
            var path = Path.Combine("" , webPath + @"\ImageFiles\" + filename);
            var pathToSave = @"/ImageFiles/" + filename;

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext].ToString();
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpg"},
                {".jpg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        // GET: BuildingImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingImage = await _context.BuildingImage
                .FirstOrDefaultAsync(m => m.IdBuildingImage == id);
            if (buildingImage == null)
            {
                return NotFound();
            }
            ViewData["BuildingQuoteId"] = buildingImage.BuildingQuoteIdBuildingQuote;
            return View(buildingImage);
        }

        // POST: BuildingImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildingImage = await _context.BuildingImage.FindAsync(id);
            _context.BuildingImage.Remove(buildingImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(actionName: "Index", controllerName: "BuildingImages", routeValues: new { idBuildingQoute = buildingImage.BuildingQuoteIdBuildingQuote });
        }

        private bool BuildingImageExists(int id)
        {
            return _context.BuildingImage.Any(e => e.IdBuildingImage == id);
        }
    }
}
