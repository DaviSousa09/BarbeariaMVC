using System.ComponentModel.DataAnnotations;

namespace BarbeariaMVC.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome.")]
    [StringLength(80)]
    public string Nome { get; set; } = "";

    [Required(ErrorMessage = "Informe o telefone.")]
    [StringLength(20)]
    public string Telefone { get; set; } = "";

    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string? Email { get; set; }
}
