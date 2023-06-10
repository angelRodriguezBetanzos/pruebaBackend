using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Host
{
    public class HostDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Rfc { get; set; }
        public string RazonSocial { get; set; }
        public bool EsActivo { get; set; }
        public ICollection<HabitacionHost> Habitaciones { get; set; }
    }
}
