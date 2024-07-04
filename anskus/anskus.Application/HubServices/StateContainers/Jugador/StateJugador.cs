using anskus.Application.DTOs;
using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers.Jugador
{
    public class StateJugador : IStateJugador
    {
        public ParticipanteEnCuestDTO participante { get; set; } = new ParticipanteEnCuestDTO();
        public DatosPregunta DatosPregunta { get; set; } = new();
        public List<ParticipanteEnCuestDTO> participantes { get; set; } = new List<ParticipanteEnCuestDTO>();
       public int Posicion { get; set; }
        public void AddParticipanteRanking(ParticipanteEnCuestDTO participante)
        {
            participantes.Add(participante);
        }
        public void GenerarPuestoRanking()
        {
            var participantesOrdenados = participantes.OrderByDescending(x => x.PuntosActuales).ToList();
            for(int i =0; i< participantesOrdenados.Count;i++)
            {
                if (participantesOrdenados[i].IdPeC == this.participante.IdPeC)
                {
                    Posicion = i + 1;
                    break;
                }
            }
        }
       
    }
   
}
