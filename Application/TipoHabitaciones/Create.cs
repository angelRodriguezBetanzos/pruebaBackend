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

namespace Application.TipoHabitaciones
{
    public class Create
    {
        public class Execute : IRequest
        {
            public string Nombre { get; set; }
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public bool EsActivo { get; set; }
        }

        public class ValidateExecute : AbstractValidator<Execute>
        {
            public ValidateExecute()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Codigo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
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
                var tipoHabitacion = new TipoHabitacion
                {
                    Nombre = request.Nombre,
                    Codigo = request.Codigo,
                    Descripcion = request.Descripcion,
                    EsActivo = request.EsActivo
                };

                _context.Add(tipoHabitacion);
                var resultados = await _context.SaveChangesAsync();

                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new ExceptionHandler(HttpStatusCode.BadRequest, new { mensaje = "No se pudo insertar el tipo de habitación" });
            }
        }
    }
}
