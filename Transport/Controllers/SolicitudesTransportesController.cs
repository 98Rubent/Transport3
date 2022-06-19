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
    public class SolicitudesTransportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SolicitudesTransportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SolicitudesTransportes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SolicitudesTransportes.Include(s => s.Cliente).Include(s => s.Destino).Include(s => s.Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SolicitudesTransportes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudTransporte = await _context.SolicitudesTransportes
                .Include(s => s.Cliente)
                .Include(s => s.Destino)
                .Include(s => s.Producto)
                .FirstOrDefaultAsync(m => m.SolicitudTransporteID == id);
            if (solicitudTransporte == null)
            {
                return NotFound();
            }

            return View(solicitudTransporte);
        }

        // GET: SolicitudesTransportes/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteId", "Apellidos");
            ViewData["DestinoID"] = new SelectList(_context.Directorios, "DirectorioId", "Direccion");
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoId", "Descripcion");
            return View();
        }

        // POST: SolicitudesTransportes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SolicitudTransporteID,FechaSolicitud,FechaEntrega,Cantidad,UnidadMedida,Receptor,Contacto,Pagado,Total,DestinoID,ProductoID,ClienteID")] SolicitudTransporte solicitudTransporte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solicitudTransporte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteId", "Apellidos", solicitudTransporte.ClienteID);
            ViewData["DestinoID"] = new SelectList(_context.Directorios, "DirectorioId", "Direccion", solicitudTransporte.DestinoID);
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoId", "Descripcion", solicitudTransporte.ProductoID);
            return View(solicitudTransporte);
        }

        // GET: SolicitudesTransportes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudTransporte = await _context.SolicitudesTransportes.FindAsync(id);
            if (solicitudTransporte == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteId", "Apellidos", solicitudTransporte.ClienteID);
            ViewData["DestinoID"] = new SelectList(_context.Directorios, "DirectorioId", "Direccion", solicitudTransporte.DestinoID);
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoId", "Descripcion", solicitudTransporte.ProductoID);
            return View(solicitudTransporte);
        }

        // POST: SolicitudesTransportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SolicitudTransporteID,FechaSolicitud,FechaEntrega,Cantidad,UnidadMedida,Receptor,Contacto,Pagado,Total,DestinoID,ProductoID,ClienteID")] SolicitudTransporte solicitudTransporte)
        {
            if (id != solicitudTransporte.SolicitudTransporteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitudTransporte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudTransporteExists(solicitudTransporte.SolicitudTransporteID))
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
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteId", "Apellidos", solicitudTransporte.ClienteID);
            ViewData["DestinoID"] = new SelectList(_context.Directorios, "DirectorioId", "Direccion", solicitudTransporte.DestinoID);
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoId", "Descripcion", solicitudTransporte.ProductoID);
            return View(solicitudTransporte);
        }

        // GET: SolicitudesTransportes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudTransporte = await _context.SolicitudesTransportes
                .Include(s => s.Cliente)
                .Include(s => s.Destino)
                .Include(s => s.Producto)
                .FirstOrDefaultAsync(m => m.SolicitudTransporteID == id);
            if (solicitudTransporte == null)
            {
                return NotFound();
            }

            return View(solicitudTransporte);
        }

        // POST: SolicitudesTransportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitudTransporte = await _context.SolicitudesTransportes.FindAsync(id);
            _context.SolicitudesTransportes.Remove(solicitudTransporte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudTransporteExists(int id)
        {
            return _context.SolicitudesTransportes.Any(e => e.SolicitudTransporteID == id);
        }
    }
}
