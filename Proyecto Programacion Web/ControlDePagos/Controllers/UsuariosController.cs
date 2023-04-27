using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibreriaPagos.Models;
using ControlDePagos.Models;

namespace ControlDePagos.Controllers
{
    public class UsuariosController : Controller
    {
        HttpClient clienthttp;
        public string url { get; set; } = new URL().urlApi;

        public UsuariosController()
        {
            clienthttp = new HttpClient();
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            HttpClient clienthttp = new HttpClient();
            var Usuarios = await clienthttp.GetFromJsonAsync<IEnumerable<Usuario>>(url + "api/Usuarios");
            var empleados = await clienthttp.GetFromJsonAsync<IEnumerable<Empleado>>(url + "api/Empleados");
            Usuarios.ToList().ForEach(x => x.EmpleadoIdEmpleadoNavigation = empleados.FirstOrDefault(y => y.IdEmpleado.Equals(x.EmpleadoIdEmpleado)));
            return View(Usuarios);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var usuario = await clienthttp.GetFromJsonAsync<Usuario>(url + "api/Usuarios/" + id.ToString());
            usuario.EmpleadoIdEmpleadoNavigation = await clienthttp.GetFromJsonAsync<Empleado>(url + "api/Empleados/" + usuario.EmpleadoIdEmpleado.ToString());
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public async Task<IActionResult> Create()
        {

            var empleados = await clienthttp.GetFromJsonAsync<IEnumerable<Empleado>>(url + "api/Empleados");
            ViewData["EmpleadoIdEmpleado"] = new SelectList(empleados, "IdEmpleado", "CodigoPersonal");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreDeUsuario,Pasword,EmpleadoIdEmpleado")] Usuario usuario, string cc)
        {
            if (cc != usuario.Pasword)
            {
                //mensaje contraseña no coincide
            }
            else if (ModelState.IsValid)
            {
                await clienthttp.PostAsJsonAsync<Usuario>(url + "api/Usuarios", usuario);
                return RedirectToAction(nameof(Index));
            }
            //mensaje de campos no validos
            var empleados = await clienthttp.GetFromJsonAsync<IEnumerable<Empleado>>(url + "api/Empleados");
            ViewData["EmpleadoIdEmpleado"] = new SelectList(empleados, "IdEmpleado", "Email", usuario.EmpleadoIdEmpleado);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await clienthttp.GetFromJsonAsync<Usuario>(url + "api/Usuarios/" + id.ToString());

            if (usuario == null)
            {
                return NotFound();
            }
            usuario.EmpleadoIdEmpleadoNavigation = await clienthttp.GetFromJsonAsync<Empleado>(url + "api/Empleados/" + usuario.EmpleadoIdEmpleado.ToString());
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // PUT: api/Usuarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdUsuario,NombreDeUsuario,Pasword,EmpleadoIdEmpleado")] Usuario usuario)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                try
                {
                    response = await clienthttp.PutAsJsonAsync<Usuario>(url + "api/Usuarios/" + usuario.IdUsuario, usuario);

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
            var empleados = await clienthttp.GetFromJsonAsync<IEnumerable<Empleado>>(url + "api/Empleados");
            ViewData["EmpleadoIdEmpleado"] = new SelectList(empleados, "IdEmpleado", "CodigoPersonal");
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await clienthttp.GetFromJsonAsync<Usuario>(url + "api/Usuarios/" + id.ToString());
            usuario.EmpleadoIdEmpleadoNavigation = await clienthttp.GetFromJsonAsync<Empleado>(url + "api/Empleados/" + usuario.EmpleadoIdEmpleado.ToString());
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var usuarios = await clienthttp.GetFromJsonAsync<IEnumerable<Usuario>>(url + "api/Usuarios");
            if (usuarios == null)
            {
                return Problem("Entity set 'PaycontroldbContext.Clientes'  is null.");
            }
            var usuario = usuarios.FirstOrDefault(x => x.IdUsuario == id);
            if (usuario != null)
            {
                var Response = await clienthttp.DeleteAsync(url + "api/Usuarios/" + usuario.IdUsuario.ToString());
            }
            return RedirectToAction(nameof(Index));
        }

        //private bool UsuarioExists(int id)
        //{
        //  return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        //}
    }
}
