using BarbeariaMVC.Data;
using BarbeariaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarbeariaMVC.Controllers;

public class ClientesController : Controller
{
    private readonly BarbeariaRepository _dados;
    public ClientesController(BarbeariaRepository dados) => _dados = dados;

    public IActionResult Index() => View(_dados.Clientes);

    public IActionResult Create() => View();
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Cliente cliente)
    {
        if (!ModelState.IsValid) return View(cliente);
        _dados.AdicionarCliente(cliente);
        TempData["Sucesso"] = "Cliente cadastrado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var cliente = _dados.BuscarCliente(id);
        return cliente is null ? NotFound() : View(cliente);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Cliente cliente)
    {
        if (id != cliente.Id) return BadRequest();
        if (!ModelState.IsValid) return View(cliente);
        _dados.AtualizarCliente(cliente);
        TempData["Sucesso"] = "Cliente atualizado!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (_dados.Agendamentos.Any(x => x.ClienteId == id))
            TempData["Erro"] = "Não é possível excluir: existem agendamentos para este cliente.";
        else
        {
            _dados.RemoverCliente(id);
            TempData["Sucesso"] = "Cliente removido.";
        }
        return RedirectToAction(nameof(Index));
    }
}
