using Microsoft.AspNetCore.Mvc;
using SaldoFacil.Model.Auxiliares;
using SaldoFacil.Model.Dtos;
using SaldoFacil.Model.Models.Entities;
using SaldoFacil.Transacao.API.Dados.Context;
using System.Drawing;

namespace SaldoFacil.Transacao.API.Servicos
{
    public class ServicoTransacao : IServicoTransacao
    {
        private readonly SaldoFacilDbContext _db;

        public ServicoTransacao(SaldoFacilDbContext db)
        {
            _db = db;
        }
        
        public void Transferencia(TransferenciaDto transferenciaDto)
        {
            var cliente = _db.Clientes.SingleOrDefault(c => c.Id == transferenciaDto.clienteRemetenteId);

            var saldo = decimal.Zero;

            if (transferenciaDto.Valor >= cliente.Saldo)
            {
                throw new SaldoInsuficienteException();
            }            

            saldo = cliente.Saldo -= transferenciaDto.Valor;
            cliente.LancarCredito(saldo);

            _db.Clientes.Update(cliente);
            _db.SaveChanges();

            CreditoDto creditoDto = new CreditoDto();
            creditoDto.ClienteId = transferenciaDto.DestinatarioId;
            creditoDto.Valor = transferenciaDto.Valor;

            Credito(creditoDto);

        }

        public void Credito(CreditoDto creditoDto)
        {
            var cliente = _db.Clientes.SingleOrDefault(c => c.Id == creditoDto.ClienteId);
            var saldo = cliente.Saldo += creditoDto.Valor;

            cliente.LancarCredito(saldo);

            _db.Clientes.Update(cliente);
            _db.SaveChanges();
        }

        public void Debito(DebitoDto debitoDto)
        {
            var cliente = _db.Clientes.SingleOrDefault(c => c.Id == debitoDto.ClienteId);

            var saldo = decimal.Zero;

            if (debitoDto.Valor >= cliente.Saldo) 
            {
                throw new SaldoInsuficienteException();
            }

            saldo = cliente.Saldo -= debitoDto.Valor;
            cliente.LancarCredito(saldo);

            _db.Clientes.Update(cliente);
            _db.SaveChanges();

        }
    }
}
