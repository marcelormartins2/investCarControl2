using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvestCarControl.Data;
using InvestCarControl.Models;

namespace InvestCarControl.Controllers
{
    public class ParceirosController : Controller
    {
        private readonly MyDbContext _context;

        public ParceirosController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Parceiros
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parceiro.ToListAsync());
        }

        // GET: Parceiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parceiro = await _context.Parceiro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parceiro == null)
            {
                return NotFound();
            }

            return View(parceiro);
        }

        // GET: Parceiros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parceiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Telefone,Endereço")] Parceiro parceiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parceiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parceiro);
        }

        // GET: Parceiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parceiro = await _context.Parceiro.FindAsync(id);
            if (parceiro == null)
            {
                return NotFound();
            }
            return View(parceiro);
        }

        // POST: Parceiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Telefone,Endereço")] Parceiro parceiro)
        {
            if (id != parceiro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parceiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParceiroExists(parceiro.Id))
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
            return View(parceiro);
        }

        // GET: Parceiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parceiro = await _context.Parceiro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parceiro == null)
            {
                return NotFound();
            }

            return View(parceiro);
        }

        // POST: Parceiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parceiro = await _context.Parceiro.FindAsync(id);
            _context.Parceiro.Remove(parceiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParceiroExists(int id)
        {
            return _context.Parceiro.Any(e => e.Id == id);
        }
    }
}
