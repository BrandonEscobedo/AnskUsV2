using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace anskus.Domain.Models
{
    public class Cuestionario
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.String)]
        public Guid? IdCuestionario { get; set; }
        public string? Titulo { get; set; } = "";
        public EstadoCuestionario Estado { get; set; } 
        public string Iduser { get; set; } = "";
        public Cuestionario()
        {
            Estado = EstadoCuestionario.Borrador;     
        }
        public int TiempoRespuesta { get; set; } = 10;
        public int MaximoUsuarios { get; set; } = 2;
        public List<Pregunta> Pregunta { get; set; } = new List<Pregunta>();
    }
}
