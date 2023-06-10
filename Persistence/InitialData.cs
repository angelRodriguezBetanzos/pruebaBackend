using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class InitialData
    {
        public static async Task InsertData(HostsDbContext context)
        {
            var habitaciones = new List<Habitacion>();
            var habitacion = await context.Habitaciones.FirstOrDefaultAsync(x => x.Nombre == "Sencilla");
            if (habitacion is null)
            {
                try
                {
                    habitaciones.Add(new Habitacion { Nombre = "Sencilla", Codigo = "Q", Descripcion = "Habitación desde una a dos personas", EsActivo = true });
                    habitaciones.Add(new Habitacion { Nombre = "Doble", Codigo = "QQ", Descripcion = "Habitación desde una a 4 personas", EsActivo = true });
                    context.AddRange(habitaciones);
                    var s = await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }
            
        }
    }
}
