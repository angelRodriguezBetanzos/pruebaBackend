using Application.ErrorHandler;
using AutoMapper;
using Domain.Dtos.Habitacion;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habitaciones
{
    public class ReadId
    {
        public class Execute : IRequest<HabitacionDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Execute, HabitacionDto>
        {
            private readonly HostsDbContext _context;
            private readonly IMapper _mapper;
            public Handler(HostsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<HabitacionDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var habitacion = await _context.Habitaciones
                    .Include(x => x.Host)
                    .Include(x => x.TipoHabitacion)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (habitacion == null)
                {
                    //throw new Exception("No se puede eliminar curso");
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new { mensaje = "No se encontró la habitación" });
                }

                var habitacionDto = _mapper.Map<Habitacion, HabitacionDto>(habitacion);
                return habitacionDto;
            }
        }
    }
}
