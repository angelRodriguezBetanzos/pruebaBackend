using AutoMapper;
using Domain.Dtos.Host;
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

namespace Application.Hosts
{
    public class Read
    {
        public class ReadHosts : IRequest<List<HostDto>> { }

        public class Handler : IRequestHandler<ReadHosts, List<HostDto>>
        {
            private readonly HostsDbContext _context;
            private readonly IMapper _mapper;
            public Handler(HostsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<HostDto>> Handle(ReadHosts request, CancellationToken cancellationToken)
            {
                var hoteles = await _context.Hosts
                    .Include(x => x.Habitaciones)
                .ToListAsync();
                var hotelesDto = _mapper.Map<List<Host>, List<HostDto>>(hoteles);

                return hotelesDto;
            }
        }
    }
}
