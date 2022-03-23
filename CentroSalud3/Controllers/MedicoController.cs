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
    public class MedicoController : Controller
    {
        #region ConexionBD
        private readonly AmbulatorioDbContext _context;

        public MedicoController(AmbulatorioDbContext context)
        {
            _context = context;
        }
        #endregion ConexionBD

        #region Index
        public async Task<IActionResult> Index()
        {
            var medicos = await _context.Medicos.ToListAsync();
            return View(medicos);
        }
        #endregion Index

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FirstOrDefaultAsync(m => m.MedicoId == id);

            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }
        #endregion Details

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicoId,MedicoNombre,MedicoConsulta,MedicoTelefono,MedicoEmail,NumPacientes")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }
        #endregion Create

        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FindAsync(id);

            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicoId,MedicoNombre,MedicoConsulta,MedicoTelefono,MedicoEmail,NumPacientes")] Medico medico)
        {
            if (id != medico.MedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.MedicoId))
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
            return View(medico);
        }

        private bool MedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.MedicoId == id);
        }
        #endregion Edit

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FirstOrDefaultAsync(m => m.MedicoId == id);

            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion Delete
    }
}
