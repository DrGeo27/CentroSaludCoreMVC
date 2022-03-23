using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CentroSalud3.DbContext;
using CentroSalud3.Models;
using Microsoft.AspNetCore.Authorization;

namespace CentroSalud3.Controllers
{
    [Authorize(Roles = "Médico,Administrador")]
    public class MedicacionController : Controller
    {
        #region ConexionBD
        private readonly AmbulatorioDbContext _context;

        public MedicacionController(AmbulatorioDbContext context)
        {
            _context = context;
        }
        #endregion ConexionBD

        #region Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medicaciones.ToListAsync());
        }
        #endregion Index

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicacion = await _context.Medicaciones
                .FirstOrDefaultAsync(m => m.MedicacionId == id);
            if (medicacion == null)
            {
                return NotFound();
            }

            return View(medicacion);
        }
        #endregion Details

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicacionId,MedicacionNombre,MedicacionDosis,MedicacionGrupo,MedicacionDescripcion,NumPacientesPautados")] Medicacion medicacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicacion);
        }
        #endregion Create

        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicacion = await _context.Medicaciones.FindAsync(id);
            if (medicacion == null)
            {
                return NotFound();
            }
            return View(medicacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicacionId,MedicacionNombre,MedicacionDosis,MedicacionGrupo,MedicacionDescripcion,NumPacientesPautados")] Medicacion medicacion)
        {
            if (id != medicacion.MedicacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicacionExists(medicacion.MedicacionId))
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
            return View(medicacion);
        }

        private bool MedicacionExists(int id)
        {
            return _context.Medicaciones.Any(e => e.MedicacionId == id);
        }
        #endregion Edit

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicacion = await _context.Medicaciones
                .FirstOrDefaultAsync(m => m.MedicacionId == id);
            if (medicacion == null)
            {
                return NotFound();
            }

            return View(medicacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicacion = await _context.Medicaciones.FindAsync(id);
            _context.Medicaciones.Remove(medicacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion Delete
    }
}
