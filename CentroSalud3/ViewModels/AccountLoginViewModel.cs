using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required(ErrorMessage = "Introduzca su correo electrónico"), EmailAddress, DataType(DataType.EmailAddress)]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Introduzca su contraseña"), DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
