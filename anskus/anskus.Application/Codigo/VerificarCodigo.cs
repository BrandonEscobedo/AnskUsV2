using anskus.Domain.Cuestionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Codigo
{
    public class VerificarCodigo:IVerificarCodigo
    {
        private readonly ICodeValidator _codeValidator;
        public VerificarCodigo(ICodeValidator codeValidator)
        {
            _codeValidator = codeValidator;
        }
        public async Task<bool> ExecuteAsync(int code)
        {
            return await _codeValidator.VerificarCodigoAsync(code);
        }
    }
}
