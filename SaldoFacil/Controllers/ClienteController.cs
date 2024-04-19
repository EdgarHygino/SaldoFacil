using Microsoft.AspNetCore.Mvc;
using SaldoFacil.Dados.Context;
using SaldoFacil.Model.Auxiliares;
using SaldoFacil.Model.Models;
using SaldoFacil.Model.Models.Entities;
using SaldoFacil.Servicos;
using System.Diagnostics;

namespace SaldoFacil.Controllers
{
    public class ClienteController : Controller
    {
        private readonly SaldoFacilDbContext _db;
        private readonly IServicoTransacao _servicoTransacao;

        public ClienteController(SaldoFacilDbContext db, IServicoTransacao servicoTransacao)
        {
            _db = db;
            _servicoTransacao = servicoTransacao;
        }
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(Clientes cliente)
        {
            if (ModelState.IsValid)
            {
                _db.Clientes.Add(cliente);
                _db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(cliente);
        }

        public IActionResult Saldo()
        {
            var clientes = _db.Clientes.ToList();

            return View(clientes);
        }

        public IActionResult ExibirSaldo(int clienteId)
        {
            var cliente = _db.Clientes.FirstOrDefault(c => c.Id == clienteId);
            if (cliente != null)
            {
                ViewBag.Saldo = cliente.Saldo;
            }
            else
            {
                ViewBag.Saldo = null;
            }
            return PartialView("_SaldoPartial");
        }

        // GET: /Cliente/Credito
        public IActionResult Credito()
        {
            var clientes = _db.Clientes.ToList();

            return View(clientes);
        }

        // POST: /Cliente/Credito
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Credito(int clienteId, decimal valor)
        {
            _servicoTransacao.Credito(clienteId, valor);


            // Redireciona para a página de saldo após a transação
            return RedirectToAction("Saldo");
        }

        // GET: /Cliente/Debito
        public IActionResult Debito()
        {
            var clientes = _db.Clientes.ToList();

            return View(clientes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Debito(int clienteId, decimal valor)
        {
            try
            {
                await _servicoTransacao.Debito(clienteId, valor);
                return RedirectToAction("Saldo");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var clientes = _db.Clientes.ToList();
                return View(clientes);
            }
        }

        public IActionResult Transferir()
        {
            var clientes = _db.Clientes.ToList();
            ViewBag.Clientes = clientes;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transferir(int clienteRemetente, int destinatario, decimal valor)
        {        

            try
            {
                await _servicoTransacao.Transferencia(clienteRemetente, destinatario, valor);
                return RedirectToAction("Saldo");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Clientes = _db.Clientes.ToList();
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
