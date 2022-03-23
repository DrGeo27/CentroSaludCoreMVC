using CentroSalud3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.ViewModels
{
    public class PacienteAsignacionEnfermeraViewModel
    {
        public PacienteConEnfermera PacienteConEnfermera { get; set; }

        public SelectList PacientesLista { get; set; }
    }
}
