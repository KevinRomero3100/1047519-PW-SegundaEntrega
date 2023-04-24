using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibreriaPagos.Models;
using ControlDePagos.Models;
using Google.Protobuf.WellKnownTypes;

namespace ControlDePagos.Controllers
{
    public class ClientesController : Controller
    {



        HttpClient clienthttp;
        public string url { get; set; } = new URL().urlApi;

        public ClientesController()
        {
            clienthttp = new HttpClient();
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            HttpClient clienthttp = new HttpClient();
            var list = await clienthttp.GetFromJsonAsync<IEnumerable<Cliente>>(url + "api/Clientes");
            return View(list);
        }


        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await clienthttp.GetFromJsonAsync<Cliente>(url + "api/Clientes/" + id.ToString());
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Clientes/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,CodigoPersonal,Dpi,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Telefono,Email,Estado")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(cliente);
                await clienthttp.PostAsJsonAsync<Cliente>(url + "api/Clientes",cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await clienthttp.GetFromJsonAsync<Cliente>(url + "api/Clientes/" + id.ToString());
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdCliente,CodigoPersonal,Dpi,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Telefono,Email,Estado")] Cliente cliente)
        {   
            HttpResponseMessage response = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                try
                {
                   response = await clienthttp.PutAsJsonAsync<Cliente>(url + "api/Clientes/" + cliente.IdCliente.ToString(), cliente);
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var cliente = await clienthttp.GetFromJsonAsync<Cliente>(url + "api/Clientes/" + id.ToString());
            //await clienthttp.DeleteFromJsonAsync<Cliente>(url + "api/Clientes/" + id.ToString());

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("IdCliente,CodigoPersonal,Dpi,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Telefono,Email,Estado")] Cliente cl)
        {

            var list = await clienthttp.GetFromJsonAsync<IEnumerable<Cliente>>(url + "api/Clientes");
            if (list == null)
            {
                return Problem("Entity set 'PaycontroldbContext.Clientes'  is null.");
            }
            var cliente = list.FirstOrDefault(x => x.IdCliente == cl.IdCliente );
            if (cliente != null)
            {

               var Response = await clienthttp.DeleteAsync(url + "api/Clientes/" + cl.IdCliente.ToString());
            }
            return RedirectToAction(nameof(Index));
        }

        //private bool ClienteExists(int id)
        //{
        //    return (_context.Clientes?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        //}
    }
}
