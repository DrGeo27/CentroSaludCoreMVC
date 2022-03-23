using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CentroSalud3.DbContext;
using CentroSalud3.Models;
using CentroSalud3.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CentroSalud3.Controllers
{
    [Authorize(Roles = "Paciente,Administrador")]
    public class PacienteController : Controller
    {
        #region ConexionBD
        private readonly AmbulatorioDbContext _context;

        public PacienteController(AmbulatorioDbContext context)
        {
            _context = context;
        }
        #endregion ConexionDB

        #region Index
        public async Task<IActionResult> Index()
        {
            var pacientes = await _context.Pacientes.ToListAsync();
            return View(pacientes);
        }
        #endregion Index

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.PacienteId == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }
        #endregion Details

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacienteId,PacienteNombre,PacienteFxNacimiento,PacienteEdad,PacienteSexo,PacienteTelefono,PacienteEmail")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        /*public int CalcularEdad(int? id)
        {
            var pacienteSelect = _context.Pacientes.Where(p => p.PacienteId == id).FirstOrDefault();

            Paciente paciente = new Paciente();

            var hoy = DateTime.Now.Date.Year;
            var edad = hoy - paciente.PacienteFxNacimiento.Date.Year;

            _context.Add(paciente);
            _context.SaveChanges();

            return paciente.PacienteEdad;

            //ViewBag.Paciente = paciente;
        }*/
        #endregion Create

        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PacienteId,PacienteNombre,PacienteFxNacimiento,PacienteEdad,PacienteSexo,PacienteTelefono,PacienteEmail")] Paciente paciente)
        {
            if (id != paciente.PacienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.PacienteId))
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
            return View(paciente);
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.PacienteId == id);
        }
        #endregion Edit

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.PacienteId == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion Delete        

        #region PacienteAsignacionMedico
        //Asignación de un paciente a un médico
        public async Task<IActionResult> PacienteAsignacionMedico(int? id)
        {
            var pacienteDisplay = await _context.Pacientes.Select(p => new
            {
                Id = p.PacienteId,
                Value = p.PacienteNombre
            }).ToListAsync();

            PacienteAsignacionMedicoViewModel vm = new PacienteAsignacionMedicoViewModel();

            vm.PacientesLista = new SelectList(pacienteDisplay, "Id", "Value");

            var medico = await _context.Medicos.SingleOrDefaultAsync(m => m.MedicoId == id);
            //?? var medicoMin = await _context.Medicos.Where(m => m.MedicoId == id).MinAsync(m => m.NumPacientes);

            ViewBag.Medico = medico;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> PacienteAsignacionMedico(PacienteAsignacionMedicoViewModel vm, int? id)
        {
            if (CompruebaPacienteConMedico(vm.PacienteConMedico.Medico.MedicoId, vm.PacienteConMedico.Paciente.PacienteId))
            {
                return RedirectToAction("InformacionMedico");
            }
            else
            {
                var pcm = await _context.PacientesConMedicos.Where(p => p.PacienteConMedicoId == id).FirstOrDefaultAsync();

                var paciente = await _context.Pacientes.SingleOrDefaultAsync(p => p.PacienteId == vm.PacienteConMedico.Paciente.PacienteId);
                var medico = await _context.Medicos.SingleOrDefaultAsync(m => m.MedicoId == vm.PacienteConMedico.Medico.MedicoId);
                medico.NumPacientes++;

                PacienteConMedico pacienteConMedico = new PacienteConMedico();
                pacienteConMedico.Medico = medico;
                pacienteConMedico.Paciente = paciente;

                _context.Add(pacienteConMedico);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Medico");
            }
        }        

        private bool CompruebaPacienteConMedico(int medicoid, int pacienteid)
        {
            bool pacienteEncontrado;
            var asignado = _context.PacientesConMedicos
                .Where(p => p.Medico.MedicoId != 0 && p.Paciente.PacienteId == pacienteid)
                .FirstOrDefault();

            pacienteEncontrado = (asignado != null);

            return pacienteEncontrado;
        }

        public IActionResult InformacionMedico()
        {
            return View();
        }

        public async Task<IActionResult> TodosPacientesConMedico(int? id)
        {
            var asignadosMedico = await _context.PacientesConMedicos
                .Where(p => p.Medico.MedicoId == id)
                .Include(p => p.Paciente)
                .Include(p => p.Medico).ToListAsync();

            List<Paciente> pacientesLista = new List<Paciente>();

            foreach (var paciente in asignadosMedico)
            {
                var pacientes = await _context.Pacientes.SingleOrDefaultAsync(p => p.PacienteId == paciente.Paciente.PacienteId);

                pacientesLista.Add(pacientes);
            }

            ViewData["medico"] = _context.Medicos.Find(id).MedicoNombre;

            return View(pacientesLista);
        }

        public async Task<IActionResult> DeleteAsignacion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var pcm = await _context.PacientesConMedicos.FirstOrDefaultAsync(p => p.PacienteConMedicoId == id);
            var pcm = await _context.PacientesConMedicos.Where(p => p.PacienteConMedicoId == id)
                .Include(p => p.Paciente)
                .Include(p => p.Medico)
                .FirstOrDefaultAsync();

            if (pcm == null)
            {
                return NotFound();
            }

            return View(pcm);
        }

        [HttpPost, ActionName("DeleteAsignacion")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsignacionConfirmed(PacienteAsignacionMedicoViewModel vm, int id)
        {
            //var pcm = await _context.PacientesConMedicos.FindAsync(id);
            var pcm = await _context.PacientesConMedicos.Where(p => p.PacienteConMedicoId == id).FirstOrDefaultAsync();

            //No consigo restar una unidad al número de pacientes cuando se da de baja a uno de ellos
            //var medico = await _context.PacientesConMedicos.Where(m => m.Medico.MedicoId == vm.PacienteConMedico.Medico.MedicoId);
            //medico.Medico.NumPacientes--;

            _context.PacientesConMedicos.Remove(pcm);            
            await _context.SaveChangesAsync();

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Medico");
        }
        #endregion PacienteAsignacionMedico

        #region PacienteAsignacionEnfermera
        //Asignación de un paciente a una enfermera
        public async Task<IActionResult> PacienteAsignacionEnfermera(int? id)
        {
            var pacienteDisplay = await _context.Pacientes.Select(p => new
            {
                Id = p.PacienteId,
                Value = p.PacienteNombre
            }).ToListAsync();

            PacienteAsignacionEnfermeraViewModel vm = new PacienteAsignacionEnfermeraViewModel();

            vm.PacientesLista = new SelectList(pacienteDisplay, "Id", "Value");

            var enfermera = await _context.Enfermeras.SingleOrDefaultAsync(e => e.EnfermeraId == id);

            ViewBag.Enfermera = enfermera;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> PacienteAsignacionEnfermera(PacienteAsignacionEnfermeraViewModel vm, int? id)
        {
            if (CompruebaPacienteConEnfermera(vm.PacienteConEnfermera.Enfermera.EnfermeraId, vm.PacienteConEnfermera.Paciente.PacienteId))
            {
                return RedirectToAction("InformacionEnfermera");
            }
            else
            {
                var pce = await _context.PacientesConEnfermeras.Where(p => p.PacienteConEnfermeraId == id).FirstOrDefaultAsync();

                var paciente = await _context.Pacientes.SingleOrDefaultAsync(p => p.PacienteId == vm.PacienteConEnfermera.Paciente.PacienteId);
                var enfermera = await _context.Enfermeras.SingleOrDefaultAsync(e => e.EnfermeraId == vm.PacienteConEnfermera.Enfermera.EnfermeraId);
                enfermera.EnfermeraNumPacientes++;

                PacienteConEnfermera pacienteConEnfermera = new PacienteConEnfermera();
                pacienteConEnfermera.Enfermera = enfermera;
                pacienteConEnfermera.Paciente = paciente;

                _context.Add(pacienteConEnfermera);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Enfermera");
            }
        }

        private bool CompruebaPacienteConEnfermera(int enfermeraid, int pacienteid)
        {
            bool pacienteEncontrado;
            var asignado = _context.PacientesConEnfermeras
                .Where(p => p.Enfermera.EnfermeraId != 0 && p.Paciente.PacienteId == pacienteid)
                .FirstOrDefault();

            pacienteEncontrado = (asignado != null);

            return pacienteEncontrado;
        }

        public IActionResult InformacionEnfermera()
        {
            return View();
        }

        public async Task<IActionResult> TodosPacientesConEnfermera(int? id)
        {
            var asignadosEnfermera = await _context.PacientesConEnfermeras
                .Where(p => p.Enfermera.EnfermeraId == id)
                .Include(p => p.Paciente)
                .Include(p => p.Enfermera).ToListAsync();

            List<Paciente> pacientesLista = new List<Paciente>();

            foreach (var paciente in asignadosEnfermera)
            {
                var pacientes = await _context.Pacientes.SingleOrDefaultAsync(p => p.PacienteId == paciente.Paciente.PacienteId);

                pacientesLista.Add(pacientes);
            }

            ViewData["enfermera"] = _context.Enfermeras.Find(id).EnfermeraNombre;

            return View(pacientesLista);
        }

        public async Task<IActionResult> DeleteAsignacionEnf(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pce = await _context.PacientesConEnfermeras.Where(p => p.PacienteConEnfermeraId == id)
                .Include(p => p.Paciente)
                .Include(p => p.Enfermera)
                .FirstOrDefaultAsync();

            if (pce == null)
            {
                return NotFound();
            }

            return View(pce);
        }

        [HttpPost, ActionName("DeleteAsignacionEnf")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsignacionEnfConfirmed(PacienteAsignacionEnfermeraViewModel vm, int id)
        {
            var pce = await _context.PacientesConEnfermeras.Where(p => p.PacienteConEnfermeraId == id).FirstOrDefaultAsync();

            _context.PacientesConEnfermeras.Remove(pce);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Enfermera");
        }
        #endregion PacienteAsignacionEnfermera

        #region PacienteAsignacionMedicacion
        //Asignación de un paciente a una medicación
        public async Task<IActionResult> PacienteAsignacionMedicacion(int? id)
        {
            var pacienteDisplay = await _context.Pacientes.Select(p => new
            {
                Id = p.PacienteId,
                Value = p.PacienteNombre
            }).ToListAsync();

            PacienteAsignacionMedicacionViewModel vm = new PacienteAsignacionMedicacionViewModel();

            vm.PacientesLista = new SelectList(pacienteDisplay, "Id", "Value");

            var medicacion = await _context.Medicaciones.SingleOrDefaultAsync(m => m.MedicacionId == id);

            ViewBag.Medicacion = medicacion;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> PacienteAsignacionMedicacion(PacienteAsignacionMedicacionViewModel vm, int? id)
        {
            if (CompruebaPacienteConMedicacion(vm.PacienteConMedicacion.Medicacion.MedicacionId, vm.PacienteConMedicacion.Paciente.PacienteId))
            {
                return RedirectToAction("InformacionMedicacion");
            }
            else
            {
                var pcm = await _context.PacientesConMedicaciones.Where(p => p.PacienteConMedicacionId == id).FirstOrDefaultAsync();

                var paciente = await _context.Pacientes.SingleOrDefaultAsync(p => p.PacienteId == vm.PacienteConMedicacion.Paciente.PacienteId);
                var medicacion = await _context.Medicaciones.SingleOrDefaultAsync(m => m.MedicacionId == vm.PacienteConMedicacion.Medicacion.MedicacionId);
                medicacion.NumPacientesPautados++;

                PacienteConMedicacion pacienteConMedicacion = new PacienteConMedicacion();
                pacienteConMedicacion.Medicacion = medicacion;
                pacienteConMedicacion.Paciente = paciente;

                _context.Add(pacienteConMedicacion);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Medicacion");
            }
        }

        private bool CompruebaPacienteConMedicacion(int medicacionid, int pacienteid)
        {
            bool pacienteEncontrado;
            var asignado = _context.PacientesConMedicaciones
                .Where(p => p.Medicacion.MedicacionId != 0 && p.Paciente.PacienteId == pacienteid)
                .FirstOrDefault();

            pacienteEncontrado = (asignado != null);

            return pacienteEncontrado;
        }

        public IActionResult InformacionMedicacion()
        {
            return View();
        }

        public async Task<IActionResult> TodosPacientesConMedicacion(int? id)
        {
            var asignadosMedicacion = await _context.PacientesConMedicaciones
                .Where(p => p.Medicacion.MedicacionId == id)
                .Include(p => p.Paciente)
                .Include(p => p.Medicacion).ToListAsync();

            List<Paciente> pacientesLista = new List<Paciente>();

            foreach (var paciente in asignadosMedicacion)
            {
                var pacientes = await _context.Pacientes.SingleOrDefaultAsync(p => p.PacienteId == paciente.Paciente.PacienteId);

                pacientesLista.Add(pacientes);
            }

            ViewData["medicacion"] = _context.Medicaciones.Find(id).MedicacionNombre;

            return View(pacientesLista);
        }

        public async Task<IActionResult> DeleteAsignacionFar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pcm = await _context.PacientesConMedicaciones.Where(p => p.PacienteConMedicacionId == id)
                .Include(p => p.Paciente)
                .Include(p => p.Medicacion)
                .FirstOrDefaultAsync();

            if (pcm == null)
            {
                return NotFound();
            }

            return View(pcm);
        }

        [HttpPost, ActionName("DeleteAsignacionFar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsignacionFarConfirmed(PacienteAsignacionMedicacionViewModel vm, int id)
        {
            var pcm = await _context.PacientesConMedicaciones.Where(p => p.PacienteConMedicacionId == id).FirstOrDefaultAsync();

            _context.PacientesConMedicaciones.Remove(pcm);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Medicacion");
        }
        #endregion PacienteAsignacionMedicacion

        #region PacienteVerMedicacion
        /*public async Task<IActionResult> PacienteVerMedicacion(int? id)
        {
            return null;
        }*/
        #endregion PacienteVerMedicacion
    }
}
