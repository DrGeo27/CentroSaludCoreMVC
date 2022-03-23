using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Models
{
    public class PacienteConMedicacion
    {
        public int PacienteConMedicacionId { get; set; }

        public virtual Paciente Paciente { get; set; }
        public virtual Medicacion Medicacion { get; set; }
        public virtual Medico Medico { get; set; }
    }
}
