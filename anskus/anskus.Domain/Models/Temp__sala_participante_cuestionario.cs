using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Models
{
    public class Temp__sala_participante_cuestionario
    {
        public Guid IdParticipante { get; set; }
        public int code { get; set; }
        public string name_user { get; set; } = "";

        public Temp__sala_participante_cuestionario()
        {
            IdParticipante = Guid.NewGuid();
        }
    }
}
