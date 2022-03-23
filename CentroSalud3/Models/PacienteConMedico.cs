using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Models
{
    public class PacienteConMedico
    {
        public int PacienteConMedicoId { get; set; }

        public virtual Paciente Paciente { get; set; }
        public virtual Medico Medico { get; set; }
    }
}
