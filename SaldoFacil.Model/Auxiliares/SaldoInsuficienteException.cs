using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaldoFacil.Model.Auxiliares
{
    public class SaldoInsuficienteException : Exception
    {
        public SaldoInsuficienteException() : base("Cliente não possue saldo suficiente!")
        {
        }
    }
}
