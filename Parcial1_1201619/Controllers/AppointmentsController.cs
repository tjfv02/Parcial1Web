using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial1_1201619.Models;

namespace Parcial1_1201619.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ParcialWeb1Context _context;

        public AppointmentsController(ParcialWeb1Context context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var parcialWeb1Context = _context.Appointments.Include(a => a.PersonalInformationDoctor).Include(a => a.PersonalInformationPatient);
            return View(await parcialWeb1Context.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.PersonalInformationDoctor)
                .Include(a => a.PersonalInformationPatient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["PersonalInformationDoctorId"] = new SelectList(_context.PersonalInformations, "Id", "Name");
            ViewData["PersonalInformationPatientId"] = new SelectList(_context.PersonalInformations, "Id", "Name");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonalInformationPatientId,PersonalInformationDoctorId,AppointmentDate,AppointmentTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonalInformationDoctorId"] = new SelectList(_context.PersonalInformations, "Id", "Name", appointment.PersonalInformationDoctorId);
            ViewData["PersonalInformationPatientId"] = new SelectList(_context.PersonalInformations, "Id", "Name", appointment.PersonalInformationPatientId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["PersonalInformationDoctorId"] = new SelectList(_context.PersonalInformations, "Id", "Name", appointment.PersonalInformationDoctorId);
            ViewData["PersonalInformationPatientId"] = new SelectList(_context.PersonalInformations, "Id", "Name", appointment.PersonalInformationPatientId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonalInformationPatientId,PersonalInformationDoctorId,AppointmentDate,AppointmentTime")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
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
            ViewData["PersonalInformationDoctorId"] = new SelectList(_context.PersonalInformations, "Id", "Name", appointment.PersonalInformationDoctorId);
            ViewData["PersonalInformationPatientId"] = new SelectList(_context.PersonalInformations, "Id", "Name", appointment.PersonalInformationPatientId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.PersonalInformationDoctor)
                .Include(a => a.PersonalInformationPatient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'ParcialWeb1Context.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
