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
    public class FamiliasController : Controller
    {
        private readonly DbproductosContext _context;
        private readonly IConfiguration _configuration;

        public FamiliasController(DbproductosContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Familias
        public async Task<IActionResult> Index()
        {
              return View(await _context.Familia.ToListAsync());
        }

        // GET: Familias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Familia == null)
            {
                return NotFound();
            }

            var familia = await _context.Familia
                .FirstOrDefaultAsync(m => m.IdFamilia == id);
            if (familia == null)
            {
                return NotFound();
            }

            return View(familia);
        }

        // GET: Familias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Familias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFamilia,Descripcion,FechaModificacion,Baja,FechaBaja")] Familia familia)
        {
            bool registrado;
            string mensaje;

            if (ModelState.IsValid)
            {
                using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("conexion")))
                {
                    SqlCommand cmd = new SqlCommand("sp_AltaFamilia", cn);
                    cmd.Parameters.AddWithValue("Descripcion", familia.Descripcion);
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
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(familia);
        }

        // GET: Familias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Familia == null)
            {
                return NotFound();
            }

            var familia = await _context.Familia.FindAsync(id);
            if (familia == null)
            {
                return NotFound();
            }
            return View(familia);
        }

        // POST: Familias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFamilia,Descripcion,FechaModificacion,Baja,FechaBaja")] Familia familia)
        {
            bool modificado;
            string mensaje;

            if (id != familia.IdFamilia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("conexion")))
                    {
                        SqlCommand cmd = new SqlCommand("sp_ModificacionFamilia", cn);
                        cmd.Parameters.AddWithValue("IdFamilia", familia.IdFamilia);
                        cmd.Parameters.AddWithValue("Descripcion", familia.Descripcion);
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
                    if (!FamiliaExists(familia.IdFamilia))
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
            return View(familia);
        }

        // GET: Familias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Familia == null)
            {
                return NotFound();
            }

            var familia = await _context.Familia
                .FirstOrDefaultAsync(m => m.IdFamilia == id);
            if (familia == null)
            {
                return NotFound();
            }

            return View(familia);
        }

        // POST: Familias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool baja;
            string mensaje;

            if (_context.Familia == null)
            {
                return Problem("Entity set 'DbproductosContext.Familia'  is null.");
            }
            var familia = await _context.Familia.FindAsync(id);
            if (familia != null)
            {
                using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("conexion")))
                {
                    SqlCommand cmd = new SqlCommand("sp_BajaFamilia", cn);
                    cmd.Parameters.AddWithValue("IdFamilia", familia.IdFamilia);
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

        private bool FamiliaExists(int id)
        {
          return _context.Familia.Any(e => e.IdFamilia == id);
        }
    }
}
