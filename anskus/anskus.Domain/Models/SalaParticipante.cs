using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Models
{
    public class SalaParticipante
    {
        [NotMapped]
        public Guid IdParticipante { get; set; }
     
        public int code { get; set; }
        public Guid IdCuestionario { get; set; }
      
        public string name_user { get; set; } = "";

        public virtual CuestionarioActivo IdCuestionrioNavigation { get; set; } = null!;
        public SalaParticipante()
        {
            IdParticipante = Guid.NewGuid();
        }
       
    }
}
