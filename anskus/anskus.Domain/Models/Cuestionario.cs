﻿using anskus.Domain.Cuestionarios;
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
        public DateTime FechaCreacion { get; set; }
        public EstadoCuestionario Estado { get; set; }
        private ICuestionarioState _estadoActual;
        public Guid Iduser { get; set; }
        public Cuestionario()
        {
            Estado = EstadoCuestionario.Borrador;
            _estadoActual = new BorradorState();
        }
        public void SetEstado(ICuestionarioState NuevoEstado)
        {
            _estadoActual = NuevoEstado;
        }
        public bool Completo()
        {
            if (string.IsNullOrWhiteSpace(Titulo))
            {
                return false;
            }
            foreach (var pregunta in Pregunta)
            {
                if (string.IsNullOrWhiteSpace(pregunta.pregunta))
                    return false;
                int validResponsesCount = 0;
                bool hasCorrectResponse = false;              
                foreach (var respuesta in pregunta.Respuesta)
                {
                    if (!string.IsNullOrWhiteSpace(respuesta.respuesta) )
                    {
                        validResponsesCount++;
                        if (respuesta.RCorrecta)
                        {
                            hasCorrectResponse = true;
                        }
                    }
                }
                if (!hasCorrectResponse || validResponsesCount < 2)
                {
                    return false;
                }
            }
            return true;
        }
        public void Guardar()
        {
            _estadoActual.Guardar(this);
        }
        public void Borrador()
        {
            _estadoActual.Borrador(this);
        }
        public int TiempoRespuesta { get; set; } = 10;
        public int MaximoUsuarios { get; set; } = 2;
        public List<Pregunta> Pregunta { get; set; } = new List<Pregunta>();
    }
}
