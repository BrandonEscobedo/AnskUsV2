using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Services
{
    public interface IVerificarCodigoUsuarioService
    {
        public  Task<bool> VerificarCodigoAsync(int code);
        public Task<bool> EntrarASalaAsync(int code,string Name);
    }
}
