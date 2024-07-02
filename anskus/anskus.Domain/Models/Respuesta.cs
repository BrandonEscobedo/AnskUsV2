using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Models
{
    public class Respuesta
    {     
 
     public string respuesta { get; set; } = null!;

      public bool RCorrecta { get; set; }=false;

    }
}
