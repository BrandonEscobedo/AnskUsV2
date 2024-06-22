using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Codigo
{
    public interface IVerificarCodigo
    {
        Task<bool> ExecuteAsync(int code);
    }
}
