using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Host
{
    public class HabitacionHost
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public string Codigo { get; set; }
        public bool EsActivo { get; set; }
        public int? HostId { get; set; }
        public int? TipoHabitacionId { get; set; }
    }
}
