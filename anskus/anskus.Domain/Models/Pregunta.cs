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

        public Guid IdPregunta { get; set; }
        public Pregunta()
        {
            IdPregunta = Guid.NewGuid();
        }
        public string pregunta { get; set; } = "";
        public string Imagen { get; set; } = "";
       
        public  List<Respuesta> Respuesta { get; set; } = new List<Respuesta>();

        public static implicit operator string(Pregunta v)
        {
            throw new NotImplementedException();
        }
    }
}
