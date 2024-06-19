using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.DTOs
{
    public class ParticipanteEnCuestDTO
    {
        public Guid IdPeC { get; set; }
        public string Nombre { get; set; } = "";
        public int? PuntosActuales { get; set; } = 0;
        public int? PuntosAnteriores { get; set; } = 0;
        public int Codigo { get; set; }
        public int? CantidadPacertadas { get; set; } = 0;
    }
}
