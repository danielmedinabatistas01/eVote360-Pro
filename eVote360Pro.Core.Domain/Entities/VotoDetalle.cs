using eVote360Pro.Core.Domain.Common;
using eVote360Pro.Core.Domain.Entities;

public class VotoDetalle : BaseEntity
{
    public required int VotoId { get; set; }
    public Voto Voto { get; set; } = null!;

    public required int PuestoElectivoId { get; set; }
    public PuestoElectivo PuestoElectivo { get; set; } = null!;

    public int? CandidatoId { get; set; }
    public Candidato? Candidato { get; set; }
}