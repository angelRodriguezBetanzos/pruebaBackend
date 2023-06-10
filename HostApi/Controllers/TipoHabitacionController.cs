using Application.TipoHabitaciones;
using Domain.Dtos.TipoHabitacion;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostApi.Controllers
{
    public class TipoHabitacionController : CustomBaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<TipoHabitacionDto>>> Read()
        {
            return await Mediator.Send(new Read.ReadTipoHabitaciones());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoHabitacionDto>> ReadOne(int id)
        {
            return await Mediator.Send(new ReadId.Execute { Id = id });
        }


        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Execute data)
        {
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(int id, Edit.Execute execute)
        {
            execute.Id = id;
            return await Mediator.Send(execute);
        }

        [HttpDelete]
        public async Task<ActionResult<Unit>> Delete(Delete.Execute execute)
        {
            return await Mediator.Send(execute);
        }
    }
}
