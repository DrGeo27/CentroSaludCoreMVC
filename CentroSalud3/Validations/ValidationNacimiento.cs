using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Validations
{
    public class ValidationNacimiento : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime fecha = (DateTime)value;
            if (fecha.Date.CompareTo(DateTime.Now) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
