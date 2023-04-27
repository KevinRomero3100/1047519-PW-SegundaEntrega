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
using System.Net;

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

           
            

            var clientes_direccions = await clienthttp.GetFromJsonAsync<IEnumerable<ClienteDireeccion>>(url + "api/ClienteDireeccion");
            var cliente_direccion = clientes_direccions.FirstOrDefault(x => x.ClienteIdCliente == id);

            if (cliente_direccion == null)
            {
                return NotFound();
            }

            cliente_direccion.ClienteIdClienteNavigation = await clienthttp.GetFromJsonAsync<Cliente>(url + "api/Clientes/" + cliente_direccion.ClienteIdCliente.ToString());
            cliente_direccion.DireccionIdDireccionNavigation = await clienthttp.GetFromJsonAsync<Direccion>(url + "api/Direccion/" + cliente_direccion.DireccionIdDireccion.ToString());
            cliente_direccion.ColoniaIdColoniaNavigation = await clienthttp.GetFromJsonAsync<Colonium>(url + "api/Colonia/" + cliente_direccion.ColoniaIdColonia.ToString());

            return View(cliente_direccion);
        }

        // GET: Clientes/Create
        public async Task<IActionResult> Create()
        {
            var colonias = await clienthttp.GetFromJsonAsync<IEnumerable<Colonium>>(url + "api/Colonia");
            ViewData["Colonias"] = new SelectList(colonias, "IdColonia", "Nombre");
            return View();
        }

        //// POST: Clientes/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? _colonia, string? _referencia, string? _descripccion, [Bind("CodigoPersonal,Dpi,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Telefono,Email,Estado")] Cliente cliente)
        {
            if (cliente != null && _colonia != null  && _descripccion != null && _referencia != null )
            {
                var direccion = new Direccion()
                {
                    Descripcion = _descripccion,
                    Referencia= _referencia,
                };
                var resp1 = await clienthttp.PostAsJsonAsync<Direccion>(url + "api/Direccion", direccion);
                var resp2 = await clienthttp.PostAsJsonAsync<Cliente>(url + "api/Clientes",cliente);
                if ((int)resp1.StatusCode == 201 && (int)resp2.StatusCode == 201)
                {
                    var Clientes = await clienthttp.GetFromJsonAsync<IEnumerable<Cliente>>(url + "api/Clientes");
                    var Direcciones = await clienthttp.GetFromJsonAsync<IEnumerable<Direccion>>(url + "api/Direccion");

                    var Cliente_Direccion = new ClienteDireeccion()
                    {
                        ClienteIdCliente = Clientes.FirstOrDefault(x => x.CodigoPersonal.Equals(cliente.CodigoPersonal)).IdCliente,
                        DireccionIdDireccion = Direcciones.FirstOrDefault(x => x.Descripcion.Equals(_descripccion)).IdDireccion,
                        ColoniaIdColonia = _colonia
                    };
                    var resp3 = await clienthttp.PostAsJsonAsync<ClienteDireeccion>(url + "api/ClienteDireeccion", Cliente_Direccion);
                    return RedirectToAction(nameof(Index));
                }
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
            var direccionesClientes = await clienthttp.GetFromJsonAsync <IEnumerable<ClienteDireeccion>>(url + "api/ClienteDireeccion");
            var direccionCliente = direccionesClientes.FirstOrDefault(x => x.ClienteIdCliente== id);
            direccionCliente.ClienteIdClienteNavigation = cliente;
            var Direcciones = await clienthttp.GetFromJsonAsync<IEnumerable<Direccion>>(url + "api/Direccion");
            direccionCliente.DireccionIdDireccionNavigation = Direcciones.FirstOrDefault(x => x.IdDireccion == direccionCliente.DireccionIdDireccion);



            if (cliente == null)
            {
                return NotFound();
            }

            var colonias = await clienthttp.GetFromJsonAsync<IEnumerable<Colonium>>(url + "api/Colonia");
            ViewData["Colonias"] = new SelectList(colonias, "IdColonia", "Nombre");
            return View(direccionCliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClienteDireeccion cliente)
        {   
            HttpResponseMessage response = new HttpResponseMessage();
            if (ModelState.IsValid)
            {

                try
                {
                   response = await clienthttp.PutAsJsonAsync<Cliente>(url + "api/Clientes/" + cliente.ClienteIdClienteNavigation.IdCliente.ToString(), cliente.ClienteIdClienteNavigation);
                    await clienthttp.PutAsJsonAsync<Direccion>(url + "api/Direccion/" + cliente.DireccionIdDireccionNavigation.IdDireccion.ToString(), cliente.DireccionIdDireccionNavigation);
                    //var Cliente_Direccion = new ClienteDireeccion()
                    //{
                    //    ClienteIdCliente = cliente.,
                    //    DireccionIdDireccion = Direcciones.FirstOrDefault(x => x.Descripcion.Equals(_descripccion)).IdDireccion,
                    //    ColoniaIdColonia = _colonia
                    //};
                    cliente.DireccionIdDireccion = cliente.DireccionIdDireccionNavigation.IdDireccion;
                    cliente.ClienteIdCliente = cliente.ClienteIdClienteNavigation.IdCliente;
                    await clienthttp.PutAsJsonAsync<ClienteDireeccion>(url + "api/ClienteDireeccion/" + cliente.IdClienteDireeccion.ToString(), cliente);


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
