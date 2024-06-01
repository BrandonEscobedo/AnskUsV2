using anskus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Cuestionarios
{
    public interface ICuestionarioRepository
    {
        Task Add(Cuestionario cuestionario );
        Task<List<Cuestionario>> GetbyUser(string email);
        Task<Cuestionario> GetbyId(Guid id);
        Task Update(Cuestionario cuestionario);

    }
}
