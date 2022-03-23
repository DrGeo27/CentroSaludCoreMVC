using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Models
{
    public class Medico
    {
        public int MedicoId { get; set; }

        [Required(ErrorMessage = "Introduzca el nombre del médico")]
        //[RegularExpression(@"^[a-zA-ZÀ-ÿ0-9]*$", ErrorMessage = "No se admiten caracteres especiales ni números")]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string MedicoNombre { get; set; }

        [Required(ErrorMessage = "Introduzca el número de consulta")]
        [Display(Name = "Consulta")]
        public string MedicoConsulta { get; set; }

        [Required(ErrorMessage = "Introduzca un número de teléfono válido")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "El número de teléfono debe tener 9 dígitos")]
        [DataType(DataType.PhoneNumber)] [Phone]        
        [Display(Name = "Número de teléfono")]
        public string MedicoTelefono { get; set; }

        [Required(ErrorMessage = "Introduzca un correo electrónico válido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo electrónico")]
        public string MedicoEmail { get; set; }

        [Display(Name = "Pacientes asignados")]
        public int NumPacientes { get; set; }
    }
}
