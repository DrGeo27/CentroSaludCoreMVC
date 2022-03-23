using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Models
{
    public class Medicacion
    {
        public int MedicacionId { get; set; }

        [Required(ErrorMessage = "Introduzca el nombre del fármaco")]
        [Display(Name = "Fármaco")]
        [DataType(DataType.Text)]
        public string MedicacionNombre { get; set; }

        //[Required(ErrorMessage = "Seleccione una opción")]
        //[Display(Name = "Medicación")]
        //public MedicacionNombre MedicacionNombre { get; set; }

        [Required(ErrorMessage = "Introduzca una dosis válida")]        
        [Display(Name = "Dosis")]
        public decimal MedicacionDosis { get; set; }

        [Required(ErrorMessage = "Seleccione una opción")]
        [Display(Name = "Grupo de medicamentos")]
        public MedicacionGrupo MedicacionGrupo { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Información relevante")]
        public string MedicacionDescripcion { get; set; }

        [Display(Name = "Pacientes pautados")]
        public int NumPacientesPautados { get; set; } = 0;
    }

    public enum MedicacionGrupo
    {
        Analgésicos = 1,
        Antiácidos = 2,
        Antialérgicos = 3,
        Antidiarreicos = 4,
        Antiinfecciosos = 5,
        Antiinflamatorios = 6,
        Antipiréticos = 7,
        Antitusivos = 8,
    }
    public enum MedicacionNombre
    {
        Aspirina = 1,
        Omeprazol = 2,
        Ebastina = 3,
        Loperamida = 4,
        Tobradex = 5,
        Ibuprofeno = 6,
        Paracetamol = 7,
        Efedrina = 8,
    }
}
