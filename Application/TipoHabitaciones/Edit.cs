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
    public class Edit
    {
        public class Execute : IRequest
        {
            public int Id { get; set; }
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
                var tipoHabitacion = await _context.TipoHabitaciones.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (tipoHabitacion is null)
                {
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new { mensaje = "No se encontró el tipo de habitación indicado" });
                }

                tipoHabitacion.Nombre = request.Nombre;
                tipoHabitacion.Codigo = request.Codigo;
                tipoHabitacion.Descripcion = request.Descripcion;
                tipoHabitacion.EsActivo = request.EsActivo;


                var resultados = await _context.SaveChangesAsync();

                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new ExceptionHandler(HttpStatusCode.BadRequest, new { mensaje = "No se pudo actualizar el tipo de habitación" });

            }
        }
    }
}
