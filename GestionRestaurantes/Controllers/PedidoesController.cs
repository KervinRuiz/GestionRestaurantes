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
    public class PedidoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var pedido = _context.Pedidos.Where(n => n.estado == true);
            return View(await pedido.ToListAsync());
        }
        [HttpGet]
        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var pedido = _context.Pedidos.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }
        [HttpGet]
        // GET: Pedidoes/Create
        public async Task<IActionResult> Create()
        {
           ViewBag.Mesa = await _context.Mesas.Where(n => n.Estado).ToListAsync();
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.estado = true;
                pedido.Fecha_Pedido = DateTime.Now;
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }
        [HttpGet]
        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido =  _context.Pedidos.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewBag.Mesa = await _context.Mesas.Where(n => n.Estado).ToListAsync();
            return View(pedido);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pedido pedido)
        {
           if(ModelState.IsValid)
            {
                if(id == pedido.Id)
                {
                    pedido.estado = true;
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
           return View(pedido);
        }
        [HttpGet]
        // GET: Pedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var pedido = _context.Pedidos.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost,]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            pedido.estado = false;
            _context.Update(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
          return (_context.Pedidos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
