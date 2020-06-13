using InvestCarControl.Data;
using InvestCarControl.Migrations;
using InvestCarControl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace InvestCarControl.Controllers
{
    public class ParceirosController : Controller
    {
        private readonly IdentyDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Parceiro> _userManager;
        private IHostEnvironment _env;
        
        public ParceirosController(IdentyDbContext context, RoleManager<IdentityRole> roleManager, UserManager<Parceiro> userManager, IHostEnvironment env)
        {
            _context = context;
            _env = env;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Parceiros
        public async Task<IActionResult> Index()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/img/avatars", this.User.Identity.Name + ".jpg");
            if (System.IO.File.Exists(path))
            {
                ViewData["PathAvatar"] = "/img/avatars/" + User.Identity.Name + ".jpg?" + DateTime.Now.Ticks;
            }
            else
            {
                ViewData["PathAvatar"] = "/img/avatars/default2.png";
            }
            //ViewData["NomeUsuario"] = User.Identity.Name;
            var parceiro = await _context.Parceiro
                .FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);
            return View(parceiro);
        }
        public async Task<IActionResult> Lista()
        {
            ViewData["Role"] = "User";
            if (User.IsInRole("Administrator")) {
                ViewData["Role"] = "Administrator";
            }
            var parceiro = _context.Parceiro;
            return View(await parceiro.ToListAsync());
        }

        public async Task<IActionResult> Avatar() 
        {
            return View();
        }

        public Task<IActionResult> SaveImage(string base64image)
        {
            if (base64image != null)
            {
                byte[] bytes = Convert.FromBase64String(base64image.Substring(23));
                var path = Path.Combine(
                                 Directory.GetCurrentDirectory(), "wwwroot/img/avatars", this.User.Identity.Name + ".jpg");
                using (var imageFile = new FileStream(path, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
            }
            return null;
        }

        // GET: Parceiros/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id== null)
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
        //public IActionResult Create()
        //{
        //    return View();
        //}

        

        // POST: Parceiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        //public async Task<IActionResult> Create([Bind("Id,Nome,Email,Telefone,Endereço")] Parceiro parceiro)
        public async Task<IActionResult> Create(Parceiro parceiro)
        {
        
            if (ModelState.IsValid)

            {
                // parceiro = parceiro.UserId = Guid.Parse("40b72079-0bd7-4fef-97aa-add4289f23aa");
                parceiro.UserName = User.Identity.Name;
                _context.Add(parceiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parceiro);
        }

        // GET: Parceiros/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
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

        // POST: Parceiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Edit(string id, Parceiro parceiro)
        {
            if (id != parceiro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var parceiroAtual = await _context.Parceiro
                    .FirstOrDefaultAsync(m => m.Id == id);
                parceiroAtual.Nome = parceiro.Nome;
                parceiroAtual.Telefone = parceiro.Telefone;
                parceiroAtual.Endereço = parceiro.Endereço;

                try
                {
                    _context.Update(parceiroAtual);
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
                return RedirectToAction(nameof(Lista));
            }
            return View(parceiro);
        }

        // GET: Parceiros/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
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
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var participacao = await _context.Participacao
                .FirstOrDefaultAsync(m => m.ParceiroId == id);
            var responsavel = await _context.Responsavel
                .FirstOrDefaultAsync(m => m.ParceiroId == id);
            if (participacao != null || responsavel != null)
            {
                var txtMensagem = "";
                if (participacao != null)
                {
                    if (responsavel != null)
                    {
                        txtMensagem = "Não é possível excluir o Parceiro," +
                             " pois ele tem participação em algum veículo" +
                             " e é responsável por alguma despesa.";
                    }
                    else
                    {
                        txtMensagem = "Não é possível excluir o Parceiro," +
                             " pois ele tem participação em algum veículo cadastrado.";
                    }
                } else
                {
                    txtMensagem = "Não é possível excluir o Parceiro," +
                         " pois ele é responsável por alguma despesa.";
                }
                ViewData["Mensagem"] = txtMensagem;
                return View("Error");
            }

            var parceiro = await _context.Parceiro.FindAsync(id);
            _context.Parceiro.Remove(parceiro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Lista));
        }

        private bool ParceiroExists(string id)
        {
            return _context.Parceiro.Any(e => e.Id == id);
        }
    }
}
