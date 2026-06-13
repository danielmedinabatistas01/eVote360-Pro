using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.Eleccion
{
    public class EleccionCreateViewModel
    {
        [Required(ErrorMessage = "Debe ingresar el nombre de la elección")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar la fecha de la elección")]
        public DateTime FechaRealizacion { get; set; }
    }
}