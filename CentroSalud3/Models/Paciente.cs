using CentroSalud3.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Models
{
    public class Paciente
    {
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "Introduzca el nombre del paciente")]
        [Display(Name = "Nombre")]
        //[RegularExpression(@"^[a-zA-ZÀ-ÿ0-9]*$", ErrorMessage = "No se admiten caracteres especiales ni números")]
        public string PacienteNombre { get; set; }

        [Required(ErrorMessage = "Introduzca una fecha de nacimiento válida")]
        [DataType(DataType.Date)]
        //[Range(minimum: DateTime.Today.Date, maximum: 31/12/1900, ErrorMessage = "¿Más de 120 años?")]
        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [ValidationNacimiento(ErrorMessage = "La fecha de nacimiento no puede ser superior a la de hoy")]
        public DateTime PacienteFxNacimiento { get; set; }

        [Display(Name = "Edad")]
        public int PacienteEdad { get; set; } /*= (DateTime.Now.Date.Year - PacienteFxNacimiento.Date.Year);*/

        [Required(ErrorMessage = "Seleccione una opción")]
        [Display(Name = "Sexo")]
        public Sexo PacienteSexo { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "El número de teléfono debe tener 9 dígitos")]
        [DataType(DataType.PhoneNumber)] [Phone]        
        [Display(Name = "Número de teléfono")]
        public string PacienteTelefono { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo electrónico")]
        public string PacienteEmail { get; set; }
    }

    public enum Sexo
    {
        Masculino = 1,
        Femenino = 2
    }
}
