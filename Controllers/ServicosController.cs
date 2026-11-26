using BarbeariaMVC.Data;
using BarbeariaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarbeariaMVC.Controllers;

public class ServicosController : Controller
{
    private readonly BarbeariaRepository _dados;
    public ServicosController(BarbeariaRepository dados) => _dados = dados;

    public IActionResult Index() => View(_dados.Servicos);
    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Servico servico)
    {
        if (!ModelState.IsValid) return View(servico);
        _dados.AdicionarServico(servico);
        TempData["Sucesso"] = "Serviço cadastrado!";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var item = _dados.BuscarServico(id);
        return item is null ? NotFound() : View(item);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Servico servico)
    {
        if (id != servico.Id) return BadRequest();
        if (!ModelState.IsValid) return View(servico);
        _dados.AtualizarServico(servico);
        TempData["Sucesso"] = "Serviço atualizado!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (_dados.Agendamentos.Any(x => x.ServicoId == id))
            TempData["Erro"] = "Não é possível excluir: este serviço está em um agendamento.";
        else
        {
            _dados.RemoverServico(id);
            TempData["Sucesso"] = "Serviço removido.";
        }
        return RedirectToAction(nameof(Index));
    }
}
