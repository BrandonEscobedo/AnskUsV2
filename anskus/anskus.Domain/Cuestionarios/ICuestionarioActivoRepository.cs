﻿using anskus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Cuestionarios
{
    public interface ICuestionarioActivoRepository
    {
        public Task RemoveParticipanteFromRoom(Guid IdParticipante, int Codigo);

        public Task RemoveCuestionarioActivo(Guid IdCuestionario );
        Task<bool> IsParticipanteUniqueAsync(int code, string name);
        Task<CuestionarioActivo>     ActivarCuestionarioAsync(Guid idcuestionario, Guid Id);
        Task<Guid> AddParticipanteToRoomAsync(int Code, string Name);
        public  Task<bool> IsCuestionarioActivoUnique(string Email);
    }
}
