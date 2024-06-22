using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
using anskus.Infrestructure.Factory;
using anskus.Infrestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Guid> AddParticipanteToRoomAsync(int Code, string Name)
        {
            Temp__sala_participante_cuestionario ParticipanteSala = new()
            {
                code = Code,
                name_user = Name,
            };
            _context.SalaParticipante.Add(ParticipanteSala);
            await _context.SaveChangesAsync();
            return ParticipanteSala.IdParticipante;
        }
        public async Task<bool> IsParticipanteUniqueAsync(int code, string name)
        {
            return !await _context.SalaParticipante.AnyAsync(p => p.code == code && p.name_user == name);
           
        }
        public async Task<bool> IsCuestionarioActivoUnique(string Email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email) ?? throw new Exception("No se encontro el usuario");
            return !await _context.cuestionarioActivo.Where(x => x.IdUsuario == user.Id).AnyAsync();
        }
    }
}
