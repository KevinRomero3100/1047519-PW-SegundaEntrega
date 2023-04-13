using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibreriaPagos.Models;

namespace ControlDePagos.Controllers
{
    public class EmpleadosController : Controller
    {

        HttpClient clienthttp;
        public string url { get; set; } = "https://localhost:7010/";

        public EmpleadosController()
        {
            clienthttp = new HttpClient();
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var list = await clienthttp.GetFromJsonAsync<IEnumerable<Empleado>>(url + "api/Empleados");
            /*foreach (var item in list)
            {
                item.RolIdRolNavigation = await clienthttp.GetFromJsonAsync<Rol>(url + "api/Rols/" + item.RolIdRol.ToString());
            }*/
            var rols = await clienthttp.GetFromJsonAsync<IEnumerable<Empleado>>(url + "api/Rols");

            list.Select(p => p.RolIdRolNavigation.Equals(rols.FirstOrDefault(x => x.RolIdRol.Equals(p.RolIdRol))));
            return View(list);
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var empleado = await  clienthttp.GetFromJsonAsync<Empleado>(url + "api/Empleados/" + id.ToString());
            
            if (empleado == null)
            {
                return NotFound();
            }
            empleado.RolIdRolNavigation = await clienthttp.GetFromJsonAsync<Rol>(url + "api/Rols/" + empleado.RolIdRol.ToString());
            return View(empleado);
        }

        // GET: Empleados/Create
        public async Task<IActionResult> Create()
        {
            
            var rols = await clienthttp.GetFromJsonAsync<IEnumerable<Rol>>(url + "api/Rols");
            ViewData["RolIdRol"] = new SelectList(rols, "IdRol", "Type");
            return View();
        }



        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see api/Empleados http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoPersonal,Dpi,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Telefono,Email,Estado,RolIdRol")] Empleado empleado)
        {
           if (ModelState.IsValid)
            {
                await clienthttp.PostAsJsonAsync<Empleado>(url + "api/Empleados", empleado);
                return RedirectToAction(nameof(Index));
            }
            var rols = await clienthttp.GetFromJsonAsync<IEnumerable<Rol>>(url + "api/Rols");
            ViewData["RolIdRol"] = new SelectList(rols, "IdRol", "Type"); 
            return View(empleado);
        }

        //// GET: Empleados/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Empleados == null)
        //    {
        //        return NotFound();
        //    }

        //    var empleado = await _context.Empleados.FindAsync(id);
        //    if (empleado == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["RolIdRol"] = new SelectList(_context.Rols, "IdRol", "Type", empleado.RolIdRol);
        //    return View(empleado);
        //}

        //// POST: Empleados/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IdEmpleado,CodigoPersonal,Dpi,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Telefono,Email,Estado,RolIdRol")] Empleado empleado)
        //{
        //    if (id != empleado.IdEmpleado)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(empleado);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EmpleadoExists(empleado.IdEmpleado))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["RolIdRol"] = new SelectList(_context.Rols, "IdRol", "Type", empleado.RolIdRol);
        //    return View(empleado);
        //}

        //// GET: Empleados/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Empleados == null)
        //    {
        //        return NotFound();
        //    }

        //    var empleado = await _context.Empleados
        //        .Include(e => e.RolIdRolNavigation)
        //        .FirstOrDefaultAsync(m => m.IdEmpleado == id);
        //    if (empleado == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(empleado);
        //}

        //// POST: Empleados/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Empleados == null)
        //    {
        //        return Problem("Entity set 'PaycontroldbContext.Empleados'  is null.");
        //    }
        //    var empleado = await _context.Empleados.FindAsync(id);
        //    if (empleado != null)
        //    {
        //        _context.Empleados.Remove(empleado);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool EmpleadoExists(int id)
        //{
        //  return (_context.Empleados?.Any(e => e.IdEmpleado == id)).GetValueOrDefault();
        //}
    }
}
