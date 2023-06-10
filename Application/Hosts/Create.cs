using Application.ErrorHandler;
using Domain.Entities;
using FluentValidation;
using MediatR;
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
    public class Create
    {
        public class Execute : IRequest
        {
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
                var hotel = new Host
                {
                    Nombre = request.Nombre,
                    Email = request.Email,
                    RazonSocial = request.RazonSocial,
                    Rfc = request.Rfc,
                    Telefono = request.Telefono,
                    EsActivo = request.EsActivo
                };

                _context.Add(hotel);
                var resultados = await _context.SaveChangesAsync();

                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new ExceptionHandler(HttpStatusCode.BadRequest, new { mensaje = "No se pudo insertar el hotel" });
            }
        }
    }
}
