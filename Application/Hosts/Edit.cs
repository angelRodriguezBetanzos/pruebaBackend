using Application.ErrorHandler;
using FluentValidation;
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
    public class Edit
    {
        public class Execute : IRequest
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Email { get; set; }
            public string Telefono { get; set; }
            public string Rfc { get; set; }
            public string RazonSocial { get; set; }
            public bool EsActivo { get; set; }
        }

        public class ValidateExecute : AbstractValidator<Execute>
        {
            public ValidateExecute()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Telefono).NotEmpty();
                RuleFor(x => x.Rfc).NotEmpty();
                RuleFor(x => x.RazonSocial).NotEmpty();
                RuleFor(x => x.EsActivo).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private HostsDbContext _context;

            public Handler(HostsDbContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var hotel = await _context.Hosts.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (hotel is null)
                {
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new { mensaje = "No se encontró el hotel indicado" });
                }

                hotel.Nombre = request.Nombre;
                hotel.Email = request.Email;
                hotel.RazonSocial = request.RazonSocial;
                hotel.Rfc = request.Rfc;
                hotel.Telefono = request.Telefono;
                hotel.EsActivo = request.EsActivo;


                var resultados = await _context.SaveChangesAsync();

                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new ExceptionHandler(HttpStatusCode.BadRequest, new { mensaje = "No se pudo actualizar el hotel" });

            }
        }
    }
}
