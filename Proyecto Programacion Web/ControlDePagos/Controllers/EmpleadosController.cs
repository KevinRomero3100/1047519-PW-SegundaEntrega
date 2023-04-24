using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibreriaPagos.Models;
using ControlDePagos.Models;
using Org.BouncyCastle.Utilities.Collections;
using Azure;

namespace ControlDePagos.Controllers
{
    public class EmpleadosController : Controller
    {

        HttpClient clienthttp;
        public string url { get; set; } = new URL().urlApi;

        public EmpleadosController()
        {
            clienthttp = new HttpClient();
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var empleados = await clienthttp.GetFromJsonAsync<IEnumerable<Empleado>>(url + "api/Empleados");
            var rols = await clienthttp.GetFromJsonAsync<IEnumerable<Rol>>(url + "api/Rols");
            empleados.ToList().ForEach(x => x.RolIdRolNavigation = rols.FirstOrDefault(y => y.IdRol.Equals(x.RolIdRol)));
            return View(empleados);
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await clienthttp.GetFromJsonAsync<Empleado>(url + "api/Empleados/" + id.ToString());
            if (empleado == null)
            {
                return NotFound();
            }
            var rols = await clienthttp.GetFromJsonAsync<IEnumerable<Rol>>(url + "api/Rols");
            ViewData["RolIdRol"] = new SelectList(rols, "IdRol", "Type", empleado.RolIdRol);
            return View(empleado);
        }

        //// POST: Empleados/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpleado,CodigoPersonal,Dpi,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Telefono,Email,Estado,RolIdRol")] Empleado empleado)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                try
                {
                    response = await clienthttp.PutAsJsonAsync<Empleado>(url + "api/Empleados/" + id, empleado);
                    
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (response.StatusCode.ToString() == "404")
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
            var rols = await clienthttp.GetFromJsonAsync<IEnumerable<Rol>>(url + "api/Rols");
            ViewData["RolIdRol"] = new SelectList(rols, "IdRol", "Type");
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await clienthttp.GetFromJsonAsync<Empleado>(url + "api/Empleados/" + id.ToString());
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("IdEmpleado,CodigoPersonal,Dpi,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Telefono,Email,Estado,RolIdRol")] Empleado empl)
        {
            var empleados = await clienthttp.GetFromJsonAsync<IEnumerable<Empleado>>(url + "api/Empleados");
            if (empleados == null)
            {
                return Problem("Entity set 'PaycontroldbContext.Clientes'  is null.");
            }
            var empleado = empleados.FirstOrDefault(x => x.IdEmpleado == empl.IdEmpleado);
            if (empleado != null)
            {

                var Response = await clienthttp.DeleteAsync(url + "api/Empleados/" + empleado.IdEmpleado.ToString());
            }
            return RedirectToAction(nameof(Index));
        }

        //private bool EmpleadoExists(int id)
        //{
        //  return (_context.Empleados?.Any(e => e.IdEmpleado == id)).GetValueOrDefault();
        //}
    }
}
