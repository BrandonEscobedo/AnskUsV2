﻿using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers
{
    public interface IStateBase
    {
        public Pregunta Pregunta { get; }
        public string Titulo { get; }
        public event Action? OnIniciarCuestionario;
        public event Action? OnSiguientePregunta;
        public void SetTituloPregunta(string Titulo, Pregunta pregunta);
        public void SetPregunta(Pregunta pregunta);
    }
}
