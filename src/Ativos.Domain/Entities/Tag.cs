namespace Ativos.Domain.Entities;

public class Tag
{
    public long Id { get; set; }
    public Enums.Tag Value { get; set; }
    public long ChamadoId { get; set; }
    public Chamado Chamado { get; set; } = default!;
}                 