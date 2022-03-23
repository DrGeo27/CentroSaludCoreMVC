using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Models
{
    public class Enfermera
    {
        public int EnfermeraId { get; set; }

        [Required(ErrorMessage = "Introduzca el nombre de la enfermera")]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string EnfermeraNombre { get; set; }

        [Required(ErrorMessage = "Introduzca el número de consulta")]
        [Display(Name = "Consulta")]
        public string EnfermeraConsulta { get; set; }

        [Required(ErrorMessage = "Introduzca un número de teléfono válido")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "El número de teléfono debe tener 9 dígitos")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [Display(Name = "Número de teléfono")]
        public string EnfermeraTelefono { get; set; }

        [Required(ErrorMessage = "Introduzca un correo electrónico válido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo electrónico")]
        public string EnfermeraEmail { get; set; }

        [Display(Name = "Pacientes asignados")]
        public int EnfermeraNumPacientes { get; set; }
    }
}
