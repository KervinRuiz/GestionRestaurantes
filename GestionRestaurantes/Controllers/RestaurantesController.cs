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
    public class RestaurantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: Restaurantes
        public async Task<IActionResult> Index()
        {
            var restaurante = _context.Restaurantes.Where(n => n.Estado == true);
            return View(await restaurante.ToListAsync());
        }

        [HttpGet]
        // GET: Restaurantes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var resta = _context.Restaurantes.Find(id);
            if (resta == null)
            {
                return NotFound();
            }
            return View(resta);
        }

        [HttpGet]
        // GET: Restaurantes/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                restaurante.Estado = true;
                _context.Add(restaurante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurante);
        }

        [HttpGet]
        // GET: Restaurantes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var resta = _context.Restaurantes.Find(id);
            if (resta == null)
            {
                return NotFound();
            }
            return View(resta);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                if (id == restaurante.Id)
                {
                    restaurante.Estado = true;
                    _context.Update(restaurante);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(restaurante);
        }
        [HttpGet]
        // GET: Restaurantes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var resta = _context.Restaurantes.Find(id);
            if (resta == null)
            {
                return NotFound();
            }
            return View(resta);
        }

        // POST: Restaurantes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var resta = await _context.Restaurantes.FindAsync(id);
            if (resta == null)
            {
                return NotFound();
            }
            resta.Estado = false;
            _context.Restaurantes.Update(resta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool RestauranteExists(int id)
        {
          return (_context.Restaurantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
