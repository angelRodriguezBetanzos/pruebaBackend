using AutoMapper;
using Domain.Dtos.TipoHabitacion;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TipoHabitaciones
{
    public class Read
    {
        public class ReadTipoHabitaciones : IRequest<List<TipoHabitacionDto>> { }

        public class Handler : IRequestHandler<ReadTipoHabitaciones, List<TipoHabitacionDto>>
        {
            private readonly HostsDbContext _context;
            private readonly IMapper _mapper;
            public Handler(HostsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<TipoHabitacionDto>> Handle(ReadTipoHabitaciones request, CancellationToken cancellationToken)
            {
                var tipoHabitaciones = await _context.TipoHabitaciones.ToListAsync();
                var tipoHabitacionesDto = _mapper.Map<List<TipoHabitacion>, List<TipoHabitacionDto>>(tipoHabitaciones);

                return tipoHabitacionesDto;
            }
        }
    }
}
