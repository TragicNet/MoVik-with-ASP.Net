using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoVik.Data;
using MoVik.Models;

namespace MoVik.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly MoVikDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MenuItemsController(MoVikDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: MenuItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.MenuItem.ToListAsync());
        }

        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItem
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // GET: MenuItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,Name,ImageFile")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                String wwwRoot = _hostEnvironment.WebRootPath;
                String fileName = Path.GetFileNameWithoutExtension(menuItem.ImageFile.FileName);
                String extension = Path.GetExtension(menuItem.ImageFile.FileName);

                fileName += DateTime.Now.ToString("yymmssfff") + extension;

                String path = Path.Combine(wwwRoot + "/img/", fileName);
                menuItem.ImagePath = fileName;

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await menuItem.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItem.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuId,Name,ImagePath,ImageFile")] MenuItem menuItem)
        {
            if (id != menuItem.MenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img", menuItem.ImagePath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                String wwwRoot = _hostEnvironment.WebRootPath;
                String fileName = Path.GetFileNameWithoutExtension(menuItem.ImageFile.FileName);
                String extension = Path.GetExtension(menuItem.ImageFile.FileName);

                fileName += DateTime.Now.ToString("yymmssfff") + extension;

                String path = Path.Combine(wwwRoot + "/img/", fileName);
                menuItem.ImagePath = fileName;

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await menuItem.ImageFile.CopyToAsync(fileStream);
                }

                try
                {
                    _context.Update(menuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.MenuId))
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
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItem
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItem = await _context.MenuItem.FindAsync(id);

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img", menuItem.ImagePath);
            if(System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.MenuItem.Remove(menuItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItem.Any(e => e.MenuId == id);
        }
    }
}
