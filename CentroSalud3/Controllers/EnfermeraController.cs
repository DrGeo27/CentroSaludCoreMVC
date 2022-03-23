using CentroSalud3.DbContext;
using CentroSalud3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Controllers
{
    [Authorize(Roles = "Enfermera,Administrador")]
    public class EnfermeraController : Controller
    {
        #region ConexionBD
        private readonly AmbulatorioDbContext _context;

        public EnfermeraController(AmbulatorioDbContext context)
        {
            _context = context;
        }
        #endregion ConexionBD

        #region Index
        public async Task<IActionResult> Index()
        {
            var enfermeras = await _context.Enfermeras.ToListAsync();
            return View(enfermeras);
        }
        #endregion Index

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermera = await _context.Enfermeras.FirstOrDefaultAsync(m => m.EnfermeraId == id);

            if (enfermera == null)
            {
                return NotFound();
            }

            return View(enfermera);
        }
        #endregion Details

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnfermeraId,EnfermeraNombre,EnfermeraConsulta,EnfermeraTelefono,EnfermeraEmail,EnfermeraNumPacientes")] Enfermera enfermera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enfermera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enfermera);
        }
        #endregion Create

        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermera = await _context.Enfermeras.FindAsync(id);

            if (enfermera == null)
            {
                return NotFound();
            }

            return View(enfermera);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnfermeraId,EnfermeraNombre,EnfermeraConsulta,EnfermeraTelefono,EnfermeraEmail,NumPacientes")] Enfermera enfermera)
        {
            if (id != enfermera.EnfermeraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermeraExists(enfermera.EnfermeraId))
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
            return View(enfermera);
        }

        private bool EnfermeraExists(int id)
        {
            return _context.Enfermeras.Any(e => e.EnfermeraId == id);
        }
        #endregion Edit

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermera = await _context.Enfermeras.FirstOrDefaultAsync(m => m.EnfermeraId == id);

            if (enfermera == null)
            {
                return NotFound();
            }

            return View(enfermera);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enfermera = await _context.Enfermeras.FindAsync(id);
            _context.Enfermeras.Remove(enfermera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion Delete
    }
}
