﻿using anskus.Application.DTOs;
using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers
{
    public class StateJugador : IStateJugador
    {
        public ParticipanteEnCuestDTO participante { get; set; } = new ParticipanteEnCuestDTO();
        public Pregunta Pregunta { get; private set; } = new Pregunta();
        public List<ParticipanteEnCuestDTO> participantes { get; set; } = new List<ParticipanteEnCuestDTO>();
        public string Titulo { get; private set; } = "";
        public event Action? OnIniciarCuestionario;
        public event Action? OnSiguientePregunta;

        public void SetPregunta(Pregunta pregunta)
        {
            Pregunta = pregunta;
            OnSiguientePregunta?.Invoke();
        }

        public void SetTituloPregunta(string Titulo, Pregunta pregunta)
        {
            Pregunta = pregunta;
            this.Titulo = Titulo;
            OnIniciarCuestionario?.Invoke();
        }
    }
}
