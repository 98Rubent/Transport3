using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transport.Data;
using Transport.Models.Tablas;

namespace Transport.Controllers
{
    public class TiposLugaresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TiposLugaresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TiposLugares
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposLugares.ToListAsync());
        }

        // GET: TiposLugares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoLugar = await _context.TiposLugares
                .FirstOrDefaultAsync(m => m.TipoLugarID == id);
            if (tipoLugar == null)
            {
                return NotFound();
            }

            return View(tipoLugar);
        }

        // GET: TiposLugares/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposLugares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoLugarID,Lugar")] TipoLugar tipoLugar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoLugar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoLugar);
        }

        // GET: TiposLugares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoLugar = await _context.TiposLugares.FindAsync(id);
            if (tipoLugar == null)
            {
                return NotFound();
            }
            return View(tipoLugar);
        }

        // POST: TiposLugares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoLugarID,Lugar")] TipoLugar tipoLugar)
        {
            if (id != tipoLugar.TipoLugarID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoLugar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoLugarExists(tipoLugar.TipoLugarID))
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
            return View(tipoLugar);
        }

        // GET: TiposLugares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoLugar = await _context.TiposLugares
                .FirstOrDefaultAsync(m => m.TipoLugarID == id);
            if (tipoLugar == null)
            {
                return NotFound();
            }

            return View(tipoLugar);
        }

        // POST: TiposLugares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoLugar = await _context.TiposLugares.FindAsync(id);
            _context.TiposLugares.Remove(tipoLugar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoLugarExists(int id)
        {
            return _context.TiposLugares.Any(e => e.TipoLugarID == id);
        }
    }
}
