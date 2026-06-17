using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.PuestoElectivo
{
    public class SavePuestoElectivoViewModel : BasicViewModel<int>
    {
        [Required(ErrorMessage = "Nombre de puesto obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Descripción obligatoria.")]
        [StringLength(250, ErrorMessage = "Máximo 250 caracteres.")]
        public string Descripcion { get; set; } = null!;

        public bool EsActivo { get; set; }
    }
}