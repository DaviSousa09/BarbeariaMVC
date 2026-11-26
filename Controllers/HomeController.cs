using System.Diagnostics;
using BarbeariaMVC.Data;
using BarbeariaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarbeariaMVC.Controllers;

public class HomeController : Controller
{
    private readonly BarbeariaRepository _dados;
    public HomeController(BarbeariaRepository dados) => _dados = dados;

    public IActionResult Index()
    {
        var model = new HomeViewModel
        {
            TotalClientes = _dados.Clientes.Count,
            TotalServicos = _dados.Servicos.Count,
            TotalBarbeiros = _dados.Barbeiros.Count,
            TotalAgendamentos = _dados.Agendamentos.Count(x => x.Status == "Agendado"),
            ProximosAgendamentos = _dados.Agendamentos
                .Where(x => x.DataHora >= DateTime.Now && x.Status == "Agendado")
                .OrderBy(x => x.DataHora).Take(5).ToList(),
            Clientes = _dados.Clientes,
            Servicos = _dados.Servicos,
            Barbeiros = _dados.Barbeiros
        };
        return View(model);
    }

    public IActionResult Erro() => View();
}
