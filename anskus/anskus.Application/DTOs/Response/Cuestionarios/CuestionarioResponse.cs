using anskus.Domain.Models;

namespace anskus.Application.DTOs.Response.Cuestionarios
{
    public class CuestionarioResponse
    {
        public Guid? IdCuestionario { get; set; }
        public string? Titulo { get; set; } = "";

        public int TiempoRespuesta { get; set; } = 10;
        public int MaximoUsuarios { get; set; } = 2;
        public List<Pregunta> Pregunta { get; set; } = new List<Pregunta>();
    }
}
