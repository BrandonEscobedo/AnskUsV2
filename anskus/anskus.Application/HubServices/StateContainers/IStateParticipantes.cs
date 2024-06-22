using anskus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.HubServices.StateContainers
{
    public interface IStateParticipantes
    {
        List<ParticipanteEnCuestionario> participanteEnCuestionario { get; set; }
        event Action<ParticipanteEnCuestionario>? OnParticipante;
        public void AddParticipanteToList(ParticipanteEnCuestionario participante);
        public void RemoveParticipanteToList(ParticipanteEnCuestionario participante);
    }
}
