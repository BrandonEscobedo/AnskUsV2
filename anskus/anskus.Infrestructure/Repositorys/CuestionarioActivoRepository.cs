using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
using anskus.Infrestructure.Factory;
using anskus.Infrestructure.Persistence.Context;
using anskus.Infrestructure.Persistence.Dapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Infrestructure.Repositorys
{
    internal sealed class CuestionarioActivoRepository(AnskusDbContext _context,
        IRandomCodeFactory randomCode) : ICuestionarioActivoRepository
    {
        public async Task<CuestionarioActivo> ActivarCuestionarioAsync(Guid idcuestionario,string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email) ?? throw new Exception("No se encontro el usuario");
            if (!await _context.cuestionarioActivo.Where(x => x.IdUsuario == user.Id).AnyAsync())
            {
                int code = await randomCode.GenerarCodigo();
                if (code != 0)
                {
                    CuestionarioActivo cuestionarioActivo = new()
                    {
                        Idcuestionario = idcuestionario,
                        Codigo = code,
                        IdUsuario = user.Id
                    };
                    _context.cuestionarioActivo.Add(cuestionarioActivo);
                    await _context.SaveChangesAsync();
                    return cuestionarioActivo;
                }

            }
            return null!;
        }

        public Task<bool> AddParticipanteCuestionarioAsync(string code, string name)
        {
            throw new NotImplementedException();
        }
    }
}
