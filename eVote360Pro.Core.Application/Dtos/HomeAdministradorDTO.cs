public class HomeAdministradorDTO
{
    public int AnioSeleccionado { get; set; }

    public List<int> AniosDisponibles { get; set; } = new();

    public List<ResumenEleccionDTO> Resumenes { get; set; } = new();
}