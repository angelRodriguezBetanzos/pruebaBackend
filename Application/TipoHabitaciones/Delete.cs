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

namespace Application.TipoHabitaciones
{
    public class Delete
    {
        public class Execute : IRequest
        {
            public int Id { get; set; }
        }

        public class ValidateExecute : AbstractValidator<Execute>
        {
            public ValidateExecute()
            {
                RuleFor(x => x.Id).NotEmpty();
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
                var tipoHabitacion = await _context.TipoHabitaciones.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (tipoHabitacion is null)
                {
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new { mensaje = "No se encontró el tipo de habitación indicado" });
                }

                _context.TipoHabitaciones.Remove(tipoHabitacion);

                var resultados = await _context.SaveChangesAsync();

                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new ExceptionHandler(HttpStatusCode.BadRequest, new { mensaje = "No se pudo eliminar el tipo de habitación" });
            }
        }
    }
}
