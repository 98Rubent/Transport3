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
    public class TiposVehiculosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TiposVehiculosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TiposVehiculos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposVehiculos.ToListAsync());
        }

        // GET: TiposVehiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVehiculo = await _context.TiposVehiculos
                .FirstOrDefaultAsync(m => m.TipoVehiculoID == id);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }

            return View(tipoVehiculo);
        }

        // GET: TiposVehiculos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposVehiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoVehiculoID,Nombre,Capacidad,UnidadMedida")] TipoVehiculo tipoVehiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoVehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoVehiculo);
        }

        // GET: TiposVehiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVehiculo = await _context.TiposVehiculos.FindAsync(id);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }
            return View(tipoVehiculo);
        }

        // POST: TiposVehiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoVehiculoID,Nombre,Capacidad,UnidadMedida")] TipoVehiculo tipoVehiculo)
        {
            if (id != tipoVehiculo.TipoVehiculoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoVehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoVehiculoExists(tipoVehiculo.TipoVehiculoID))
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
            return View(tipoVehiculo);
        }

        // GET: TiposVehiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVehiculo = await _context.TiposVehiculos
                .FirstOrDefaultAsync(m => m.TipoVehiculoID == id);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }

            return View(tipoVehiculo);
        }

        // POST: TiposVehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoVehiculo = await _context.TiposVehiculos.FindAsync(id);
            _context.TiposVehiculos.Remove(tipoVehiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoVehiculoExists(int id)
        {
            return _context.TiposVehiculos.Any(e => e.TipoVehiculoID == id);
        }
    }
}
