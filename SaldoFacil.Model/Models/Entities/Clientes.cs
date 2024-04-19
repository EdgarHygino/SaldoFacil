using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaldoFacil.Model.Models.Entities
{
    public class Clientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O e-mail do cliente é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail inserido não é válido.")]
        public string Email { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal Saldo { get; set; } = decimal.Zero;

        public void LancarCredito(decimal saldo)
        {
            Saldo = saldo;
        }
    }
}
