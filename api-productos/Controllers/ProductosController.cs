using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using api_productos.Models;
using Microsoft.Data.SqlClient;

namespace api_productos.Controllers
{
    public class ProductosController : Controller
    {
        private readonly DbproductosContext _context;
        private readonly IConfiguration _configuration;

        public ProductosController(DbproductosContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Productos
        public async Task<IActionResult> Index(string? codigoProducto, int? idMarca, int? idFamilia, bool mostrarBajas)
        {
            var productos = from producto in _context.Productos select producto;

            if (!String.IsNullOrEmpty(codigoProducto))
            {
                productos = productos.Where(producto => producto.CodigoProducto!.Contains(codigoProducto));
            }

            if (idMarca != null || idMarca == 0)
            {
                productos = productos.Where(producto => producto.IdMarca == idMarca);
            }

            if (idFamilia != null || idFamilia == 0)
            {
                productos = productos.Where(producto => producto.IdFamilia == idFamilia);
            }

            productos = productos.OrderByDescending(producto => producto.FechaModificacion);

            if (!mostrarBajas)
            {
                productos = productos.Where(producto => producto.Baja == false);
            }

            return View(await productos.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdFamiliaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.CodigoProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["IdFamilia"] = new SelectList(_context.Familia, "IdFamilia", "IdFamilia");
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "IdMarca");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoProducto,DescripcionProducto,PrecioCosto,PrecioVenta,IdMarca,IdFamilia,FechaModificacion,Baja,FechaBaja")] Producto producto)
        {
            bool registrado;
            string mensaje;

           
                using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("conexion")))
                {
                    SqlCommand cmd = new SqlCommand("sp_AltaProducto", cn);
                    cmd.Parameters.AddWithValue("CodigoProducto", producto.CodigoProducto);
                    cmd.Parameters.AddWithValue("DescripcionProducto", producto.DescripcionProducto);
                    cmd.Parameters.AddWithValue("PrecioCosto", producto.PrecioCosto);
                    cmd.Parameters.AddWithValue("PrecioVenta", producto.PrecioVenta);
                    cmd.Parameters.AddWithValue("IdMarca", producto.IdMarca);
                    cmd.Parameters.AddWithValue("IdFamilia", producto.IdFamilia);
                    cmd.Parameters.Add("Registrado", System.Data.SqlDbType.Bit).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", System.Data.SqlDbType.VarChar, 100).Direction = System.Data.ParameterDirection.Output;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
                ViewData["Mensaje"] = mensaje;
                if (registrado)
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            
            ViewData["IdFamilia"] = new SelectList(_context.Familia, "IdFamilia", "IdFamilia", producto.IdFamilia);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "IdMarca", producto.IdMarca);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdFamilia"] = new SelectList(_context.Familia, "IdFamilia", "IdFamilia", producto.IdFamilia);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "IdMarca", producto.IdMarca);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodigoProducto,DescripcionProducto,PrecioCosto,PrecioVenta,IdMarca,IdFamilia,FechaModificacion,Baja,FechaBaja")] Producto producto)
        {
            bool modificado;
            string mensaje;

            if (id != producto.CodigoProducto)
            {
                return NotFound();
            }

           
                try
                {
                    using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("conexion")))
                    {
                        SqlCommand cmd = new SqlCommand("sp_ModificacionProducto", cn);
                        cmd.Parameters.AddWithValue("CodigoProducto", producto.CodigoProducto);
                        cmd.Parameters.AddWithValue("DescripcionProducto", producto.DescripcionProducto);
                        cmd.Parameters.AddWithValue("PrecioCosto", producto.PrecioCosto);
                        cmd.Parameters.AddWithValue("PrecioVenta", producto.PrecioVenta);
                        cmd.Parameters.AddWithValue("IdMarca", producto.IdMarca);
                        cmd.Parameters.AddWithValue("IdFamilia", producto.IdFamilia);
                        cmd.Parameters.Add("Modificado", System.Data.SqlDbType.Bit).Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add("Mensaje", System.Data.SqlDbType.VarChar, 100).Direction = System.Data.ParameterDirection.Output;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cn.Open();
                        cmd.ExecuteNonQuery();

                        modificado = Convert.ToBoolean(cmd.Parameters["Modificado"].Value);
                        mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    }
                    ViewData["Mensaje"] = mensaje;
                    if (modificado)
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.CodigoProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["IdFamilia"] = new SelectList(_context.Familia, "IdFamilia", "IdFamilia", producto.IdFamilia);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "IdMarca", producto.IdMarca);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdFamiliaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.CodigoProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            bool baja;
            string mensaje;

            if (_context.Productos == null)
            {
                return Problem("Entity set 'DbproductosContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("conexion")))
                {
                    SqlCommand cmd = new SqlCommand("sp_BajaProducto", cn);
                    cmd.Parameters.AddWithValue("CodigoProducto", producto.CodigoProducto);
                    cmd.Parameters.Add("Baja", System.Data.SqlDbType.Bit).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", System.Data.SqlDbType.VarChar, 100).Direction = System.Data.ParameterDirection.Output;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    baja = Convert.ToBoolean(cmd.Parameters["Baja"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
                ViewData["Mensaje"] = mensaje;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(string id)
        {
          return _context.Productos.Any(e => e.CodigoProducto == id);
        }
    }
}
