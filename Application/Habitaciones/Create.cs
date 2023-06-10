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

namespace Application.Habitaciones
{
    public class Create
    {
        public class Execute : IRequest
        {
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public int Cantidad { get; set; }
            public float Precio { get; set; }
            public string Codigo { get; set; }
            public bool EsActivo { get; set; }
            public int IdHost { get; set; }
            public int IdTipoHabitacion { get; set; }
        }

        public class ValidateExecute : AbstractValidator<Execute>
        {
            public ValidateExecute()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.Cantidad).NotEmpty();
                RuleFor(x => x.Precio).NotEmpty();
                RuleFor(x => x.Codigo).NotEmpty();
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
                var habitacion = new Habitacion
                {
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    Cantidad = request.Cantidad,
                    Precio = request.Precio,
                    Codigo = request.Codigo,
                    EsActivo = request.EsActivo
                };

                _context.Add(habitacion);
                var resultados = await _context.SaveChangesAsync();

                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new ExceptionHandler(HttpStatusCode.BadRequest, new { mensaje = "No se pudo insertar la habitación" });
            }
        }
    }
}
