using anskus.Infrestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Infrestructure.Factory
{
    public class RandomCodeFactory : IRandomCodeFactory
    {

        private readonly AnskusDbContext _context;

        public RandomCodeFactory(AnskusDbContext context)
        {
            _context = context;
        }
        private async Task<int> GenerarCodigoAleatorio(Random random, int longitud)
        {
            int[] codigo = new int[longitud];
            for (int i = 0; i < longitud; i++)
            {
                codigo[i] = random.Next(10);
            }
            int cadena = Convert.ToInt32(string.Join("", codigo));
            return cadena;
        }
        public async Task<int> GenerarCodigo()
        {
            Random random = new Random();
            int codigo = await GenerarCodigoAleatorio(random, 4);
            do
            {
                codigo = await GenerarCodigoAleatorio(random, 4);
            }
            while (_context.cuestionarioActivo.Where(x => x.Codigo == codigo).Any());
            return codigo;
        }

        public  async Task<bool> IsCodeValid(int code)
        {
            if (await _context.cuestionarioActivo.Where(x => x.Codigo == code).FirstOrDefaultAsync() == null)
                return false;
            else
                return true;
        }
    }
}
