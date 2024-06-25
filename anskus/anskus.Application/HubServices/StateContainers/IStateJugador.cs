using anskus.Application.DTOs;
using anskus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.HubServices.StateContainers
{
    public interface IStateJugador
    {
        public ParticipanteEnCuestDTO participante { get; set; }
        public Pregunta Pregunta { get;  }
        public List<ParticipanteEnCuestDTO> participantes { get; set; }
        public string Titulo { get; set; }
        public event Action? OnIniciarCuestionario;
        public event Action? OnSiguientePregunta;
        public void SetTituloPregunta(string Titulo, Pregunta pregunta);
        public void SetPregunta(Pregunta pregunta);
    }
}
