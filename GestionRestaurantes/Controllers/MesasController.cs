using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionRestaurantes.Data;
using GestionRestaurantes.Models;

namespace GestionRestaurantes.Controllers
{
    public class MesasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MesasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: Mesas
        public async Task<IActionResult> Index()
        {
            var mesa = _context.Mesas.Where(n => n.Estado == true);
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes.Where(n => n.Estado == true), "Id", "Nombre_Restaurante");

            return View(await mesa.ToListAsync());
        }
        [HttpGet]
        // GET: Mesas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var mesa = _context.Mesas.Find(id);
            if(mesa == null)
            {
                return NotFound();
            }
            return View(mesa);
        }
        [HttpGet]   
        // GET: Mesas/Create
        public async Task<IActionResult> Create()
        {
            var tipoMesa = Enum.GetNames(typeof(Enums.Enums.TipoMesa));
            var estadomesa = Enum.GetNames(typeof (Enums.Enums.EstadoMesa));
            ViewData["Tipo_Mesa"] = new SelectList(tipoMesa);
            ViewData["Estado_Reservado"] = new SelectList(estadomesa);
            ViewBag.Restaurantes = await _context.Restaurantes.Where(n=> n.Estado == true).ToListAsync();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                mesa.Estado = true;
                _context.Add(mesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mesa);
        }

        [HttpGet]
        // GET: Mesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var mesa =  _context.Mesas.Find(id);
            if (mesa == null)
            {
                return NotFound();
            }
            var tipoMesa = Enum.GetNames(typeof(Enums.Enums.TipoMesa));
            var estadomesa = Enum.GetNames(typeof(Enums.Enums.EstadoMesa));
            ViewData["Tipo_Mesa"] = new SelectList(tipoMesa);
            ViewData["Estado_Reservado"] = new SelectList(estadomesa);
            ViewBag.Restaurantes = await _context.Restaurantes.Where(n => n.Estado == true).ToListAsync();
            return View(mesa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mesa mesa)
        {
            if(ModelState.IsValid)
            {
                if(id == mesa.Id)
                {
                    mesa.Estado = true;
                    _context.Update(mesa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(mesa);
        }
        [HttpGet]
        // GET: Mesas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var mesa = _context.Mesas.Find(id);
            if (mesa == null)
            {
                return NotFound();
            }

            return View(mesa);
        }

        // POST: Mesas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
           var mesa = await _context.Mesas.FindAsync(id);
            if (mesa == null)
            {
                return NotFound();
            }
            mesa.Estado = false;
            _context.Mesas.Update(mesa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MesaExists(int id)
        {
          return (_context.Mesas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
