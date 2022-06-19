using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transport.Data;
using Transport.Models.Tablas;

namespace Transport.Controllers
{
    [Authorize]
    public class DirectorioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DirectorioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Directorio
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Directorios.Include(d => d.TipoLugar);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Directorio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorio = await _context.Directorios
                .Include(d => d.TipoLugar)
                .FirstOrDefaultAsync(m => m.DirectorioId == id);
            if (directorio == null)
            {
                return NotFound();
            }

            return View(directorio);
        }

        // GET: Directorio/Create
        public IActionResult Create()
        {
            ViewData["TipoLugarID"] = new SelectList(_context.TiposLugares, "TipoLugarID", "Lugar");
            return View();
        }

        // POST: Directorio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DirectorioId,Nombre,Direccion,Indicaciones,Contacto,Correo,TipoLugarID")] Directorio directorio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(directorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoLugarID"] = new SelectList(_context.TiposLugares, "TipoLugarID", "Lugar", directorio.TipoLugarID);
            return View(directorio);
        }

        // GET: Directorio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorio = await _context.Directorios.FindAsync(id);
            if (directorio == null)
            {
                return NotFound();
            }
            ViewData["TipoLugarID"] = new SelectList(_context.TiposLugares, "TipoLugarID", "Lugar", directorio.TipoLugarID);
            return View(directorio);
        }

        // POST: Directorio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DirectorioId,Nombre,Direccion,Indicaciones,Contacto,Correo,TipoLugarID")] Directorio directorio)
        {
            if (id != directorio.DirectorioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(directorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorioExists(directorio.DirectorioId))
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
            ViewData["TipoLugarID"] = new SelectList(_context.TiposLugares, "TipoLugarID", "Lugar", directorio.TipoLugarID);
            return View(directorio);
        }

        // GET: Directorio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorio = await _context.Directorios
                .Include(d => d.TipoLugar)
                .FirstOrDefaultAsync(m => m.DirectorioId == id);
            if (directorio == null)
            {
                return NotFound();
            }

            return View(directorio);
        }

        // POST: Directorio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var directorio = await _context.Directorios.FindAsync(id);
            _context.Directorios.Remove(directorio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectorioExists(int id)
        {
            return _context.Directorios.Any(e => e.DirectorioId == id);
        }
    }
}
