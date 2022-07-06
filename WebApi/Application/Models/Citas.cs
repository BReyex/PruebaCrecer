using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Citas
    {
        [Key]
        public int IdCita { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public string FechaHora { get; set; }
        public string Diagnostico { get; set; }
    }
}
