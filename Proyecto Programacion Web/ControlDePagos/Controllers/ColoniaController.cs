using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlDePagos.Models;
using LibreriaPagos.Models;
using MySqlX.XDevAPI;

namespace ControlDePagos.Controllers
{
    public class ColoniaController : Controller
    {
        HttpClient clienthttp;
        public string url { get; set; } = new URL().urlApi;

        public ColoniaController()
        {
            clienthttp = new HttpClient();
        }

        // GET: Colonia
        public async Task<IActionResult> Index()
        {
            HttpClient clienthttp = new HttpClient();
            var colonias = await clienthttp.GetFromJsonAsync<IEnumerable<Colonium>>(url + "api/Colonia");
            return View(colonias);
        }

        // GET: Colonia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colinas = await clienthttp.GetFromJsonAsync<Colonium>(url + "api/Colonia/" + id.ToString());
            if (colinas == null)
            {
                return NotFound();
            }

            return View(colinas);

        }

        // GET: Colonia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colonia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Municipio,Departamento")] Colonium colonium)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(cliente);
                await clienthttp.PostAsJsonAsync<Colonium>(url + "api/Colonia", colonium);
                return RedirectToAction(nameof(Index));
            }
            return View(colonium);
        }

        // GET: Colonia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colonia = await clienthttp.GetFromJsonAsync<Colonium>(url + "api/Colonia/" + id.ToString());
            if (colonia == null)
            {
                return NotFound();
            }
            return View(colonia);
        }

        // POST: Colonia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nombre,Municipio,Departamento")] Colonium colonium)
        {
           
            HttpResponseMessage response = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                try
                {
                    response = await clienthttp.PutAsJsonAsync<Colonium>(url + "api/Colonia/" + id, colonium);
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
            return View(colonium);
        }

        // GET: Colonia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colonia = await clienthttp.GetFromJsonAsync<Colonium>(url + "api/Colonia/" + id.ToString());
            if (colonia == null)
            {
                return NotFound();
            }
            return View(colonia);
        }

        // POST: Colonia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var colonias = await clienthttp.GetFromJsonAsync<IEnumerable<Colonium>>(url + "api/Colonia");
            if (colonias == null)
            {
                return Problem("Entity set 'PaycontroldbContext.Clientes'  is null.");
            }
            var colonia = colonias.FirstOrDefault(x => x.IdColonia == id);
            if (colonia != null)
            {
                var Response = await clienthttp.DeleteAsync(url + "api/Colonia/" + id);
            }
            return RedirectToAction(nameof(Index));
        }

        //private bool ColoniumExists(int id)
        //{
        //  return (_context.Colonia?.Any(e => e.IdColonia == id)).GetValueOrDefault();
        //}
    }
}
