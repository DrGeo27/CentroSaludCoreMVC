using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Models
{
    public class PacienteConEnfermera
    {
        public int PacienteConEnfermeraId { get; set; }

        public virtual Paciente Paciente { get; set; }
        public virtual Enfermera Enfermera { get; set; }
    }
}
