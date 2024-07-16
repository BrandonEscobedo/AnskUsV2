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
        Task<Cuestionario> Add(Cuestionario cuestionario,string email );
        Task<List<Cuestionario>> GetbyUser(string email);
        Task<Cuestionario> GetbyId(Guid id,Guid IdUser);
        Task<Cuestionario> Update(Cuestionario cuestionario);
        public Task RemoveCuestionario(Guid idCuestionario, Guid Iduser);


    }
}
