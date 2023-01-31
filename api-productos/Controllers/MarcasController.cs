using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using api_productos.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace api_productos.Controllers
{
    public class MarcasController : Controller
    {
        private readonly DbproductosContext _context;
        private readonly IConfiguration _configuration;

        public MarcasController(DbproductosContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Marcas
        public async Task<IActionResult> Index()
        {
              return View(await _context.Marcas.ToListAsync());
        }

        // GET: Marcas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.IdMarca == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // GET: Marcas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marcas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMarca,Descripcion,FechaModificacion,Baja,FechaBaja")] Marca marca)
        {
            bool registrado;
            string mensaje;

            if (ModelState.IsValid)
            {
                using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("conexion")))
                {
                    SqlCommand cmd = new SqlCommand("sp_AltaMarca", cn);
                    cmd.Parameters.AddWithValue("Descripcion", marca.Descripcion);
                    cmd.Parameters.Add("Registrado", System.Data.SqlDbType.Bit).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", System.Data.SqlDbType.VarChar,100).Direction = System.Data.ParameterDirection.Output;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
                ViewData["Mensaje"] = mensaje;
                if (registrado)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(marca);
        }

        // GET: Marcas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        // POST: Marcas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMarca,Descripcion,FechaModificacion,Baja,FechaBaja")] Marca marca)
        {
            bool modificado;
            string mensaje;

            if (id != marca.IdMarca)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("conexion")))
                    {
                        SqlCommand cmd = new SqlCommand("sp_ModificacionMarca", cn);
                        cmd.Parameters.AddWithValue("IdMarca", marca.IdMarca);
                        cmd.Parameters.AddWithValue("Descripcion", marca.Descripcion);
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
                    if (!MarcaExists(marca.IdMarca))
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
            return View(marca);
        }

        // GET: Marcas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.IdMarca == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool baja;
            string mensaje;

            if (_context.Marcas == null)
            {
                return Problem("Entity set 'DbproductosContext.Marcas'  is null.");
            }
            var marca = await _context.Marcas.FindAsync(id);
            if (marca != null)
            {
                using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("conexion")))
                {
                    SqlCommand cmd = new SqlCommand("sp_BajaMarca", cn);
                    cmd.Parameters.AddWithValue("IdMarca", marca.IdMarca);
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

        private bool MarcaExists(int id)
        {
          return _context.Marcas.Any(e => e.IdMarca == id);
        }
    }
}
