using Application.ErrorHandler;
using AutoMapper;
using Domain.Dtos.Host;
using Domain.Dtos.TipoHabitacion;
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

namespace Application.TipoHabitaciones
{
    public class ReadId
    {
        public class Execute : IRequest<TipoHabitacionDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Execute, TipoHabitacionDto>
        {
            private readonly HostsDbContext _context;
            private readonly IMapper _mapper;
            public Handler(HostsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<TipoHabitacionDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var tipoHabitacion = await _context.TipoHabitaciones.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (tipoHabitacion == null)
                {
                    //throw new Exception("No se puede eliminar curso");
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new { mensaje = "No se encontró el tipo de habitacion" });
                }

                var tipoHabitacionDto = _mapper.Map<TipoHabitacion, TipoHabitacionDto>(tipoHabitacion);
                return tipoHabitacionDto;
            }
        }
    }
}
