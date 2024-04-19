using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaldoFacil.Model.Dtos
{
    public class TransferenciaDto
    {
        public int clienteRemetenteId { get; set; }
        public int DestinatarioId { get; set; }
        public decimal Valor { get; set;}
    }
}
