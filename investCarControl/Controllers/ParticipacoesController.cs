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
    public class ParticipacoesController : Controller
    {
        private readonly MyDbContext _context;

        public ParticipacoesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Participacoes
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Participacao.Include(p => p.Parceiro).Include(p => p.Veiculo);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Participacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participacao = await _context.Participacao
                .Include(p => p.Parceiro)
                .Include(p => p.Veiculo)
                .FirstOrDefaultAsync(m => m.ParceiroId == id);
            if (participacao == null)
            {
                return NotFound();
            }

            return View(participacao);
        }

        // GET: Participacoes/Create
        public IActionResult Create()
        {
            ViewData["ParceiroId"] = new SelectList(_context.Parceiro, "Id", "Id");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Id");
            return View();
        }

        // POST: Participacoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParceiroId,VeiculoId,PorcentagemCompra,PorcentagemLucro")] Participacao participacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParceiroId"] = new SelectList(_context.Parceiro, "Id", "Id", participacao.ParceiroId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Id", participacao.VeiculoId);
            return View(participacao);
        }

        // GET: Participacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participacao = await _context.Participacao.FindAsync(id);
            if (participacao == null)
            {
                return NotFound();
            }
            ViewData["ParceiroId"] = new SelectList(_context.Parceiro, "Id", "Id", participacao.ParceiroId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Id", participacao.VeiculoId);
            return View(participacao);
        }

        // POST: Participacoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParceiroId,VeiculoId,PorcentagemCompra,PorcentagemLucro")] Participacao participacao)
        {
            if (id != participacao.ParceiroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipacaoExists(participacao.ParceiroId))
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
            ViewData["ParceiroId"] = new SelectList(_context.Parceiro, "Id", "Id", participacao.ParceiroId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Id", participacao.VeiculoId);
            return View(participacao);
        }

        // GET: Participacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participacao = await _context.Participacao
                .Include(p => p.Parceiro)
                .Include(p => p.Veiculo)
                .FirstOrDefaultAsync(m => m.ParceiroId == id);
            if (participacao == null)
            {
                return NotFound();
            }

            return View(participacao);
        }

        // POST: Participacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participacao = await _context.Participacao.FindAsync(id);
            _context.Participacao.Remove(participacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipacaoExists(int id)
        {
            return _context.Participacao.Any(e => e.ParceiroId == id);
        }
    }
}
