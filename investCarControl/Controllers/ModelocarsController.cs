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
    public class ModelocarsController : Controller
    {
        private readonly MyDbContext _context;

        public ModelocarsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Modelocars
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Modelocar.Include(m => m.Fabricante);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Modelocars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelocar = await _context.Modelocar
                .Include(m => m.Fabricante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelocar == null)
            {
                return NotFound();
            }

            return View(modelocar);
        }

        // GET: Modelocars/Create
        public IActionResult Create()
        {
            ViewData["FabricanteId"] = new SelectList(_context.Fabricante, "Id", "Id");
            return View();
        }

        // POST: Modelocars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,FabricanteId")] Modelocar modelocar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelocar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FabricanteId"] = new SelectList(_context.Fabricante, "Id", "Id", modelocar.FabricanteId);
            return View(modelocar);
        }

        // GET: Modelocars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelocar = await _context.Modelocar.FindAsync(id);
            if (modelocar == null)
            {
                return NotFound();
            }
            ViewData["FabricanteId"] = new SelectList(_context.Fabricante, "Id", "Id", modelocar.FabricanteId);
            return View(modelocar);
        }

        // POST: Modelocars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,FabricanteId")] Modelocar modelocar)
        {
            if (id != modelocar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelocar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelocarExists(modelocar.Id))
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
            ViewData["FabricanteId"] = new SelectList(_context.Fabricante, "Id", "Id", modelocar.FabricanteId);
            return View(modelocar);
        }

        // GET: Modelocars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelocar = await _context.Modelocar
                .Include(m => m.Fabricante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelocar == null)
            {
                return NotFound();
            }

            return View(modelocar);
        }

        // POST: Modelocars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modelocar = await _context.Modelocar.FindAsync(id);
            _context.Modelocar.Remove(modelocar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelocarExists(int id)
        {
            return _context.Modelocar.Any(e => e.Id == id);
        }
    }
}
