using InvestCarControl.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InvestCarControl.Extentions.ViewComponents.Helpers
{
    public static class Util
    {
        //public static int TotReg(MyDbContext ctx)
        //{
        //    return (from pac in ctx.Paciente.AsNoTracking() select pac).Count();
        //}

        //public static decimal GetNumRegEstado(MyDbContext ctx, string estado)
        //{
        //    return ctx.Paciente
        //        .Include(x => x.EstadoPaciente)
        //        .AsNoTracking()
        //        .Count(x => x.EstadoPaciente.Descricao.Contains(estado));
        //}

    }
}
