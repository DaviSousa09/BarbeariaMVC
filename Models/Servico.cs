using System.ComponentModel.DataAnnotations;

namespace BarbeariaMVC.Models;

public class Servico
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome do serviço.")]
    [StringLength(60)]
    public string Nome { get; set; } = "";

    [Required]
    [Range(1, 1000, ErrorMessage = "Informe um preço válido.")]
    public decimal Preco { get; set; }

    [Required]
    [Range(5, 300, ErrorMessage = "A duração deve ser entre 5 e 300 minutos.")]
    public int DuracaoMinutos { get; set; }
}
