using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers
{
    public class HubStateCreador : IHubStateCreador
    {
        public CuestionarioResponse Cuestionario { get; set; } = new();
        public int Codigo { get; set; }
        public List<ParticipanteEnCuestionario> participanteEnCuestionario { get; set; } = new();
        public event Action<ParticipanteEnCuestionario>? OnParticipante;
        public void AddParticipanteToList(ParticipanteEnCuestionario participante)
        {
            participanteEnCuestionario.Add(participante);
            OnParticipante?.Invoke(participante);
        }
        public void RemoveParticipanteToList(ParticipanteEnCuestionario participante)
        {
            participanteEnCuestionario.Remove(participante);
            OnParticipante?.Invoke(participante);
        }
        public Pregunta MandarSiguientePregunta()
        {
            if (Cuestionario.Pregunta.Count > 0)
            {
                var pregunta = Cuestionario.Pregunta.First();
                Cuestionario.Pregunta.RemoveAt(0);
                return pregunta;
            }
            return null!;
        }

        public void SetCuestionario(CuestionarioResponse Cuestionario, int Codigo)
        {
            this.Cuestionario = Cuestionario;
            this.Codigo = Codigo;
        }
    }
}
