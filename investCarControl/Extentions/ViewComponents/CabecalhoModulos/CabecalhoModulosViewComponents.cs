using System.Threading.Tasks;
using InvestCarControl.Extentions.ViewComponents.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace InvestCarControl.Extentions.ViewComponents.CabecalhoModulos
{
    [ViewComponent(Name = "Cabecalho")]
    public class CabecalhoModulosViewComponents : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string titulo, string subtitulo)
        {
            var model = new Modulo()
            {
                Titulo = titulo,
                Subtitulo = subtitulo
            };

            return View(model);
        }
    }
}
