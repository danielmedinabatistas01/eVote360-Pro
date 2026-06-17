public class ResumenEleccionDTO
{
    public int EleccionId { get; set; }
    public string NombreEleccion { get; set; } = string.Empty;
    public DateTime FechaRealizacion { get; set; }
    public string Estado { get; set; } = string.Empty;
    public int CantidadPartidosParticipantes { get; set; }
    public int CantidadCandidatosParticipantes { get; set; }
    public int CantidadCiudadanosVotaron { get; set; }
}