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
        public string? Titulo { get; set; }
        public string Iduser { get; set; }
        public Cuestionario()
        {
            IdCuestionario = Guid.NewGuid();
        }
        public  ICollection<Pregunta> Pregunta { get; set; } = new List<Pregunta>();
    }
}
