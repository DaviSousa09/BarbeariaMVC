using BarbeariaMVC.Models;

namespace BarbeariaMVC.Data;

// Repositório em memória: os dados existem enquanto a aplicação estiver aberta.
// Ao reiniciar o projeto, os dados cadastrados são apagados.
public class BarbeariaRepository
{
    private readonly List<Cliente> _clientes = new()
    {
        new Cliente { Id = 1, Nome = "Lucas Almeida", Telefone = "(14) 99999-1111", Email = "lucas@email.com" },
        new Cliente { Id = 2, Nome = "Pedro Santos", Telefone = "(14) 98888-2222", Email = "pedro@email.com" }
    };

    private readonly List<Servico> _servicos = new()
    {
        new Servico { Id = 1, Nome = "Corte masculino", Preco = 35m, DuracaoMinutos = 40 },
        new Servico { Id = 2, Nome = "Barba", Preco = 25m, DuracaoMinutos = 30 },
        new Servico { Id = 3, Nome = "Corte + barba", Preco = 55m, DuracaoMinutos = 60 }
    };

    private readonly List<Barbeiro> _barbeiros = new()
    {
        new Barbeiro { Id = 1, Nome = "Rafael Oliveira", Telefone = "(14) 97777-3333", Especialidade = "Degradê e freestyle" },
        new Barbeiro { Id = 2, Nome = "Marcos Lima", Telefone = "(14) 96666-4444", Especialidade = "Barba e corte clássico" }
    };

    private readonly List<Agendamento> _agendamentos = new()
    {
        new Agendamento { Id = 1, ClienteId = 1, ServicoId = 1, BarbeiroId = 1, DataHora = DateTime.Today.AddDays(1).AddHours(10), Status = "Agendado" },
        new Agendamento { Id = 2, ClienteId = 2, ServicoId = 2, BarbeiroId = 2, DataHora = DateTime.Today.AddDays(1).AddHours(11), Status = "Agendado" }
    };

    private int ProximoId<T>(List<T> lista) where T : class
    {
        var prop = typeof(T).GetProperty("Id")!;
        return lista.Count == 0 ? 1 : lista.Max(x => (int)prop.GetValue(x)!) + 1;
    }

    public List<Cliente> Clientes => _clientes;
    public List<Servico> Servicos => _servicos;
    public List<Barbeiro> Barbeiros => _barbeiros;
    public List<Agendamento> Agendamentos => _agendamentos;

    public Cliente? BuscarCliente(int id) => _clientes.FirstOrDefault(x => x.Id == id);
    public Servico? BuscarServico(int id) => _servicos.FirstOrDefault(x => x.Id == id);
    public Barbeiro? BuscarBarbeiro(int id) => _barbeiros.FirstOrDefault(x => x.Id == id);
    public Agendamento? BuscarAgendamento(int id) => _agendamentos.FirstOrDefault(x => x.Id == id);

    public void AdicionarCliente(Cliente item) { item.Id = ProximoId(_clientes); _clientes.Add(item); }
    public void AtualizarCliente(Cliente item)
    {
        var atual = BuscarCliente(item.Id);
        if (atual is null) return;
        atual.Nome = item.Nome; atual.Telefone = item.Telefone; atual.Email = item.Email;
    }
    public void RemoverCliente(int id) => _clientes.RemoveAll(x => x.Id == id);

    public void AdicionarServico(Servico item) { item.Id = ProximoId(_servicos); _servicos.Add(item); }
    public void AtualizarServico(Servico item)
    {
        var atual = BuscarServico(item.Id);
        if (atual is null) return;
        atual.Nome = item.Nome; atual.Preco = item.Preco; atual.DuracaoMinutos = item.DuracaoMinutos;
    }
    public void RemoverServico(int id) => _servicos.RemoveAll(x => x.Id == id);

    public void AdicionarBarbeiro(Barbeiro item) { item.Id = ProximoId(_barbeiros); _barbeiros.Add(item); }
    public void AtualizarBarbeiro(Barbeiro item)
    {
        var atual = BuscarBarbeiro(item.Id);
        if (atual is null) return;
        atual.Nome = item.Nome; atual.Telefone = item.Telefone; atual.Especialidade = item.Especialidade;
    }
    public void RemoverBarbeiro(int id) => _barbeiros.RemoveAll(x => x.Id == id);

    public void AdicionarAgendamento(Agendamento item) { item.Id = ProximoId(_agendamentos); _agendamentos.Add(item); }
    public void AtualizarAgendamento(Agendamento item)
    {
        var atual = BuscarAgendamento(item.Id);
        if (atual is null) return;
        atual.ClienteId = item.ClienteId; atual.ServicoId = item.ServicoId;
        atual.BarbeiroId = item.BarbeiroId; atual.DataHora = item.DataHora;
        atual.Observacao = item.Observacao; atual.Status = item.Status;
    }
    public void RemoverAgendamento(int id) => _agendamentos.RemoveAll(x => x.Id == id);
}
