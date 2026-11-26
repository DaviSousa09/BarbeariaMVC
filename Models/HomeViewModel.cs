namespace BarbeariaMVC.Models;

public class HomeViewModel
{
    public int TotalClientes { get; set; }
    public int TotalServicos { get; set; }
    public int TotalBarbeiros { get; set; }
    public int TotalAgendamentos { get; set; }
    public List<Agendamento> ProximosAgendamentos { get; set; } = new();
    public List<Cliente> Clientes { get; set; } = new();
    public List<Servico> Servicos { get; set; } = new();
    public List<Barbeiro> Barbeiros { get; set; } = new();
}
