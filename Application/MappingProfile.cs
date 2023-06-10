using AutoMapper;
using Domain.Dtos.Habitacion;
using Domain.Dtos.Host;
using Domain.Dtos.TipoHabitacion;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Host, HostHabitacion>();
            CreateMap<TipoHabitacion, TipoHabitacionHabitacion>();
            CreateMap<Habitacion, HabitacionDto>();

            CreateMap<Habitacion, HabitacionHost>();
            CreateMap<Host, HostDto>();

            CreateMap<TipoHabitacion, TipoHabitacionDto>();

        }
        
    }
}
