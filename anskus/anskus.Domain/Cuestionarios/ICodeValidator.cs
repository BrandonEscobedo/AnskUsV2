using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Cuestionarios
{
    public interface ICodeValidator
    {
        Task<bool> VerificarCodigoAsync(int Code);
    }
}
