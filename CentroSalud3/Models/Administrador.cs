using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Models
{
    public class Administrador
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public String Nombre { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
