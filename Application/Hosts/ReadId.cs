using Application.ErrorHandler;
using AutoMapper;
using Domain.Dtos.Host;
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

namespace Application.Hosts
{
    public class ReadId
    {
        public class Execute : IRequest<HostDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Execute, HostDto>
        {
            private readonly HostsDbContext _context;
            private readonly IMapper _mapper;
            public Handler(HostsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<HostDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var hotel = await _context.Hosts
                    .Include(x => x.Habitaciones)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (hotel == null)
                {
                    //throw new Exception("No se puede eliminar curso");
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new { mensaje = "No se encontró el hotel" });
                }

                var hotelDto = _mapper.Map<Host, HostDto>(hotel);
                return hotelDto;
            }
        }
    }
}
