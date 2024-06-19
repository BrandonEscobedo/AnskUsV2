using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Infrestructure.Factory
{
    public  interface IRandomCodeFactory
    {
        public Task<int> GenerarCodigo();
        Task<bool> IsCodeValid(int code);
    }
}
