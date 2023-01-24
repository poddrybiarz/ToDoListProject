using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projekt_v1._0.Models;
using projekt_v4._0.Areas.Identity.Data;

namespace projekt_v4._0.Controllers
{
    [Authorize]
    public class ModelZadaniasController : Controller
    {
        private readonly UserDbContext _context;

        public ModelZadaniasController(UserDbContext context)
        {
            _context = context;
        }

        // GET: ModelZadanias
        public async Task<IActionResult> Index(DateTime? data)
        {
            var userDbContext = _context.ModelZadania.Include(m => m.User);
            

            ViewBag.Data = data;
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = userId;

            if (data == null)
            {
                ViewBag.Data = DateTime.Today;
            }

            return View(await userDbContext.ToListAsync());
        }

        // GET: ModelZadanias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = userId;


            if (id == null || _context.ModelZadania == null)
            {
                return NotFound();
            }


            var modelZadania = await _context.ModelZadania
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ZadanieId == id);
            if (modelZadania == null)
            {
                return NotFound();
            }

            return View(modelZadania);
        }

        // GET: ModelZadanias/Create
        public IActionResult Create()
        {
            //ViewBag.Id = user.Id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = userId;

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ModelZadanias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZadanieId,Data,Start,End,Hour,Name,Description,Done,UserId")] ModelZadania modelZadania)
        {
           
            if (ModelState.IsValid)
            {
                 _context.Add(modelZadania);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
                
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", modelZadania.UserId);
            return View(modelZadania);
        }

        // GET: ModelZadanias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.ModelZadania == null)
            {
                return NotFound();
            }

            var modelZadania = await _context.ModelZadania.FindAsync(id);
            if (modelZadania == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = userId;

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", modelZadania.UserId);
            return View(modelZadania);
        }

        // POST: ModelZadanias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZadanieId,Data,Start,End,Hour,Name,Description,Done,UserId")] ModelZadania modelZadania)
        {
            if (id != modelZadania.ZadanieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelZadania);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelZadaniaExists(modelZadania.ZadanieId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", modelZadania.UserId);
            return View(modelZadania);
        }

        // GET: ModelZadanias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = userId;

            if (id == null || _context.ModelZadania == null)
            {
                return NotFound();
            }

            var modelZadania = await _context.ModelZadania
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ZadanieId == id);
            if (modelZadania == null)
            {
                return NotFound();
            }

            return View(modelZadania);
        }

        // POST: ModelZadanias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ModelZadania == null)
            {
                return Problem("Entity set 'UserDbContext.ModelZadania'  is null.");
            }
            var modelZadania = await _context.ModelZadania.FindAsync(id);
            if (modelZadania != null)
            {
                _context.ModelZadania.Remove(modelZadania);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Done(int id)
        {
           // ModelZadania zadanie = zadania.FirstOrDefault(x => x.ZadanieId == id);
           

            if (_context.ModelZadania == null)
            {
                return Problem("Entity set 'UserDbContext.ModelZadania'  is null.");
            }
            var modelZadania = await _context.ModelZadania.FindAsync(id);
            if (modelZadania != null)
            {
                if (modelZadania.Done == true) modelZadania.Done = false;
                else modelZadania.Done = true;
                _context.Update(modelZadania);
            }

            await _context.SaveChangesAsync();

           
            return base.RedirectToAction(nameof (Index));

        }


        private bool ModelZadaniaExists(int id)
        {
          return _context.ModelZadania.Any(e => e.ZadanieId == id);
        }
    }
}
