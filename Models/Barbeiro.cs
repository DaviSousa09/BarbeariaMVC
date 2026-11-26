using System.ComponentModel.DataAnnotations;

namespace BarbeariaMVC.Models;

public class Barbeiro
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome do barbeiro.")]
    [StringLength(80)]
    public string Nome { get; set; } = "";

    [StringLength(20)]
    public string? Telefone { get; set; }

    [StringLength(60)]
    public string? Especialidade { get; set; }
}
