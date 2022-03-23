using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Validations
{
    public class ValidationCitacion : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime fecha = (DateTime)value;

            DateTime hoy = DateTime.Now;

            if (fecha.Date.Date.CompareTo(hoy.Date) >= 0)
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
