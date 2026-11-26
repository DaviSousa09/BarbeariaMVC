using System.ComponentModel.DataAnnotations;

namespace BarbeariaMVC.Models;

public class Agendamento
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o cliente.")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "Informe o serviço.")]
    public int ServicoId { get; set; }

    [Required(ErrorMessage = "Informe o barbeiro.")]
    public int BarbeiroId { get; set; }

    [Required(ErrorMessage = "Informe a data e o horário.")]
    public DateTime DataHora { get; set; } = DateTime.Today.AddHours(9);

    [StringLength(300)]
    public string? Observacao { get; set; }

    public string Status { get; set; } = "Agendado";
}
