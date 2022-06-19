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
using Transport.Models.ViewModels;

namespace Transport.Controllers
{
    [Authorize]
    public class PlantasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlantasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Plantas
        public async Task<IActionResult> Index()
        {
            var viewModel = new PlantaDatosIndice();
            viewModel.Plantas = await _context.Plantas
                .Include(p => p.Departamento)
                .Include(p => p.ProductosAsignados)
                    .ThenInclude(p => p.Producto)
                .AsNoTracking()
                .OrderBy(i => i.Nombre)
                .ToListAsync();

            return View(viewModel);
        }

        // GET: Plantas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planta = await _context.Plantas
                .Include(p => p.Departamento)
                .FirstOrDefaultAsync(m => m.PlantaId == id);
            if (planta == null)
            {
                return NotFound();
            }

            return View(planta);
        }

        // GET: Plantas/Create
        public IActionResult Create()
        {
            var Planta = new Planta();
            Planta.ProductosAsignados = new List<ProductoAsignado>();
            LlenarDatosProductosAsignados(Planta);

            ViewData["DepartamentoID"] = new SelectList(_context.Departamentos, "DepartamentoId", "Nombre");
            return View();
        }

        private void LlenarDatosProductosAsignados(Planta planta)
        {
            var TodosProductos = _context.Productos;
            var PlantaProductos = new HashSet<int>(planta.ProductosAsignados.Select(c => c.ProductoID));
            var viewModel = new List<ProductosAsignadosData>();
            foreach (var producto in TodosProductos)
            {
                viewModel.Add(new ProductosAsignadosData
                {
                    ProductoID = producto.ProductoId,
                    Nombre = producto.Nombre,
                    Asignado = PlantaProductos.Contains(producto.ProductoId)
                });
            }
            ViewData["Productos"] = viewModel;
        }
        // POST: Plantas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlantaId,Nombre,Procesamiento,DepartamentoID")] Planta planta, string[] ProductoSeleccionado)
        {

            if (ProductoSeleccionado != null)
            {
                planta.ProductosAsignados = new List<ProductoAsignado>();
                foreach (var producto in ProductoSeleccionado)
                {
                    var ProductoParaAgregar = new ProductoAsignado
                    {
                        PlantaID = planta.PlantaId,
                        ProductoID = int.Parse(producto)
                    };
                    planta.ProductosAsignados.Add(ProductoParaAgregar);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(planta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoID"] = new SelectList(_context.Departamentos, "DepartamentoId", "Nombre", planta.DepartamentoID);
            return View(planta);
        }

        // GET: Plantas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planta = await _context.Plantas
                .Include(i => i.ProductosAsignados)
                    .ThenInclude(i => i.Producto)
                    .AsNoTracking()
                .FirstOrDefaultAsync(i => i.PlantaId == id);

            if (planta == null)
            {
                return NotFound();
            }

            LlenarDatosProductosAsignados(planta);

            ViewData["DepartamentoID"] = new SelectList(_context.Departamentos, "DepartamentoId", "Nombre", planta.DepartamentoID);
            return View(planta);
        }

        // POST: Plantas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("PlantaId,Nombre,Procesamiento,DepartamentoID")] Planta planta)
        public async Task<IActionResult> Edit(int? id, string[] ProductoSeleccionado)
        {
            var PlantaParaActualizar = await _context.Plantas
                .Include(i => i.ProductosAsignados)
                    .ThenInclude(i => i.Producto)
                .FirstOrDefaultAsync(s => s.PlantaId == id);

            if (await TryUpdateModelAsync<Planta>(
                PlantaParaActualizar,
                "",
                i => i.Nombre,
                i => i.Procesamiento,
                i => i.DepartamentoID,
                i => i.ProductosAsignados))
            {
                ActualizarPlantaProductos(ProductoSeleccionado, PlantaParaActualizar); /*ActualizarPlanta planta*/
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    // Log the error(uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "No se guardaron los cambios. " +
                        "Intente de neuvo, si el problema persiste, " +
                        "contacte a su administrador.");
                }
                return RedirectToAction(nameof(Index));
            }
            ActualizarPlantaProductos(ProductoSeleccionado, PlantaParaActualizar); /*ActualizarPlanta planta*/
            LlenarDatosProductosAsignados(PlantaParaActualizar);
            ViewData["DepartamentoID"] = new SelectList(_context.Departamentos, "DepartamentoId", "Nombre", PlantaParaActualizar.DepartamentoID);
            return View(PlantaParaActualizar);
        }

        private void ActualizarPlantaProductos(string[] productoSeleccionado, Planta plantaParaActualizar)
        {
            if (productoSeleccionado == null)
            {
                plantaParaActualizar.ProductosAsignados = new List<ProductoAsignado>();
                return;
            }

            var ProductoSeleccionadoHS = new HashSet<string>(productoSeleccionado);

            var PlantaProductos = new HashSet<int>(plantaParaActualizar.ProductosAsignados.Select(c => c.Producto.ProductoId));

            foreach (var producto in _context.Productos)
            {
                if (ProductoSeleccionadoHS.Contains(producto.ProductoId.ToString()))
                {
                    if (!PlantaProductos.Contains(producto.ProductoId))
                    {
                        plantaParaActualizar.ProductosAsignados.Add(new ProductoAsignado { PlantaID = plantaParaActualizar.PlantaId, ProductoID = producto.ProductoId });
                    }
                }
                else
                {

                    if (PlantaProductos.Contains(producto.ProductoId))
                    {
                        ProductoAsignado PlantaParaQuitar = plantaParaActualizar.ProductosAsignados.FirstOrDefault(i => i.ProductoID == producto.ProductoId);
                        _context.Remove(PlantaParaQuitar);
                    }
                }
            }
        }

        // GET: Plantas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planta = await _context.Plantas
                .Include(p => p.Departamento)
                .FirstOrDefaultAsync(m => m.PlantaId == id);
            if (planta == null)
            {
                return NotFound();
            }

            return View(planta);
        }

        // POST: Plantas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Planta plantaProductos = await _context.Plantas
                .Include(i=>i.ProductosAsignados)
                .SingleAsync(i => i.PlantaId == id);

            var planta = await _context.Plantas.FindAsync(id);
            _context.Plantas.Remove(planta);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantaExists(int id)
        {
            return _context.Plantas.Any(e => e.PlantaId == id);
        }
    }
}
