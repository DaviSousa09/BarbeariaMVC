using BarbeariaMVC.Data;
using BarbeariaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarbeariaMVC.Controllers;

public class AgendamentosController : Controller
{
    private readonly BarbeariaRepository _dados;
    public AgendamentosController(BarbeariaRepository dados) => _dados = dados;

    private void CarregarListas()
    {
        ViewBag.Clientes = new SelectList(_dados.Clientes, "Id", "Nome");
        ViewBag.Servicos = new SelectList(_dados.Servicos, "Id", "Nome");
        ViewBag.Barbeiros = new SelectList(_dados.Barbeiros, "Id", "Nome");
    }

    public IActionResult Index() => View(_dados.Agendamentos.OrderBy(x => x.DataHora).ToList());

    public IActionResult Create()
    {
        CarregarListas();
        return View(new Agendamento { DataHora = DateTime.Now.AddHours(1) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Agendamento agendamento)
    {
        if (_dados.BuscarCliente(agendamento.ClienteId) is null ||
            _dados.BuscarServico(agendamento.ServicoId) is null ||
            _dados.BuscarBarbeiro(agendamento.BarbeiroId) is null)
            ModelState.AddModelError("", "Selecione um cliente, serviço e barbeiro válidos.");

        if (agendamento.DataHora < DateTime.Now)
            ModelState.AddModelError(nameof(agendamento.DataHora), "Escolha uma data e horário futuros.");

        if (_dados.Agendamentos.Any(x => x.BarbeiroId == agendamento.BarbeiroId &&
            x.DataHora == agendamento.DataHora && x.Status == "Agendado"))
            ModelState.AddModelError(nameof(agendamento.DataHora), "Este barbeiro já possui agendamento nesse horário.");

        if (!ModelState.IsValid)
        {
            CarregarListas();
            return View(agendamento);
        }

        _dados.AdicionarAgendamento(agendamento);
        TempData["Sucesso"] = "Agendamento realizado!";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var item = _dados.BuscarAgendamento(id);
        if (item is null) return NotFound();
        CarregarListas();
        return View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Agendamento agendamento)
    {
        if (id != agendamento.Id) return BadRequest();
        if (_dados.BuscarCliente(agendamento.ClienteId) is null ||
            _dados.BuscarServico(agendamento.ServicoId) is null ||
            _dados.BuscarBarbeiro(agendamento.BarbeiroId) is null)
            ModelState.AddModelError("", "Selecione opções válidas.");

        if (!ModelState.IsValid)
        {
            CarregarListas();
            return View(agendamento);
        }

        _dados.AtualizarAgendamento(agendamento);
        TempData["Sucesso"] = "Agendamento atualizado!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Concluir(int id)
    {
        var item = _dados.BuscarAgendamento(id);
        if (item is not null) item.Status = "Concluído";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _dados.RemoverAgendamento(id);
        TempData["Sucesso"] = "Agendamento cancelado/removido.";
        return RedirectToAction(nameof(Index));
    }
}
