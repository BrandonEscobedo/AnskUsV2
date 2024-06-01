using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Models
{
    public class Pregunta
    {    
     
  
        public string pregunta { get; set; } = null!;

        public  ICollection<Respuesta>? Respuesta { get; set; } = new List<Respuesta>();
    }
}
