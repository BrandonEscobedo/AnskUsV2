using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Domain.Models;
using AutoMapper;
namespace anskus.Application.Utility
{
    public   class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cuestionario,CuestionarioResponse>().ReverseMap();
            CreateMap<anskus.Domain.Models.CuestionarioActivo, CuestionarioActivoResponse>() .ForMember(x=>x.IdcuestionarioActivo,x=>x.MapFrom(d=>d.Idcuestionario)) .ReverseMap();
        }
    }
}
