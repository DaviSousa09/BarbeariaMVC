using BarbeariaMVC.Data;
using BarbeariaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarbeariaMVC.Controllers;

public class BarbeirosController : Controller
{
    private readonly BarbeariaRepository _dados;
    public BarbeirosController(BarbeariaRepository dados) => _dados = dados;

    public IActionResult Index() => View(_dados.Barbeiros);
    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Barbeiro barbeiro)
    {
        if (!ModelState.IsValid) return View(barbeiro);
        _dados.AdicionarBarbeiro(barbeiro);
        TempData["Sucesso"] = "Barbeiro cadastrado!";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var item = _dados.BuscarBarbeiro(id);
        return item is null ? NotFound() : View(item);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Barbeiro barbeiro)
    {
        if (id != barbeiro.Id) return BadRequest();
        if (!ModelState.IsValid) return View(barbeiro);
        _dados.AtualizarBarbeiro(barbeiro);
        TempData["Sucesso"] = "Barbeiro atualizado!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (_dados.Agendamentos.Any(x => x.BarbeiroId == id))
            TempData["Erro"] = "Não é possível excluir: existem agendamentos para este barbeiro.";
        else
        {
            _dados.RemoverBarbeiro(id);
            TempData["Sucesso"] = "Barbeiro removido.";
        }
        return RedirectToAction(nameof(Index));
    }
}
