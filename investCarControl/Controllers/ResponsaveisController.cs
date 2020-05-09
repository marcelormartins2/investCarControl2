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
    public class ResponsaveisController : Controller
    {
        private readonly MyDbContext _context;

        public ResponsaveisController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Responsaveis
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Responsavel.Include(r => r.Despesa).Include(r => r.Parceiro);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Responsaveis/Details/5
        public async Task<IActionResult> Details(int? id1, int? id2 )
        {
            if (id1 == null || id2 == null)
            {
                return NotFound();
            }

            var responsavel = await _context.Responsavel
                .Include(r => r.Despesa)
                .Include(r => r.Parceiro)
                .FirstOrDefaultAsync(m => m.DespesaId == id1 && m.ParceiroId == id2);
            if (responsavel == null)
            {
                return NotFound();
            }

            return View(responsavel);
        }

        // GET: Responsaveis/Create
        public IActionResult Create()
        {
            ViewData["DespesaId"] = new SelectList(_context.Despesa, "Id", "Id");
            ViewData["ParceiroId"] = new SelectList(_context.Parceiro, "Id", "Id");
            return View();
        }

        // POST: Responsaveis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DespesaId,ParceiroId,Valor")] Responsavel responsavel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(responsavel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DespesaId"] = new SelectList(_context.Despesa, "Id", "Id", responsavel.DespesaId);
            ViewData["ParceiroId"] = new SelectList(_context.Parceiro, "Id", "Id", responsavel.ParceiroId);
            return View(responsavel);
        }

        // GET: Responsaveis/Edit/5
        public async Task<IActionResult> Edit(int? id1, int? id2)
        {
            if (id1 == null || id2 == null)
            {
                return NotFound();
            }
            var responsavel = await _context.Responsavel
                .FirstOrDefaultAsync(m => m.DespesaId == id1 && m.ParceiroId == id2);
            //var responsavel = await _context.Responsavel.FindAsync(id1);
            if (responsavel == null)
            {
                return NotFound();
            }
            ViewData["DespesaId"] = new SelectList(_context.Despesa, "Id", "Descricao", responsavel.DespesaId);
            ViewData["ParceiroId"] = new SelectList(_context.Parceiro, "Id", "Nome", responsavel.ParceiroId);
            return View(responsavel);
        }

        // POST: Responsaveis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("DespAntId, ParcAntId")] int despAntId, int parcAntId, [Bind("DespesaId,ParceiroId,Valor")] Responsavel responsavel)
        {
            if (responsavel == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (responsavel.DespesaId != despAntId || responsavel.ParceiroId != parcAntId)
                    {
                        var objResp = (from c in _context.Responsavel
                                       where c.DespesaId == responsavel.DespesaId && c.ParceiroId == responsavel.ParceiroId
                                       select c).SingleOrDefault();
                        if (objResp != null)
                        {
                            return NotFound();
                        }
                        
                        objResp = (from c in _context.Responsavel
                                       where c.DespesaId == despAntId && c.ParceiroId == parcAntId
                                       select c).SingleOrDefault();
                        _context.Responsavel.Remove(objResp);
                        await _context.SaveChangesAsync();
                        _context.Add(responsavel);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    _context.Update(responsavel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsavelExists(responsavel.DespesaId))
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
            ViewData["DespesaId"] = new SelectList(_context.Despesa, "Id", "Id", responsavel.DespesaId);
            ViewData["ParceiroId"] = new SelectList(_context.Parceiro, "Id", "Id", responsavel.ParceiroId);
            return View(responsavel);
        }

        // GET: Responsaveis/Delete/5
        public async Task<IActionResult> Delete(int? id1, int? id2)
        {
            if (id1 == null || id2 == null)
            {
                return NotFound();
            }

            var responsavel = await _context.Responsavel
                .Include(r => r.Despesa)
                .Include(r => r.Parceiro)
                .FirstOrDefaultAsync(m => m.DespesaId == id1);
            if (responsavel == null)
            {
                return NotFound();
            }

            return View(responsavel);
        }

        // POST: Responsaveis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("DespesaId,ParceiroId,Valor")] Responsavel responsavel)
        {
            _context.Responsavel.Remove(responsavel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponsavelExists(int id)
        {
            return _context.Responsavel.Any(e => e.DespesaId == id);
        }
    }
}
