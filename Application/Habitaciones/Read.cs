using AutoMapper;
using Domain.Dtos.Habitacion;
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

namespace Application.Habitaciones
{
    public class Read
    {
        public class ReadHabitaciones : IRequest<List<HabitacionDto>> { }

        public class Handler : IRequestHandler<ReadHabitaciones, List<HabitacionDto>>
        {
            private readonly HostsDbContext _context;
            private readonly IMapper _mapper;
            public Handler(HostsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<HabitacionDto>> Handle(ReadHabitaciones request, CancellationToken cancellationToken)
            {
                var habitaciones = await _context.Habitaciones
                    .Include(x => x.Host)
                    .Include(x => x.TipoHabitacion)
                .ToListAsync();
                var habitacionesDto = _mapper.Map<List<Habitacion>, List<HabitacionDto>>(habitaciones);

                return habitacionesDto;
            }
        }
    }
}
